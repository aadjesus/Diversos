// -----------------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
// -----------------------------------------------------------------------
using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Microsoft.Internal;
using System.Linq.Expressions;

namespace System.ComponentModel.Composition.Primitives
{
    [SuppressMessage("Microsoft.Naming", "CA1711:IdentifiersShouldNotHaveIncorrectSuffix")]
    public class ExportedDelegate
    {
        private object _instance;
        private MethodInfo _method;

        protected ExportedDelegate() { }
#if !SILVERLIGHT
        [System.Security.SecurityCritical]
#endif
        public ExportedDelegate(object instance, MethodInfo method)
        {
            Requires.NotNull(method, "method");

            this._instance = instance;
            this._method = method;
        }

        public virtual Delegate CreateDelegate(Type delegateType) 
        {
            Requires.NotNull(delegateType, "delegateType");

            if (delegateType == typeof(Delegate) || delegateType == typeof(MulticastDelegate))
            {
				throw new NotSupportedException();
            }
            
            return Delegate.CreateDelegate(delegateType, this._instance, this._method, false);
        }
    }
}

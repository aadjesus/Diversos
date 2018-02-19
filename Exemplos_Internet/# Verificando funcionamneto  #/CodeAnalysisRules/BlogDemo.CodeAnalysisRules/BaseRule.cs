﻿using Microsoft.FxCop.Sdk;

namespace BlogDemo.CodeAnalysisRules
{
    public abstract class BaseRule : BaseIntrospectionRule
    {
        protected BaseRule(string name)
            : base(

                // The name of the rule (must match exactly to an entry
                // in the manifest XML)
                name,

                // The name of the manifest XML file, qualified with the
                // namespace and missing the extension
                typeof(BaseRule).Namespace + ".RulesXX",

                // The assembly to find the manifest XML in
                typeof(BaseRule).Assembly)
        {
        }
    }
}
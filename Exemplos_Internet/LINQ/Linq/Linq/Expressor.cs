// Copyright (c) Microsoft Corporation.  All rights reserved.
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Linq.Expressions;

namespace Espresso
{
    public class Expressor
    {
        Parser parser;
        Evaluator evaluator;

        public Expressor()
        {
        }

        public bool IgnoreCase
        {
            get { return this.Parser.IgnoreCase; }
            set { this.Parser.IgnoreCase = value; }
        }

        private Parser Parser
        {
            get
            {
                if (this.parser == null)
                    this.parser = new Parser();
                return this.parser;
            }
        }

        private Evaluator Evaluator
        {
            get
            {
                if (this.evaluator == null)
                    this.evaluator = new Evaluator();
                return this.evaluator;
            }
        }

        public Expression<Func<T, R>> MakeLambda<T, R>(string expression)
        {
            return this.MakeLambda<T, R>(null, expression);
        }

        public Expression<Func<T, R>> MakeLambda<T, R>(string argName, string expression)
        {
            if (expression == null) throw new ArgumentNullException();
            ParameterExpression p = Expression.Parameter(typeof(T), argName);
            Expression b = this.Parser.Parse(expression,
                delegate(string id)
                {
                    if (id == argName)
                        return p;
                    FieldInfo fi = this.Parser.FindField(typeof(T), id);
                    if (fi != null)
                        return Expression.Field(p, fi);
                    PropertyInfo pi = this.Parser.FindProperty(typeof(T), id);
                    if (pi != null)
                        return Expression.Property(p, pi);
                    return null;
                });
            if (b.Type != typeof(R))
            {
                throw new Exception(string.Format("Cannot convert return type '{0}' to expected type '{1}'", b.Type, typeof(R)));
            }
            return Expression.Lambda<Func<T, R>>(b, p);
        }

        public Func<T, R> MakeFunc<T, R>(string expression)
        {
            return this.MakeFunc<T, R>(null, expression);
        }

        public Func<T, R> MakeFunc<T, R>(string argName, string expression)
        {
            if (expression == null) throw new ArgumentNullException();
            Expression<Func<T, R>> lambda = this.MakeLambda<T, R>(argName, expression);
            return (Func<T, R>)this.Evaluator.Eval(lambda);
        }

        public object Eval(string expression)
        {
            if (expression == null) throw new ArgumentNullException();
            Expression e = this.Parser.Parse(expression, null);
            return this.Evaluator.Eval(e);
        }

        public object Eval(string expression, Dictionary<string, Expression> map)
        {
            if (expression == null || map == null) throw new ArgumentNullException();
            Expression e = this.Parser.Parse(expression, delegate(string id)
            {
                Expression exp;
                if (map.TryGetValue(id, out exp))
                    return exp;
                return null;
            });
            return this.Evaluator.Eval(e);
        }
    }
}

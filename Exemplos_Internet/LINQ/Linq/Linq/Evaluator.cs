// Copyright (c) Microsoft Corporation.  All rights reserved.
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Linq.Expressions;

namespace Espresso
{
    internal class Evaluator
    {
        EvalFactory factory;
        Dictionary<ParameterExpression, IEvalExpression> map;

        public Evaluator()
        {
            this.factory = new EvalFactory();
            this.map = new Dictionary<ParameterExpression, IEvalExpression>();
        }

        public object Eval(Expression expr)
        {
            this.map.Clear();
            IEvalExpression ee = this.Visit(expr);
            if (ee == null) return null;
            return ee.Eval();
        }

        private IEvalExpression Visit(Expression node)
        {
            if (node == null) return null;
            switch (node.NodeType)
            {
                case ExpressionType.New:
                    return this.VisitNew((NewExpression)node);
                //case ExpressionType.BitwiseNot:
                case ExpressionType.Negate:
                case ExpressionType.Not:
                    return this.VisitUnary((UnaryExpression)node);
                case ExpressionType.Add:
                case ExpressionType.Subtract:
                case ExpressionType.Multiply:
                case ExpressionType.Divide:
                case ExpressionType.Modulo:
                case ExpressionType.And:
                case ExpressionType.Or:
                //case ExpressionType.LT:
                //case ExpressionType.LE:
                //case ExpressionType.GT:
                //case ExpressionType.GE:
                //case ExpressionType.EQ:
                //case ExpressionType.NE:
                //case ExpressionType.BitwiseAnd:
                //case ExpressionType.BitwiseOr:
                //case ExpressionType.BitwiseXor:
                case ExpressionType.Coalesce:
                    return this.VisitBinary((BinaryExpression)node);
                case ExpressionType.Constant:
                    return this.VisitConstant((ConstantExpression)node);
                case ExpressionType.MemberAccess:
                    return this.VisitMember((MemberExpression)node);
                case ExpressionType.Lambda:
                    return this.VisitLambda((LambdaExpression)node);
                case ExpressionType.Parameter:
                    return this.VisitParameter((ParameterExpression)node);
                //case ExpressionType.MethodCall:
                //    return this.VisitMethodCall((MethodCallExpression)node);
                //case ExpressionType.Cast:
                //    return this.VisitCast((UnaryExpression)node);

                /*
                case ExpressionType.Concat:
                    return this.VisitConcat((NAryExpression)node);
                case ExpressionType.Funclet:
                    return this.VisitFunclet((FuncletExpression)node);
                case ExpressionType.NewArrayInit:
                    return this.VisitNewArrayInit((NewArrayExpression)node);
                 */
                default:
                    throw new Exception("Unhandled Expression Type: " + node.NodeType);
            }
        }

        private IEvalExpression VisitLambda(LambdaExpression lambda)
        {
            IEvalExpression[] pms = new IEvalExpression[lambda.Parameters.Count];
            for (int i = 0, n = pms.Length; i < n; i++)
            {
                ParameterExpression pe = lambda.Parameters[i]; // should be only parameter expressions
                IEvalExpression eve = this.factory.Parameter(pe.Type);
                this.map.Add(pe, eve);
                pms[i] = eve;
            }
            IEvalExpression body = this.Visit(lambda.Body);
            return this.factory.Lambda(lambda.Type, body, pms);
        }

        private IEvalExpression VisitParameter(ParameterExpression pe)
        {
            IEvalExpression result;
            if (this.map.TryGetValue(pe, out result))
                return result;
            throw new Exception("Unexpected parameter: " + pe.Name);
        }

        private IEvalExpression VisitMember(MemberExpression me)
        {
            IEvalExpression expr = this.Visit(me.Expression);
            FieldInfo fi = me.Member as FieldInfo;
            if (fi != null)
                return this.factory.Field(expr, fi);
            PropertyInfo pi = me.Member as PropertyInfo;
            if (pi != null)
                return this.factory.Property(expr, pi);
            throw new ArgumentException();
        }

        private IEvalExpression VisitMethodCall(MethodCallExpression mc)
        {
            IEvalExpression expr = this.Visit(mc.Object);
            IEvalExpression[] args = null;
            if (mc.Parameters.Count > 0)
            {
                args = new IEvalExpression[mc.Parameters.Count];
                for (int i = 0, n = args.Length; i < n; i++)
                {
                    args[i] = this.Visit(mc.Parameters[i]);
                }
            }
            return this.factory.MethodCall(expr, mc.Method, args);
        }

        private IEvalExpression VisitNew(NewExpression ne)
        {
            IEvalExpression[] args = null;
            if (ne.Args.Count > 0)
            {
                args = new IEvalExpression[ne.Args.Count];
                for (int i = 0, n = args.Length; i < n; i++)
                {
                    args[i] = this.Visit(ne.Args[i]);
                }
            }
            EvalBinding[] bindings = null;
            if (ne.Bindings.Count > 0)
            {
                bindings = new EvalBinding[ne.Bindings.Count];
                for (int i = 0, n = bindings.Length; i < n; i++)
                {
                    bindings[i] = new EvalBinding(ne.Bindings[i].Member, this.Visit(ne.Bindings[i].Expression));
                }
            }
            return this.factory.New(ne.Constructor, args, bindings);
        }

        private IEvalExpression VisitUnary(UnaryExpression u)
        {
            IEvalExpression operand = this.Visit(u.Operand);
            switch (u.NodeType)
            {
                case ExpressionType.BitwiseNot:
                    return this.factory.BitNot(operand);
                case ExpressionType.Negate:
                    return this.factory.Neg(operand);
                case ExpressionType.Not:
                    return this.factory.Not(operand);
                default:
                    throw new ArgumentException();
            }
        }

        private IEvalExpression VisitBinary(BinaryExpression b)
        {
            IEvalExpression left = this.Visit(b.Left);
            IEvalExpression right = this.Visit(b.Right);
            switch (b.NodeType)
            {
                case ExpressionType.Add:
                    return this.factory.Add(left, right);
                case ExpressionType.Subtract:
                    return this.factory.Sub(left, right);
                case ExpressionType.Multiply:
                    return this.factory.Mul(left, right);
                case ExpressionType.Divide:
                    return this.factory.Div(left, right);
                case ExpressionType.Modulo:
                    return this.factory.Mod(left, right);
                case ExpressionType.And:
                    return this.factory.And(left, right);
                case ExpressionType.Or:
                    return this.factory.Or(left, right);
                case ExpressionType.LessThan:
                    return this.factory.LT(left, right);
                case ExpressionType.LessThanOrEqual:
                    return this.factory.LE(left, right);
                case ExpressionType.GreaterThan:
                    return this.factory.GT(left, right);
                case ExpressionType.GreaterThanOrEqual:
                    return this.factory.GE(left, right);
                case ExpressionType.Equal:
                    return this.factory.EQ(left, right);
                case ExpressionType.Negate:
                    return this.factory.NE(left, right);
                //case ExpressionType.BitwiseAnd:
                //    return this.factory.BitAnd(left, right);
                //case ExpressionType.BitwiseOr:
                //    return this.factory.BitOr(left, right);
                //case ExpressionType.BitwiseXor:
                //    return this.factory.BitXor(left, right);
                //case ExpressionType.Coalesce:        
                default:
                    throw new ArgumentException();
            }
        }

        private IEvalExpression VisitCast(UnaryExpression u)
        {
            return this.factory.Cast(this.Visit(u.Operand), u.Type);
        }

        private IEvalExpression VisitConstant(ConstantExpression c)
        {
            return this.factory.Constant(c.Value, c.Type);
        }
    }

    internal interface IEvalExpression
    {
        object Eval();
        Type Type { get; }
    }

    internal interface IEvalExpression<T>
    {
        T Eval();
    }

    internal class EvalFactory
    {
        Dictionary<Type, Dictionary<string, object>> typeOps;

        internal EvalFactory()
        {
            this.typeOps = new Dictionary<Type, Dictionary<string, object>>();
            this.AddOps(typeof(Boolean), typeof(BooleanOps));
            this.AddOps(typeof(Byte), typeof(ByteOps));
            this.AddOps(typeof(SByte), typeof(SByteOps));
            this.AddOps(typeof(Int16), typeof(Int16Ops));
            this.AddOps(typeof(Int32), typeof(Int32Ops));
            this.AddOps(typeof(Int64), typeof(Int64Ops));
            this.AddOps(typeof(Double), typeof(DoubleOps));
            this.AddOps(typeof(Single), typeof(SingleOps));
            this.AddOps(typeof(Decimal), typeof(DecimalOps));
            this.AddOps(typeof(DateTime), typeof(DateTimeOps));
            this.AddOps(typeof(String), typeof(StringOps));
            this.AddOps(typeof(IComparable), typeof(ComparableOps));
            this.AddOps(typeof(Object), typeof(ObjectOps));
        }
        private void AddOps(Type type, Type opsType)
        {
            Dictionary<string, object> ops = new Dictionary<string, object>();
            FieldInfo[] fields = opsType.GetFields(BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public);
            foreach (FieldInfo fi in fields)
            {
                ops.Add(fi.Name, fi.GetValue(null));
            }
            this.typeOps.Add(type, ops);
        }
        private Type GetReturnType(IEvalExpression expr)
        {
            Type tExpr = expr.GetType();
            Type[] tExprArgs = tExpr.GetGenericArguments();
            return tExprArgs[tExprArgs.Length - 1];
        }
        private bool IsNullable(Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
        }
        private Type GetNullableBase(Type type)
        {
            return type.GetGenericArguments()[0];
        }
        private object GetOperator(Type type, string opName)
        {
            Type t = type;
            if (!this.typeOps.ContainsKey(type))
            {
                if (typeof(IComparable).IsAssignableFrom(type))
                {
                    t = typeof(IComparable);
                }
                else
                {
                    t = typeof(Object);
                }
            }
            Dictionary<string, object> ops = this.typeOps[t];
            if (!ops.ContainsKey(opName))
                throw new Exception("Eval system does not contain '" + opName + "' operator for type '" + type + "'");
            return ops[opName];
        }
        private IEvalExpression GetUnary(string opName, Type returnType, IEvalExpression operand)
        {
            Type t = this.GetReturnType(operand);
            bool isNull = this.IsNullable(t);
            if (isNull)
                t = this.GetNullableBase(t);
            if (returnType == null)
                returnType = t;
            object op = this.GetOperator(t, opName);
            Type tnode = null;
            if (isNull)
            {
                tnode = typeof(EvalNullableUnary<,>).MakeGenericType(t, returnType);
            }
            else
            {
                tnode = typeof(EvalUnary<,>).MakeGenericType(t, returnType);
            }
            return (IEvalExpression)Activator.CreateInstance(tnode, new object[] { op, operand });
        }
        private IEvalExpression GetBinary(string opName, Type returnType, IEvalExpression left, IEvalExpression right)
        {
            Type t = this.GetReturnType(left);
            bool isNull = this.IsNullable(t);
            if (isNull)
                t = this.GetNullableBase(t);
            if (returnType == null)
                returnType = t;
            object op = this.GetOperator(t, opName);
            Type tnode = null;
            if (isNull)
            {
                tnode = typeof(EvalNullableBinary<,>).MakeGenericType(t, returnType);
            }
            else
            {
                tnode = typeof(EvalBinary<,>).MakeGenericType(t, returnType);
            }
            return (IEvalExpression)Activator.CreateInstance(tnode, new object[] { op, left, right });
        }
        internal IEvalExpression Add(IEvalExpression left, IEvalExpression right)
        {
            return this.GetBinary("Add", null, left, right);
        }
        internal IEvalExpression Sub(IEvalExpression left, IEvalExpression right)
        {
            return this.GetBinary("Sub", null, left, right);
        }
        internal IEvalExpression Mul(IEvalExpression left, IEvalExpression right)
        {
            return this.GetBinary("Mul", null, left, right);
        }
        internal IEvalExpression Div(IEvalExpression left, IEvalExpression right)
        {
            return this.GetBinary("Div", null, left, right);
        }
        internal IEvalExpression Mod(IEvalExpression left, IEvalExpression right)
        {
            return this.GetBinary("Mod", null, left, right);
        }
        internal IEvalExpression Neg(IEvalExpression operand)
        {
            return this.GetUnary("Neg", null, operand);
        }
        internal IEvalExpression BitAnd(IEvalExpression left, IEvalExpression right)
        {
            return this.GetBinary("BitAnd", null, left, right);
        }
        internal IEvalExpression BitOr(IEvalExpression left, IEvalExpression right)
        {
            return this.GetBinary("BitOr", null, left, right);
        }
        internal IEvalExpression BitXor(IEvalExpression left, IEvalExpression right)
        {
            return this.GetBinary("BitXor", null, left, right);
        }
        internal IEvalExpression BitNot(IEvalExpression operand)
        {
            return this.GetUnary("BitNot", null, operand);
        }
        internal IEvalExpression And(IEvalExpression left, IEvalExpression right)
        {
            return this.GetBinary("And", typeof(Boolean), left, right);
        }
        internal IEvalExpression Or(IEvalExpression left, IEvalExpression right)
        {
            return this.GetBinary("Or", typeof(Boolean), left, right);
        }
        internal IEvalExpression Not(IEvalExpression op)
        {
            return this.GetUnary("Not", typeof(Boolean), op);
        }
        internal IEvalExpression EQ(IEvalExpression left, IEvalExpression right)
        {
            return this.GetBinary("EQ", typeof(bool), left, right);
        }
        internal IEvalExpression NE(IEvalExpression left, IEvalExpression right)
        {
            return this.GetBinary("NE", typeof(bool), left, right);
        }
        internal IEvalExpression LT(IEvalExpression left, IEvalExpression right)
        {
            return this.GetBinary("LT", typeof(bool), left, right);
        }
        internal IEvalExpression LE(IEvalExpression left, IEvalExpression right)
        {
            return this.GetBinary("LE", typeof(bool), left, right);
        }
        internal IEvalExpression GT(IEvalExpression left, IEvalExpression right)
        {
            return this.GetBinary("GT", typeof(bool), left, right);
        }
        internal IEvalExpression GE(IEvalExpression left, IEvalExpression right)
        {
            return this.GetBinary("GE", typeof(bool), left, right);
        }
        internal IEvalExpression Cast(IEvalExpression operand, Type type)
        {
            Type t = typeof(EvalCast<,>).MakeGenericType(operand.Type, type);
            return (IEvalExpression)Activator.CreateInstance(t, new object[] { operand });
        }
        internal IEvalExpression Constant(object value, Type type)
        {
            Type t = typeof(EvalConstant<>).MakeGenericType(type);
            return (IEvalExpression)Activator.CreateInstance(t, new object[] { value });
        }
        internal IEvalExpression Parameter(Type type)
        {
            Type t = typeof(EvalParameter<>).MakeGenericType(type);
            return (IEvalExpression)Activator.CreateInstance(t, new object[] { });
        }
        internal IEvalExpression Field(IEvalExpression expr, FieldInfo fi)
        {
            Type t = typeof(EvalField<>).MakeGenericType(fi.FieldType);
            return (IEvalExpression)Activator.CreateInstance(t, new object[] { expr, fi });
        }
        internal IEvalExpression Property(IEvalExpression expr, PropertyInfo pi)
        {
            Type t = typeof(EvalProperty<>).MakeGenericType(pi.PropertyType);
            return (IEvalExpression)Activator.CreateInstance(t, new object[] { expr, pi });
        }
        internal IEvalExpression MethodCall(IEvalExpression expr, MethodInfo mi, params IEvalExpression[] args)
        {
            Type t = typeof(EvalMethodCall<>).MakeGenericType(mi.ReturnType);
            return (IEvalExpression)Activator.CreateInstance(t, new object[] { expr, mi, args });
        }
        internal IEvalExpression New(ConstructorInfo ci, IEvalExpression[] args, EvalBinding[] bindings)
        {
            Type t = typeof(EvalConstructor<>).MakeGenericType(ci.DeclaringType);
            return (IEvalExpression)Activator.CreateInstance(t, new object[] { ci, args, bindings });
        }
        internal IEvalExpression Lambda(Type delType, IEvalExpression body, IEvalExpression[] parameters)
        {
            if (parameters.Length == 1)
            {
                Type t = typeof(EvalLambda<,>).MakeGenericType(delType.GetGenericArguments());
                return (IEvalExpression)Activator.CreateInstance(t, new object[] { body, parameters[0] });
            }
            else if (parameters.Length == 2)
            {
                Type t = typeof(EvalLambda<,,>).MakeGenericType(delType.GetGenericArguments());
                return (IEvalExpression)Activator.CreateInstance(t, new object[] { body, parameters[0], parameters[1] });
            }
            else
            {
                throw new NotImplementedException();
            }
        }
    }

    internal delegate S GenericBinary<T, S>(T left, T right);
    internal delegate S GenericUnary<T, S>(T op);

    internal abstract class EvalExpression<T> : IEvalExpression, IEvalExpression<T>
    {
        public abstract T Eval();
        object IEvalExpression.Eval()
        {
            return this.Eval();
        }
        public Type Type
        {
            get { return typeof(T); }
        }
    }

    internal class EvalUnary<T, S> : EvalExpression<S>
    {
        GenericUnary<T, S> op;
        EvalExpression<T> operand;
        public EvalUnary(GenericUnary<T, S> op, EvalExpression<T> operand)
        {
            this.op = op;
            this.operand = operand;
        }
        public override S Eval()
        {
            return op(operand.Eval());
        }
    }

    internal class EvalNullableUnary<T, S> : EvalExpression<Nullable<S>>
        where S : struct
        where T : struct
    {
        GenericUnary<T, S> op;
        EvalExpression<Nullable<T>> operand;
        public EvalNullableUnary(GenericUnary<T, S> op, EvalExpression<Nullable<T>> operand)
        {
            this.op = op;
            this.operand = operand;
        }
        public override Nullable<S> Eval()
        {
            Nullable<T> v = operand.Eval();
            if (v.HasValue)
            {
                return new Nullable<S>(op(v.Value));
            }
            return default(Nullable<S>);
        }
    }

    internal class EvalBinary<T, S> : EvalExpression<S>
    {
        GenericBinary<T, S> op;
        EvalExpression<T> left;
        EvalExpression<T> right;
        public EvalBinary(GenericBinary<T, S> op, EvalExpression<T> left, EvalExpression<T> right)
        {
            this.op = op;
            this.left = left;
            this.right = right;
        }
        public override S Eval()
        {
            return op(left.Eval(), right.Eval());
        }
    }

    internal class EvalNullableBinary<T, S> : EvalExpression<Nullable<S>>
        where T : struct
        where S : struct
    {
        GenericBinary<T, S> op;
        EvalExpression<Nullable<T>> left;
        EvalExpression<Nullable<T>> right;
        public EvalNullableBinary(GenericBinary<T, S> op, EvalExpression<Nullable<T>> left, EvalExpression<Nullable<T>> right)
        {
            this.op = op;
            this.left = left;
            this.right = right;
        }
        public override Nullable<S> Eval()
        {
            Nullable<T> vLeft = left.Eval();
            if (vLeft.HasValue)
            {
                Nullable<T> vRight = right.Eval();
                if (vRight.HasValue)
                {
                    return new Nullable<S>(op(vLeft.Value, vRight.Value));
                }
            }
            return default(Nullable<S>);
        }
    }

    internal class EvalParameter<T> : EvalExpression<T>
    {
        internal T value;
        public EvalParameter()
        {
            this.value = default(T);
        }
        public override T Eval()
        {
            return value;
        }
    }

    internal class EvalCast<S, T> : EvalExpression<T>
    {
        Type dstType;
        EvalExpression<S> operand;

        public EvalCast(EvalExpression<S> operand)
        {
            this.operand = operand;
            this.dstType = (typeof(T).IsGenericType && typeof(T).GetGenericTypeDefinition() == typeof(Nullable<>))
                            ? typeof(T).GetGenericArguments()[0] : typeof(T);
        }

        public override T Eval()
        {
            object value = this.operand.Eval();
            if (value != null)
                value = System.Convert.ChangeType(value, this.dstType);
            return (T)value;
        }
    }

    internal class EvalConstant<T> : EvalExpression<T>
    {
        T value;
        public EvalConstant(T value)
        {
            this.value = value;
        }
        public override T Eval()
        {
            return value;
        }
    }

    internal class EvalLambda<T, R> : EvalExpression<Func<T, R>>
    {
        EvalExpression<R> body;
        EvalParameter<T> param;
        Func<T, R> del;

        public EvalLambda(EvalExpression<R> body, EvalParameter<T> p)
        {
            this.body = body;
            this.param = p;
            this.del = delegate(T t)
            {
                this.param.value = t;
                return this.body.Eval();
            };
        }

        public override Func<T, R> Eval()
        {
            return this.del;
        }
    }

    internal class EvalLambda<T, S, R> : EvalExpression<Func<T, S, R>>
    {
        EvalExpression<R> body;
        EvalParameter<T> p0;
        EvalParameter<S> p1;
        Func<T, S, R> del;

        public EvalLambda(EvalExpression<R> body, EvalParameter<T> p0, EvalParameter<S> p1)
        {
            this.body = body;
            this.p0 = p0;
            this.p1 = p1;
            this.del = delegate(T arg0, S arg1)
            {
                this.p0.value = arg0;
                this.p1.value = arg1;
                return this.body.Eval();
            };
        }

        public override Func<T, S, R> Eval()
        {
            return this.del;
        }
    }

    internal class EvalField<T> : EvalExpression<T>
    {
        IEvalExpression expr;
        FieldInfo fi;
        public EvalField(IEvalExpression expr, FieldInfo fi)
        {
            this.expr = expr;
            this.fi = fi;
        }
        public override T Eval()
        {
            object v = expr.Eval();
            return (T)this.fi.GetValue(v);
        }
    }

    internal class EvalProperty<T> : EvalExpression<T>
    {
        IEvalExpression expr;
        PropertyInfo pi;
        public EvalProperty(IEvalExpression expr, PropertyInfo pi)
        {
            this.expr = expr;
            this.pi = pi;
        }
        public override T Eval()
        {
            object v = expr.Eval();
            return (T)this.pi.GetValue(v, null);
        }
    }

    internal class EvalMethodCall<T> : EvalExpression<T>
    {
        IEvalExpression expr;
        MethodInfo mi;
        IEvalExpression[] args;
        public EvalMethodCall(IEvalExpression expr, MethodInfo mi, IEvalExpression[] args)
        {
            this.expr = expr;
            this.mi = mi;
            this.args = args;
        }

        public override T Eval()
        {
            object obj = (expr == null) ? null : expr.Eval();
            object[] parameters = null;
            if (this.args != null)
            {
                int n = this.args.Length;
                if (n > 0)
                {
                    parameters = new object[n];
                    for (int i = 0; i < n; i++)
                    {
                        parameters[i] = this.args[i].Eval();
                    }
                }
            }
            return (T)this.mi.Invoke(obj, parameters);
        }
    }

    internal class EvalBinding
    {
        MemberInfo member;
        IEvalExpression expression;
        internal EvalBinding(MemberInfo member, IEvalExpression expression)
        {
            this.member = member;
            this.expression = expression;
        }
        public MemberInfo Member
        {
            get { return this.member; }
        }
        public IEvalExpression Expression
        {
            get { return this.expression; }
        }
    }

    internal class EvalConstructor<T> : EvalExpression<T>
    {
        ConstructorInfo ci;
        IEvalExpression[] args;
        EvalBinding[] bindings;
        public EvalConstructor(ConstructorInfo ci, IEvalExpression[] args, EvalBinding[] bindings)
        {
            this.ci = ci;
            this.args = args;
            this.bindings = bindings;
        }
        private static readonly object[] EmptyArgs = new object[] { };
        public override T Eval()
        {
            object[] parameters = null;
            if (this.args != null)
            {
                int n = this.args.Length;
                if (n > 0)
                {
                    parameters = new object[n];
                    for (int i = 0; i < n; i++)
                    {
                        parameters[i] = this.args[i].Eval();
                    }
                }
            }
            object obj = this.ci.Invoke(parameters);
            if (this.bindings != null)
            {
                for (int i = 0, n = bindings.Length; i < n; i++)
                {
                    object val = this.bindings[i].Expression.Eval();
                    FieldInfo fi = bindings[i].Member as FieldInfo;
                    if (fi != null)
                    {
                        fi.SetValue(obj, val);
                    }
                    else
                    {
                        PropertyInfo pi = bindings[i].Member as PropertyInfo;
                        if (pi != null)
                        {
                            pi.SetValue(obj, val, EmptyArgs);
                        }
                        else
                        {
                            throw new Exception("Bad binding");
                        }
                    }
                }
            }
            return (T)obj;
        }
    }

    internal class ObjectOps
    {
        private static bool fEQ(object left, object right) { return left.Equals(right); }
        private static bool fNE(object left, object right) { return !left.Equals(right); }
        internal static GenericBinary<object, bool> EQ = new GenericBinary<object, bool>(fEQ);
        internal static GenericBinary<object, bool> NE = new GenericBinary<object, bool>(fNE);
    }

    internal class ComparableOps
    {
        private static bool fEQ(IComparable left, IComparable right) { return left.Equals(right); }
        private static bool fNE(IComparable left, IComparable right) { return !left.Equals(right); }
        private static bool fLT(IComparable left, IComparable right) { return left.CompareTo(right) < 0; }
        private static bool fLE(IComparable left, IComparable right) { return left.CompareTo(right) <= 0; }
        private static bool fGT(IComparable left, IComparable right) { return left.CompareTo(right) > 0; }
        private static bool fGE(IComparable left, IComparable right) { return left.CompareTo(right) >= 0; }
        internal static readonly GenericBinary<IComparable, bool> EQ = new GenericBinary<IComparable, bool>(fEQ);
        internal static readonly GenericBinary<IComparable, bool> NE = new GenericBinary<IComparable, bool>(fNE);
        internal static readonly GenericBinary<IComparable, bool> LT = new GenericBinary<IComparable, bool>(fLT);
        internal static readonly GenericBinary<IComparable, bool> LE = new GenericBinary<IComparable, bool>(fLE);
        internal static readonly GenericBinary<IComparable, bool> GT = new GenericBinary<IComparable, bool>(fGT);
        internal static readonly GenericBinary<IComparable, bool> GE = new GenericBinary<IComparable, bool>(fGE);
    }

    internal class BooleanOps
    {
        private static bool fNot(Boolean op) { return !op; }
        private static bool fAnd(Boolean left, Boolean right) { return left && right; }
        private static bool fOr(Boolean left, Boolean right) { return left || right; }
        private static bool fEQ(Boolean left, Boolean right) { return left == right; }
        private static bool fNE(Boolean left, Boolean right) { return left != right; }
        internal static readonly GenericBinary<Boolean, Boolean> And = new GenericBinary<Boolean, Boolean>(fAnd);
        internal static readonly GenericBinary<Boolean, Boolean> Or = new GenericBinary<Boolean, Boolean>(fOr);
        internal static readonly GenericBinary<Boolean, Boolean> EQ = new GenericBinary<Boolean, Boolean>(fEQ);
        internal static readonly GenericBinary<Boolean, Boolean> NE = new GenericBinary<Boolean, Boolean>(fNE);
        internal static readonly GenericUnary<Boolean, Boolean> Not = new GenericUnary<Boolean, Boolean>(fNot);
    }

    internal class ByteOps
    {
        private static Byte fAdd(Byte left, Byte right) { return (byte)(left + right); }
        private static Byte fSub(Byte left, Byte right) { return (byte)(left - right); }
        private static Byte fMul(Byte left, Byte right) { return (byte)(left * right); }
        private static Byte fDiv(Byte left, Byte right) { return (byte)(left / right); }
        private static Byte fMod(Byte left, Byte right) { return (byte)(left % right); }
        private static Byte fBitAnd(Byte left, Byte right) { return (byte)(left & right); }
        private static Byte fBitOr(Byte left, Byte right) { return (byte)(left | right); }
        private static Byte fBitXor(Byte left, Byte right) { return (byte)(left ^ right); }
        private static Byte fBitNot(Byte op) { return (byte)~op; }
        private static Byte fNeg(Byte op) { return (byte)-op; }
        private static bool fEQ(Byte left, Byte right) { return left == right; }
        private static bool fNE(Byte left, Byte right) { return left != right; }
        private static bool fLT(Byte left, Byte right) { return left < right; }
        private static bool fLE(Byte left, Byte right) { return left <= right; }
        private static bool fGT(Byte left, Byte right) { return left > right; }
        private static bool fGE(Byte left, Byte right) { return left >= right; }
        internal static readonly GenericBinary<Byte, Byte> Add = new GenericBinary<Byte, Byte>(fAdd);
        internal static readonly GenericBinary<Byte, Byte> Sub = new GenericBinary<Byte, Byte>(fSub);
        internal static readonly GenericBinary<Byte, Byte> Mul = new GenericBinary<Byte, Byte>(fMul);
        internal static readonly GenericBinary<Byte, Byte> Div = new GenericBinary<Byte, Byte>(fDiv);
        internal static readonly GenericBinary<Byte, Byte> Mod = new GenericBinary<Byte, Byte>(fMod);
        internal static readonly GenericBinary<Byte, Byte> BitAnd = new GenericBinary<Byte, Byte>(fBitAnd);
        internal static readonly GenericBinary<Byte, Byte> BitOr = new GenericBinary<Byte, Byte>(fBitOr);
        internal static readonly GenericBinary<Byte, Byte> BitXor = new GenericBinary<Byte, Byte>(fBitXor);
        internal static readonly GenericUnary<Byte, Byte> BitNot = new GenericUnary<Byte, Byte>(fBitNot);
        internal static readonly GenericUnary<Byte, Byte> Neg = new GenericUnary<Byte, Byte>(fNeg);
        internal static readonly GenericBinary<Byte, bool> EQ = new GenericBinary<Byte, bool>(fEQ);
        internal static readonly GenericBinary<Byte, bool> NE = new GenericBinary<Byte, bool>(fNE);
        internal static readonly GenericBinary<Byte, bool> LT = new GenericBinary<Byte, bool>(fLT);
        internal static readonly GenericBinary<Byte, bool> LE = new GenericBinary<Byte, bool>(fLE);
        internal static readonly GenericBinary<Byte, bool> GT = new GenericBinary<Byte, bool>(fGT);
        internal static readonly GenericBinary<Byte, bool> GE = new GenericBinary<Byte, bool>(fGE);
    }

    internal class SByteOps
    {
        private static SByte fAdd(SByte left, SByte right) { return (sbyte)(left + right); }
        private static SByte fSub(SByte left, SByte right) { return (sbyte)(left - right); }
        private static SByte fMul(SByte left, SByte right) { return (sbyte)(left * right); }
        private static SByte fDiv(SByte left, SByte right) { return (sbyte)(left / right); }
        private static SByte fMod(SByte left, SByte right) { return (sbyte)(left % right); }
        private static SByte fBitAnd(SByte left, SByte right) { return (sbyte)(left & right); }
        private static SByte fBitOr(SByte left, SByte right) { return (sbyte)(left | right); }
        private static SByte fBitXor(SByte left, SByte right) { return (sbyte)(left ^ right); }
        private static SByte fBitNot(SByte op) { return (sbyte)~op; }
        private static SByte fNeg(SByte op) { return (sbyte)-op; }
        private static bool fEQ(SByte left, SByte right) { return left == right; }
        private static bool fNE(SByte left, SByte right) { return left != right; }
        private static bool fLT(SByte left, SByte right) { return left < right; }
        private static bool fLE(SByte left, SByte right) { return left <= right; }
        private static bool fGT(SByte left, SByte right) { return left > right; }
        private static bool fGE(SByte left, SByte right) { return left >= right; }
        internal static readonly GenericBinary<SByte, SByte> Add = new GenericBinary<SByte, SByte>(fAdd);
        internal static readonly GenericBinary<SByte, SByte> Sub = new GenericBinary<SByte, SByte>(fSub);
        internal static readonly GenericBinary<SByte, SByte> Mul = new GenericBinary<SByte, SByte>(fMul);
        internal static readonly GenericBinary<SByte, SByte> Div = new GenericBinary<SByte, SByte>(fDiv);
        internal static readonly GenericBinary<SByte, SByte> Mod = new GenericBinary<SByte, SByte>(fMod);
        internal static readonly GenericBinary<SByte, SByte> BitAnd = new GenericBinary<SByte, SByte>(fBitAnd);
        internal static readonly GenericBinary<SByte, SByte> BitOr = new GenericBinary<SByte, SByte>(fBitOr);
        internal static readonly GenericBinary<SByte, SByte> BitXor = new GenericBinary<SByte, SByte>(fBitXor);
        internal static readonly GenericUnary<SByte, SByte> BitNot = new GenericUnary<SByte, SByte>(fBitNot);
        internal static readonly GenericUnary<SByte, SByte> Neg = new GenericUnary<SByte, SByte>(fNeg);
        internal static readonly GenericBinary<SByte, bool> EQ = new GenericBinary<SByte, bool>(fEQ);
        internal static readonly GenericBinary<SByte, bool> NE = new GenericBinary<SByte, bool>(fNE);
        internal static readonly GenericBinary<SByte, bool> LT = new GenericBinary<SByte, bool>(fLT);
        internal static readonly GenericBinary<SByte, bool> LE = new GenericBinary<SByte, bool>(fLE);
        internal static readonly GenericBinary<SByte, bool> GT = new GenericBinary<SByte, bool>(fGT);
        internal static readonly GenericBinary<SByte, bool> GE = new GenericBinary<SByte, bool>(fGE);
    }

    internal class Int16Ops
    {
        private static Int16 fAdd(Int16 left, Int16 right) { return (short)(left + right); }
        private static Int16 fSub(Int16 left, Int16 right) { return (short)(left - right); }
        private static Int16 fMul(Int16 left, Int16 right) { return (short)(left * right); }
        private static Int16 fDiv(Int16 left, Int16 right) { return (short)(left / right); }
        private static Int16 fMod(Int16 left, Int16 right) { return (short)(left % right); }
        private static Int16 fBitAnd(Int16 left, Int16 right) { return (short)(left & right); }
        private static Int16 fBitOr(Int16 left, Int16 right) { return (short)(left | right); }
        private static Int16 fBitXor(Int16 left, Int16 right) { return (short)(left ^ right); }
        private static Int16 fBitNot(Int16 op) { return (short)~op; }
        private static Int16 fNeg(Int16 op) { return (short)-op; }
        private static bool fEQ(Int16 left, Int16 right) { return left == right; }
        private static bool fNE(Int16 left, Int16 right) { return left != right; }
        private static bool fLT(Int16 left, Int16 right) { return left < right; }
        private static bool fLE(Int16 left, Int16 right) { return left <= right; }
        private static bool fGT(Int16 left, Int16 right) { return left > right; }
        private static bool fGE(Int16 left, Int16 right) { return left >= right; }
        internal static readonly GenericBinary<Int16, Int16> Add = new GenericBinary<Int16, Int16>(fAdd);
        internal static readonly GenericBinary<Int16, Int16> Sub = new GenericBinary<Int16, Int16>(fSub);
        internal static readonly GenericBinary<Int16, Int16> Mul = new GenericBinary<Int16, Int16>(fMul);
        internal static readonly GenericBinary<Int16, Int16> Div = new GenericBinary<Int16, Int16>(fDiv);
        internal static readonly GenericBinary<Int16, Int16> Mod = new GenericBinary<Int16, Int16>(fMod);
        internal static readonly GenericBinary<Int16, Int16> BitAnd = new GenericBinary<Int16, Int16>(fBitAnd);
        internal static readonly GenericBinary<Int16, Int16> BitOr = new GenericBinary<Int16, Int16>(fBitOr);
        internal static readonly GenericBinary<Int16, Int16> BitXor = new GenericBinary<Int16, Int16>(fBitXor);
        internal static readonly GenericUnary<Int16, Int16> BitNot = new GenericUnary<Int16, Int16>(fBitNot);
        internal static readonly GenericUnary<Int16, Int16> Neg = new GenericUnary<Int16, Int16>(fNeg);
        internal static readonly GenericBinary<Int16, bool> EQ = new GenericBinary<Int16, bool>(fEQ);
        internal static readonly GenericBinary<Int16, bool> NE = new GenericBinary<Int16, bool>(fNE);
        internal static readonly GenericBinary<Int16, bool> LT = new GenericBinary<Int16, bool>(fLT);
        internal static readonly GenericBinary<Int16, bool> LE = new GenericBinary<Int16, bool>(fLE);
        internal static readonly GenericBinary<Int16, bool> GT = new GenericBinary<Int16, bool>(fGT);
        internal static readonly GenericBinary<Int16, bool> GE = new GenericBinary<Int16, bool>(fGE);
    }

    internal class Int32Ops
    {
        private static Int32 fAdd(Int32 left, Int32 right) { return left + right; }
        private static Int32 fSub(Int32 left, Int32 right) { return left - right; }
        private static Int32 fMul(Int32 left, Int32 right) { return left * right; }
        private static Int32 fDiv(Int32 left, Int32 right) { return left / right; }
        private static Int32 fMod(Int32 left, Int32 right) { return left % right; }
        private static Int32 fBitAnd(Int32 left, Int32 right) { return left & right; }
        private static Int32 fBitOr(Int32 left, Int32 right) { return left | right; }
        private static Int32 fBitXor(Int32 left, Int32 right) { return left ^ right; }
        private static Int32 fBitNot(Int32 op) { return ~op; }
        private static Int32 fNeg(Int32 op) { return -op; }
        private static bool fEQ(Int32 left, Int32 right) { return left == right; }
        private static bool fNE(Int32 left, Int32 right) { return left != right; }
        private static bool fLT(Int32 left, Int32 right) { return left < right; }
        private static bool fLE(Int32 left, Int32 right) { return left <= right; }
        private static bool fGT(Int32 left, Int32 right) { return left > right; }
        private static bool fGE(Int32 left, Int32 right) { return left >= right; }
        internal static readonly GenericBinary<Int32, Int32> Add = new GenericBinary<Int32, Int32>(fAdd);
        internal static readonly GenericBinary<Int32, Int32> Sub = new GenericBinary<Int32, Int32>(fSub);
        internal static readonly GenericBinary<Int32, Int32> Mul = new GenericBinary<Int32, Int32>(fMul);
        internal static readonly GenericBinary<Int32, Int32> Div = new GenericBinary<Int32, Int32>(fDiv);
        internal static readonly GenericBinary<Int32, Int32> Mod = new GenericBinary<Int32, Int32>(fMod);
        internal static readonly GenericBinary<Int32, Int32> BitAnd = new GenericBinary<Int32, Int32>(fBitAnd);
        internal static readonly GenericBinary<Int32, Int32> BitOr = new GenericBinary<Int32, Int32>(fBitOr);
        internal static readonly GenericBinary<Int32, Int32> BitXor = new GenericBinary<Int32, Int32>(fBitXor);
        internal static readonly GenericUnary<Int32, Int32> BitNot = new GenericUnary<Int32, Int32>(fBitNot);
        internal static readonly GenericUnary<Int32, Int32> Neg = new GenericUnary<Int32, Int32>(fNeg);
        internal static readonly GenericBinary<Int32, bool> EQ = new GenericBinary<Int32, bool>(fEQ);
        internal static readonly GenericBinary<Int32, bool> NE = new GenericBinary<Int32, bool>(fNE);
        internal static readonly GenericBinary<Int32, bool> LT = new GenericBinary<Int32, bool>(fLT);
        internal static readonly GenericBinary<Int32, bool> LE = new GenericBinary<Int32, bool>(fLE);
        internal static readonly GenericBinary<Int32, bool> GT = new GenericBinary<Int32, bool>(fGT);
        internal static readonly GenericBinary<Int32, bool> GE = new GenericBinary<Int32, bool>(fGE);
    }

    internal class Int64Ops
    {
        private static Int64 fAdd(Int64 left, Int64 right) { return left + right; }
        private static Int64 fSub(Int64 left, Int64 right) { return left - right; }
        private static Int64 fMul(Int64 left, Int64 right) { return left * right; }
        private static Int64 fDiv(Int64 left, Int64 right) { return left / right; }
        private static Int64 fMod(Int64 left, Int64 right) { return left % right; }
        private static Int64 fBitAnd(Int64 left, Int64 right) { return left & right; }
        private static Int64 fBitOr(Int64 left, Int64 right) { return left | right; }
        private static Int64 fBitXor(Int64 left, Int64 right) { return left ^ right; }
        private static Int64 fBitNot(Int64 op) { return ~op; }
        private static Int64 fNeg(Int64 op) { return -op; }
        private static bool fEQ(Int64 left, Int64 right) { return left == right; }
        private static bool fNE(Int64 left, Int64 right) { return left != right; }
        private static bool fLT(Int64 left, Int64 right) { return left < right; }
        private static bool fLE(Int64 left, Int64 right) { return left <= right; }
        private static bool fGT(Int64 left, Int64 right) { return left > right; }
        private static bool fGE(Int64 left, Int64 right) { return left >= right; }
        internal static readonly GenericBinary<Int64, Int64> Add = new GenericBinary<Int64, Int64>(fAdd);
        internal static readonly GenericBinary<Int64, Int64> Sub = new GenericBinary<Int64, Int64>(fSub);
        internal static readonly GenericBinary<Int64, Int64> Mul = new GenericBinary<Int64, Int64>(fMul);
        internal static readonly GenericBinary<Int64, Int64> Div = new GenericBinary<Int64, Int64>(fDiv);
        internal static readonly GenericBinary<Int64, Int64> Mod = new GenericBinary<Int64, Int64>(fMod);
        internal static readonly GenericBinary<Int64, Int64> BitAnd = new GenericBinary<Int64, Int64>(fBitAnd);
        internal static readonly GenericBinary<Int64, Int64> BitOr = new GenericBinary<Int64, Int64>(fBitOr);
        internal static readonly GenericBinary<Int64, Int64> BitXor = new GenericBinary<Int64, Int64>(fBitXor);
        internal static readonly GenericUnary<Int64, Int64> BitNot = new GenericUnary<Int64, Int64>(fBitNot);
        internal static readonly GenericUnary<Int64, Int64> Neg = new GenericUnary<Int64, Int64>(fNeg);
        internal static readonly GenericBinary<Int64, bool> EQ = new GenericBinary<Int64, bool>(fEQ);
        internal static readonly GenericBinary<Int64, bool> NE = new GenericBinary<Int64, bool>(fNE);
        internal static readonly GenericBinary<Int64, bool> LT = new GenericBinary<Int64, bool>(fLT);
        internal static readonly GenericBinary<Int64, bool> LE = new GenericBinary<Int64, bool>(fLE);
        internal static readonly GenericBinary<Int64, bool> GT = new GenericBinary<Int64, bool>(fGT);
        internal static readonly GenericBinary<Int64, bool> GE = new GenericBinary<Int64, bool>(fGE);
    }

    internal class DoubleOps
    {
        private static Double fAdd(Double left, Double right) { return left + right; }
        private static Double fSub(Double left, Double right) { return left - right; }
        private static Double fMul(Double left, Double right) { return left * right; }
        private static Double fDiv(Double left, Double right) { return left / right; }
        private static Double fMod(Double left, Double right) { return left % right; }
        private static Double fNeg(Double op) { return -op; }
        private static bool fEQ(Double left, Double right) { return left == right; }
        private static bool fNE(Double left, Double right) { return left != right; }
        private static bool fLT(Double left, Double right) { return left < right; }
        private static bool fLE(Double left, Double right) { return left <= right; }
        private static bool fGT(Double left, Double right) { return left > right; }
        private static bool fGE(Double left, Double right) { return left >= right; }
        internal static readonly GenericBinary<Double, Double> Add = new GenericBinary<Double, Double>(fAdd);
        internal static readonly GenericBinary<Double, Double> Sub = new GenericBinary<Double, Double>(fSub);
        internal static readonly GenericBinary<Double, Double> Mul = new GenericBinary<Double, Double>(fMul);
        internal static readonly GenericBinary<Double, Double> Div = new GenericBinary<Double, Double>(fDiv);
        internal static readonly GenericBinary<Double, Double> Mod = new GenericBinary<Double, Double>(fMod);
        internal static readonly GenericUnary<Double, Double> Neg = new GenericUnary<Double, Double>(fNeg);
        internal static readonly GenericBinary<Double, bool> EQ = new GenericBinary<Double, bool>(fEQ);
        internal static readonly GenericBinary<Double, bool> NE = new GenericBinary<Double, bool>(fNE);
        internal static readonly GenericBinary<Double, bool> LT = new GenericBinary<Double, bool>(fLT);
        internal static readonly GenericBinary<Double, bool> LE = new GenericBinary<Double, bool>(fLE);
        internal static readonly GenericBinary<Double, bool> GT = new GenericBinary<Double, bool>(fGT);
        internal static readonly GenericBinary<Double, bool> GE = new GenericBinary<Double, bool>(fGE);
    }

    internal class SingleOps
    {
        private static Single fAdd(Single left, Single right) { return left + right; }
        private static Single fSub(Single left, Single right) { return left - right; }
        private static Single fMul(Single left, Single right) { return left * right; }
        private static Single fDiv(Single left, Single right) { return left / right; }
        private static Single fMod(Single left, Single right) { return left % right; }
        private static Single fNeg(Single op) { return -op; }
        private static bool fEQ(Single left, Single right) { return left == right; }
        private static bool fNE(Single left, Single right) { return left != right; }
        private static bool fLT(Single left, Single right) { return left < right; }
        private static bool fLE(Single left, Single right) { return left <= right; }
        private static bool fGT(Single left, Single right) { return left > right; }
        private static bool fGE(Single left, Single right) { return left >= right; }
        internal static readonly GenericBinary<Single, Single> Add = new GenericBinary<Single, Single>(fAdd);
        internal static readonly GenericBinary<Single, Single> Sub = new GenericBinary<Single, Single>(fSub);
        internal static readonly GenericBinary<Single, Single> Mul = new GenericBinary<Single, Single>(fMul);
        internal static readonly GenericBinary<Single, Single> Div = new GenericBinary<Single, Single>(fDiv);
        internal static readonly GenericBinary<Single, Single> Mod = new GenericBinary<Single, Single>(fMod);
        internal static readonly GenericUnary<Single, Single> Neg = new GenericUnary<Single, Single>(fNeg);
        internal static readonly GenericBinary<Single, bool> EQ = new GenericBinary<Single, bool>(fEQ);
        internal static readonly GenericBinary<Single, bool> NE = new GenericBinary<Single, bool>(fNE);
        internal static readonly GenericBinary<Single, bool> LT = new GenericBinary<Single, bool>(fLT);
        internal static readonly GenericBinary<Single, bool> LE = new GenericBinary<Single, bool>(fLE);
        internal static readonly GenericBinary<Single, bool> GT = new GenericBinary<Single, bool>(fGT);
        internal static readonly GenericBinary<Single, bool> GE = new GenericBinary<Single, bool>(fGE);
    }

    internal class DecimalOps
    {
        private static Decimal fAdd(Decimal left, Decimal right) { return left + right; }
        private static Decimal fSub(Decimal left, Decimal right) { return left - right; }
        private static Decimal fMul(Decimal left, Decimal right) { return left * right; }
        private static Decimal fDiv(Decimal left, Decimal right) { return left / right; }
        private static Decimal fMod(Decimal left, Decimal right) { return left % right; }
        private static Decimal fNeg(Decimal op) { return -op; }
        private static bool fEQ(Decimal left, Decimal right) { return left == right; }
        private static bool fNE(Decimal left, Decimal right) { return left != right; }
        private static bool fLT(Decimal left, Decimal right) { return left < right; }
        private static bool fLE(Decimal left, Decimal right) { return left <= right; }
        private static bool fGT(Decimal left, Decimal right) { return left > right; }
        private static bool fGE(Decimal left, Decimal right) { return left >= right; }
        internal static readonly GenericBinary<Decimal, Decimal> Add = new GenericBinary<Decimal, Decimal>(fAdd);
        internal static readonly GenericBinary<Decimal, Decimal> Sub = new GenericBinary<Decimal, Decimal>(fSub);
        internal static readonly GenericBinary<Decimal, Decimal> Mul = new GenericBinary<Decimal, Decimal>(fMul);
        internal static readonly GenericBinary<Decimal, Decimal> Div = new GenericBinary<Decimal, Decimal>(fDiv);
        internal static readonly GenericBinary<Decimal, Decimal> Mod = new GenericBinary<Decimal, Decimal>(fMod);
        internal static readonly GenericUnary<Decimal, Decimal> Neg = new GenericUnary<Decimal, Decimal>(fNeg);
        internal static readonly GenericBinary<Decimal, bool> EQ = new GenericBinary<Decimal, bool>(fEQ);
        internal static readonly GenericBinary<Decimal, bool> NE = new GenericBinary<Decimal, bool>(fNE);
        internal static readonly GenericBinary<Decimal, bool> LT = new GenericBinary<Decimal, bool>(fLT);
        internal static readonly GenericBinary<Decimal, bool> LE = new GenericBinary<Decimal, bool>(fLE);
        internal static readonly GenericBinary<Decimal, bool> GT = new GenericBinary<Decimal, bool>(fGT);
        internal static readonly GenericBinary<Decimal, bool> GE = new GenericBinary<Decimal, bool>(fGE);
    }

    internal class DateTimeOps
    {
        private static bool fEQ(DateTime left, DateTime right) { return left == right; }
        private static bool fNE(DateTime left, DateTime right) { return left != right; }
        private static bool fLT(DateTime left, DateTime right) { return left < right; }
        private static bool fLE(DateTime left, DateTime right) { return left <= right; }
        private static bool fGT(DateTime left, DateTime right) { return left > right; }
        private static bool fGE(DateTime left, DateTime right) { return left >= right; }
        internal static readonly GenericBinary<DateTime, bool> EQ = new GenericBinary<DateTime, bool>(fEQ);
        internal static readonly GenericBinary<DateTime, bool> NE = new GenericBinary<DateTime, bool>(fNE);
        internal static readonly GenericBinary<DateTime, bool> LT = new GenericBinary<DateTime, bool>(fLT);
        internal static readonly GenericBinary<DateTime, bool> LE = new GenericBinary<DateTime, bool>(fLE);
        internal static readonly GenericBinary<DateTime, bool> GT = new GenericBinary<DateTime, bool>(fGT);
        internal static readonly GenericBinary<DateTime, bool> GE = new GenericBinary<DateTime, bool>(fGE);
    }

    internal class StringOps
    {
        private static String fAdd(String left, String right) { return left + right; }
        private static bool fEQ(string left, string right) { return left == right; }
        private static bool fNE(string left, string right) { return left != right; }
        private static bool fLT(string left, string right) { return left.CompareTo(right) < 0; }
        private static bool fLE(string left, string right) { return left.CompareTo(right) <= 0; }
        private static bool fGT(string left, string right) { return left.CompareTo(right) > 0; }
        private static bool fGE(string left, string right) { return left.CompareTo(right) >= 0; }
        internal static readonly GenericBinary<string, string> Add = new GenericBinary<string, string>(fAdd);
        internal static readonly GenericBinary<string, bool> EQ = new GenericBinary<string, bool>(fEQ);
        internal static readonly GenericBinary<string, bool> NE = new GenericBinary<string, bool>(fNE);
        internal static readonly GenericBinary<string, bool> LT = new GenericBinary<string, bool>(fLT);
        internal static readonly GenericBinary<string, bool> LE = new GenericBinary<string, bool>(fLE);
        internal static readonly GenericBinary<string, bool> GT = new GenericBinary<string, bool>(fGT);
        internal static readonly GenericBinary<string, bool> GE = new GenericBinary<string, bool>(fGE);
    }
}

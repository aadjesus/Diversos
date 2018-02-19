// Copyright (c) Microsoft Corporation.  All rights reserved.
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Linq.Expressions;

namespace Espresso
{

    internal delegate Expression ExpressionMapper(string name);

    internal class Parser
    {
        Lexer lexer;
        bool ignoreCase;
        DynamoFactory dynamizer;
        ExpressionMapper mapper;
        string lastIdentifier;

        public Parser()
        {
        }

        public bool IgnoreCase
        {
            get { return this.ignoreCase; }
            set { this.ignoreCase = value; }
        }

        public Expression Parse(string source)
        {
            return this.Parse(source, null);
        }

        public Expression Parse(string source, ExpressionMapper mapper)
        {
            if (source == null) throw new ArgumentNullException();
            this.lexer = new Lexer(new Scanner(source));
            this.mapper = mapper;
            Expression result = this.ParseExpression();
            if (this.lexer.Current.Id != TokenId.Unknown)
            {
                if (this.lexer.Current.Id == TokenId.Identifier)
                {
                    throw new ParseException(this.lexer.Current.Location, string.Format("Unexpected identifier '{0}'", this.lexer.Current.Text));
                }
                else
                {
                    throw new ParseException(this.lexer.Current.Location, string.Format("Unexpected token '{0}'", this.lexer.Current.Id));
                }
            }
            return result;
        }

        private Expression ParseExpression()
        {
            return this.ParseAnd();
        }

        private Expression ParseAnd()
        {
            Expression expr = this.ParseNot();
            Expression right;
            while (true)
            {
                Token tok = this.lexer.Current;
                if (this.IsIdentifier("and") || tok.Id == TokenId.DoubleAmpersand)
                {
                    CoerceToBool(ref expr);
                    this.lexer.MoveNext();
                    right = this.ParseNot();
                    CoerceToBool(ref right);
                    expr = Expression.And(expr, right);
                    continue;
                }
                else if (this.IsIdentifier("or") || tok.Id == TokenId.DoubleBar)
                {
                    CoerceToBool(ref expr);
                    this.lexer.MoveNext();
                    right = this.ParseNot();
                    CoerceToBool(ref right);
                    expr = Expression.Or(expr, right);
                    continue;
                }
                else
                {
                    break;
                }
            }
            return expr;
        }

        private Expression ParseNot()
        {
            if (this.IsIdentifier("not") || this.lexer.Current.Id == TokenId.ExclamationPoint)
            {
                this.lexer.MoveNext();
                Expression expr = this.ParseNot();
                CoerceToBool(ref expr);
                return Expression.Not(expr);
            }
            else
            {
                return this.ParseInEquality();
            }
        }

        private Expression ParseInEquality()
        {
            Expression expr = this.ParseEquality();
            Expression right;
            for (; ; )
            {
                Token tok = this.lexer.Current;
                switch (tok.Id)
                {
                    case TokenId.OpenAngleEqual:
                        this.lexer.MoveNext();
                        right = this.ParseInEquality();
                        CoerceBinaryArgs(tok, ref expr, ref right, true);
                        expr = Expression.LessThan(expr, right);
                        continue;
                    case TokenId.OpenAngle:
                        this.lexer.MoveNext();
                        right = this.ParseInEquality();
                        CoerceBinaryArgs(tok, ref expr, ref right, true);
                        expr = Expression.LessThanOrEqual(expr, right);
                        continue;
                    case TokenId.CloseAngleEqual:
                        this.lexer.MoveNext();
                        right = this.ParseInEquality();
                        CoerceBinaryArgs(tok, ref expr, ref right, true);
                        expr = Expression.GreaterThan(expr, right);
                        continue;
                    case TokenId.CloseAngle:
                        this.lexer.MoveNext();
                        right = this.ParseInEquality();
                        CoerceBinaryArgs(tok, ref expr, ref right, true);
                        expr = Expression.GreaterThanOrEqual(expr, right);
                        continue;
                }
                break;
            }
            return expr;
        }

        private Expression ParseEquality()
        {
            Expression expr = this.ParseMultiplicative();
            Expression right;
            Token tok = this.lexer.Current;
            switch (tok.Id)
            {
                case TokenId.DoubleEqual:
                case TokenId.Equals:
                    this.lexer.MoveNext();
                    right = this.ParseEquality();
                    CoerceBinaryArgs(tok, ref expr, ref right, true);
                    expr = Expression.Equal(expr, right);
                    break;
                case TokenId.ExclamationEqual:
                case TokenId.OpenCloseAngle:
                    this.lexer.MoveNext();
                    right = this.ParseEquality();
                    CoerceBinaryArgs(tok, ref expr, ref right, true);
                    expr = Expression.Negate(expr, right);
                    break;
            }
            return expr;
        }

        private Expression ParseMultiplicative()
        {
            Expression expr = this.ParseAdditive();
            Expression right;
            for (; ; )
            {
                Token tok = this.lexer.Current;
                switch (tok.Id)
                {
                    case TokenId.Asterisk:
                        this.lexer.MoveNext();
                        right = this.ParseAdditive();
                        CoerceBinaryArgs(tok, ref expr, ref right, false);
                        expr = Expression.Multiply(expr, right);
                        continue;
                    case TokenId.Slash:
                        this.lexer.MoveNext();
                        right = this.ParseAdditive();
                        CoerceBinaryArgs(tok, ref expr, ref right, false);
                        expr = Expression.Divide(expr, right);
                        continue;
                    case TokenId.Percent:
                        this.lexer.MoveNext();
                        right = this.ParseAdditive();
                        CoerceBinaryArgs(tok, ref expr, ref right, false);
                        expr = Expression.Modulo(expr, right);
                        continue;
                }
                break;
            }
            return expr;
        }

        private Expression ParseAdditive()
        {
            Expression expr = this.ParseBit();
            Expression right;
            for (; ; )
            {
                Token tok = this.lexer.Current;
                switch (tok.Id)
                {
                    case TokenId.Plus:
                        this.lexer.MoveNext();
                        right = this.ParseBit();
                        CoerceBinaryArgs(tok, ref expr, ref right, true);
                        expr = Expression.Add(expr, right);
                        continue;
                    case TokenId.Minus:
                        this.lexer.MoveNext();
                        right = this.ParseBit();
                        CoerceBinaryArgs(tok, ref expr, ref right, false);
                        expr = Expression.Subtract(expr, right);
                        continue;
                }
                break;
            }
            return expr;
        }

        private Expression ParseBit()
        {
            Expression expr = this.ParseNegation();
            Expression right;
            for (; ; )
            {
                Token tok = this.lexer.Current;
                switch (tok.Id)
                {
                    case TokenId.Ampersand:
                        this.lexer.MoveNext();
                        right = this.ParseNegation();
                        CoerceBinaryArgs(tok, ref expr, ref right, false);
                        expr = Expression.And(expr, right);
                        continue;
                    case TokenId.Bar:
                        this.lexer.MoveNext();
                        right = this.ParseNegation();
                        CoerceBinaryArgs(tok, ref expr, ref right, false);
                        expr = Expression.Or(expr, right);
                        continue;
                    //case TokenId.Hat:
                    //    this.lexer.MoveNext();
                    //    right = this.ParseNegation();
                    //    CoerceBinaryArgs(tok, ref expr, ref right, false);
                    //    expr = Expression. Xor(expr, right);
                    //    continue;
                }
                break;
            }
            return expr;
        }

        private Expression ParseNegation()
        {
            switch (this.lexer.Current.Id)
            {
                case TokenId.Minus:
                    this.lexer.MoveNext();
                    return Expression.Negate(this.ParseNegation());
                case TokenId.Squiggle:
                    this.lexer.MoveNext();
                    return Expression.Not(this.ParseNegation());
                default:
                    return this.ParseDynamo();
            }
        }

        private Expression ParseDynamo()
        {
            if (this.IsIdentifier("new"))
            {
                this.lexer.MoveNext();
                this.AssertToken(TokenId.OpenBrace);
                this.lexer.MoveNext();

                List<DynamoBinding> dynaBindings = new List<DynamoBinding>();
                dynaBindings.Add(this.ParseDynamoBinding());
                while (true)
                {
                    if (this.lexer.Current.Id != TokenId.Comma)
                        break;
                    this.lexer.MoveNext();
                    dynaBindings.Add(this.ParseDynamoBinding());
                }
                this.AssertToken(TokenId.CloseBrace);
                this.lexer.MoveNext();

                DynamoField[] fields = new DynamoField[dynaBindings.Count];
                for (int i = 0, n = fields.Length; i < n; i++)
                {
                    fields[i] = new DynamoField(dynaBindings[i].Name, dynaBindings[i].Expression.Type);
                }

                if (this.dynamizer == null)
                    this.dynamizer = new DynamoFactory();

                Type dynamoType = this.dynamizer.GetDynamo(fields);
                Binding[] bindings = new Binding[dynaBindings.Count];
                for (int i = 0, n = bindings.Length; i < n; i++)
                {
                    bindings[i] = Expression.Bind(dynamoType.GetField(dynaBindings[i].Name, BindingFlags.Instance | BindingFlags.Public), dynaBindings[i].Expression);
                }
                return Expression.New(dynamoType, bindings);
            }
            return this.ParseDot();
        }

        private DynamoBinding ParseDynamoBinding()
        {
            DynamoBinding db;
            Location loc = this.lexer.Current.Location;
            if (this.lexer.Current.Id == TokenId.Identifier &&
                this.lexer.Peek(1).Id == TokenId.Equals)
            {
                db.Name = ParseIdentifier();
                this.lexer.MoveNext();
                db.Expression = this.ParseExpression();
                return db;
            }
            this.lastIdentifier = null;
            db.Expression = this.ParseExpression();
            if (this.lastIdentifier != null)
            {
                db.Name = this.lastIdentifier;
                return db;
            }
            throw new ParseException(loc, "Name for binding cannot be inferred from expression");
        }

        struct DynamoBinding
        {
            internal string Name;
            internal Expression Expression;
        }

        private Expression ParseDot()
        {
            Expression expr = this.ParseTerminal();
            while (true)
            {
                if (this.lexer.Current.Id != TokenId.Dot)
                    break;
                this.lexer.MoveNext();
                Location loc = this.lexer.Current.Location;
                string id = this.ParseIdentifier();
                if (this.lexer.Current.Id == TokenId.OpenParen)
                {
                    expr = ParseMethodCall(expr, id);
                    continue;
                }
                else
                {
                    PropertyInfo prop = this.FindProperty(expr.Type, id);
                    if (prop != null)
                    {
                        expr = Expression.Property(expr, prop);
                        continue;
                    }
                    FieldInfo field = this.FindField(expr.Type, id);
                    if (field != null)
                    {
                        expr = Expression.Field(expr, field);
                        continue;
                    }
                    throw new ParseException(loc, string.Format("The type '{0}' does not have member '{1}'", expr.Type, id));
                }
            }
            return expr;
        }

        private Expression ParseMethodCall(Expression expr, string methodName)
        {
            Location loc = this.lexer.Current.Location;
            this.AssertToken(TokenId.OpenParen);
            this.lexer.MoveNext();

            Expression[] args = this.ParseArgList();

            this.AssertToken(TokenId.CloseParen);
            this.lexer.MoveNext();

            Type[] types = new Type[args.Length];
            for (int i = 0, n = args.Length; i < n; i++)
            {
                types[i] = args[i].Type;
            }
            MethodInfo mi = this.FindMethod(expr.Type, methodName, types);
            if (mi == null)
            {
                if (expr.Type.GetMethod(methodName) == null)
                {
                    throw new ParseException(loc, string.Format("The type '{0}' does not have method '{1}'", expr.Type, methodName));
                }
                else
                {
                    throw new ParseException(loc, string.Format("The type '{0}' does not have method overload for '{1}' that matches the specified arguments", expr.Type, methodName));
                }
            }
            return Expression.Call(mi, expr, args);
        }

        private bool ParameterTypesMatch(ParameterInfo[] paramInfos, Type[] argTypes)
        {
            if (paramInfos.Length != argTypes.Length)
                return false;
            for (int i = 0, n = paramInfos.Length; i < n; i++)
            {
                if (paramInfos[i].ParameterType != argTypes[i])
                    return false;
            }
            return true;
        }

        private Expression[] ParseArgList()
        {
            List<Expression> exprs = new List<Expression>();
            exprs.Add(this.ParseExpression());
            while (true)
            {
                if (this.lexer.Current.Id != TokenId.Comma)
                    break;
                this.lexer.MoveNext();
                exprs.Add(this.ParseExpression());
            }
            return exprs.ToArray();
        }

        private Expression ParseTerminal()
        {
            while (true)
            {
                switch (this.lexer.Current.Id)
                {
                    case TokenId.Literal:
                        Expression expr = Expression.Constant(this.lexer.Current.Value);
                        this.lexer.MoveNext();
                        return expr;
                    case TokenId.Identifier:
                        if (this.IsIdentifier("true"))
                        {
                            this.lexer.MoveNext();
                            return Expression.Constant(true);
                        }
                        else if (this.IsIdentifier("false"))
                        {
                            this.lexer.MoveNext();
                            return Expression.Constant(false);
                        }
                        else if (this.IsIdentifier("null"))
                        {
                            this.lexer.MoveNext();
                            return Expression.Constant(null);
                        }
                        else
                        {
                            Location loc = this.lexer.Current.Location;
                            string id = this.lexer.Current.Text;
                            this.lexer.MoveNext();
                            if (this.mapper != null)
                            {
                                Expression mex = this.mapper(id);
                                if (mex != null)
                                    return mex;
                            }
                            throw new ParseException(loc, string.Format("The name '{0}' is not defined", id));
                        }
                    case TokenId.OpenParen:
                        this.lexer.MoveNext();
                        Expression e = this.ParseExpression();
                        this.AssertToken(TokenId.CloseParen);
                        this.lexer.MoveNext();
                        return e;
                    default:
                        throw new ParseException(this.lexer.Current.Location, string.Format("Unexpected token '{0}'", this.lexer.Current.Id));
                }
            }
        }

        private string ParseIdentifier()
        {
            if (this.lexer.Current.Id != TokenId.Identifier)
                throw new ParseException(this.lexer.Current.Location, "Identifier Expected");
            string id = this.lexer.Current.Text;
            this.lexer.MoveNext();
            return id;
        }

        private bool IsIdentifier(string id)
        {
            return this.lexer.Current.Id == TokenId.Identifier &&
                   string.Compare(this.lexer.Current.Text, id, this.ignoreCase) == 0;
        }

        internal PropertyInfo FindProperty(Type type, string id)
        {
            PropertyInfo[] props = type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.FlattenHierarchy);
            for (int i = props.Length - 1; i >= 0; i--)
            {
                PropertyInfo pi = props[i];
                if (string.Compare(id, pi.Name, this.ignoreCase) == 0)
                    return pi;
            }
            return null;
        }

        internal FieldInfo FindField(Type type, string id)
        {
            FieldInfo[] fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.FlattenHierarchy);
            for (int i = fields.Length - 1; i >= 0; i--)
            {
                FieldInfo fi = fields[i];
                if (string.Compare(id, fi.Name, this.ignoreCase) == 0)
                    return fi;
            }
            return null;
        }

        internal MethodInfo FindMethod(Type type, string name, Type[] argTypes)
        {
            MethodInfo[] meths = type.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.FlattenHierarchy);
            for (int i = meths.Length - 1; i >= 0; i--)
            {
                MethodInfo mi = meths[i];
                if (string.Compare(name, mi.Name, this.ignoreCase) != 0)
                    continue;
                ParameterInfo[] pis = mi.GetParameters();
                if (!this.ParameterTypesMatch(pis, argTypes))
                    continue;
                return mi; // return first match
            }
            return null;
        }

        private void CoerceToBool(ref Expression expr)
        {
            AssertType(this.lexer.Current.Location, typeof(bool), expr.Type);
        }

        private void CoerceBinaryArgs(Token tok, ref Expression left, ref Expression right, bool allowToString)
        {
            Converter.PromoteBinaryArgs(ref left, ref right, allowToString);
            AssertBinaryArgsEqual(tok, left, right);
        }

        private void AssertIdentifier(string id)
        {
            if (!this.IsIdentifier(id))
                throw new ParseException(this.lexer.Current.Location, string.Format("Expecting '{0}'", id));
        }

        private void AssertToken(TokenId id)
        {
            if (this.lexer.Current.Id != id)
                throw new ParseException(this.lexer.Current.Location, string.Format("Expecting '{0}", id));
        }

        private void AssertBinaryArgsEqual(Token token, Expression e1, Expression e2)
        {
            if (e1.Type != e2.Type)
                throw new ParseException(
                    token.Location,
                    string.Format("Cannot apply binary operator {0} to arguments of type '{1}' and '{2}'",
                        token.Id, e1.Type, e2.Type
                    ));
        }

        private void AssertType(Location loc, Type check, Type actual)
        {
            if (check != actual)
            {
                throw new ParseException(loc, string.Format("Expecting type '{0}' not '{1}'", check, actual));
            }
        }
    }
}

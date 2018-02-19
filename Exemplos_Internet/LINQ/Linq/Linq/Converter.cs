// Copyright (c) Microsoft Corporation.  All rights reserved.
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Linq.Expressions;

namespace Espresso
{
    internal static class Converter
    {
        public static void PromoteBinaryArgs(ref Expression left, ref Expression right, bool promoteToString)
        {
            Type leftType = left.Type;
            Type rightType = right.Type;
            bool isLeftNullable = leftType.IsGenericType && leftType.GetGenericTypeDefinition() == typeof(Nullable<>);
            bool isRightNullable = rightType.IsGenericType && rightType.GetGenericTypeDefinition() == typeof(Nullable<>);
            if (isLeftNullable)
                leftType = leftType.GetGenericArguments()[0];
            if (isRightNullable)
                rightType = rightType.GetGenericArguments()[0];

            if (IsNumeric(leftType) && IsNumeric(rightType))
            {
                Type promo = GetNumericPromotion(leftType, rightType);
                if (leftType != promo)
                {
                    leftType = promo;
                    if (isLeftNullable)
                        leftType = typeof(Nullable<>).MakeGenericType(leftType);
                    //left = Expression.Cast(left, leftType);
                }
                if (rightType != promo)
                {
                    rightType = promo;
                    if (isRightNullable)
                        rightType = typeof(Nullable<>).MakeGenericType(rightType);
                    //right = Expression.Cast(right, rightType);
                }
            }
            else if (promoteToString && (leftType == typeof(string) || rightType == typeof(string)))
            {
                CoerceToString(ref left);
                CoerceToString(ref right);
            }
        }

        private static void CoerceToString(ref Expression expr)
        {
            if (expr.Type != typeof(string))
            {
                expr = Expression.Call(expr.Type.GetMethod("ToString", BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy, null, new Type[] { }, null), expr, null);
            }
        }

        private static bool IsNumeric(Type type)
        {
            switch (Type.GetTypeCode(type))
            {
                case TypeCode.SByte:
                case TypeCode.Byte:
                case TypeCode.Int16:
                case TypeCode.UInt16:
                case TypeCode.Int32:
                case TypeCode.UInt32:
                case TypeCode.Int64:
                case TypeCode.UInt64:
                case TypeCode.Single:
                case TypeCode.Double:
                case TypeCode.Decimal:
                    return true;
                default:
                    return false;
            }
        }

        private static Type GetNumericPromotion(Type t1, Type t2)
        {
            TypeCode tc1 = Type.GetTypeCode(t1);
            TypeCode tc2 = Type.GetTypeCode(t2);
            switch (tc1)
            {
                case TypeCode.SByte:
                case TypeCode.Int16:
                case TypeCode.Int32:
                    switch (tc2)
                    {
                        case TypeCode.SByte:
                        case TypeCode.Byte:
                        case TypeCode.Int16:
                        case TypeCode.Int32:
                        case TypeCode.UInt16:
                            return typeof(Int32);
                        case TypeCode.Int64:
                        case TypeCode.Single:
                        case TypeCode.Double:
                        case TypeCode.Decimal:
                            return t2;
                        case TypeCode.UInt32:
                            return typeof(Int64);
                        case TypeCode.UInt64:
                            return typeof(decimal);
                    }
                    break;
                case TypeCode.Int64:
                    switch (tc2)
                    {
                        case TypeCode.SByte:
                        case TypeCode.Byte:
                        case TypeCode.Int16:
                        case TypeCode.Int32:
                        case TypeCode.UInt16:
                        case TypeCode.UInt32:
                        case TypeCode.Int64:
                            return t1;
                        case TypeCode.Single:
                        case TypeCode.Double:
                        case TypeCode.Decimal:
                        case TypeCode.UInt64:
                            return typeof(decimal);
                    }
                    break;
                case TypeCode.Byte:
                case TypeCode.UInt16:
                    switch (tc2)
                    {
                        case TypeCode.SByte:
                        case TypeCode.Byte:
                        case TypeCode.UInt16:
                        case TypeCode.UInt32:
                        case TypeCode.Int16:
                        case TypeCode.Int32:
                            return typeof(Int32);
                        case TypeCode.UInt64:
                        case TypeCode.Int64:
                        case TypeCode.Single:
                        case TypeCode.Double:
                        case TypeCode.Decimal:
                            return t2;
                    }
                    break;
                case TypeCode.UInt32:
                    switch (tc2)
                    {
                        case TypeCode.Byte:
                        case TypeCode.UInt16:
                        case TypeCode.UInt32:
                            return t1;
                        case TypeCode.SByte:
                        case TypeCode.Int16:
                        case TypeCode.Int32:
                            return typeof(Int64);
                        case TypeCode.UInt64:
                        case TypeCode.Int64:
                            return t2;
                        case TypeCode.Single:
                        case TypeCode.Double:
                            return typeof(Double);
                        case TypeCode.Decimal:
                            return typeof(Decimal);
                    }
                    break;
                case TypeCode.UInt64:
                    switch (tc2)
                    {
                        case TypeCode.Byte:
                        case TypeCode.UInt16:
                        case TypeCode.UInt32:
                        case TypeCode.UInt64:
                            return typeof(UInt64);
                        case TypeCode.SByte:
                        case TypeCode.Int16:
                        case TypeCode.Int32:
                        case TypeCode.Int64:
                        case TypeCode.Decimal:
                        case TypeCode.Single:
                        case TypeCode.Double:
                            return typeof(decimal);
                    }
                    break;
                case TypeCode.Single:
                    switch (tc2)
                    {
                        case TypeCode.Byte:
                        case TypeCode.SByte:
                        case TypeCode.UInt16:
                        case TypeCode.Int16:
                        case TypeCode.Single:
                            return typeof(Single);
                        case TypeCode.UInt32:
                        case TypeCode.Int32:
                        case TypeCode.Double:
                            return typeof(Double);
                        case TypeCode.UInt64:
                        case TypeCode.Int64:
                        case TypeCode.Decimal:
                            return typeof(Decimal);
                    }
                    break;
                case TypeCode.Double:
                    switch (tc2)
                    {
                        case TypeCode.Byte:
                        case TypeCode.SByte:
                        case TypeCode.UInt16:
                        case TypeCode.Int16:
                        case TypeCode.Single:
                        case TypeCode.UInt32:
                        case TypeCode.Int32:
                        case TypeCode.Double:
                            return typeof(Double);
                        case TypeCode.UInt64:
                        case TypeCode.Int64:
                        case TypeCode.Decimal:
                            return typeof(Decimal);
                    }
                    break;
                case TypeCode.Decimal:
                    switch (tc2)
                    {
                        case TypeCode.Byte:
                        case TypeCode.SByte:
                        case TypeCode.UInt16:
                        case TypeCode.Int16:
                        case TypeCode.Single:
                        case TypeCode.UInt32:
                        case TypeCode.Int32:
                        case TypeCode.Double:
                        case TypeCode.UInt64:
                        case TypeCode.Int64:
                        case TypeCode.Decimal:
                            return typeof(Decimal);
                    }
                    break;
            }
            return null;
        }
    }
}

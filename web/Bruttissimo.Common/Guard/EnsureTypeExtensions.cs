using System;
using System.Diagnostics;
using Bruttissimo.Common.Extensions;
using Bruttissimo.Common.Guard.Resources;

namespace Bruttissimo.Common.Guard
{
    public static class EnsureTypeExtensions
    {
        private static class Types
        {
            internal static readonly Type IntType = typeof(int);

            internal static readonly Type ShortType = typeof(short);

            internal static readonly Type DecimalType = typeof(decimal);

            internal static readonly Type DoubleType = typeof(double);

            internal static readonly Type FloatType = typeof(float);

            internal static readonly Type BoolType = typeof(bool);

            internal static readonly Type DateTimeType = typeof(DateTime);

            internal static readonly Type StringType = typeof(string);
        }

        [DebuggerStepThrough]
        public static TypeParam IsInt(this TypeParam param)
        {
            return IsOfType(param, Types.IntType);
        }

        [DebuggerStepThrough]
        public static TypeParam IsShort(this TypeParam param)
        {
            return IsOfType(param, Types.ShortType);
        }

        [DebuggerStepThrough]
        public static TypeParam IsDecimal(this TypeParam param)
        {
            return IsOfType(param, Types.DecimalType);
        }

        [DebuggerStepThrough]
        public static TypeParam IsDouble(this TypeParam param)
        {
            return IsOfType(param, Types.DoubleType);
        }

        [DebuggerStepThrough]
        public static TypeParam IsFloat(this TypeParam param)
        {
            return IsOfType(param, Types.FloatType);
        }

        [DebuggerStepThrough]
        public static TypeParam IsBool(this TypeParam param)
        {
            return IsOfType(param, Types.BoolType);
        }

        [DebuggerStepThrough]
        public static TypeParam IsDateTime(this TypeParam param)
        {
            return IsOfType(param, Types.DateTimeType);
        }

        [DebuggerStepThrough]
        public static TypeParam IsString(this TypeParam param)
        {
            return IsOfType(param, Types.StringType);
        }

        [DebuggerStepThrough]
        public static TypeParam IsOfType(this TypeParam param, Type type)
        {
            if (param.Type != type)
                throw ExceptionFactory.Create(param, Exceptions.EnsureExtensions_IsNotOfType.FormatWith(type.FullName, param.Type.FullName));

            return param;
        }

        [DebuggerStepThrough]
        public static TypeParam IsOfType<T>(this TypeParam param)
        {
            if (!(param.Type == typeof(T)))
                throw ExceptionFactory.Create(param, Exceptions.EnsureExtensions_IsNotOfType.FormatWith(typeof(T).FullName, param.Type.FullName));

            return param;
        }
        
        [DebuggerStepThrough]
        public static TypeParam Subclasses<T>(this TypeParam param)
        {
            if (!(typeof(T).IsAssignableFrom(param.Type)))
                throw ExceptionFactory.Create(param, Exceptions.EnsureExtensions_IsNotOfType.FormatWith(typeof(T).FullName, param.Type.FullName));

            return param;
        }

        [DebuggerStepThrough]
        public static Param<Type> IsClass(this Param<Type> param)
        {
            if (param.Value == null)
                throw ExceptionFactory.Create(param, Exceptions.EnsureExtensions_IsNotClass_WasNull);

            if (!param.Value.IsClass)
                throw ExceptionFactory.Create(param, Exceptions.EnsureExtensions_IsNotClass.FormatWith(param.Value.FullName));

            return param;
        }
    }
}
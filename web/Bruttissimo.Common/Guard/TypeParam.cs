using System;

namespace Bruttissimo.Common.Guard
{
    public class TypeParam : Param
    {
        public readonly Type Type;

        internal TypeParam(string name, Type type, Func<string> extraMessage = null)
            : base(name, extraMessage)
        {
            Type = type;
        }
    }
}
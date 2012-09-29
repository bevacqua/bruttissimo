using System;

namespace Bruttissimo.Common.Guard
{
    public abstract class Param
    {
        public const string DefaultName = "";
        public Func<string> ExtraMessage;

        public readonly string Name;

        protected Param(string name, Func<string> extraMessage = null)
        {
            Name = name;
            ExtraMessage = extraMessage;
        }
    }

    public class Param<T> : Param
    {
        public readonly T Value;

        internal Param(string name, T value, Func<string> extraMessage = null)
            : base(name, extraMessage)
        {
            Value = value;
        }
    }
}
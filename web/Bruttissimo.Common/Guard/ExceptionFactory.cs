using System;
using System.Collections.Generic;
using Bruttissimo.Common.Guard.Resources;
using log4net;

namespace Bruttissimo.Common.Guard
{
    public static class ExceptionFactory
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(ExceptionFactory));

        internal static IDictionary<Type, Func<Param, string, Exception>> _factory;

        static ExceptionFactory()
        {
            _factory = new Dictionary<Type, Func<Param, string, Exception>>();
            _factory.Add(typeof(ArgumentException), (p, m) => new ArgumentException(GetMessage(p, m), p.Name));
            _factory.Add(typeof(ArgumentNullException), (p, m) => new ArgumentNullException(p.Name, GetMessage(p, m)));
            _factory.Add(typeof(ArgumentOutOfRangeException), (p, m) => new ArgumentOutOfRangeException(p.Name, GetMessage(p, m)));
            _factory.Add(typeof(InvalidOperationException), (p, m) => new InvalidOperationException(GetMessage(p, m)));
        }

        public static Exception Create(Param param, string message)
        {
            return Create<ArgumentException>(param, message);
        }

        public static Exception CreateForNull(Param param, string message)
        {
            return Create<ArgumentNullException>(param, message);
        }

        public static Exception CreateForNonNull(Param param, string message)
        {
            return Create<InvalidOperationException>(param, message);
        }

        public static Exception Create<TException>(Param param, string message) where TException : Exception
        {
            Type type = typeof(TException);
            if (_factory.ContainsKey(type))
            {
                return _factory[type](param, message);
            }
            log.Warn(Exceptions.ExceptionFactory_NotFound);
            return _factory[typeof(ArgumentException)](param, message);
        }

        internal static string GetMessage(Param param, string message)
        {
            if (param.ExtraMessage == null)
            {
                return message;
            }
            else
            {
                return string.Concat(message, Environment.NewLine, param.ExtraMessage());
            }
        }
    }
}
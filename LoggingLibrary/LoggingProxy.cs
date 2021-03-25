using System;
using System.Dynamic;
using ImpromptuInterface;

namespace LoggingLibrary
{
    public class LoggingProxy<T> : DynamicObject where T : class
    {
        private readonly T obj;
        private readonly ILogger logger;

        private LoggingProxy(T obj, ILogger logger)
        {
            this.obj = obj;
            this.logger = logger;
        }

        public static T CreateInstance(T obj, ILogger logger)
        {
            if (!typeof(T).IsInterface)
                throw new ArgumentException("T must be an interface type");

            return new LoggingProxy<T>(obj, logger).ActLike<T>();
        }

        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            try
            {
                logger.Info($"Invoking {GetTypeSignature(obj.GetType())}.{binder.Name} with arguments [{string.Join(",", args)}]");

                var argTypes = new Type[args.Length];
                var i = 0;

                foreach (var arg in args)
                {
                    argTypes[i++] = arg.GetType();
                }

                result = obj.GetType().GetMethod(binder.Name, argTypes).Invoke(obj, args);

                return true;
            }
            catch(Exception e)
            {
                logger.Error(e);

                result = null;

                return false;
            }
        }

        private static string GetTypeSignature(Type type)
        {
            if (type.IsGenericType)
            {
                string temp = type.GetGenericTypeDefinition().Name;
                string result = temp.Substring(0, temp.IndexOf('`')) + '<';

                Type[] arguments = type.GenericTypeArguments;
                foreach (var param in arguments)
                {
                    result += GetTypeSignature(param) + ", ";
                }

                return result.Trim(' ', ',') + ">";
            }
            else
            {
                return type.Name;
            }
        }
    }
}

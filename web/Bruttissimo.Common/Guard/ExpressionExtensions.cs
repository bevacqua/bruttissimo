using System.Linq.Expressions;

namespace Bruttissimo.Common.Guard
{
    internal static class ExpressionExtensions
    {
        internal static string ToPath(this MemberExpression e)
        {
            var path = "";
            var parent = e.Expression as MemberExpression;

            if (parent != null)
                path = parent.ToPath() + ".";

            return path + e.Member.Name;
        }

        internal static MemberExpression GetRightMostMember(this Expression expression)
        {
            var lambdaExpression = expression as LambdaExpression;
            if (lambdaExpression != null)
                return GetRightMostMember(lambdaExpression.Body);

            var rightMostMember = expression as MemberExpression;
            if (rightMostMember != null)
                return rightMostMember;

            var callExpression = expression as MethodCallExpression;
            if (callExpression != null)
            {
                if (callExpression.Object is MethodCallExpression || callExpression.Object is MemberExpression)
                    return GetRightMostMember(callExpression.Object);

                var member = callExpression.Arguments.Count > 0 ? callExpression.Arguments[0] : callExpression.Object;
                return GetRightMostMember(member);
            }

            var unaryExpression = expression as UnaryExpression;
            if (unaryExpression != null)
            {
                return GetRightMostMember(unaryExpression.Operand);
            }

            return null;
        }
    }
}
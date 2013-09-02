using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Dynamo.Templates
{
	internal class ExpressionHelper
	{
		public static MethodInfo GetMethodInfo(LambdaExpression expression)
		{
			MethodCallExpression outermostExpression = expression.Body as MethodCallExpression;

			if (outermostExpression == null)
			{
				throw new ArgumentException("Invalid Expression. Expression should consist of a Method call only.");
			}

			return outermostExpression.Method;
		}
	}
}

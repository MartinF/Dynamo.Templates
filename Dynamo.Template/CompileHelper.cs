using System;
using System.Linq.Expressions;
using System.Web.WebPages;
using Dynamo.Template.Core;

namespace Dynamo.Template
{
	internal static class CompileHelper
	{
		public static String CompileTemplate(String templateName, String source)
		{
			return new TemplateCompiler(templateName, source).Compile().ToString();
		}

		public static String CompileTemplate(String templateName, Func<String> sourceFactory)
		{
			return CompileTemplate(templateName, sourceFactory());
		}

		public static String CompileTemplate(String templateName, Func<HelperResult> sourceFactory)
		{
			return CompileTemplate(templateName, sourceFactory().ToString());
		}

		public static String CompileTemplate(Expression<Func<String>> sourceFactory)
		{
			var templateName = ExpressionHelper.GetMethodInfo(sourceFactory).Name;
			return CompileTemplate(templateName, sourceFactory.Compile()());
		}

		public static String CompileTemplate(Expression<Func<HelperResult>> sourceFactory)
		{
			var templateName = ExpressionHelper.GetMethodInfo(sourceFactory).Name;
			return CompileTemplate(templateName, sourceFactory.Compile());
		}
	}
}

using System;
using System.Linq.Expressions;
using System.Web.WebPages;
using Dynamo.Templates.Core;

namespace Dynamo.Templates
{
	internal static class CompileHelper
	{
		public static String CompileTemplate(String source)
		{
			// Default impl.
			return new MinifierCompiler<TemplateCompiler>(new TemplateCompiler(source)).Compile().ToString();
		}

		public static String CompileTemplate(Func<String> sourceFactory)
		{
			return CompileTemplate(sourceFactory());
		}

		public static String CompileTemplate(Func<HelperResult> sourceFactory)
		{
			return CompileTemplate(sourceFactory().ToString());
		}

		public static String CompileTemplate(Expression<Func<String>> sourceFactory)
		{
			return CompileTemplate(sourceFactory.Compile()());
		}

		public static String CompileTemplate(Expression<Func<HelperResult>> sourceFactory)
		{
			return CompileTemplate(sourceFactory.Compile());
		}
	}
}

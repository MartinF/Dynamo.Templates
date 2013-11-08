using System;
using System.Linq.Expressions;
using System.Web.WebPages;

// Any way to cache expressions and store method name and Func instead of having to get name and create func every time - GetHashCode()?

namespace Dynamo.Templates
{
	public static class TemplateCacheExtensions
	{
		public static String GetOrAdd(this ITemplateCache cache, String templateName, Func<HelperResult> sourceFactory)
		{
			if (cache == null)
				throw new ArgumentNullException("cache");
			if (templateName == null)
				throw new ArgumentNullException("templateName");
			if (sourceFactory == null)
				throw new ArgumentNullException("sourceFactory");
			
			return cache.GetOrAdd(templateName, (Func<String>)(() => sourceFactory().ToString()));
		}

		public static String GetOrAdd(this ITemplateCache cache, Expression<Func<String>> sourceFactory)
		{
			if (cache == null)
				throw new ArgumentNullException("cache");
			if (sourceFactory == null)
				throw new ArgumentNullException("sourceFactory");

			var templateName = ExpressionHelper.GetMethodInfo(sourceFactory).Name;

			return cache.GetOrAdd(templateName, (Func<String>)(() => sourceFactory.Compile()()));
		}

		public static String GetOrAdd(this ITemplateCache cache, Expression<Func<HelperResult>> sourceFactory)
		{
			if (cache == null)
				throw new ArgumentNullException("cache");
			if (sourceFactory == null)
				throw new ArgumentNullException("sourceFactory");

			var templateName = ExpressionHelper.GetMethodInfo(sourceFactory).Name;

			return cache.GetOrAdd(templateName, (Func<String>)(() => sourceFactory.Compile()().ToString()));
		}
	}
}

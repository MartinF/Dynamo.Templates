using System;
using System.Linq.Expressions;
using System.Web;
using System.Web.WebPages;

// Currently just using simple Singleton implementation
// Use the framework Cache feature instead of creating my own - make it configurable?
// Get vs Compile as method name ?
// Method that automatically wraps in a Script tag ? GetWithScript(Tag) / GetInScript / GetWrappedInScript?

namespace Dynamo.Template
{
	public static class Template
	{
		// Fields
		private static readonly ITemplateCache _cache = new TemplateCache();

		// Properties
		public static ITemplateCache Cache { get { return _cache; } }

		// Methods
		public static IHtmlString Get(String templateName, Func<String> sourceFactory, Boolean debugMode)
		{
			if (debugMode)
			{
				return new HtmlString(CompileHelper.CompileTemplate(templateName, sourceFactory));
			}

			return new HtmlString(_cache.GetOrAdd(templateName, sourceFactory));
		}

		public static IHtmlString Get(String templateName, Func<HelperResult> sourceFactory, Boolean debugMode)
		{
			if (debugMode)
			{
				return new HtmlString(CompileHelper.CompileTemplate(templateName, sourceFactory));
			}

			return new HtmlString(_cache.GetOrAdd(templateName, sourceFactory));
		}

		public static IHtmlString Get(Expression<Func<String>> sourceFactory, Boolean debugMode)
		{
			if (debugMode)
			{
				return new HtmlString(CompileHelper.CompileTemplate(sourceFactory));
			}

			return new HtmlString(_cache.GetOrAdd(sourceFactory));
		}

		public static IHtmlString Get(Expression<Func<HelperResult>> sourceFactory, Boolean debugMode)
		{
			if (debugMode)
			{
				return new HtmlString(CompileHelper.CompileTemplate(sourceFactory));
			}

			return new HtmlString(_cache.GetOrAdd(sourceFactory));
		}

		public static IHtmlString Get(String templateName, Func<String> sourceFactory)
		{
			var debugMode = IsDebuggingEnabled();
			return Get(templateName, sourceFactory, debugMode);
		}

		public static IHtmlString Get(String templateName, Func<HelperResult> sourceFactory)
		{
			var debugMode = IsDebuggingEnabled();
			return Get(templateName, sourceFactory, debugMode);
		}

		public static IHtmlString Get(Expression<Func<String>> sourceFactory)
		{
			var debugMode = IsDebuggingEnabled();
			return Get(sourceFactory, debugMode);
		}

		public static IHtmlString Get(Expression<Func<HelperResult>> sourceFactory)
		{
			var debugMode = IsDebuggingEnabled();
			return Get(sourceFactory, debugMode);
		}

		private static Boolean IsDebuggingEnabled()
		{
			var debugMode = false;

			if (HttpContext.Current != null)
			{
				debugMode = HttpContext.Current.IsDebuggingEnabled;
			}

			return debugMode;
		}
	}
}

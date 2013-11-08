using System;
using System.Linq.Expressions;
using System.Web;
using System.Web.WebPages;

// Currently just using simple Singleton implementation for the Cache
	// Make it configurable? Request Mvc DepdencyResolver for interface and if not returned use own default implementation?
	// Use the framework Caching feature (System.Runtime.Caching.ObjectCache) instead of creating my own
// Include a way to get by Key without providing the source?

namespace Dynamo.Templates
{
	public static class Template
	{
		// Fields
		private static readonly ITemplateCache _cache = new TemplateCache();

		// Properties
		public static ITemplateCache Cache { get { return _cache; } }

		// Methods
		public static IHtmlString Get(String key, Func<String> sourceFactory, Boolean debugMode)
		{
			if (debugMode)
			{
				return new HtmlString(CompileHelper.CompileTemplate(sourceFactory));
			}

			return new HtmlString(_cache.GetOrAdd(key, sourceFactory));
		}

		public static IHtmlString Get(String key, Func<HelperResult> sourceFactory, Boolean debugMode)
		{
			if (debugMode)
			{
				return new HtmlString(CompileHelper.CompileTemplate(sourceFactory));
			}

			return new HtmlString(_cache.GetOrAdd(key, sourceFactory));
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
		
		public static IHtmlString Get(String key, Func<String> sourceFactory)
		{
			var debugMode = IsDebuggingEnabled();
			return Get(key, sourceFactory, debugMode);
		}

		public static IHtmlString Get(String key, Func<HelperResult> sourceFactory)
		{
			var debugMode = IsDebuggingEnabled();
			return Get(key, sourceFactory, debugMode);
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

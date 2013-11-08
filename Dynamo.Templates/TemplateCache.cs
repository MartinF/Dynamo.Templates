using System;
using System.Collections.Concurrent;

// Store all compiled sources as MvcHtmlString and return MvcHtmlString instead? - ties it to Mvc
// Take a CompilerFactory (Func<ICompiler>) that is used to create Compiler instance

namespace Dynamo.Templates
{
	public class TemplateCache : ITemplateCache
	{
		// Fields
		private readonly ConcurrentDictionary<String, String> _cache = new ConcurrentDictionary<String, String>();

		// Methods
		public String GetOrAdd(String key, Func<String> sourceFactory)
		{
			if (key == null)
				throw new ArgumentNullException("key");
			if (sourceFactory == null)
				throw new ArgumentNullException("sourceFactory");

			return _cache.GetOrAdd(key, (x) => CompileHelper.CompileTemplate(sourceFactory));	// x is the key
		}
	}
}

﻿using System;
using System.Collections.Concurrent;

// Store all compiled sources as MvcHtmlString and return MvcHtmlString instead?

namespace Dynamo.Template
{
	public class TemplateCache : ITemplateCache
	{
		// Fields
		private readonly ConcurrentDictionary<String, String> _cache = new ConcurrentDictionary<String, String>();

		// Methods
		public String GetOrAdd(String templateName, Func<String> sourceFactory)
		{
			if (templateName == null)
				throw new ArgumentNullException("templateName");
			if (sourceFactory == null)
				throw new ArgumentNullException("sourceFactory");

			return _cache.GetOrAdd(templateName, (x) => CompileHelper.CompileTemplate(x, sourceFactory));
		}
	}
}

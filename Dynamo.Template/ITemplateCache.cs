using System;

namespace Dynamo.Template
{
	public interface ITemplateCache
	{
		String GetOrAdd(String templateName, Func<String> sourceFactory);
	}
}
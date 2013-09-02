using System;

namespace Dynamo.Templates
{
	public interface ITemplateCache
	{
		String GetOrAdd(String templateName, Func<String> sourceFactory);
	}
}
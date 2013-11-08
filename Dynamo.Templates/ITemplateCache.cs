using System;

namespace Dynamo.Templates
{
	public interface ITemplateCache
	{
		String GetOrAdd(String key, Func<String> sourceFactory);
	}
}
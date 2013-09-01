using System;
using System.Text;
using Microsoft.Ajax.Utilities;

// Throw CompileException if any errors?

namespace Dynamo.Template.Core
{
	public class MinifiedTemplateCompiler : TemplateCompiler
	{
		private readonly CodeSettings _settings = new CodeSettings() { OutputMode = OutputMode.SingleLine };

		public MinifiedTemplateCompiler(String templateName, String source)
			: base(templateName, source)
		{
		}

		public override StringBuilder Compile()
		{
			var compiledTemplate = base.Compile().ToString();

			var minifier = new Minifier();
			var minifiedTemplate = minifier.MinifyJavaScript(compiledTemplate, _settings);

			//if (minifier.ErrorList.Count > 0)
			//{
			//	string errors = minifier.ErrorList.Aggregate("", (current, error) => current + error);
			//	throw new CompileException(errors);
			//}

			return new StringBuilder(minifiedTemplate);
		}
	}
}

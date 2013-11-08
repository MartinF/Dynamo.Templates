using System;
using System.Text;
using Microsoft.Ajax.Utilities;

// Just a wrapper around any other compiler that minifies the output of Compile()

// Throw CompileException if any errors?

namespace Dynamo.Templates.Core
{
	public class MinifierCompiler<TCompiler> : ICompiler
		where TCompiler : class, ICompiler
	{
		private readonly CodeSettings _settings = new CodeSettings() { OutputMode = OutputMode.SingleLine };

		public MinifierCompiler(TCompiler compiler)
		{
			if (compiler == null)
				throw new ArgumentNullException("compiler");

			InnerCompiler = compiler;
		}

		public ICompiler InnerCompiler { get; set; }

		public StringBuilder Compile()
		{
			var compiledTemplate = InnerCompiler.Compile().ToString();

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

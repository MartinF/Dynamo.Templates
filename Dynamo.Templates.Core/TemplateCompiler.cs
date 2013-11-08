using System;
using System.Text;

// Compiles template as an anonymous function

namespace Dynamo.Templates.Core
{
	public class TemplateCompiler : SourceCompiler, ICompiler
	{
		public TemplateCompiler(String source)
			: base(source)
		{
		}

		// Methods
		public override StringBuilder Compile()
		{
			var sb = base.Compile();

			// function open
			sb.Insert(0, "function (model){");
			// function close
			sb.Append("return " + JSVariableName + ";}");

			return sb;
		}
	}
}

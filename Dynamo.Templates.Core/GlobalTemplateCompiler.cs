using System;
using System.Text;

// Compiles Template as a global Function - function TemplateName(model) { ... }

// Need to check that templateName in constructor is a valid javascript method name

namespace Dynamo.Templates.Core
{
	public class GlobalTemplateCompiler : SourceCompiler, ICompiler
	{
		public GlobalTemplateCompiler(String templateName, String source)
			: base(source)
		{
			if (String.IsNullOrWhiteSpace(templateName))
				throw new ArgumentException("Is null or whitespace", "templateName");

			TemplateName = templateName;
		}

		// Properties
		public String TemplateName { get; private set; }

		// Methods
		public override StringBuilder Compile()
		{
			var sb = base.Compile();

			// function open
			sb.Insert(0, "function " + TemplateName + "(model){");
			// function close
			sb.Append("return " + JSVariableName + ";}");
			
			return sb;
		}
	}
}

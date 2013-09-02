using System;
using System.Text;

// Need to check that templateName in constructor is a valid javascript method name

namespace Dynamo.Templates.Core
{
	public class TemplateCompiler : SourceCompiler, ICompiler
	{
		private readonly String _templateName;

		public TemplateCompiler(String templateName, String source)
			: base(source)
		{
			if (String.IsNullOrWhiteSpace(templateName))
				throw new ArgumentException("Is null or whitespace", "templateName");

			_templateName = templateName;
		}

		// Properties
		public String TemplateName { get; set; }

		// Methods
		public override StringBuilder Compile()
		{
			var sb = base.Compile();

			// function open
			sb.Insert(0, "function " + _templateName + "(model){");
			// function close
			sb.Append("return " + JSVariableName + ";}");
			
			return sb;
		}
	}
}

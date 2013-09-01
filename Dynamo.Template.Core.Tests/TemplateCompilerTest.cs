using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dynamo.Template.Core.Tests
{
	[TestClass]
	public class TemplateCompilerTest
	{
		private const String TemplateName = "methodName";

		[TestMethod]
		public void SimpleTemplateCompilesCorrectly()
		{
			var source = GetTemplateSource("simple.html");
			var compiler = new TemplateCompiler(TemplateName, source);
			var result = compiler.Compile().ToString();

			Assert.AreEqual(result, "function methodName(model){var t='';t+='<div>'+ model.test ;t+='</div>';return t;}");
		}

		[TestMethod]
		public void SimpleBlockTemplateCompilesCorrectly()
		{
			var source = GetTemplateSource("simple-block.html");
			var compiler = new TemplateCompiler(TemplateName, source);
			var result = compiler.Compile().ToString();

			Assert.AreEqual(result, "function methodName(model){var t='';t+='<ul>'; for(var i = 0; i < model.users.length; i++) { t+='<li>'+ model.users[i].name ;t+='</li>'; } t+='</ul>';return t;}");			
		}

		private static string GetTemplateSource(String filename)
		{
			String templateFolder = @"..\..\templates\";
			var path = templateFolder + filename;

			return File.ReadAllText(path);
		}
	}
}

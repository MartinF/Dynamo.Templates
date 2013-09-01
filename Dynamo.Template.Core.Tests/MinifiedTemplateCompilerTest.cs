using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dynamo.Template.Core.Tests
{
	[TestClass]
	public class MinifiedTemplateCompilerTest
	{
		private const String TemplateName = "methodName";

		[TestMethod]
		public void SimpleTemplateCompilesCorrectly()
		{
			var source = GetTemplateSource("simple.html");
			var compiler = new MinifiedTemplateCompiler(TemplateName, source);
			var result = compiler.Compile().ToString();

			Assert.AreEqual(result, "function methodName(n){var t=\"\";return t+=\"<div>\"+n.test,t+\"<\\/div>\"}");
		}

		[TestMethod]
		public void SimpleBlockTemplateCompilesCorrectly()
		{
			var source = GetTemplateSource("simple-block.html");
			var compiler = new MinifiedTemplateCompiler(TemplateName, source);
			var result = compiler.Compile().ToString();

			Assert.AreEqual(result, "function methodName(n){var t=\"\",i;for(t+=\"<ul>\",i=0;i<n.users.length;i++)t+=\"<li>\"+n.users[i].name,t+=\"<\\/li>\";return t+\"<\\/ul>\"}");
		}

		private static string GetTemplateSource(String filename)
		{
			String templateFolder = @"..\..\templates\";
			var path = templateFolder + filename;

			return File.ReadAllText(path);
		}
	}
}

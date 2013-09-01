using System;
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
			var source = FileHelper.GetTemplateSource("simple.html");
			var compiler = new MinifiedTemplateCompiler(TemplateName, source);
			var result = compiler.Compile().ToString();

			Assert.AreEqual(result, "function methodName(n){var t=\"\";return t+=\"<div>\"+n.test,t+\"<\\/div>\"}");
		}

		[TestMethod]
		public void SimpleBlockTemplateCompilesCorrectly()
		{
			var source = FileHelper.GetTemplateSource("simple-block.html");
			var compiler = new MinifiedTemplateCompiler(TemplateName, source);
			var result = compiler.Compile().ToString();

			Assert.AreEqual(result, "function methodName(n){var t=\"\",i;for(t+=\"<ul>\",i=0;i<n.users.length;i++)t+=\"<li>\"+n.users[i].name,t+=\"<\\/li>\";return t+\"<\\/ul>\"}");
		}
	}
}

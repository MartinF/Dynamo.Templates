using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dynamo.Templates.Core.Tests
{
	[TestClass]
	public class MinifiedTemplateCompilerTest
	{
		[TestMethod]
		public void SimpleTemplateCompilesCorrectly()
		{
			var source = FileHelper.GetTemplateSource("simple.html");
			var compiler = new TemplateCompiler(source);
			var minifier = new MinifierCompiler<TemplateCompiler>(compiler);
			
			var result = minifier.Compile().ToString();

			Assert.AreEqual(result, "function(n){var t=\"\";return t+=\"<div>\"+n.test,t+\"<\\/div>\"}");
		}

		[TestMethod]
		public void SimpleBlockTemplateCompilesCorrectly()
		{
			var source = FileHelper.GetTemplateSource("simple-block.html");
			var compiler = new TemplateCompiler(source);
			var minifier = new MinifierCompiler<TemplateCompiler>(compiler);

			var result = minifier.Compile().ToString();

			Assert.AreEqual(result, "function(n){var t=\"\",i;for(t+=\"<ul>\",i=0;i<n.users.length;i++)t+=\"<li>\"+n.users[i].name,t+=\"<\\/li>\";return t+\"<\\/ul>\"}");
		}
	}
}

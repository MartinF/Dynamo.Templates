﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dynamo.Templates.Core.Tests
{
	[TestClass]
	public class GlobalTemplateCompilerTest
	{
		private const String TemplateName = "methodName";

		[TestMethod]
		public void SimpleTemplateCompilesCorrectly()
		{
			var source = FileHelper.GetTemplateSource("simple.html");
			var compiler = new GlobalTemplateCompiler(TemplateName, source);
			var result = compiler.Compile().ToString();

			Assert.AreEqual(result, "function methodName(model){var t='';t+='<div>'+ model.test ;t+='</div>';return t;}");
		}

		[TestMethod]
		public void SimpleBlockTemplateCompilesCorrectly()
		{
			var source = FileHelper.GetTemplateSource("simple-block.html");
			var compiler = new GlobalTemplateCompiler(TemplateName, source);
			var result = compiler.Compile().ToString();

			Assert.AreEqual(result, "function methodName(model){var t='';t+='<ul>'; for(var i = 0; i < model.users.length; i++) { t+='<li>'+ model.users[i].name ;t+='</li>'; } t+='</ul>';return t;}");			
		}
	}
}

using System;
using System.IO;

namespace Dynamo.Template.Core.Tests
{
	internal static class FileHelper
	{
		private const String _templateFolder = @"..\..\templates\";

		public static string GetTemplateSource(String filename)
		{
			var path = _templateFolder + filename;
			return File.ReadAllText(path);
		}
	}
}

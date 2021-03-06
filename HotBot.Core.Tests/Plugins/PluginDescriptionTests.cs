﻿using HotBot.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace HotBot.Core.Plugins.Tests
{
	[TestClass()]
	public class PluginDescriptionTests
	{
		[TestMethod()]
		public void PluginDescription_Constructor()
		{
			var name = "HelloWorld1";
			var desc = "Description test!";
			var pluginDescription = new PluginDescription(name, desc);

			TestUtils.AssertArgumentException(() => new PluginDescription(null, ""));
			TestUtils.AssertArgumentException(() => new PluginDescription("", ""));
			TestUtils.AssertArgumentException(() => new PluginDescription("!!!", ""));
			TestUtils.AssertArgumentException(() => new PluginDescription("A!0", ""));
			TestUtils.AssertArgumentException(() => new PluginDescription("ABC", null));

			Assert.AreEqual(name, pluginDescription.Name);
			Assert.AreEqual(desc, pluginDescription.Description);
		}

		[TestMethod()]
		public void VerifyName()
		{
			TestUtils.AssertArgumentException(() => PluginDescription.VerifyName(null));
			TestUtils.AssertArgumentException(() => PluginDescription.VerifyName(""));
			TestUtils.AssertArgumentException(() => PluginDescription.VerifyName("!"));
			TestUtils.AssertArgumentException(() => PluginDescription.VerifyName("a!a"));
			TestUtils.AssertArgumentException(() => PluginDescription.VerifyName("a 0a"));
			PluginDescription.VerifyName("a");
			PluginDescription.VerifyName("0");
			PluginDescription.VerifyName("a00aa0");
		}

		[TestMethod()]
		public void VerifyDescription()
		{
			TestUtils.AssertArgumentException(() => PluginDescription.VerifyDescription(null));

			PluginDescription.VerifyDescription("");
			PluginDescription.VerifyDescription("a");
			PluginDescription.VerifyDescription("0");
			PluginDescription.VerifyDescription("a00aa0");
			PluginDescription.VerifyDescription("hello world!!");
			PluginDescription.VerifyDescription(":D");
		}
	}
}
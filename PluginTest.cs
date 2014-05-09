using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using NUnit.Framework;

namespace Wox.Plugin.Putty {
[TestFixture]
public class PluginTest {
	[Test]
	public void TestQuery() {
		Assert.AreEqual(18, new PuttyPlugin().Query(new Query("pt")).Count);
		Assert.AreEqual(3, new PuttyPlugin().Query(new Query("pt tel")).Count);
		Assert.AreEqual(1, new PuttyPlugin().Query(new Query("pt we123")).Count);
		List<Result> res = new PuttyPlugin().Query(new Query("pt tel"));
		foreach (Result r in res) {
			Console.Out.WriteLine(r);
		}
	}
}
// end namespace
}

using System.Diagnostics;
using System.Linq;
using System.Xml.Linq;
using NUnit.Framework;

namespace MSBuild.Community.Tasks.Tests
{
    [TestFixture]
    public partial class UpdatePlistTest
    {
        [Test, Explicit]
        public void TestPlistModification()
        {
            var debugPlistDoc = XDocument.Parse(DebugPlist);
            var task = new UpdatePlist
            {
                Protocol = "myproto",
                BundleVersion = "1.2.3.456789",
                BundleShortVersionString = "1.2.3",
                BundleIdentifier = "company.fancy_app",
                BundleURLName = "com.pany.fancy_app",
                BundleDisplayName = "App",
                PlistPath = null,
            };

            var newDoc = task.ModifyPlistXml(debugPlistDoc);
            var oldDoc = XDocument.Parse(DebugPlist);
            Assert.AreEqual(newDoc.Descendants().Count(), oldDoc.Descendants().Count());

            var dictElement = newDoc.Element("plist")?.Element("dict") ?? throw new AssertionException("root is null");
            var expectedStrings = new[]
            {
                task.Protocol, task.BundleVersion, task.BundleShortVersionString, task.BundleIdentifier, task.BundleDisplayName,
                task.BundleURLName
            };
            foreach (var expectedString in expectedStrings)
            {
                AssertOneElementWithValue(dictElement, expectedString);
            }
        }

        private static void AssertOneElementWithValue(XElement dictElement, string value)
        {
            Assert.AreEqual(
                1,
                dictElement.Descendants().Count(el => el.Name == "string" && el.Value == value),
                $"Expected there to be one occurrence of '{value}'"
            );
        }
    }
}
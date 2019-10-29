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
                BundleIdentifier = "com.company.app",
                BundleURLName = "com.company.app",
                BundleDisplayName = "App",
                PlistPath = null,
            };

            var newDoc = task.ModifyPlistXml(debugPlistDoc);
            var oldDoc = XDocument.Parse(DebugPlist);
            Assert.Equals(newDoc.Root, oldDoc.Root);
        }
    }
}
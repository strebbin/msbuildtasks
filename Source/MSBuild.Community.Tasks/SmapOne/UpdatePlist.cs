//-----------------------------------------------------------------------
// <copyright file="Sound.cs" company="MSBuild Community Tasks Project">
//     Copyright © 2008 Ignaz Kohlbecker
// </copyright>
//-----------------------------------------------------------------------


using System.Linq;
using System.Xml.Linq;

namespace MSBuild.Community.Tasks
{
    using System.IO;
    using Microsoft.Build.Utilities;

	public class UpdatePlist : Task
	{
        #region Input Parameters

        public string Protocol { get; set; }

        public string BundleURLName { get; set; }

        public string BundleVersion { get; set; }

        public string BundleShortVersionString { get; set; }

        public string BundleIdentifier { get; set; }

        public string BundleDisplayName { get; set; }

        public string PlistPath { get; set; }

        public bool VersionOnly { get; set; }

        #endregion Input Parameters

        #region Task overrides

        public override bool Execute()
		{
            if (!File.Exists(PlistPath))
            {
                Log.LogError(string.Format("The appxmanifest could not be located at path '{0}'", PlistPath));
                return false;
            }

            var doc = XDocument.Load(PlistPath);

            var plist = doc.Element("plist");
            if (plist == null)
            {
                return false;
            }

            try
            {
                var keys = plist.Descendants("key").ToList();

                var bundleShortVersion = keys.Single(k => k.Value == "CFBundleShortVersionString").NextNode as XElement;
                bundleShortVersion.Value = BundleShortVersionString;

                var bundleVersion = keys.Single(k => k.Value == "CFBundleVersion").NextNode as XElement;
                bundleVersion.Value = BundleVersion;

                if (!VersionOnly)
                {
                    var bundleDisplayName = keys.Single(k => k.Value == "CFBundleDisplayName").NextNode as XElement;
                    bundleDisplayName.Value = BundleDisplayName;

                    var bundleIdentifier = keys.Single(k => k.Value == "CFBundleIdentifier").NextNode as XElement;
                    bundleIdentifier.Value = BundleIdentifier;

                    var bundleUrlName = keys.Single(k => k.Value == "CFBundleURLName").NextNode as XElement;
                    bundleUrlName.Value = BundleURLName;

                    var urlScheme = keys.Single(k => k.Value == "CFBundleURLSchemes").NextNode as XElement;
                    var urlString = urlScheme.Elements().First();
                    urlString.Value = Protocol;
                }

                doc.Save(PlistPath);
                return true;
            }
            catch (System.NullReferenceException)
            {
                return false;
            }
        }

	    #endregion Task overrides
	}
}
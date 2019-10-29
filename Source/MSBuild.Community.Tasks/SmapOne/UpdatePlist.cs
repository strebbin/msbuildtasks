//-----------------------------------------------------------------------
// <copyright file="Sound.cs" company="MSBuild Community Tasks Project">
//     Copyright © 2008 Ignaz Kohlbecker
// </copyright>
//-----------------------------------------------------------------------


using System;
using System.Collections.Generic;
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

                var bundleShortVersion = GetValueElement(keys, "CFBundleShortVersionString");
                bundleShortVersion.Value = BundleShortVersionString;

                var bundleVersion = GetValueElement(keys, "CFBundleVersion");
                bundleVersion.Value = BundleVersion;

                if (!VersionOnly)
                {
                    var bundleDisplayName = GetValueElement(keys, "CFBundleDisplayName");
                    bundleDisplayName.Value = BundleDisplayName;

                    var bundleIdentifier = GetValueElement(keys, "CFBundleIdentifier");
                    bundleIdentifier.Value = BundleIdentifier;

                    var bundleUrlName = GetValueElement(keys, "CFBundleURLName");
                    bundleUrlName.Value = BundleURLName;

                    var urlScheme = GetValueElement(keys, "CFBundleURLSchemes");
                    var urlString = urlScheme.Elements().First();
                    urlString.Value = Protocol;
                }

                doc.Save(PlistPath);
                return true;
            }
            catch (System.NullReferenceException e)
            {
                Log.LogError($"NullRef: {e.Message}, data {e.Data}.  Trace: {e.StackTrace}");
                return false;
            }
        }

        private static XElement GetValueElement(List<XElement> keys, string key)
        {
            var nextNode = keys.Single(k => k.Value == key).NextNode as XElement;
            if (nextNode == null)
            {
                throw new Exception($"Could not find value element for key '{key}' in plist file");
            }

            return nextNode;
        }

	    #endregion Task overrides
	}
}
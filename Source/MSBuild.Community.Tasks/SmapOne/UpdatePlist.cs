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
            try
            {
                doc = ModifyPlistXml(doc);
            }
            catch (NullReferenceException e)
            {
                Log.LogError($"NullRef: {e.Message}, data {e.Data}.  Trace: {e.StackTrace}");
                return false;
            }
            catch (Exception e)
            {
                // TODO make this a `PListModifyException`
                Log.LogError($"Got an exception: {e.Message}");
                return false;
            }
            doc.Save(PlistPath);

            return true;
        }

        /// <summary>
        /// Modifies the PList xml file by setting certain values.
        /// </summary>
        /// <param name="doc">The plist xml document.  This is going to be modified as a side effect.</param>
        /// <returns>The modified plist xml document</returns>
        /// <exception cref="Exception"></exception>
        // ReSharper disable once MemberCanBePrivate.Global
        public XDocument ModifyPlistXml(XDocument doc)
        {
            var keys = KeysFromPlistDoc(doc);

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

            return doc;
        }

        internal static List<XElement> KeysFromPlistDoc(XDocument doc)
        {
            var plist = doc.Element("plist");
            if (plist == null)
            {
                throw new Exception("No plist tag!");
            }

            return plist.Descendants("key").ToList();
        }

        /// <exception cref="Exception"></exception>
        internal static XElement GetValueElement(List<XElement> keys, string key)
        {
            var keyNode = keys.Single(k => k.Value == key);
            var nextNodes = keyNode.NodesAfterSelf().TakeWhile(el => !(el is XElement xel && xel.Name == "key"));
            // We don't cover
            try
            {
                // Comments are not `XElement`s because they e.g. have no names
                return nextNodes.OfType<XElement>().First();
            }
            catch (InvalidOperationException)  // when there is nothing for `First()`
            {
                throw new Exception($"Could not find value element for key '{key}' in plist file");
            }
        }

	    #endregion Task overrides
	}
}
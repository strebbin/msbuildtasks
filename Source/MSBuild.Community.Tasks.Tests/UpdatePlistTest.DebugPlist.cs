namespace MSBuild.Community.Tasks.Tests
{
    public partial class UpdatePlistTest
    {
        private const string DebugPlist = @"<?xml version=""1.0"" encoding=""utf-8""?>
<!DOCTYPE plist PUBLIC ""-//Apple//DTD PLIST 1.0//EN"" ""http://www.apple.com/DTDs/PropertyList-1.0.dtd""[]>
<plist version=""1.0"">
  <dict>
    <key>CFBundleDisplayName</key>
    <!-- Anzeigename -->
    <string>Some name</string>
    <key>CFBundleIdentifier</key>
    <string>com.company.app.dev</string>
    <!--<string>com.company.app.dev</string>-->
    <key>CFBundleShortVersionString</key>
    <!-- Versions Nummer fuer Store -->
    <string>1.23.45</string>
    <key>CFBundleVersion</key>
    <string>1.23.0456789</string>
    <key>CFBundleDevelopmentRegion</key>
    <string>en_US</string>
    <key>CFBundleLocalizations</key>
    <array>
      <string>de</string>
      <string>en</string>
    </array>
    <key>MinimumOSVersion</key>
    <string>9.0</string>
    <key>NSLocationWhenInUseUsageDescription</key>
    <string>Please allow company app to use the location.</string>
    <key>NSCameraUsageDescription</key>
    <string>Please allow company app to use the camera.</string>
    <key>NSPhotoLibraryUsageDescription</key>
    <string>Please allow company app to access the picture gallery.</string>
    <key>NSPhotoLibraryAddUsageDescription</key>
    <string>Please allow company app to access the picture gallery.</string>
    <key>NSMicrophoneUsageDescription</key>
    <string>Please allow company app to use the microphone.</string>
    <key>UIBackgroundModes</key>
    <array>
      <string>remote-notification</string>
    </array>
    <key>CFBundleURLTypes</key>
    <array>
      <dict>
        <key>CFBundleURLTypes</key>
        <string>Viewer</string>
        <key>CFBundleURLName</key>
            <!-- Wie CFBundleIdentifier -->
            <string>com.company.app.dev</string>
            <!--<string>com.company.app.dev</string>-->
        <key>CFBundleURLSchemes</key>
        <array>
          <!-- Kommentar -->
          <string>companydev</string>
          <!--<string>companydev</string>-->
        </array>
      </dict>
    </array>
    <key>UIDeviceFamily</key>
    <array>
      <integer>1</integer>
      <integer>2</integer>
    </array>
  </dict>
</plist>";
    }
}
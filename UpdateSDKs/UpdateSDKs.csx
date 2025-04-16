#!/usr/bin/env dotnet-script

/*
 * This is a C# script which does the following:
 * - Pull the latest Android binaries
 * - Pull the latest iOS binaries
 * - Reflects the latest native Android SDK in the Android bindings project
 * - Reflects the latest native iOS SDK in the iOS bindings project
 *
 * https://github.com/dotnet-script/dotnet-script
 */

#r "nuget: LibGit2Sharp, 0.29.0"

using System.IO;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using LibGit2Sharp;
using Version = System.Version;

if (Args == null || Args.Count == 0)
{
    Console.WriteLine("Usage: dotnet script UpdateSDKs.csx -- <platform:ios|android> [x.y.z]");
    throw new ArgumentNullException(nameof(Args));
}

string platform = Args[0].ToString().ToLower();
Version version = Version.TryParse(Args.Skip(1).FirstOrDefault(), out Version parsed)
    ? parsed
    : null;
Console.WriteLine($"Platform: '{platform}', Version: '{version}'");

string root = new FileInfo(GetSourceFile()).Directory.Parent.FullName;

switch (platform)
{
    case "android":
        Console.WriteLine("Updating the Android project...");
        UpdateAndroid(root, version);
        break;
    case "ios":
        Console.WriteLine("Updating the iOS project...");
        UpdateiOS(root, version);
        break;
    default:
        throw new ArgumentException($"Unknown platform: {platform}");
}

private static void UpdateAndroid(string root, Version version)
{
    string androidBinary = "cobrowse-sdk-android-binary";
    using var android = new Repository(Path.Combine(root, androidBinary));
    Console.WriteLine("Android repository is at {0}", android.Info.Path);
    android.PullLatest();

    string androidVersion;
    if (version != null)
    {
        androidVersion = version.ToString();
        Tag tag = android.Tags["v" + androidVersion];
        if (tag == null)
        {
            throw new Exception("Cannot find tag v" + androidVersion);
        }
        Commit targetCommit = (tag.Target as Commit) 
            ?? ((tag.Target as TagAnnotation)?.Target as Commit);
        Commands.Checkout(android, targetCommit);
        Console.WriteLine("Requested native Android SDK is {0}", androidVersion);     
    }
    else
    {
        string maven = Path.Combine(android.Info.WorkingDirectory, "io", "cobrowse", "cobrowse-sdk-android", "maven-metadata.xml");
        Debug.Assert(File.Exists(maven));

        androidVersion = Regex.Match(File.ReadAllText(maven), @"\<release\>([\S]*?)\<\/release\>").Groups[1].Value;
        Console.WriteLine("Latest native Android SDK is {0}", androidVersion);
    }
    
    string csproj = Path.Combine(root, "Android", "CobrowseIO.Android", "CobrowseIO.Android.csproj");
    Debug.Assert(File.Exists(csproj));

    string text = File.ReadAllText(csproj);

    // Replace Version
    text = new Regex(@"<Version>[0-9]+\.[0-9]+\.[0-9]+<\/Version>")
        .Replace(text,
                 "<Version>" + androidVersion + "</Version>");

    // Replace AndroidLibrary
    text = new Regex("(?<=AndroidLibrary\\ Include\\=\").*(?=\")")
        .Replace(text,
                 @"..\..\" + androidBinary + @"\io\cobrowse\cobrowse-sdk-android\" + androidVersion + @"\cobrowse-sdk-android-" + androidVersion + ".aar");

    // Replace JavaDocJar
    text = new Regex("(?<=JavaDocJar\\ Include\\=\").*(?=\")")
        .Replace(text,
                 @"..\..\" + androidBinary + @"\io\cobrowse\cobrowse-sdk-android\" + androidVersion + @"\cobrowse-sdk-android-" + androidVersion + "-javadoc.jar");

    // Replace pom comment
    text = new Regex(@"https:\/\/github\.com\/cobrowseio\/cobrowse-sdk-android-binary\/blob\/master\/io\/cobrowse\/cobrowse-sdk-android\/[0-9]+\.[0-9]+\.[0-9]+\/cobrowse-sdk-android-[0-9]+\.[0-9]+\.[0-9]+\.pom")
        .Replace(text,
                 "https://github.com/cobrowseio/cobrowse-sdk-android-binary/blob/master/io/cobrowse/cobrowse-sdk-android/" + androidVersion + "/cobrowse-sdk-android-" + androidVersion + ".pom");

    File.WriteAllText(csproj, text);
}

private static void UpdateiOS(string root, Version version)
{
    string iOSBinary = "cobrowse-sdk-ios-binary";
    using var iOS = new Repository(Path.Combine(root, iOSBinary));
    Console.WriteLine("iOS repository is at {0}", iOS.Info.Path);
    iOS.PullLatest();

    string iOSVersion;
    if (version != null)
    {
        iOSVersion = version.ToString();
        Tag tag = iOS.Tags["v" + iOSVersion];
        if (tag == null)
        {
            throw new Exception("Cannot find tag v" + iOSVersion);
        }
        Commit targetCommit = (tag.Target as Commit) 
            ?? ((tag.Target as TagAnnotation)?.Target as Commit);
        Commands.Checkout(iOS, targetCommit);
        Console.WriteLine("Requested native iOS SDK is {0}", iOSVersion);     
    }
    else
    {
        string podspec = Path.Combine(iOS.Info.WorkingDirectory, "CobrowseIO.podspec");
        Debug.Assert(File.Exists(podspec));

        iOSVersion = Regex.Match(File.ReadAllText(podspec), @"s\.version = '([\S]*?)'").Groups[1].Value;
        Console.WriteLine("Latest native iOS SDK is {0}", iOSVersion);
    }

    string csproj = Path.Combine(root, "iOS", "CobrowseIO.iOS", "CobrowseIO.iOS.csproj");
    Debug.Assert(File.Exists(csproj));

    string text = File.ReadAllText(csproj);

    // Replace Version
    text = new Regex(@"<Version>[0-9]+\.[0-9]+\.[0-9]+<\/Version>")
        .Replace(text,
                    "<Version>" + iOSVersion + "</Version>");

    File.WriteAllText(csproj, text);
}

private static string GetSourceFile([CallerFilePath] string file = "") => file;

private static void PullLatest(this Repository repo)
{
    Commands.Checkout(repo, "master");
    Commands.Pull(
        repo,
        new Signature(
            new Identity("Cobrowse.io Bot", "github@cobrowse.io"),
            DateTimeOffset.Now),
        new PullOptions { MergeOptions = new MergeOptions { FastForwardStrategy = FastForwardStrategy.Default } });
}

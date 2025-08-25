using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;
using System;
using System.IO;
using System.Collections.Generic;

public static class BuildScript
{
    public static void BuildiOS()
    {
        string iosDirEnv = Environment.GetEnvironmentVariable("UNITY_IOS_DIR");
        if (string.IsNullOrEmpty(iosDirEnv)) iosDirEnv = "iOSBuild";
        string buildPath = Path.GetFullPath(Path.Combine(Application.dataPath, "..", iosDirEnv));
        Directory.CreateDirectory(buildPath);

        EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.iOS, BuildTarget.iOS);
        PlayerSettings.SetScriptingBackend(BuildTargetGroup.iOS, ScriptingImplementation.IL2CPP);
        PlayerSettings.SetArchitecture(BuildTargetGroup.iOS, 1);
        PlayerSettings.iOS.sdkVersion = iOSSdkVersion.DeviceSDK;
        if (string.IsNullOrEmpty(PlayerSettings.iOS.buildNumber))
            PlayerSettings.iOS.buildNumber = "1";

        PlayerSettings.defaultInterfaceOrientation = UIOrientation.Portrait;
        PlayerSettings.allowedAutorotateToPortrait = true;
        PlayerSettings.allowedAutorotateToPortraitUpsideDown = false;
        PlayerSettings.allowedAutorotateToLandscapeLeft = false;
        PlayerSettings.allowedAutorotateToLandscapeRight = false;

#if UNITY_2021_3_OR_NEWER
        PlayerSettings.iOS.hideHomeButton = false;
        PlayerSettings.iOS.cameraUsageDescription = null;      
        PlayerSettings.iOS.microphoneUsageDescription = null;   
#endif

        string[] scenes = GetEnabledScenes();
        if (scenes.Length == 0)
        {
            throw new Exception("No enabled scenes in Build Settings. Add at least one scene to build.");
        }

        var options = new BuildPlayerOptions
        {
            scenes = scenes,
            locationPathName = buildPath,
            target = BuildTarget.iOS,
            options = BuildOptions.None
        };

        BuildReport report = BuildPipeline.BuildPlayer(options);
        var result = report.summary.result;

        Debug.Log($"Build completed with a result of '{result}'");

        if (result != BuildResult.Succeeded)
        {
            throw new Exception($"iOS Build failed. Errors: {report.summary.totalErrors}, Warnings: {report.summary.totalWarnings}");
        }

        Debug.Log($"✅ iOS Xcode project exported to: {buildPath}");
    }

    private static string[] GetEnabledScenes()
    {
        List<string> enabled = new List<string>();
        foreach (var s in EditorBuildSettings.scenes)
            if (s.enabled) enabled.Add(s.path);
        return enabled.ToArray();
    }
}

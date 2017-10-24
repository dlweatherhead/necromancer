using UnityEngine;
using UnityEditor;

public class BuildScript {
	private static int major_version = 0;
	private static int minor_version = 1;

	private static string build_dir = "Build";
	private static string app_name = "Necromancer";
	private static string app_build_name = app_name + "." + major_version + "." + minor_version;

	private static string win64_dir = build_dir + "/Windows_64/" + app_build_name + ".exe";
	private static string mac64_dir = build_dir + "/OSX_64/" + app_build_name;
	private static string webgl_dir = build_dir + "/WebGL/" + app_build_name;
	private static string android_dir = build_dir + "/Android/" + app_build_name + ".apk";

	private static string[] scenes = { 
		"Assets/Scenes/Tutorial.unity", 
		"Assets/Scenes/dungeon.unity"
	};

	private static BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();

	static BuildScript() {
		buildPlayerOptions.scenes = scenes;
		buildPlayerOptions.options = BuildOptions.None;
	}

	public const string testThingy = "Build/Test Thingy";

	[MenuItem("Build/CleanBuildDirectory")]
	static void DeleteBuildDir() {
		FileUtil.DeleteFileOrDirectory (build_dir);
	}

	[MenuItem(testThingy)]
	static void TestThingy() {
		Debug.Log ("TestThingy Invoked");
	}

	[MenuItem("Build/Build All")]
	static void BuildAll() {
		DeleteBuildDir ();
		BuildWindows64 ();
		BuildMac64 ();
		BuildWebGL ();
		BuildAndroid ();
	}

	[MenuItem("Build/Build OSX")]
	static void BuildMac64() {
		buildPlayerOptions.locationPathName = mac64_dir;
		buildPlayerOptions.target = BuildTarget.StandaloneOSXIntel64;
		BuildPipeline.BuildPlayer (buildPlayerOptions);
	}

	[MenuItem("Build/Build Android")]
	static void BuildAndroid() {
		buildPlayerOptions.locationPathName = android_dir;
		buildPlayerOptions.target = BuildTarget.Android;
		BuildPipeline.BuildPlayer (buildPlayerOptions);
	}

	[MenuItem("Build/Build WebGL")]
	static void BuildWebGL() {
		buildPlayerOptions.locationPathName = webgl_dir;
		buildPlayerOptions.target = BuildTarget.WebGL;
		BuildPipeline.BuildPlayer (buildPlayerOptions);
	}

	[MenuItem("Build/Build Windows")]
	static void BuildWindows64() {
		buildPlayerOptions.locationPathName = win64_dir;
		buildPlayerOptions.target = BuildTarget.StandaloneWindows64;
		BuildPipeline.BuildPlayer (buildPlayerOptions);
	}

	[MenuItem("Build/Is Building")]
	static void IsBuilding() {
		Debug.Log("Is Building: " + BuildPipeline.isBuildingPlayer);
		System.Diagnostics.Debug.WriteLine ("Is Building: " + BuildPipeline.isBuildingPlayer);
	}
}

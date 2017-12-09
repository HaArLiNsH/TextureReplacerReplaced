using UnityEditor;

public class BuildShaders {

    [MenuItem("My Mod/Build Bundles")]
    static void BuildAllAssetBundles()
    {
        BuildPipeline.BuildAssetBundles("Shaders_W", BuildAssetBundleOptions.CollectDependencies, BuildTarget.StandaloneWindows);
        BuildPipeline.BuildAssetBundles("Shaders_L", BuildAssetBundleOptions.CollectDependencies, BuildTarget.StandaloneLinuxUniversal);
        BuildPipeline.BuildAssetBundles("Shaders_M", BuildAssetBundleOptions.CollectDependencies, BuildTarget.StandaloneOSXUniversal);
    }
}

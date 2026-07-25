using UnityEditor;
using System.IO;
public class CreateAssetBundles
{
    [MenuItem("Assets/Build AssetBundles")]
    public static void BuildAllAssetBundles()
    {
        string assetBundleDirectory = "Assets/StreamingAssets/AssetBundles";
        if (!Directory.Exists(assetBundleDirectory))
        {
            //Build new
            Directory.CreateDirectory(assetBundleDirectory);
        }

        BuildPipeline.BuildAssetBundles(
            assetBundleDirectory,
            BuildAssetBundleOptions.None,
            EditorUserBuildSettings.activeBuildTarget
        );
    }
    [MenuItem("Assets/Reload AssetBundles")]
    public static void BuildAllAssetBundlesHard()
    {
        string assetBundleDirectory = "Assets/StreamingAssets/AssetBundles";
        if (!Directory.Exists(assetBundleDirectory))
        {
            //Build new
            Directory.CreateDirectory(assetBundleDirectory);
        }
        else
        {
            // Delete all current to account for changes
            Directory.Delete(assetBundleDirectory, true);
            Directory.CreateDirectory(assetBundleDirectory);
        }

        BuildPipeline.BuildAssetBundles(
            assetBundleDirectory,
            BuildAssetBundleOptions.None,
            EditorUserBuildSettings.activeBuildTarget
        );
    }
}

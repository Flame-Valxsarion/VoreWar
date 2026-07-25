#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
public class AssetBundlesPreBuilder : IPreprocessBuildWithReport
{
    public int callbackOrder { get { return 100; } }
    public void OnPreprocessBuild(BuildReport report)
    {
        
    }
}
#endif
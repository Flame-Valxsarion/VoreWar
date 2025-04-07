using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LumberSiteSettings : MonoBehaviour
{
    public InputField WorkerCap;

    internal void Save()
    {
        Config.BuildConfig.LumberSiteWorkerCap = int.TryParse(WorkerCap.text, out int wc) ? wc : 2;
    }

    internal void Load()
    {
        WorkerCap.text = Config.BuildConfig.LumberSiteWorkerCap.ToString();
    }
}

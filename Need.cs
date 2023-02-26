using iSurvivedBonelab.MonoBehaviours;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnhollowerBaseLib;
using UnityEngine;
using UnityEngine.UI;

namespace iSurvivedBonelab
{
    [System.Serializable]
    public class Need
    {
        public string DisplayName;
        public bool enabled;

        public Color barColor;
        public Sprite icon;

        public Gauge gauge;

        public bool decayHealthWhenEmpty;
        public bool passiveDecay;

        public float curValue;
        public float maxValue;
        public float startValue;
        public float decayRate;
        public float regenRate;
        public float healthDecayRate;

        public NeedPref prefs;
        public NeedEle ele;

        public void Add(float amount)
        {
            if (enabled)
                curValue = Mathf.Min(curValue + amount, maxValue);
        }

        public void Subtract(float amount)
        {
            if (enabled)
                curValue = Mathf.Min(curValue - amount, 0.0f);
        }

        public float GetPercentage()
        {
            return curValue / maxValue;
        }
        
        public void CreatePrefsAndEle()
        {
            prefs = new NeedPref(this);
            prefs.Create(Prefs.root_categ);
            ele = new NeedEle(this);
            ele.Create(MenuStuff.root_categ, MenuStuff.menuColor);
        }

        public static class GaugeMaker
        {
            private static AssetBundle _bundle;
            private static List<GameObject> _bundleObjects;

            public static AssetBundle Bundle => _bundle;
            public static IReadOnlyList<GameObject> BundleObjects { get => _bundleObjects.AsReadOnly(); }

            public static void CreateTemplateBundle()
            {
                _bundleObjects = new List<GameObject>();
                _bundle = GetTemplateBundle();
                _bundle.hideFlags = HideFlags.DontUnloadUnusedAsset;

                Il2CppReferenceArray<Object> assets = Bundle.LoadAllAssets();

                foreach (var asset in assets)
                {
                    if (asset.TryCast<GameObject>() != null)
                    {
                        GameObject assetObject = asset.Cast<GameObject>();
                        assetObject.hideFlags = HideFlags.DontUnloadUnusedAsset;
                        _bundleObjects.Add(assetObject);
                    }
                }
            }

            public static GameObject FindBundleObject(string name)
            {
                return _bundleObjects.Find(x => x.name == name);
            }

            public static AssetBundle GetTemplateBundle()
            {
                Assembly assembly = Assembly.GetExecutingAssembly();

                string path = "iSurvivedBonelab.Assets.gaugetemplate";

                using (Stream resourceStream = assembly.GetManifestResourceStream(path))
                {
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        resourceStream.CopyTo(memoryStream);
                        return AssetBundle.LoadFromMemory(memoryStream.ToArray());
                    }
                }
            }

            public static Gauge SetupGauge(Need need)
            {
                Transform gaugeTransform = FindBundleObject("NeedGaugeTemplate").transform;

                gaugeTransform.name = need.DisplayName + "Gauge";
                gaugeTransform.parent = Main.hud.GetComponent<PlayerNeeds>().needGaugeContainer; //TODO: make compatible with effects

                Gauge gauge = gaugeTransform.GetComponent<Gauge>();
                gauge.simpleBar.fillRect.GetComponent<Image>().color = need.barColor;
                gauge.simpleBar.transform.Find("label").GetComponent<TMPro.TMP_Text>().text = need.DisplayName;
                gauge.imageBar.sprite = need.icon;
                
                return gauge;
            }
        }

    }
}

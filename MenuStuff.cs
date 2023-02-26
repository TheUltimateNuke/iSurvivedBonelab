using BoneLib.BoneMenu;
using BoneLib.BoneMenu.Elements;
using MelonLoader;
using System.IO;
using System.Reflection;
using UnhollowerBaseLib;
using UnityEngine;
using System.Collections.Generic;
using System;

namespace iSurvivedBonelab
{
    [RegisterTypeInIl2Cpp]
    public class NeedEle
    {
        public SubPanelElement subPanelEle;

        public BoolElement enabledEle;
        public BoolElement passiveDecayEle;
        public BoolElement decayHealthEle;

        public FloatElement decayRateEle;
        public FloatElement startValueEle;
        public FloatElement maxValueEle;
        public FloatElement regenRateEle;
        public FloatElement healthDecayRateEle;

        private Need Need { get; }

        public NeedEle(Need need)
        {
            Need = need;
        }

        public void Create(MenuCategory rootCateg, Color menuColor)
        {
            subPanelEle = rootCateg.CreateSubPanel(Need.DisplayName + " Settings", menuColor);

            enabledEle = subPanelEle.CreateBoolElement(Need.DisplayName + " Enabled", menuColor, Need.prefs.enabledEnt.Value);
            passiveDecayEle = subPanelEle.CreateBoolElement(Need.DisplayName + " Passive Decay", menuColor, Need.prefs.passiveDecayEnt.Value);
            decayHealthEle = subPanelEle.CreateBoolElement(Need.DisplayName + " Health Decay", menuColor, Need.prefs.decayHealthEnt.Value);

            decayRateEle = subPanelEle.CreateFloatElement(Need.DisplayName + " Decay Rate", menuColor, Need.prefs.decayRateEnt.Value, 0.1f, 0f, float.MaxValue);
            startValueEle = subPanelEle.CreateFloatElement(Need.DisplayName + " Start Value", menuColor, Need.prefs.startValueEnt.Value, 0.1f, 0f, float.MaxValue);
            maxValueEle = subPanelEle.CreateFloatElement(Need.DisplayName + " Max Value", menuColor, Need.prefs.maxValueEnt.Value, 0.1f, 0f, float.MaxValue);
            regenRateEle = subPanelEle.CreateFloatElement(Need.DisplayName + " Regen Rate", menuColor, Need.prefs.regenRateEnt.Value, 0.1f, 0f, float.MaxValue);
            healthDecayRateEle = subPanelEle.CreateFloatElement(Need.DisplayName + " Health Decay Rate", menuColor, Need.prefs.healthDecayRateEnt.Value, 0.1f, 0f, float.MaxValue);
        }

        public void Update()
        {
            Need.enabled = enabledEle.Value;
            Need.passiveDecay = passiveDecayEle.Value;
            Need.decayHealthWhenEmpty = decayHealthEle.Value;

            Need.decayRate = decayRateEle.Value;
            Need.startValue = startValueEle.Value;
            Need.maxValue = maxValueEle.Value;
            Need.regenRate = regenRateEle.Value;
            Need.healthDecayRate = healthDecayRateEle.Value;
        }
    }

    [RegisterTypeInIl2Cpp]
    internal static class MenuStuff
    {
        public static Color menuColor = new Color(173, 113, 0);

        public static MenuCategory root_categ;

        private static BoolElement _autoEnableEle;

        private static BoolElement _hudEnabledEle;
        private static FloatElement _hudXEle;
        private static FloatElement _hudYEle;
        private static FloatElement _hudZEle;
        private static IntElement _hudHandEle;
        private static IntElement _hudTypeEle;


        internal static void CreateElements()
        {
            // Create root categ
            root_categ = MenuManager.CreateCategory("BLSurvival", menuColor);

            // HUD settings
            SubPanelElement hud_categ = root_categ.CreateSubPanel("HUD Settings", menuColor);
            _hudEnabledEle = hud_categ.CreateBoolElement("Hud Enabled", menuColor, Prefs.hudEnabledEnt.Value);
            _hudXEle = hud_categ.CreateFloatElement("Hud Offset X", menuColor, Prefs.hudOffsetXEnt.Value, 0.01f, -10f, 10f);
            _hudYEle = hud_categ.CreateFloatElement("Hud Offset Y", menuColor, Prefs.hudOffsetYEnt.Value, 0.01f, -10f, 10f);
            _hudZEle = hud_categ.CreateFloatElement("Hud Offset Z", menuColor, Prefs.hudOffsetZEnt.Value, 0.01f, -10f, 10f);
            _hudTypeEle = hud_categ.CreateIntElement("Hud Type", menuColor, Prefs.hudTypeEnt.Value, 1, 0, 1);
            _hudHandEle = hud_categ.CreateIntElement("Hud Hand", menuColor, Prefs.hudHandEnt.Value, 1, 0, 1);

            // Create settings subpanels for each system
            // Need Settings

            // Temperature Settings
            SubPanelElement tempur_categ = root_categ.CreateSubPanel("Temperature Settings", menuColor);
            tempur_categ.CreateFunctionElement("TBD", menuColor, () => Melon<Main>.Logger.Msg("Player pressed useless FunctionElement!"));

            // Disease Settings
            SubPanelElement disease_categ = root_categ.CreateSubPanel("Disease Settings", menuColor);
            disease_categ.CreateFunctionElement("TBD", menuColor, () => Melon<Main>.Logger.Msg("Player pressed useless FunctionElement!"));

            // Create root settings
            _autoEnableEle = root_categ.CreateBoolElement("Enable Mod", menuColor, true);
        }

        internal static void UpdateSettings()
        {
            // Update the prefs according to the BoneMenu element values

            // Root Prefs
            Prefs.autoEnableEnt.Value = _autoEnableEle.Value;

            // HUD Prefs
            Prefs.hudEnabledEnt.Value = _hudEnabledEle.Value;
            Prefs.hudOffsetXEnt.Value = _hudXEle.Value;
            Prefs.hudOffsetYEnt.Value = _hudYEle.Value;
            Prefs.hudOffsetZEnt.Value = _hudZEle.Value;
            Prefs.hudOffsetEnt.Value = new Vector3(Prefs.hudOffsetXEnt.Value, Prefs.hudOffsetYEnt.Value, Prefs.hudOffsetZEnt.Value);
            Prefs.hudHandEnt.Value = _hudHandEle.Value;
            Prefs.hudTypeEnt.Value = _hudTypeEle.Value;
        }

        public static class HudBundleStuff
        {
            private static AssetBundle _bundle;
            private static System.Collections.Generic.List<GameObject> _bundleObjects;

            public static AssetBundle bundle => _bundle;
            public static System.Collections.Generic.IReadOnlyList<GameObject> BundleObjects { get => _bundleObjects.AsReadOnly(); }

            public static void Init()
            {
                _bundleObjects = new List<GameObject>();
                _bundle = GetHudBundle();
                _bundle.hideFlags = HideFlags.DontUnloadUnusedAsset;

                Il2CppReferenceArray<UnityEngine.Object> assets = bundle.LoadAllAssets();

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

            public static AssetBundle GetHudBundle()
            {
                Assembly assembly = Assembly.GetExecutingAssembly();

                string path = "iSurvivedBonelab.Assets.blsurvivalhud";

                using (Stream resourceStream = assembly.GetManifestResourceStream(path))
                {
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        resourceStream.CopyTo(memoryStream);
                        return AssetBundle.LoadFromMemory(memoryStream.ToArray());
                    }
                }
            }
        }
    }
}

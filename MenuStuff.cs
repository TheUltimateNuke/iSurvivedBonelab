using BoneLib;
using BoneLib.BoneMenu;
using BoneLib.BoneMenu.Elements;
using MelonLoader;
using SLZ.Interaction;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace iSurvivedBonelab
{
    internal static class MenuStuff
    {
        private static Color menuColor = new Color(173, 113, 0);

        public static GameObject menuAsset;

        private static MenuCategory root_categ;

        private static BoolElement autoEnableEle;

        private static BoolElement hudEnabledEle;
        private static FloatElement hudXEle;
        private static FloatElement hudYEle;
        private static FloatElement hudZEle;
        private static IntElement hudHandEle;
        private static IntElement hudTypeEle;

        private static BoolElement hungerEnabledEle;
        private static FloatElement hungerDecayRateEle;
        private static FloatElement hungerStartValueEle;
        private static FloatElement maxHungerEle;

        private static BoolElement thirstEnabledEle;
        private static FloatElement thirstDecayRateEle;
        private static FloatElement thirstStartValueEle;
        private static FloatElement maxThirstEle;


        internal static void CreateElements()
        {
            // Create root categ
            root_categ = MenuManager.CreateCategory("BLSurvival", menuColor);

            // HUD settings
            SubPanelElement hud_categ = root_categ.CreateSubPanel("HUD Settings", menuColor);
            hudEnabledEle = hud_categ.CreateBoolElement("Hud Enabled", menuColor, Prefs.hudEnabledEnt.Value);
            hudXEle = hud_categ.CreateFloatElement("Hud Offset X", menuColor, Prefs.hudOffsetXEnt.Value, 0.01f, -10f, 10f);
            hudYEle = hud_categ.CreateFloatElement("Hud Offset Y", menuColor, Prefs.hudOffsetYEnt.Value, 0.01f, -10f, 10f);
            hudZEle = hud_categ.CreateFloatElement("Hud Offset Z", menuColor, Prefs.hudOffsetZEnt.Value, 0.01f, -10f, 10f);
            hudTypeEle = hud_categ.CreateIntElement("Hud Type", menuColor, Prefs.hudTypeEnt.Value, 1, 0, 1);
            hudHandEle = hud_categ.CreateIntElement("Hud Hand", menuColor, Prefs.hudHandEnt.Value, 1, 0, 1);

            // Create settings subpanels for each system
            // Hunger Settings
            SubPanelElement hunger_categ = root_categ.CreateSubPanel("Hunger Settings", menuColor);
            hungerEnabledEle = hud_categ.CreateBoolElement("Hunger Enabled", menuColor, NeedsStuff.hungerNeed.enabled);
            hungerDecayRateEle = hunger_categ.CreateFloatElement("Hunger Decay Rate", menuColor, NeedsStuff.hungerNeed.decayRate, 0.5f, 0.5f, float.MaxValue);
            hungerStartValueEle = hunger_categ.CreateFloatElement("Hunger Start Value", menuColor, NeedsStuff.hungerNeed.startValue, 0.5f, 0.5f, float.MaxValue);
            maxHungerEle = hunger_categ.CreateFloatElement("Max Hunger", menuColor, NeedsStuff.hungerNeed.maxValue, 10, 10, float.MaxValue);

            // Thirst Settings
            SubPanelElement thirst_categ = root_categ.CreateSubPanel("Thirst Settings", menuColor);
            thirstEnabledEle = hud_categ.CreateBoolElement("Thirst Enabled", menuColor, NeedsStuff.thirstNeed.enabled);
            thirstDecayRateEle = thirst_categ.CreateFloatElement("Thirst Decay Time", menuColor, NeedsStuff.thirstNeed.decayRate, 0.5f, 0.5f, float.MaxValue);
            thirstStartValueEle = hunger_categ.CreateFloatElement("Thirst Start Value", menuColor, NeedsStuff.thirstNeed.startValue, 1f, 100f, float.MaxValue);
            maxThirstEle = thirst_categ.CreateFloatElement("Max Thirst", menuColor, NeedsStuff.thirstNeed.maxValue, 10, 10, float.MaxValue);

            // Temperature Settings
            SubPanelElement tempur_categ = root_categ.CreateSubPanel("Temperature Settings", menuColor);
            tempur_categ.CreateFunctionElement("TBD", menuColor, () => Melon<Main>.Logger.Msg("Player pressed useless FunctionElement!"));

            // Disease Settings
            SubPanelElement disease_categ = root_categ.CreateSubPanel("Disease Settings", menuColor);
            disease_categ.CreateFunctionElement("TBD", menuColor, () => Melon<Main>.Logger.Msg("Player pressed useless FunctionElement!"));

            // Create root settings
            autoEnableEle = root_categ.CreateBoolElement("Enable Mod", menuColor, true);
        }

        // Instantiate the BLSurvival HUD
        internal static void CreateHud()
        {
            var resourceName = "iSurvivedBonelab.Assets.blsurvivalhud";

            using (Stream stream = Main.assembly.GetManifestResourceStream(resourceName))
            {
                // Load Embedded AssetBundle
                using (MemoryStream memStream = new MemoryStream())
                {
                    stream.CopyTo(memStream);
                    byte[] memStreamArray = memStream.ToArray();
                    AssetBundle localAssetBundle = AssetBundle.LoadFromMemory(memStreamArray);

                    // Display error if something went wrong
                    if (localAssetBundle == null) Melon<Main>.Logger.Error("failed to load BLSurvivalHUD AssetBundle. Please make sure you have your stuff set up correctly.");

                    // Instantiate the HUD
                    Object menuAssetObj = localAssetBundle.LoadAsset("BLSurvivalHUD");
                    menuAsset = GameObject.Instantiate(menuAssetObj.Cast<GameObject>());

                    // Unload the AssetBundle
                    localAssetBundle.Unload(false);
                }
            }
        }

        private static void DestroyHud()
        {
            if (menuAsset != null) GameObject.Destroy(menuAsset);
        }

        // OnUpdate in relation to HUD settings
        internal static void UpdateSettings()
        {
            // Update the prefs according to the BoneMenu element values

            // Root Prefs
            Prefs.autoEnableEnt.Value = autoEnableEle.Value;

            // HUD Prefs
            Prefs.hudEnabledEnt.Value = hudEnabledEle.Value;
            Prefs.hudOffsetXEnt.Value = hudXEle.Value;
            Prefs.hudOffsetYEnt.Value = hudYEle.Value;
            Prefs.hudOffsetZEnt.Value = hudZEle.Value;
            Prefs.hudOffsetEnt.Value = new Vector3(Prefs.hudOffsetXEnt.Value, Prefs.hudOffsetYEnt.Value, Prefs.hudOffsetZEnt.Value);
            Prefs.hudHandEnt.Value = hudHandEle.Value;
            Prefs.hudTypeEnt.Value = hudTypeEle.Value;

            // Hunger Prefs
            NeedsStuff.hungerNeed.enabled = hungerEnabledEle.Value;
            NeedsStuff.hungerNeed.maxValue = maxHungerEle.Value;
            NeedsStuff.hungerNeed.decayRate = hungerDecayRateEle.Value;
            NeedsStuff.hungerNeed.startValue = hungerStartValueEle.Value;

            // Thirst Prefs
            NeedsStuff.thirstNeed.enabled = thirstEnabledEle.Value;
            NeedsStuff.thirstNeed.maxValue = maxThirstEle.Value;
            NeedsStuff.thirstNeed.decayRate = thirstDecayRateEle.Value;
            NeedsStuff.thirstNeed.startValue = thirstStartValueEle.Value;
        }

        // Parent the hud to the set hudHand
        private static void MoveHud(int hudHand)
        {
            Hand hand = Player.leftHand;
            if (hudHand == 1) hand = Player.rightHand;

            menuAsset.transform.parent = hand.transform;
            menuAsset.transform.position = hand.transform.position + Prefs.hudOffsetEnt.Value;
            menuAsset.transform.rotation = hand.transform.rotation;
        }

        // OnUpdate in relation to HUD elements
        internal static void UpdateHud()
        {
            if (menuAsset != null && Prefs.hudEnabledEnt.Value)
            {
                if (Player.handsExist) MoveHud(Prefs.hudHandEnt.Value);

                //FoodGauge();
                //WaterGauge();
                HudGauges(menuAsset.transform.FindRecursive("Gauges"));
            }
            else if (menuAsset == null && Prefs.hudEnabledEnt.Value) CreateHud();
            else DestroyHud();
        }

        private static void HudGauges(Transform gaugeContainer)
        {
            switch (Prefs.hudTypeEnt.Value)
            {
                case 0:
                    // hunger bar
                    if (NeedsStuff.hungerNeed.enabled)
                    {
                        Image hungerGaugeBar = gaugeContainer.Find("FoodGauge").GetComponentInChildren<Image>();
                        gaugeContainer.Find("HungerGauge").GetComponentInChildren<Slider>().transform.parent.gameObject.SetActive(false);
                        hungerGaugeBar.transform.parent.gameObject.SetActive(true);
                        hungerGaugeBar.fillAmount = NeedsStuff.hungerNeed.GetPercentage();
                    }

                    // thirst bar
                    if (NeedsStuff.thirstNeed.enabled)
                    {
                        Image thirstGaugeBar = gaugeContainer.Find("ThirstGauge").GetComponentInChildren<Image>();
                        gaugeContainer.Find("ThirstGauge").GetComponentInChildren<Slider>().transform.parent.gameObject.SetActive(false);
                        thirstGaugeBar.transform.parent.gameObject.SetActive(true);
                        thirstGaugeBar.fillAmount = NeedsStuff.thirstNeed.GetPercentage();
                    }
                    break;
                case 1:
                    // hunger bar 2
                    if (NeedsStuff.hungerNeed.enabled)
                    {
                        Slider hungerGaugeBar = gaugeContainer.Find("HungerGauge").GetComponentInChildren<Slider>();
                        gaugeContainer.Find("HungerGauge").GetComponentInChildren<Image>().transform.parent.gameObject.SetActive(false);
                        gaugeContainer.Find("HungerGauge").GetComponentInChildren<RawImage>().transform.parent.gameObject.SetActive(false);
                        hungerGaugeBar.transform.parent.gameObject.SetActive(true);
                        hungerGaugeBar.value = NeedsStuff.hungerNeed.GetPercentage();
                    }

                    // thirst bar 2
                    if (NeedsStuff.thirstNeed.enabled)
                    {
                        Slider thirstGaugeBar = menuAsset.transform.Find("Pos").Find("Canvas").Find("Grid").Find("Gauges").Find("ThirstGauge").GetComponentInChildren<Slider>();
                        gaugeContainer.Find("ThirstGauge").GetComponentInChildren<Image>().transform.parent.gameObject.SetActive(false);
                        gaugeContainer.Find("HungerGauge").GetComponentInChildren<RawImage>().transform.parent.gameObject.SetActive(false);
                        thirstGaugeBar.transform.parent.gameObject.SetActive(true);
                        thirstGaugeBar.value = NeedsStuff.thirstNeed.GetPercentage();
                    }
                    break;
            }
        }
    }
}

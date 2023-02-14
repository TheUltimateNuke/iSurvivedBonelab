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

        private static FloatElement hungerDecayTimeEle;
        private static IntElement hungerDecayAmountEle;
        private static IntElement maxHungerEle;

        private static FloatElement thirstDecayTimeEle;
        private static IntElement thirstDecayAmountEle;
        private static IntElement maxThirstEle;


        internal static void CreateElements()
        {
            // Create root categ
            root_categ = MenuManager.CreateCategory("BLSurvival", menuColor);

            // HUD settings
            SubPanelElement hud_categ = root_categ.CreateSubPanel("HUD Settings", menuColor);
            hudEnabledEle = hud_categ.CreateBoolElement("Hud Enabled", menuColor, Prefs.hudEnabledEnt.Value);
            hudXEle = hud_categ.CreateFloatElement("Hud Offset X", menuColor, Prefs.hudOffsetXEnt.Value, 0.1f, -10f, 10f);
            hudYEle = hud_categ.CreateFloatElement("Hud Offset Y", menuColor, Prefs.hudOffsetYEnt.Value, 0.1f, -10f, 10f);
            hudZEle = hud_categ.CreateFloatElement("Hud Offset Z", menuColor, Prefs.hudOffsetZEnt.Value, 0.1f, -10f, 10f);
            hudHandEle = hud_categ.CreateIntElement("Hud Hand", menuColor, Prefs.hudHandEnt.Value, 1, 0, 1);

            // Create settings subpanels for each system
            // Hunger Settings
            SubPanelElement hunger_categ = root_categ.CreateSubPanel("Hunger Settings", menuColor);
            hungerDecayTimeEle = hunger_categ.CreateFloatElement("Hunger Decay Time", menuColor, Prefs.hungerDecayTimeEnt.Value, 0.5f, 0.5f, 100f);
            hungerDecayAmountEle = hunger_categ.CreateIntElement("Hunger Decay Amount", menuColor, Prefs.hungerDecayAmount.Value, 1, 1, 100);
            maxHungerEle = hunger_categ.CreateIntElement("Max Hunger", menuColor, Prefs.maxHungerEnt.Value, 10, 10, int.MaxValue);

            // Thirst Settings
            SubPanelElement thirst_categ = root_categ.CreateSubPanel("Thirst Settings", menuColor);
            thirstDecayTimeEle = thirst_categ.CreateFloatElement("Thirst Decay Time", menuColor, Prefs.thirstDecayTimeEnt.Value, 0.5f, 0.5f, 100f);
            thirstDecayAmountEle = hunger_categ.CreateIntElement("Thirst Decay Amount", menuColor, Prefs.thirstDecayAmount.Value, 1, 1, 100);
            maxThirstEle = thirst_categ.CreateIntElement("Max Thirst", menuColor, Prefs.maxThirstEnt.Value, 10, 10, int.MaxValue);


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
            // hilariously unoptimized but it should do the job. if anyone sees this and knows how to do this a better way, please do so.
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

            // Hunger Prefs
            Prefs.hungerDecayTimeEnt.Value = hungerDecayTimeEle.Value;
            Prefs.hungerDecayAmount.Value = hungerDecayAmountEle.Value;
            Prefs.maxHungerEnt.Value = maxHungerEle.Value;

            // Thirst Prefs
            Prefs.thirstDecayTimeEnt.Value = thirstDecayTimeEle.Value;
            Prefs.thirstDecayAmount.Value = thirstDecayAmountEle.Value;
            Prefs.maxThirstEnt.Value = maxThirstEle.Value;
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
            if (menuAsset != null && Prefs.enabledEnt.Value && Prefs.hudEnabledEnt.Value)
            {
                MoveHud(Prefs.hudHandEnt.Value);

                FoodGauge();
                WaterGauge();
            }
            else DestroyHud();

            if (menuAsset == null && Prefs.hudEnabledEnt.Value)
            {
                CreateHud();
            }
        }

        private static void FoodGauge()
        {
            Image gaugeBar = menuAsset.transform.Find("Rot").Find("Gauges").Find("FoodGauge").Find("bar").GetComponent<Image>();
            gaugeBar.fillAmount = Prefs.curHungerEnt.Value / Prefs.maxHungerEnt.Value;
        }

        private static void WaterGauge()
        {
            Image gaugeBar = menuAsset.transform.Find("Rot").Find("Gauges").Find("ThirstGauge").Find("bar").GetComponent<Image>();
            gaugeBar.fillAmount = Prefs.curThirstEnt.Value / Prefs.maxThirstEnt.Value;
        }
    }
}

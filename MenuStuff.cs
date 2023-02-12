using BoneLib;
using BoneLib.BoneMenu;
using BoneLib.BoneMenu.Elements;
using MelonLoader;
using SLZ.Interaction;
using SLZ.Marrow.Utilities;
using System.IO;
using UnityEngine;

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

        private static FloatElement thirstDecayTimeEle;

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
            hungerDecayTimeEle = hunger_categ.CreateFloatElement("Hunger Decay Time", menuColor, 5f, 1f, 0.5f, 100f);

            // Thirst Settings
            SubPanelElement thirst_categ = root_categ.CreateSubPanel("Thirst Settings", menuColor);
            thirstDecayTimeEle = thirst_categ.CreateFloatElement("Thirst Decay Time", menuColor, 4.5f, 1f, 0.5f, 100f);

            // Temperature Settings
            SubPanelElement tempur_categ = root_categ.CreateSubPanel("Temperature Settings", menuColor);

            // Disease Settings
            SubPanelElement disease_categ = root_categ.CreateSubPanel("Disease Settings", menuColor);

            // Create root settings
            autoEnableEle = root_categ.CreateBoolElement("Auto Enable Mod", menuColor, true);
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

        public static void DestroyHud()
        {
            if (menuAsset != null) GameObject.Destroy(menuAsset);
        }

        // OnUpdate in relation to HUD settings
        public static void UpdateSettings()
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

            // Thirst Prefs
            Prefs.thirstDecayTimeEnt.Value = thirstDecayTimeEle.Value;
        }

        // Parent the hud to the set hudHand
        private static void MoveHud(int hudHand)
        {
            Transform handTransform = Player.leftHand.transform;
            if (hudHand == 1) handTransform = Player.rightHand.transform;

            SimpleTransform wrist = handTransform.GetComponent<Hand>().GetControllerWristTransform(true);
            menuAsset.transform.parent = handTransform;
            menuAsset.transform.position = wrist.position + Prefs.hudOffsetEnt.Value;
            menuAsset.transform.rotation = wrist.rotation;
        }

        // OnUpdate in relation to HUD elements
        internal static void UpdateHud()
        {
            if (menuAsset != null && Prefs.enabledEnt.Value && Prefs.hudEnabledEnt.Value)
            {
                MoveHud(Prefs.hudHandEnt.Value);

                //FoodGauge();
            }
            else DestroyHud();

            if (menuAsset == null && Prefs.hudEnabledEnt.Value)
            {
                CreateHud();
            }
        }
    }
}

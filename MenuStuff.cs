using BoneLib.BoneMenu.Elements;
using BoneLib.BoneMenu;
using UnityEngine;
using SLZ.Interaction;
using System.IO;
using MelonLoader;
using System.Reflection;
using UnhollowerBaseLib;
using UnhollowerBaseLib.Runtime;

namespace iSurvivedBonelab
{
    internal static class MenuStuff
    {
        public static Color menuColor = new Color(173, 113, 0);
        private static Vector3 hudOffset = new Vector3(Prefs.hudOffsetX, Prefs.hudOffsetY, Prefs.hudOffsetZ);
        public static void CreateElements()
        {
            // Create root categ
            MenuCategory root_categ = MenuManager.CreateCategory("BLSurvival", menuColor);

            // UI settings
            SubPanelElement hud_categ = root_categ.CreateSubPanel("HUD Settings", menuColor);
            hud_categ.CreateFloatElement("Hud Offset X", menuColor, Prefs.hudOffsetX, 0.1f, -10f, 10f);
            hud_categ.CreateFloatElement("Hud Offset Y", menuColor, Prefs.hudOffsetY, 0.1f, -10f, 10f);
            hud_categ.CreateFloatElement("Hud Offset Z", menuColor, Prefs.hudOffsetZ, 0.1f, -10f, 10f);
            hud_categ.CreateIntElement("Hud Hand", menuColor, Prefs.hudHand, 1, 0, 1);

            // Create settings subpanels for each system
            SubPanelElement hunger_categ = root_categ.CreateSubPanel("Hunger Settings", menuColor);
            SubPanelElement thirst_categ = root_categ.CreateSubPanel("Thirst Settings", menuColor);
            SubPanelElement tempur_categ = root_categ.CreateSubPanel("Temperature Settings", menuColor);
            SubPanelElement disease_categ = root_categ.CreateSubPanel("Disease Settings", menuColor);

            // Create root settings
            root_categ.CreateBoolElement("Enable Mod", menuColor, Prefs.enabled);
            root_categ.CreateBoolElement("Auto Enable Mod", menuColor, Prefs.autoEnable);
        }

        // Instantiate the BLSurvival HUD on right or left hand with configurable positioning
        public static void CreateHud(Hand hand) 
        {
            Transform spawnPos = hand.transform;

            Assembly assembly = Assembly.GetExecutingAssembly();
            string assetBundlePath = Path.Combine(assembly.Location, Path.Combine("Assets", "blsurvivalhud"));
            AssetBundle localAssetBundle = AssetBundle.LoadFromFile(assetBundlePath);

            if (localAssetBundle == null)
            {
                Melon<Main>.Logger.Error("failed to load BLSurvivalHUD AssetBundle. Please make sure you have your stuff set up correctly.");
            }

            GameObject asset = (GameObject)localAssetBundle.LoadAsset("BLSurvivalHUDCanvas");
            GameObject.Instantiate(asset);
            localAssetBundle.Unload(false);
        }

        // OnUpdate in relation to settings
        /*
        public static void UpdateSettings()
        {

        }
        */

        // OnUpdate in relation to HUD elements
        public static void UpdateHud(Hand hand)
        {
            
        }
    }
}

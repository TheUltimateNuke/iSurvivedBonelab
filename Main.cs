using BoneLib;
using MelonLoader;
using System.Reflection;
using UnityEngine;

namespace iSurvivedBonelab
{
    public static class BuildInfo
    {
        public const string Name = "BLSurvival";
        public const string Author = "TheUltimateNuke";
        public const string Version = "0.0.4";
    }

    public class Main : MelonMod
    {
        public static Assembly assembly = Assembly.GetExecutingAssembly();

        public override void OnInitializeMelon()
        {
            // Create MelonPreferences values
            Prefs.CreatePrefs();

            // Create settings menu elements for BoneLib
            MenuStuff.CreateElements();

            // Listen out for Bonelib.Hooking events
            Hooking.OnLevelInitialized += OnLevelInitialized;
            Hooking.OnLevelUnloaded += OnLevelUnloaded;
        }

        private void OnLevelInitialized(LevelInfo info)
        {
            if (Prefs.enabledEnt.Value)
            {
                if (Prefs.hudEnabledEnt.Value && !(info.title == "Main Menu" || info.title == "Void G114"))
                {
                    MenuStuff.CreateHud();
                }
            }
        }

        public override void OnUpdate()
        {
            Prefs.enabledEnt.Value = CheckEnabled();

            MenuStuff.UpdateHud();
            MenuStuff.UpdateSettings();
        }

        // Enable mod if the level has empty GameObject named BLSURVIVAL_AUTOENABLE or autoEnable toggled on
        public static bool CheckEnabled()
        {
            if (Prefs.autoEnableEnt.Value) { return true; }
            else if (GameObject.Find("BLSURVIVAL" + "AUTOENABLE")) { return true; }
            else { return false; }
        }

        private void OnLevelUnloaded()
        {
            Prefs.SaveMelonPrefs();
        }

    }
}

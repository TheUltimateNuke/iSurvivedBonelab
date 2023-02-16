using BoneLib;
using MelonLoader;
using System;
using System.Reflection;
using UnityEngine;

namespace iSurvivedBonelab
{
    public static class TransformExtensions
    {
        public static Transform FindRecursive(this Transform self, string exactName) => self.FindRecursive(child => child.name == exactName);

        public static Transform FindRecursive(this Transform self, Func<Transform, bool> selector)
        {
            foreach (Transform child in self)
            {
                if (selector(child))
                {
                    return child;
                }

                var finding = child.FindRecursive(selector);

                if (finding != null)
                {
                    return finding;
                }
            }

            return null;
        }
    }

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
            if (MelonPreferences.GetCategory("BLSurvivalSettings") == null)
                Prefs.CreatePrefs();
            else
                Prefs.LoadMelonPrefs();

            // Create settings menu elements for BoneLib
            MenuStuff.CreateElements();

            // Listen out for Bonelib.Hooking events
            Hooking.OnLevelInitialized += OnLevelInitialized;
            Hooking.OnLevelUnloaded += OnLevelUnloaded;
        }

        private void OnLevelInitialized(LevelInfo info)
        {
            NeedsStuff.ResetHungerTimer();
            if (Prefs.enabledEnt.Value)
            {
                // Create HUD if not in main menu (doesn't work for some reason?)
                if (Prefs.hudEnabledEnt.Value && !(info.title.Equals("00 - Main Menu") || info.title.Equals("15 - Void G114")))
                {
                    MenuStuff.CreateHud();
                }
            }
        }

        public override void OnUpdate()
        {
            Prefs.enabledEnt.Value = CheckEnabled();

            if (Prefs.enabledEnt.Value) 
            {
                MenuStuff.UpdateHud();
                MenuStuff.UpdateSettings();
                if (Prefs.hungerEnabledEnt.Value)
                {
                    NeedsStuff.UpdateHunger();
                }
            }
  
        }

        // Enable mod if the level has empty GameObject named BLSURVIVAL_AUTOENABLE or autoEnable toggled on
        public static bool CheckEnabled()
        {
            if (Prefs.autoEnableEnt.Value || GameObject.Find("BLSURVIVAL_AUTOENABLE")) { return true; }
            else { return false; }
        }

        private void OnLevelUnloaded()
        {
            Prefs.SaveMelonPrefs();
        }

        public override void OnDeinitializeMelon()
        {
            Prefs.SaveMelonPrefs();
        }

    }
}

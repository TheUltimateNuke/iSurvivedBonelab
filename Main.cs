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
        public const string Version = "1.0.0";
    }
    
    public class Main : MelonMod
    {
        public static Assembly assembly = Assembly.GetExecutingAssembly();

        public override void OnInitializeMelon()
        {
            // Create settings menu elements for BoneLib
            MenuStuff.CreateElements();

            // Listen out for Bonelib.Hooking events
            Hooking.OnLevelInitialized += OnLevelInitialized;
        }

        private void OnLevelInitialized(LevelInfo info)
        {
            if (Prefs.enabled) MenuStuff.CreateHud();
        }

        public override void OnUpdate()
        {
            Prefs.enabled = CheckEnabled();

            MenuStuff.UpdateHud();
        }
        
        // Enable mod if the level has empty GameObject named BLSURVIVAL_AUTOENABLE or autoEnable toggled on
        public static bool CheckEnabled()
        {
            if (Prefs.autoEnable) return true;
            else if (GameObject.Find("BLSURVIVAL_AUTOENABLE")) return true;
            else return false;
        }
    }
}

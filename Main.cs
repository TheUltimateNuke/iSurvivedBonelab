using BoneLib;
using iSurvivedBonelab.MonoBehaviours;
using MelonLoader;
using System;
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
        public override void OnInitializeMelon()
        {
            //NeedsManager.Start();

            if (MelonPreferences.GetCategory("BLSurvivalSettings") == null && Prefs.autoEnableEnt.Value)
            {
                // Create MelonPreferences values
                Prefs.CreatePrefs();
            }

            if (Prefs.autoEnableEnt.Value)
            {
                // Create settings menu elements for BoneLib
                MenuStuff.CreateElements();

                // Listen out for Bonelib.Hooking events
                Hooking.OnLevelUnloaded += OnLevelUnloaded;
                Hooking.OnLevelInitialized += OnLevelInitialized;
            }

        }

        private void OnLevelInitialized(LevelInfo lvl)
        {
            if (Prefs.autoEnableEnt.Value)
                MenuStuff.SpawnHud();
        }

        public override void OnUpdate()
        {
            Prefs.enabledEnt.Value = CheckEnabled();

            if (Prefs.autoEnableEnt.Value) 
            {
                //NeedsManager.Update();
                MenuStuff.UpdateSettings();
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

    /*
    public static class SomeUtils
    {
        public static Transform FindDebug(Transform parent, string name)
        {
            Melon<Main>.Logger.Msg("Starting FindDebug. parent = " + parent.name, ", name = " + name);
            Transform transform = parent.Find(name);
            if (!transform)
            {
                Melon<Main>.Logger.Error("FindDebug failed. Returning null.", new NullReferenceException());
                return null;
            }
            else
            {
                Melon<Main>.Logger.Msg("FindDebug Succeeded. Returning transform.");
                return transform;
            }
        }
    }
    */
}

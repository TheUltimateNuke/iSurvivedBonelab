using BoneLib;
using MelonLoader;
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
        public static GameObject hud;
        public static GameObject mouth;

        public override void OnInitializeMelon()
        {
            if (MelonPreferences.GetCategory("BLSurvivalSettings") == null)
            {
                // Create MelonPreferences values
                Prefs.CreatePrefs();
            }

            // Create settings menu elements for BoneLib
            MenuStuff.CreateElements();

            if (Prefs.autoEnableEnt.Value)
            {

                // Listen out for Bonelib.Hooking events
                Hooking.OnLevelUnloaded += OnLevelUnloaded;
                Hooking.OnLevelInitialized += OnLevelInitialized;
            }
        }

        private void OnLevelInitialized(LevelInfo obj)
        {
            // prepare the hud bundle
            MenuStuff.HudBundleStuff.Init();

            // spawn the hud bundle
            hud = Object.Instantiate(MenuStuff.HudBundleStuff.FindBundleObject("BLSurvivalHUD"));

            mouth = CreateMouth();

        }

        public override void OnUpdate()
        {
            MenuStuff.UpdateSettings();
        }

        // Save Melon Preferences on unload of a level or the whole mod
        private void OnLevelUnloaded()
        {
            Prefs.SaveMelonPrefs();
        }

        public override void OnDeinitializeMelon()
        {
            Prefs.SaveMelonPrefs();
        }

        public GameObject CreateMouth()
        {
            GameObject mouth = new GameObject("Mouth");

            mouth.transform.parent = Player.playerHead;
            mouth.transform.localPosition = Vector3.zero + Player.playerHead.forward; // TODO: add config for this
            BoxCollider mouthTrigger = mouth.AddComponent<BoxCollider>();
            mouthTrigger.isTrigger = true;
            mouthTrigger.size = new Vector3(0.2f, 0.2f, 0.2f);

            return mouth;
        }

    }
}
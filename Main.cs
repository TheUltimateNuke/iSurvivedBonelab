using BoneLib;
using MelonLoader;
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
        public override void OnInitializeMelon()
        {
            // Create settings menu elements for BoneLib
            MenuStuff.CreateElements();

            // Listen out for Bonelib.Hooking events
            Hooking.OnLevelInitialized += OnLevelInitialized;
        }

        private void OnLevelInitialized(LevelInfo info)
        {
            GameObject survivalObject = GameObject.Find("BLSURVIVAL_AUTOENABLE");
            // Enable mod if the level has empty GameObject named BLSURVIVAL_AUTOENABLE or autoEnable toggled on
            if (Prefs.autoEnable)
            {
                Prefs.enabled = true;
                if (Prefs.hudHand == 1)
                {
                    MenuStuff.CreateHud(Player.rightHand);
                }
                else
                {
                    MenuStuff.CreateHud(Player.leftHand);
                }
            }
            else if (survivalObject)
            {
                Prefs.enabled = true;
                if (Prefs.hudHand == 1)
                {
                    MenuStuff.CreateHud(Player.rightHand);
                }
                else
                {
                    MenuStuff.CreateHud(Player.leftHand);
                }
                GameObject.Destroy(survivalObject);
            }
            else 
            { 
                Prefs.enabled = false;
            }
        }

        public override void OnUpdate()
        {
            if (Prefs.hudHand == 1)
            {
                MenuStuff.UpdateHud(Player.rightHand);
            }
            else 
            {
                MenuStuff.UpdateHud(Player.leftHand);
            }
            
        }
    }
}

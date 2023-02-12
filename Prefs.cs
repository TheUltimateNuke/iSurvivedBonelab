using MelonLoader;
using UnityEngine;

namespace iSurvivedBonelab
{
    internal static class Prefs
    {
        private static string savePath = "TheUltimateNuke/iSurvivedBonelab.cfg";

        internal static MelonPreferences_Category root_categ;
        
        internal static MelonPreferences_Entry<bool> enabledEnt;
        internal static MelonPreferences_Entry<bool> autoEnableEnt;
        internal static MelonPreferences_Entry<bool> debugEnt;
        
        internal static MelonPreferences_Entry<bool> hudEnabledEnt;
        internal static MelonPreferences_Entry<float> hudOffsetXEnt;
        internal static MelonPreferences_Entry<float> hudOffsetYEnt;
        internal static MelonPreferences_Entry<float> hudOffsetZEnt;
        internal static MelonPreferences_Entry<Vector3> hudOffsetEnt;
        internal static MelonPreferences_Entry<int> hudHandEnt;
        
        internal static MelonPreferences_Entry<float> hungerDecayTimeEnt;
        internal static MelonPreferences_Entry<float> maxHunger;
        internal static MelonPreferences_Entry<float> curHunger;

        internal static MelonPreferences_Entry<float> thirstDecayTimeEnt;
        internal static MelonPreferences_Entry<float> maxThirst;
        internal static MelonPreferences_Entry<float> curThirst;

        internal static void CreatePrefs()
        {
            root_categ = MelonPreferences.CreateCategory("BLSurvivalSettings");

            enabledEnt = root_categ.CreateEntry("_enabled", false, is_hidden: true);
            autoEnableEnt = root_categ.CreateEntry("AutoEnable", true);
            debugEnt = root_categ.CreateEntry("Debug", false);

            // HUD prefs
            hudEnabledEnt = root_categ.CreateEntry("HudEnabled", true);
            hudOffsetXEnt = root_categ.CreateEntry("HudOffsetX", 0f);
            hudOffsetYEnt = root_categ.CreateEntry("HudOffsetY", 0f);
            hudOffsetZEnt = root_categ.CreateEntry("HudOffsetZ", 0f);
            hudOffsetEnt = root_categ.CreateEntry("HudOffset", new Vector3(hudOffsetXEnt.Value, hudOffsetYEnt.Value, hudOffsetZEnt.Value), is_hidden: true);
            hudHandEnt = root_categ.CreateEntry("HudHand", 0);

            // Hunger prefs
            hungerDecayTimeEnt = root_categ.CreateEntry("HungerDecayTime", 0f);
            maxHunger = root_categ.CreateEntry("MaxHunger", 100f);
            curHunger = root_categ.CreateEntry("_curHunger", 0f, is_hidden: true);

            // Thirst prefs
            thirstDecayTimeEnt = root_categ.CreateEntry("ThirstDecayTime", 0f);
            maxThirst = root_categ.CreateEntry("MaxThirst", 100f);
            curThirst = root_categ.CreateEntry("_curThirst", 0f, is_hidden: true);
        }

        internal static void SaveMelonPrefs()
        {
            MelonPreferences_Category root_categ = MelonPreferences.GetCategory("BLSurvivalSettings");
            root_categ.SetFilePath(savePath, false);
            root_categ.SaveToFile();
        }
    }
}

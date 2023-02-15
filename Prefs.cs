using MelonLoader;
using UnityEngine;

namespace iSurvivedBonelab
{
    internal static class Prefs
    {
        //private static string savePath = MelonUtils.UserDataDirectory + "/TheUltimateNuke/iSurvivedBonelab.cfg";

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
        internal static MelonPreferences_Entry<int> hungerDecayAmount;
        internal static MelonPreferences_Entry<int> maxHungerEnt;
        internal static MelonPreferences_Entry<int> curHungerEnt;

        internal static MelonPreferences_Entry<float> thirstDecayTimeEnt;
        internal static MelonPreferences_Entry<int> thirstDecayAmount;
        internal static MelonPreferences_Entry<int> maxThirstEnt;
        internal static MelonPreferences_Entry<int> curThirstEnt;

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
            hudOffsetEnt = root_categ.CreateEntry("_hudOffset", new Vector3(hudOffsetXEnt.Value, hudOffsetYEnt.Value, hudOffsetZEnt.Value), is_hidden: true);
            hudHandEnt = root_categ.CreateEntry("HudHand", 0);

            // Hunger prefs
            hungerDecayTimeEnt = root_categ.CreateEntry("HungerDecayTime", 10f); //TODO: Change to a higher value before release
            hungerDecayAmount = root_categ.CreateEntry("HungerDecayAmount", 1);
            maxHungerEnt = root_categ.CreateEntry("MaxHunger", 100);
            curHungerEnt = root_categ.CreateEntry("_curHunger", maxHungerEnt.Value, is_hidden: true);

            // Thirst prefs
            thirstDecayTimeEnt = root_categ.CreateEntry("ThirstDecayTime", 10f); //TODO: Change to a higher value before release
            thirstDecayAmount = root_categ.CreateEntry("ThirstDecayAmount", 2);
            maxThirstEnt = root_categ.CreateEntry("MaxThirst", 100);
            curThirstEnt = root_categ.CreateEntry("_curThirst", maxThirstEnt.Value, is_hidden: true);
        }

        internal static void SaveMelonPrefs()
        {
            MelonPreferences_Category root_categ = MelonPreferences.GetCategory("BLSurvivalSettings");

            root_categ.SaveToFile();
        }

        internal static void LoadMelonPrefs()
        {
            MelonPreferences_Category root_categ = MelonPreferences.GetCategory("BLSurvivalSettings");

            root_categ.LoadFromFile();
        }
    }
}

using MelonLoader;
using UnityEngine;

namespace iSurvivedBonelab
{
    [System.Serializable]
    public class NeedPref
    {
        public MelonPreferences_Entry<bool> enabledEnt;
        public MelonPreferences_Entry<bool> decayHealthEnt;
        public MelonPreferences_Entry<bool> passiveDecayEnt;

        public MelonPreferences_Entry<float> curValueEnt;
        public MelonPreferences_Entry<float> maxValueEnt;
        public MelonPreferences_Entry<float> startValueEnt;
        public MelonPreferences_Entry<float> decayRateEnt;
        public MelonPreferences_Entry<float> regenRateEnt;
        public MelonPreferences_Entry<float> healthDecayRateEnt;

        private Need Need { get; }

        public NeedPref(Need need) 
        {
            this.Need = need;
        }

        public void Create(MelonPreferences_Category category)
        {
            enabledEnt = category.CreateEntry(Need.DisplayName.ToLowerInvariant() + nameof(enabledEnt), Need.enabled);
            decayHealthEnt = category.CreateEntry(Need.DisplayName.ToLowerInvariant() + nameof(decayHealthEnt), Need.decayHealthWhenEmpty);
            passiveDecayEnt = category.CreateEntry(Need.DisplayName.ToLowerInvariant() + nameof(passiveDecayEnt), Need.passiveDecay);

            curValueEnt = category.CreateEntry(Need.DisplayName.ToLower() + nameof(curValueEnt), Need.curValue);
            maxValueEnt = category.CreateEntry(Need.DisplayName.ToLower() + nameof(maxValueEnt), Need.maxValue);
            startValueEnt = category.CreateEntry(Need.DisplayName.ToLower() + nameof(startValueEnt), Need.startValue);
            decayRateEnt = category.CreateEntry(Need.DisplayName.ToLower() + nameof(decayRateEnt), Need.decayRate);
            regenRateEnt = category.CreateEntry(Need.DisplayName.ToLower() + nameof(regenRateEnt), Need.regenRate);
            healthDecayRateEnt = category.CreateEntry(Need.DisplayName.ToLower() + nameof(healthDecayRateEnt), Need.healthDecayRate);
        }

        public void Update()
        {
            enabledEnt.Value = Need.enabled;
            decayHealthEnt.Value = Need.decayHealthWhenEmpty;
            passiveDecayEnt.Value = Need.passiveDecay;

            curValueEnt.Value = Need.curValue;
            maxValueEnt.Value = Need.maxValue;
            startValueEnt.Value = Need.startValue;
            decayRateEnt.Value = Need.decayRate;
            healthDecayRateEnt.Value = Need.healthDecayRate;
        }
    }

    public static class Prefs
    {
        //private static string savePath = MelonUtils.UserDataDirectory + "/TheUltimateNuke/iSurvivedBonelab.cfg";

        public static MelonPreferences_Category root_categ;

        public static MelonPreferences_Entry<bool> enabledEnt;
        public static MelonPreferences_Entry<bool> autoEnableEnt;
        public static MelonPreferences_Entry<bool> debugEnt;

        public static MelonPreferences_Entry<bool> hudEnabledEnt;
        public static MelonPreferences_Entry<float> hudOffsetXEnt;
        public static MelonPreferences_Entry<float> hudOffsetYEnt;
        public static MelonPreferences_Entry<float> hudOffsetZEnt;
        public static MelonPreferences_Entry<Vector3> hudOffsetEnt;
        public static MelonPreferences_Entry<int> hudHandEnt;
        public static MelonPreferences_Entry<int> hudTypeEnt;

        public static void CreatePrefs()
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
            hudTypeEnt = root_categ.CreateEntry("HudType", 0);

            // Need prefs are managed in NeedPref
        }

        public static void SaveMelonPrefs()
        {
            MelonPreferences_Category root_categ = MelonPreferences.GetCategory("BLSurvivalSettings");

            root_categ.SaveToFile();
        }

        public static void LoadMelonPrefs()
        {
            MelonPreferences_Category root_categ = MelonPreferences.GetCategory("BLSurvivalSettings");

            root_categ.LoadFromFile();
        }
    }
}

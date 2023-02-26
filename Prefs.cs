using MelonLoader;
using UnityEngine;

namespace iSurvivedBonelab
{
    [System.Serializable]
    [RegisterTypeInIl2Cpp]
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

        private Need need;

        public NeedPref(Need need) 
        {
            this.need = need;
        }

        public void Create(MelonPreferences_Category category)
        {
            enabledEnt = category.CreateEntry(need.DisplayName.ToLowerInvariant() + nameof(enabledEnt), need.enabled);
            decayHealthEnt = category.CreateEntry(need.DisplayName.ToLowerInvariant() + nameof(decayHealthEnt), need.decayHealthWhenEmpty);
            passiveDecayEnt = category.CreateEntry(need.DisplayName.ToLowerInvariant() + nameof(passiveDecayEnt), need.passiveDecay);

            curValueEnt = category.CreateEntry(need.DisplayName.ToLower() + nameof(curValueEnt), need.curValue);
            maxValueEnt = category.CreateEntry(need.DisplayName.ToLower() + nameof(maxValueEnt), need.maxValue);
            startValueEnt = category.CreateEntry(need.DisplayName.ToLower() + nameof(startValueEnt), need.startValue);
            decayRateEnt = category.CreateEntry(need.DisplayName.ToLower() + nameof(decayRateEnt), need.decayRate);
            regenRateEnt = category.CreateEntry(need.DisplayName.ToLower() + nameof(regenRateEnt), need.regenRate);
            healthDecayRateEnt = category.CreateEntry(need.DisplayName.ToLower() + nameof(healthDecayRateEnt), need.healthDecayRate);
        }

        public void Update()
        {
            enabledEnt.Value = need.enabled;
            decayHealthEnt.Value = need.decayHealthWhenEmpty;
            passiveDecayEnt.Value = need.passiveDecay;

            curValueEnt.Value = need.curValue;
            maxValueEnt.Value = need.maxValue;
            startValueEnt.Value = need.startValue;
            decayRateEnt.Value = need.decayRate;
            healthDecayRateEnt.Value = need.healthDecayRate;
        }
    }

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
        internal static MelonPreferences_Entry<int> hudTypeEnt;

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
            hudTypeEnt = root_categ.CreateEntry("HudType", 0);

            // Need prefs
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

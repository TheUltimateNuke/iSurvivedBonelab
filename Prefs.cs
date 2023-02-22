using iSurvivedBonelab.MonoBehaviours;
using MelonLoader;
using UnityEngine;

namespace iSurvivedBonelab
{
    [System.Serializable]
    public class NeedPref {
        public MelonPreferences_Entry<bool> enabledEnt;
        public MelonPreferences_Entry<float> curValueEnt;
        public MelonPreferences_Entry<float> maxValueEnt;
        public MelonPreferences_Entry<float> startValueEnt;
        public MelonPreferences_Entry<float> decayRateEnt;

        private Need need;

        public NeedPref(Need need) 
        {
            this.need = need;
        }

        public void Create(MelonPreferences_Category category)
        {
            enabledEnt = category.CreateEntry(need.displayName.ToLowerInvariant() + nameof(enabledEnt), need.enabled, need.displayName);
            curValueEnt = category.CreateEntry(need.displayName.ToLower() + nameof(curValueEnt), need.curValue, need.displayName);
            maxValueEnt = category.CreateEntry(need.displayName.ToLower() + nameof(maxValueEnt), need.maxValue, need.displayName);
            startValueEnt = category.CreateEntry(need.displayName.ToLower() + nameof(startValueEnt), need.startValue, need.displayName);
            decayRateEnt = category.CreateEntry(need.displayName.ToLower() + nameof(decayRateEnt), need.decayRate, need.displayName);
        }

        public void Update()
        {
            enabledEnt.Value = need.enabled;
            curValueEnt.Value = need.curValue;
            maxValueEnt.Value = need.maxValue;
            startValueEnt.Value = need.startValue;
            decayRateEnt.Value = need.decayRate;
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
            //NeedsManager.CreateAllPref();
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

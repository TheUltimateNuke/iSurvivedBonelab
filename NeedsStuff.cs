using System;
using UnityEngine;
using UnityEngine.UI;

namespace iSurvivedBonelab
{
    public static class NeedsStuff
    {
        public static Need hungerNeed;
        public static Need thirstNeed;

        public static float noHungerHealthDecay;

        public static void CreateNeeds()
        {
            hungerNeed = new Need
            {
                displayName = "Hunger",
                enabled = true,
                maxValue = 100,
                startValue = hungerNeed.maxValue,
                decayRate = 1f
            };

            thirstNeed = new Need
            {
                displayName = "Thirst",
                enabled = true,
                maxValue = 100,
                startValue = thirstNeed.maxValue,
                decayRate = 2f
            };
        }

        internal static void Update()
        {
            hungerNeed.BindPrefs();
            thirstNeed.BindPrefs();

            hungerNeed.Subtract(hungerNeed.decayRate * Time.deltaTime);
            thirstNeed.Subtract(thirstNeed.decayRate * Time.deltaTime);
        }
    }

    [System.Serializable]
    public class Need
    {
        public NeedPref prefs;

        public string displayName;
        public bool enabled;
        public float curValue;
        public float maxValue;
        public float startValue;
        public float decayRate;

        public void Add(float amount)
        {
            curValue = Mathf.Min(curValue + amount, maxValue);
        }

        public void Subtract(float amount)
        {
            curValue = Mathf.Min(curValue - amount, 0.0f);
        }

        public float GetPercentage()
        {
            return curValue / maxValue;
        }

        public Need()
        {
            prefs = new NeedPref(this, MelonLoader.MelonPreferences.GetCategory("BLSurvivalSettings"));
        }

        public void BindPrefs()
        {
            prefs.maxValueEnt.Value = maxValue;
            prefs.startValueEnt.Value = startValue;
            prefs.curValueEnt.Value = curValue;
            prefs.decayRateEnt.Value = decayRate;
        }
    }
}

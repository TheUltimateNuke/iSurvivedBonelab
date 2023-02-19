using SLZ.Props;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace iSurvivedBonelab
{
    [System.Serializable]
    public class Need
    {
        public NeedPref prefs;
        public NeedEle ele;

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
            prefs = new NeedPref(this);
            ele = new NeedEle(this);
        }
    }

    public static class NeedsStuff
    {
        public static Need hungerNeed;
        public static Need thirstNeed;

        internal static void CreateNeeds()
        {
            hungerNeed = new Need
            {
                displayName = "Hunger",
                enabled = true,
                maxValue = 100,
                startValue = 100,
                decayRate = 1f
            };

            thirstNeed = new Need
            {
                displayName = "Thirst",
                enabled = true,
                maxValue = 100,
                startValue = 100,
                decayRate = 2f
            };
        }

        internal static void Update()
        {   
            hungerNeed.Subtract(hungerNeed.decayRate * Time.deltaTime);
            thirstNeed.Subtract(thirstNeed.decayRate * Time.deltaTime);
        }
    }
}

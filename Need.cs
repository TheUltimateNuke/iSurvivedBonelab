using MelonLoader;
using UnityEngine;
using UnityEngine.UI;

namespace iSurvivedBonelab
{
    [System.Serializable]
    public class Need
    {
        public NeedPref prefs;
        public NeedEle ele;

        public Image imageBar;
        public Slider simpleBar;

        public string DisplayName { get; }

        public bool enabled;
        public bool decayHealthWhenEmpty;
        public bool passiveDecay;

        public float curValue;
        public float maxValue;
        public float startValue;
        public float decayRate;
        public float regenRate;
        public float healthDecayRate;

        public void Add(float amount)
        {
            if (enabled)
                curValue = Mathf.Min(curValue + amount, maxValue);
        }

        public void Subtract(float amount)
        {
            if (enabled)
                curValue = Mathf.Min(curValue - amount, 0.0f);
        }

        public float GetPercentage()
        {
            return curValue / maxValue;
        }
        
        public void CreatePrefsAndEle()
        {
            prefs = new NeedPref(this);
            prefs.Create(Prefs.root_categ);
            ele = new NeedEle(this);
            ele.Create(MenuStuff.root_categ, MenuStuff.menuColor);
        }
    }
}

using BoneLib;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace iSurvivedBonelab.MonoBehaviours
{
    // TODO: On HUD gameObject
    public class PlayerNeeds : MonoBehaviour
    {
        public PlayerNeeds(IntPtr ptr) : base(ptr) { }

        public List<Need> needs;

        public Transform needGaugeContainer;
        public Transform effectGaugeContainer;

        public Need GetNeed(string name)
        {
            return needs.Find(item => item.DisplayName == name);
        }

        private void Start()
        {
            foreach (Need need in needs)
            {
                if (need.gauge == null)
                {
                    Need.GaugeMaker.CreateTemplateBundle();
                    need.gauge = Need.GaugeMaker.SetupGauge(need);
                }

                need.CreatePrefsAndEle();
            }

            if (needGaugeContainer == null)
                needGaugeContainer = MenuStuff.HudBundleStuff.FindBundleObject("NeedGauges").transform;
            if (effectGaugeContainer == null)
                effectGaugeContainer = MenuStuff.HudBundleStuff.FindBundleObject("EffectGauges").transform;
        }

        private void Update()
        {
            foreach (Need need in needs)
            {
                need.prefs.Update();
                need.ele.Update();

                if (need.enabled)
                {
                    if (need.passiveDecay) need.Subtract(need.decayRate * Time.deltaTime);

                    if (need.decayHealthWhenEmpty && need.curValue <= 0)
                        DecayPlayerHealth(need.healthDecayRate * Time.deltaTime);

                    switch (Prefs.hudTypeEnt.Value)
                    {
                        case 0:
                            need.gauge.simpleBar.enabled = false;
                            need.gauge.imageBar.enabled = true;
                            need.gauge.imageBar.fillAmount = need.GetPercentage();
                            break;
                        case 1:
                            need.gauge.imageBar.enabled = false;
                            need.gauge.simpleBar.enabled = true;
                            need.gauge.simpleBar.value = need.GetPercentage();
                            break;
                    }
                }
                else
                {
                    
                }
            }
        }

        private void DecayPlayerHealth(float amount)
        {
            Player.rigManager.health.curr_Health = Mathf.Min(Player.rigManager.health.curr_Health - amount, 0.0f);
        }
    }
}

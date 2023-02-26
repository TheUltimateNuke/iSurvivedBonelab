using BoneLib;
using System;
using MelonLoader;
using System.Collections.Generic;
using UnityEngine;

namespace iSurvivedBonelab.MonoBehaviours
{
    // TODO: On HUD gameObject
    [RegisterTypeInIl2Cpp]
    public class PlayerNeeds : MonoBehaviour
    {
        public PlayerNeeds(IntPtr ptr) : base(ptr) { }

        public List<Need> needs;

        public Need GetNeed(string name)
        {
            return needs.Find(item => item.DisplayName == name);
        }

        private void Start()
        {
            foreach (Need need in needs)
            {
                need.CreatePrefsAndEle();
            }
        }

        private void Update()
        {

            foreach (Need need in needs)
            {
                need.prefs.Update();
                need.ele.Update();

                if (need.passiveDecay) need.Subtract(need.decayRate * Time.deltaTime);

                if (need.decayHealthWhenEmpty && need.curValue <= 0)
                    DecayPlayerHealth(need.healthDecayRate * Time.deltaTime);
                
                switch (Prefs.hudTypeEnt.Value)
                {
                    case 0:
                        need.simpleBar.enabled = false;
                        need.imageBar.enabled = true;
                        need.imageBar.fillAmount = need.GetPercentage();
                        break;
                    case 1:
                        need.imageBar.enabled = false;
                        need.simpleBar.enabled = true;
                        need.simpleBar.value = need.GetPercentage();
                        break;
                }
            }
        }

        private void DecayPlayerHealth(float amount)
        {
            Player.rigManager.health.curr_Health = Mathf.Min(Player.rigManager.health.curr_Health - amount, 0.0f);
        }
    }
}

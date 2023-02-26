using BoneLib;
using System;
using MelonLoader;
using UnityEngine;

namespace iSurvivedBonelab.MonoBehaviours
{
    // TODO: On HUD gameObject
    [RegisterTypeInIl2Cpp]
    public class TrackHand : MonoBehaviour
    {
        public TrackHand(IntPtr ptr) : base(ptr) { }

        private void Update()
        {
            if (Player.handsExist)
            {
                switch (Prefs.hudHandEnt.Value)
                {
                    case 0:
                        transform.parent = Player.leftHand.transform;
                        break;
                    case 1:
                        transform.parent = Player.rightHand.transform;
                        break;
                }
                transform.localPosition = Prefs.hudOffsetEnt.Value;
            }
        }
    }
}

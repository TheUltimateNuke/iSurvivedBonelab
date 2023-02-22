using BoneLib;
using UnityEngine;

namespace iSurvivedBonelab.MonoBehaviours
{
    public class TrackHand : MonoBehaviour
    {
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
                transform.localPosition = Vector3.zero + Prefs.hudOffsetEnt.Value;
            }
        }
    }
}

using System;
using MelonLoader;
using UnityEngine;
using UnityEngine.UI;

namespace iSurvivedBonelab.MonoBehaviours
{
    // TODO: on GaugeTemplate
    [RegisterTypeInIl2Cpp]
    public class Gauge : MonoBehaviour
    {
        public Gauge(IntPtr ptr) : base(ptr) { }

        public enum GaugeType
        {
            Need = 0,
            Effect = 1
        }

        public GaugeType type;

        public Image imageBar;
        public Slider simpleBar;
    }
}

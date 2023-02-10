using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace iSurvivedBonelab
{
    internal static class Prefs
    {
        internal static bool enabled = false;
        internal static bool autoEnable = true;
        internal static bool debug = false;

        // HUD prefs
        internal static float hudOffsetX = 0f;
        internal static float hudOffsetY = 0f;
        internal static float hudOffsetZ = 0f;
        internal static Vector3 hudOffset = new Vector3(Prefs.hudOffsetX, Prefs.hudOffsetY, Prefs.hudOffsetZ);
        internal static int hudHand = 0;
    }
}

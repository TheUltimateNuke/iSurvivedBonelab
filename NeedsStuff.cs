using UnityEngine;

namespace iSurvivedBonelab
{
    internal static class NeedsStuff
    {
        private static float hungerTimer;
        private static bool warned;

        internal static void ResetHungerTimer()
        {
            warned = false;
            hungerTimer = Prefs.hungerDecayTimeEnt.Value;
        }

        internal static void UpdateHunger()
        {
            if (hungerTimer > 0) hungerTimer -= Time.deltaTime;
            else
            {
                ResetHungerTimer();
                OnHungerTimerUp();
            }
        }

        private static void OnHungerTimerUp()
        {
            Prefs.curHungerEnt.Value -= Prefs.hungerDecayAmount.Value;
            if (Prefs.curHungerEnt.Value <= Prefs.maxHungerEnt.Value / 2)
            {
                WarnHunger();
            }
            if (Prefs.curHungerEnt.Value <= 0) Starve();
        }

        public static void WarnHunger()
        {
            if (!warned)
            {
                warned = true;
                // Play warning sound
            }
        }

        public static void Starve() 
        { 
            
        }
    }
}

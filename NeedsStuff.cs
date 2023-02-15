using BoneLib;
using Il2CppSystem;
using SLZ.Rig;
using UnityEngine;

namespace iSurvivedBonelab
{
    public class NeedsStuff
    {
        private static float _hungerTimer = Prefs.hungerDecayTimeEnt.Value;
        private static bool _hungerWarned;

        internal static void ResetHungerTimer()
        {
            _hungerWarned = false;
            _hungerTimer = Prefs.hungerDecayTimeEnt.Value;
        }

        internal static void UpdateHunger()
        {
            Prefs.curHungerEnt.Value = Mathf.Clamp(Prefs.curHungerEnt.Value, 0, Prefs.maxHungerEnt.Value);
            if (_hungerTimer > 0) _hungerTimer -= Time.deltaTime;
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
            else if (Prefs.curHungerEnt.Value <= 0) Starve();
            ResetHungerTimer();
        }

        public static void WarnHunger()
        {
            if (!_hungerWarned)
            {
                _hungerWarned = true;
                // TODO: Play warning sound
            }
        }

        public static void Starve() 
        {
            Player_Health _playerHealthManager = Player.rigManager.GetComponentInChildren<Player_Health>();
            _playerHealthManager.Death();
        }

    }
}

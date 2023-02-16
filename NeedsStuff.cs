using BoneLib;
using iSurvivedBonelab.MonoBehaviours;
using System;
using UnityEngine;

namespace iSurvivedBonelab
{
    public static class NeedsStuff
    {
        private static float _hungerTimer = Prefs.hungerDecayTimeEnt.Value;
        private static float _thirstTimer = Prefs.thirstDecayTimeEnt.Value;
        private static bool _hungerWarned;
        private static bool _thirstWarned;

        #region Hunger Methods
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
            Prefs.curHungerEnt.Value -= Prefs.hungerDecayAmountEnt.Value;
            if (Prefs.curHungerEnt.Value <= Prefs.maxHungerEnt.Value / 2) WarnHunger();
            if (Prefs.curHungerEnt.Value <= 0) Starve();
            ResetHungerTimer();
        }

        private static void WarnHunger()
        {
            if (!_hungerWarned)
            {
                _hungerWarned = true;
                // TODO: Play warning sound
            }
        }

        private static void Starve() 
        {
            // TODO: Play warning sound slightly lower pitched
            Player_Health _playerHealthManager = Player.rigManager.GetComponentInChildren<Player_Health>();
            _playerHealthManager.Death();
            ResetHungerTimer();
        }
        #endregion Hunger Methods

        #region Thirst Methods
        internal static void ResetThirstTimer()
        {
            _thirstWarned = false;
            _thirstTimer = Prefs.thirstDecayTimeEnt.Value;
        }
        internal static void UpdateThirst()
        {
            Prefs.curThirstEnt.Value = Mathf.Clamp(Prefs.curThirstEnt.Value, 0, Prefs.maxThirstEnt.Value);
            if (_thirstTimer > 0) _thirstTimer -= Time.deltaTime;
            else
            {
                ResetThirstTimer();
                OnThirstTimerUp();
            }
        }

        private static void OnThirstTimerUp()
        {
            Prefs.curThirstEnt.Value -= Prefs.thirstDecayAmountEnt.Value;
            if (Prefs.curHungerEnt.Value <= Prefs.maxHungerEnt.Value / 2) WarnHunger();
            if (Prefs.curHungerEnt.Value <= 0) Starve();
            ResetHungerTimer();
        }

        #endregion Thirst Methods

        public static void Bite(Consumable consumable)
        {
            switch (consumable.type)
            {
                case Consumable.ConsumableType.Food:
                    Prefs.curHungerEnt.Value += consumable.PointsGivenPerBite;
                    ResetHungerTimer();
                    break;
                case Consumable.ConsumableType.Drink:
                    Prefs.curThirstEnt.Value += consumable.PointsGivenPerBite;
                    ResetThirstTimer();
                    break;
            }
        }
    }
}

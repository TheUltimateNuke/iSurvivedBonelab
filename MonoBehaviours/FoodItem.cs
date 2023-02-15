using BoneLib;
using UltEvents;
using UnityEngine;

namespace iSurvivedBonelab.MonoBehaviours
{
    public class FoodItem : MonoBehaviour
    {
        public float FoodGiven = 15f;

        public UltEvent onFoodGrabbed;
        public UltEvent<Collider> onFoodEaten;

        private bool _grabbedEventAlreadyFired = false;

        private void Update()
        {
            if (Player.GetObjectInHand(Player.leftHand) == gameObject || Player.GetObjectInHand(Player.rightHand) == gameObject)
            {
                if (!_grabbedEventAlreadyFired)
                {
                    _grabbedEventAlreadyFired = true;
                    onFoodGrabbed.Invoke();
                }
            }
            else
            {
                _grabbedEventAlreadyFired = false;
            }
        }

        private void OnTriggerEnter(Collider collider)
        {
            onFoodEaten.Invoke(collider);
        }

    }
}

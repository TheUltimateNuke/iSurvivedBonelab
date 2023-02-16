using MelonLoader;
using SLZ.VFX;
using System;
using UltEvents;
using UnityEngine;

namespace iSurvivedBonelab.MonoBehaviours
{
    public class Consumable : MonoBehaviour
    {
        public enum ConsumableType
        {
            Food,
            Drink,
            Medicine
        }
        public ConsumableType type;

        public AudioClip[] biteSounds;
        public AudioClip[] consumeSounds;
        
        public AudioSource audioOutput;

        public Blip BlipScript;

        public int MaxBites = 1;

        public int PointsGivenPerBite = 15;

        public UltEvent<Collider, Consumable> onConsumed;
        public UltEvent<Collider, Consumable> onBite;

        public string MouthTag = "Mouth";

        private int _curBites;

        private void Awake()
        {
            switch (type)
            {
                case ConsumableType.Food:
                    if (!Prefs.hungerEnabledEnt.Value)
                    {
                        gameObject.SetActive(false);
                    }
                    break;
                case ConsumableType.Drink:
                    if (!Prefs.thirstEnabledEnt.Value)
                    {
                        gameObject.SetActive(false);
                    }
                    break;
                default:
                    gameObject.SetActive(false);
                    Melon<Main>.Logger.Msg("ConsumableType of " + gameObject.name + " is invalid, setting to inactive");
                    break;
                // TODO: Add medicine system for disease
            }
            _curBites = MaxBites;
        }

        private void OnTriggerEnter(Collider collider)
        {
            if (collider.CompareTag(MouthTag))
            {
                NeedsStuff.Bite(this);
                onBite.Invoke(collider, this);
                if (_curBites <= 1) {
                    onConsumed.Invoke(collider, this);
                    BlipScript.Despawn();
                    PlayRandomSound(consumeSounds);
                }
                else {
                    _curBites--;
                    PlayRandomSound(biteSounds);
                }
            }
        }

        private void PlayRandomSound(AudioClip[] sounds)
        {
            if (sounds != Array.Empty<AudioClip>())
            {
                audioOutput.clip = sounds[UnityEngine.Random.Range(0, sounds.Length)];
                audioOutput.Play();
            }
        }

    }
}

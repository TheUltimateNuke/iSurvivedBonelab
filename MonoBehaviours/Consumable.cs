using MelonLoader;
using SLZ.VFX;
using System;
using UltEvents;
using UnityEngine;

namespace iSurvivedBonelab.MonoBehaviours
{
    public class Consumable : MonoBehaviour
    {
        public Need type;

        public AudioClip[] biteSounds;
        public AudioClip[] consumeSounds;
        
        public AudioSource audioOutput;

        public Blip blipScript;

        public int maxBites = 1;

        public int PointsGivenPerBite = 15;

        public UltEvent<Collider, Consumable> onConsumed;
        public UltEvent<Collider, Consumable> onBite;

        public string MouthTag = "Mouth";

        private int _curBites;

        private void Awake()
        {
            if (!type.enabled)
            {
                gameObject.SetActive(false);
            }
            _curBites = maxBites;
        }

        private void OnTriggerEnter(Collider collider)
        {
            if (collider.CompareTag(MouthTag))
            {
                type.Add(PointsGivenPerBite);
                onBite.Invoke(collider, this);
                if (_curBites <= 1) {
                    onConsumed.Invoke(collider, this);
                    if (blipScript) blipScript.Despawn();
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

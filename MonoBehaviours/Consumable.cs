using MelonLoader;
using SLZ.VFX;
using System;
using UltEvents;
using UnityEngine;

namespace iSurvivedBonelab.MonoBehaviours
{
    public class Consumable : MonoBehaviour
    {
        public string needDisplayName;

        // TODO: Soon maybe
        //public MeshRenderer[] biteRenderers;

        public AudioClip[] biteSounds;
        public AudioClip[] consumeSounds;
        
        public AudioSource audioOutput;

        public Blip blipScript;

        public int maxBites = 1;

        public int PointsGivenPerBite = 15;

        public UltEvent<Collider, Consumable> onConsumed;
        public UltEvent<Collider, Consumable> onBite;

        public string mouthTag = "Mouth";

        private int _curBites;

        // TODO: Soon maybe
        //private MeshRenderer _curBiteRenderer;

        private PlayerNeeds _playerNeeds;

        private Need _curType;

        private void Start()
        {
            _playerNeeds = Main.hud.GetComponent<PlayerNeeds>();
            _curType = _playerNeeds.GetNeed(needDisplayName);
            if (_curType == null) {
                Melon<Main>.Logger.Error(
                    "Could not find a managed Need that corresponds to the provided string on this consumable. gameObject.name = " 
                    + gameObject.name 
                    + " needDisplayName = " 
                    + needDisplayName
                    );
                Destroy(gameObject);
            }

            if (!_curType.enabled)
            {
                gameObject.SetActive(false);
            }

            _curBites = maxBites;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(mouthTag))
            {
                Consume(other);
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

        private void Consume(Collider other)
        {
            Main.hud.GetComponent<PlayerNeeds>().GetNeed(needDisplayName).Add(PointsGivenPerBite);

            onBite.Invoke(other, this);
            if (_curBites <= 1)
            {
                onConsumed.Invoke(other, this);
                if (blipScript) blipScript.Despawn(); else Destroy(gameObject);
                PlayRandomSound(consumeSounds);
            }
            else
            {
                _curBites--;
                PlayRandomSound(biteSounds);
            }
        }
    }
}

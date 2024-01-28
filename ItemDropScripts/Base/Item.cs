using Assets.Scripts.PlayerScripts;
using Assets.Scripts.Pool;
using UnityEngine;

namespace Assets.Scripts.ItemDropScripts.Base
{
    public abstract class Item : PoolObject
    {
        protected static Player _player;
        protected static AudioSource AudioSource;
        protected static AudioClip _sound;


        public static void SetPlayer(Player player)
        {
            _player = player;
            AudioSource = player.GetComponent<AudioSource>();
            _sound = Resources.Load<AudioClip>("Sounds/Player/PlayerGotItem_sound");
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
                ContactWithPlayer();
        }

        /// <summary>
        /// Base rotate animation
        /// </summary>
        /// <param name="objectToRotate"></param>
        protected void Rotate(Transform objectToRotate, Vector3 axis, Space space)
        {
            objectToRotate.Rotate(axis, space);
        }

        /// <summary>
        /// Base rotate animation
        /// </summary>
        /// <param name="objectToRotate"></param>
        protected void Rotate(Transform objectToRotate, Vector3 axis)
        {
            objectToRotate.Rotate(axis);
        }

        protected abstract void ContactWithPlayer();
    }
}

using Assets.Scripts.ItemDropScripts.Base;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.ItemDropScripts.Items.Shield
{
    /// <summary>
    /// Shield which spawns on player's circle and generate <see cref="ActivatedShield"/>
    /// </summary>
    public class ShieldSpawnerItem : Item
    {
        [SerializeField] private ActivatedShield _energyShieldPrefab;

        public bool AllowAutoSelectionOfPosition = true;
        private void Start()
        {
            if (AllowAutoSelectionOfPosition)
                transform.position = _player.Center.GetRandomPointOnCircle(1.3f);
        }

        protected override void ContactWithPlayer()
        {
            // invoke event
            EventManager.OnPlayerTookBonus();

            // create shield and set its position
            ActivatedShield _shield = Instantiate(_energyShieldPrefab);
            _shield.transform.position = _player.transform.position;

            // play sound
            AudioSource.PlayOneShot(_sound);

            Destroy(gameObject);
        }

        private void Update()
        {
            Rotate(transform, Vector3.up);
        }
    }
}

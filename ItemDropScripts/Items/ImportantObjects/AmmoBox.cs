using UnityEngine;
using Assets.Scripts.ItemDropScripts.Base;


namespace Assets.Scripts.ItemDropScripts.Items
{
    public sealed class AmmoBox : Item
    {
        private Transform _ammoBoxModel;

        [SerializeField] private int _minBullets;
        [SerializeField] private int _maxBullets;
        private int AddBullets => Random.Range(_minBullets, _maxBullets);

        private void Awake()
        {
            _ammoBoxModel = transform.Find("AmmoBoxModel");
        }

        private void Update()
        {
            Rotate(_ammoBoxModel, Vector3.up);
        }

        protected override void ContactWithPlayer()
        {
            _player.BulletCount += AddBullets;
            AudioSource.PlayOneShot(_sound);

            ReturnToPool();
            EventManager.OnPlayerTookAmmo();
        }
    }
}

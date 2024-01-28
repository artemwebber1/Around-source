using Assets.Scripts.PlayerScripts;
using Assets.Scripts.UI.MainMenuEntities.ShopScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.PlayerScripts
{
    public sealed class Gun : MonoBehaviour
    {
        [SerializeField] private GameObject _fireVFX;
        public float ScoresPerHit;
        public float MoneyPerHit;

        private Player _player;
        private Timer _lifeTimeTimer;

        private AudioClip _shotSound;
        private AudioSource _audioSource;

        #region Monobehaviour

        private void Awake()
        {
            _player = GetComponentInParent<Player>();
            _audioSource = GetComponentInParent<AudioSource>();
            _shotSound = Resources.Load<AudioClip>("Sounds/Player/PlayerShot_sound");

            _lifeTimeTimer = new Timer();
            _lifeTimeTimer.OnComplete(() => _fireVFX.SetActive(false));

            if (!PlayerPrefs.HasKey("PlayerScoresPerHit"))
                PlayerPrefs.SetFloat("PlayerScoresPerHit", ScoresPerHit);

            if (!PlayerPrefs.HasKey("PlayerMoneyPerHit"))
                PlayerPrefs.SetFloat("PlayerMoneyPerHit", MoneyPerHit);
        }

        private void Start()
        {
            ScoresPerHit = PlayerPrefs.GetFloat("PlayerScoresPerHit");
            MoneyPerHit = PlayerPrefs.GetFloat("PlayerMoneyPerHit");
        }

        private void Update()
        {
            if (!_fireVFX.activeInHierarchy) return;
            _fireVFX.transform.forward = -(_player.CurrentEnemy.transform.position - transform.position);
            _lifeTimeTimer.Tick();
        }

        #endregion


        public void DamageEnemy()
        {
            CreateFireVFX();
            _player.BulletCount--;
            _player.CurrentEnemy.TakeDamage();

            _audioSource.PlayOneShot(_shotSound);
            EventManager.OnPlayerShot();
        }

        private void CreateFireVFX()
        {
            _lifeTimeTimer.SetStart(0.15f);
            _fireVFX.SetActive(true);
        }
    }
}
using Assets.Scripts.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.EnemyScripts.SpikeScripts.Abilities
{
    public sealed class BigCactus : PoolObject
    {
        private ParticleSystem _appearEffect;
        private AudioClip _appearSound;

        /// <summary>
        /// Max BigCactus' position by Y axis
        /// </summary>
        [SerializeField] private float _maxPositionY;
        [SerializeField] private float _lifeTime;
        private Timer _timerOfLifeTime;
        private Timer _waitTimer;

        public static int MaxCactiOnScene { get; private set; }
        
        public static void SetAudiosource(AudioSource audioSource) => _audioSource = audioSource;
        private static AudioSource _audioSource;

        public void Awake()
        {
            MaxCactiOnScene = 3;

            _appearEffect = GetComponentInChildren<ParticleSystem>();
            _appearSound = Resources.Load<AudioClip>("Sounds/Enemy/Spike/SpikeBigCactusCanonizong_sound");

            _timerOfLifeTime = new Timer();
            _timerOfLifeTime.OnComplete(() =>
            {
                transform.position = Vector3.Lerp(transform.position, transform.position - Vector3.up, 6 * Time.deltaTime);
                if (transform.position.y <= -7)
                {
                    ReturnToPool();
                    _timerOfLifeTime.Break();
                }
            });

            _waitTimer = new Timer();
            _waitTimer.OnStart(_appearEffect.Play);
            _waitTimer.OnComplete(() =>
            {
                // when waitTimer is completed,
                // start moving cacti up
                if (transform.position.y <= _maxPositionY)
                {
                    transform.position = Vector3.Lerp(transform.position, transform.position + Vector3.up, 5 * Time.deltaTime);
                }
                else
                {
                    // when cacti are on the max Y position,
                    // break waitTimer and stop particle system (appear effect)
                    _appearEffect.Stop();
                    _waitTimer.Break();
                }
            });
        }

        private bool _firstEnable = true;
        private void OnEnable()
        {
            // restart timers
            _waitTimer.SetStart(2);
            _timerOfLifeTime.SetStart(10);

            transform.eulerAngles = new Vector3(0, Random.Range(-180, 180), 0);

            _appearEffect.transform.position = new Vector3(transform.position.x, 1, transform.position.z);
            _appearEffect.Play();

            if (!_firstEnable)
                _audioSource.PlayOneShot(_appearSound);

            _firstEnable = false;
        }

        private void Update()
        {
            _appearEffect.transform.position = new Vector3(transform.position.x, 1, transform.position.z);

            _waitTimer.Tick();
            _timerOfLifeTime.Tick(_waitTimer.IsCompleted);
        }

        private void OnTriggerEnter(Collider collision)
        {
            if (collision.gameObject.CompareTag("Player"))
                EventManager.OnPlayerGotHit();
        }
    }
}

using Assets.Scripts.PlayerScripts;
using Assets.Scripts.Pool;
using UnityEngine;


namespace Assets.Scripts.EnemyScripts.Base
{
    public sealed class Bullet : PoolObject
    {
        private Timer _timerOfLifeTime;
        private Vector3 _direction;

        private Rigidbody _bulletRb;

        #region Monobehaviour
        private void Awake()
        {
            _timerOfLifeTime = new Timer();
            _timerOfLifeTime.OnComplete(ReturnToPool);

            _bulletRb = GetComponent<Rigidbody>();
        }

        private void OnEnable()
        {
            _timerOfLifeTime.SetStart(3);
        }

        private void OnDisable()
        {
            _bulletRb.velocity = Vector3.zero;
        }


        private void Update()
        {
            transform.Rotate(0, 5, 0);
            transform.position += _direction * Time.deltaTime;

            _timerOfLifeTime.Tick();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "Player")
                EventManager.OnPlayerGotHit();

            ReturnToPool();
        }
        #endregion

        public void SetDirection(Vector3 flyDirection) => _direction = flyDirection;
    }
}


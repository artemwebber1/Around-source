using Assets.Scripts.ItemDropScripts.Base;
using Assets.Scripts.UI.MainMenuEntities.ShopScripts;
using UnityEngine;

namespace Assets.Scripts.ItemDropScripts.Items
{
	public class SpeedBuffItem : Item
	{
        [SerializeField] private GameObject _effectPrefab;
        private GameObject _effect;

        [SerializeField] private float _speedBuffCoefficient;

        [SerializeField] private float _timeOfAction;
        private Timer _speedBuffTime;

        private bool _isPlayerTookBuff;


        #region Monobehaviour
        private void Awake()
        {
            if (!PlayerPrefs.HasKey("SpeedBuff"))  // init key "SpeedBuff"
                PlayerPrefs.SetFloat("SpeedBuff", _speedBuffCoefficient);
        }

        private void Start()
        {
            _speedBuffCoefficient = PlayerPrefs.GetFloat("SpeedBuff");

            // init timer
            _speedBuffTime = new Timer();
            _speedBuffTime.OnStart(() =>
            {
                _effect = Instantiate(_effectPrefab, _player.transform.position, Quaternion.identity);

                _player.Speed *= _speedBuffCoefficient;
                _player.Animator.speed *= _speedBuffCoefficient;
                _player.AttackSpeed /= _speedBuffCoefficient;
            });
            _speedBuffTime.OnComplete(() =>
            {
                _player.Speed /= _speedBuffCoefficient;
                _player.Animator.speed /= _speedBuffCoefficient;
                _player.AttackSpeed *= _speedBuffCoefficient;

                Destroy(gameObject);
                Destroy(_effect);
            });

            _isPlayerTookBuff = false;

            transform.position = _player.Center.GetRandomPointOnCircle(1.5f);
        }

        private void Update()
        {
            _speedBuffTime.TickWith(() =>
            {
                _effect.transform.position = _player.transform.position;
            }, _isPlayerTookBuff);

            Rotate(transform, Vector3.up);
        }
        #endregion

        protected override void ContactWithPlayer()
        {
            EventManager.OnPlayerTookBonus();
            _isPlayerTookBuff = true;

            AudioSource.PlayOneShot(_sound);

            _speedBuffTime.SetStart(_timeOfAction);

            transform.position = Vector3.down * 10;  // hide gameobject
        }
    }
}

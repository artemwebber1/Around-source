using Assets.Scripts.ItemDropScripts.Base;
using Assets.Scripts.UI.Text.Popups;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.ItemDropScripts.Items
{
    public sealed class MoneyItem : Item
    {
        private static Popup _moneyRecievedPopupPrefab;
        private Popup _moneyRecievedPopup;

        [SerializeField] private int _minRecievedMoney;
        [SerializeField] private int _maxRecievedMoney;

        private int _recievedMoney;

        private void OnEnable()
        {
            _recievedMoney = Random.Range(_minRecievedMoney, _maxRecievedMoney);
        }

        private void Start()
        {
            transform.position = _player.Center.GetRandomPointOnCircle(posY: 2);

            _moneyRecievedPopup = Instantiate(_moneyRecievedPopupPrefab);
            _moneyRecievedPopup.gameObject.SetActive(false);
        }

        private void Update()
        {
            Rotate(transform, Vector3.up, Space.World);
        }

        public static void LoadMoneyRecievedPopup()
            => _moneyRecievedPopupPrefab = Resources.Load<Popup>("UI/Popups/Popup");

        protected override void ContactWithPlayer()
        {
            AudioSource.PlayOneShot(_sound);

            float playerBalance = PlayerPrefs.GetFloat("PlayerBalance");
            playerBalance += _recievedMoney;
            PlayerPrefs.SetFloat("PlayerBalance", playerBalance);

            _moneyRecievedPopup.transform.position = _player.transform.position + (_player.Center.Position - _player.transform.position).normalized * 2.5f;
            _moneyRecievedPopup.SetText($"+{_recievedMoney}$");
            _moneyRecievedPopup.gameObject.SetActive(true);

            ReturnToPool();
            EventManager.OnPlayerTookMoney();
        }
    }
}

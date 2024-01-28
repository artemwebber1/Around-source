using Assets.Scripts.Localization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace Assets.Scripts.UI.MainMenuEntities.ShopScripts
{
    public class ShopOffer : MonoBehaviour
    {
        [Header("Offer info")]
        [SerializeField] private string _name;
        [SerializeField] private int _cost;
        [SerializeField] private int _maxLevel;
         
        [Space(15)]
        [Header("Characteristic to buff")]
        [SerializeField] private string _characteristicName;
        [SerializeField] private float _characteristicBuffValue;

        #region PlayerPrefs keys

        private string _costKey;
        private string _levelKey;

        #endregion

        private BuyShopOfferButton _buyButton;

        private TranslateableText _levelText;
        private int _level;

        private PlayerBalanceText _balanceText;

        #region MonoBehaviour
        private void Awake()
        {
            // player's balance on text
            _balanceText = FindObjectOfType<PlayerBalanceText>();

            // find childs
            _buyButton = GetComponentInChildren<BuyShopOfferButton>();

            _levelText = transform.Find("CurrentLevel").GetComponent<TranslateableText>();

            // generate keys for PlayerPrefs
            _costKey = _name + "Cost";  // generate cost key
            if (!PlayerPrefs.HasKey(_costKey))
                PlayerPrefs.SetInt(_costKey, _cost);

            _levelKey = _name + "Level";  // generate level key
            if (!PlayerPrefs.HasKey(_levelKey))
                PlayerPrefs.SetInt(_levelKey, 1);
        }

        private void Start()
        {
            _cost = PlayerPrefs.GetInt(_costKey);

            _buyButton.Text = _cost.ToString();  // update button's text
            _buyButton.SetOnClick(Buy);  // set onclick event

            _level = PlayerPrefs.GetInt(_levelKey);
            OnLevelUp();
        }
        #endregion

        private void Buy()
        {
            if (_balanceText.BalanceValue < _cost || _level == _maxLevel) return;

            // upgrade given characteristic
            UpgradeCharacteristic(_characteristicName, _characteristicBuffValue);

            // update player balance
            UpdatePlayerBalance();

            // update cost
            UpdateOfferCost();

            // update level
            UpdateCharacteristicLevel();
        }

        private void UpgradeCharacteristic(string characteristicName, float buffValue)
        {
            float buffableCharacteristic = PlayerPrefs.GetFloat(characteristicName);
            PlayerPrefs.SetFloat(characteristicName, buffableCharacteristic + buffValue);  // save value
        }

        #region Update visual information

        private void UpdatePlayerBalance()
        {
            PlayerPrefs.SetFloat("PlayerBalance", _balanceText.BalanceValue - _cost);
            _balanceText.BalanceValue = PlayerPrefs.GetFloat("PlayerBalance");
            _balanceText.UpdatePlayerBalanceText(_balanceText.BalanceValue);
        }

        private void UpdateOfferCost()
        {
            _cost += _cost / 2;
            PlayerPrefs.SetInt(_costKey, _cost);
            _buyButton.Text = _cost.ToString();
        }

        private void UpdateCharacteristicLevel()
        {
            _level++;
            PlayerPrefs.SetInt(_levelKey, _level);
            OnLevelUp();
        }

        private void OnLevelUp()
        {
            if (_level != _maxLevel)
                _levelText.AddValue(_level.ToString());
            else
            {
                _levelText.AddValue("MAX");
                _buyButton.Button.image.color = Color.gray;
            }
        }

        #endregion
    }
}

using TMPro;
using UnityEngine;


namespace Assets.Scripts.UI.MainMenuEntities.ShopScripts
{
    /// <summary>
    /// Text which shows player's balance in the shop
    /// </summary>
    public class PlayerBalanceText : MonoBehaviour
    {
        public float BalanceValue { get; set; }
        private TextMeshProUGUI _valueText;

        private void Awake()
        {
            _valueText = GetComponent<TextMeshProUGUI>();
        }

        private void OnEnable()
        {
            BalanceValue = PlayerPrefs.GetFloat("PlayerBalance");
            UpdatePlayerBalanceText(BalanceValue);
        }

        public void UpdatePlayerBalanceText(float currentBalance)
        {
            _valueText.text = currentBalance.ToString() + '$';
        }
    }
}

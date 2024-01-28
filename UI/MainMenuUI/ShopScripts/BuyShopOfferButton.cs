using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


namespace Assets.Scripts.UI.MainMenuEntities.ShopScripts
{
    public sealed class BuyShopOfferButton : MonoBehaviour, IPointerDownHandler
    {
        public delegate void OnClick();
        private OnClick _onClick;

        public Button Button { get; private set; }

        private TextMeshProUGUI _textMeshPro;
        public string Text
        {
            get => _textMeshPro.text;
            set => _textMeshPro.text = value + '$';
        }

        private void Awake()
        {
            Button = GetComponent<Button>();
            _textMeshPro = GetComponentInChildren<TextMeshProUGUI>();
        }

        public void SetOnClick(OnClick onClick) => _onClick = onClick;

        public void OnPointerDown(PointerEventData eventData)
        {
            _onClick?.Invoke();
        }
    }
}

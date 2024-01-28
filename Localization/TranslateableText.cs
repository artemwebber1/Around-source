using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Localization
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class TranslateableText : MonoBehaviour
    {
        private static int _currentLanguage;

        [SerializeField] private string[] _textValues;

        /// <summary>
        /// Translated text without any added value
        /// </summary>
        private string _defaultText;

        /// <summary>
        /// Text component to show text translation in game
        /// </summary>
        private TextMeshProUGUI _textComponent;

        /// <summary>
        /// Translated text can me inited before its OnEnable, Awake... methods invoked
        /// </summary>
        private bool _initedBefore;

        private void Awake()
        {
            if (!_initedBefore)
            {
                Init();
                _initedBefore = false;
            }
        }

        public void Init()
        {
            if (!PlayerPrefs.HasKey("GameLanguage"))
                PlayerPrefs.SetInt("GameLanguage", 0);  // set english as default language

            _currentLanguage = PlayerPrefs.GetInt("GameLanguage");

            _textComponent = GetComponent<TextMeshProUGUI>();
            _textComponent.text = _textValues[_currentLanguage];
            _defaultText = _textValues[_currentLanguage];   // save default text (without any added string)

            _initedBefore = true;
        }

        /// <summary>
        /// Adds string to translated text
        /// </summary>
        /// <param name="add"></param>
        public void AddValue(object add)
        {
            ResetString();
            _textValues[_currentLanguage] += add.ToString();
            _textComponent.text = _textValues[_currentLanguage];
        }

        private void ResetString()
        {
            _textValues[_currentLanguage] = _defaultText;
            _textComponent.text = _defaultText;
        }
    }
}

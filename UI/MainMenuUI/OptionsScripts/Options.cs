using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


namespace Assets.Scripts.UI.MainMenuEntities
{
    public sealed class Options : MonoBehaviour
    {
        #region Localization components
        [Header("Localization options")]
        [SerializeField] private Image _currentGameLanguageImage;

        [Header("Enabled languages sprites")]
        [SerializeField] private Sprite _englishLanguageSelected;
        [SerializeField] private Sprite _russianLanguageSelected;
        #endregion

        #region Music/Sound options components
        [Header("Sound/Music options")]
        [SerializeField] private Image _musicImage;
        [SerializeField] private Sprite _musicDisabledSprite;
        [SerializeField] private Sprite _musicEnabledSprite;
        [SerializeField] private Slider _volumeChangeSlider;
        #endregion

        public void Init()
        {
            if (!PlayerPrefs.HasKey("MusicVolume"))
                PlayerPrefs.SetFloat("MusicVolume", 1);

            _currentGameLanguageImage.sprite = PlayerPrefs.GetInt("GameLanguage") switch
            {
                0 => _englishLanguageSelected,
                1 => _russianLanguageSelected,
                _ => _englishLanguageSelected
            };

            _volumeChangeSlider.value = PlayerPrefs.GetFloat("MusicVolume");

            if (_volumeChangeSlider.value <= 0)
                _musicImage.sprite = _musicDisabledSprite;
            else
                _musicImage.sprite = _musicEnabledSprite;
        }

        public void OpenOptions() => gameObject.SetActive(true);

        public void CloseOptions() => gameObject.SetActive(false);

        public void ChangeLanguage()
        {
            int gameLanguage = PlayerPrefs.GetInt("GameLanguage");
            switch (gameLanguage)
            {
                case 0:
                    PlayerPrefs.SetInt("GameLanguage", 1);
                    break;

                    case 1:
                    PlayerPrefs.SetInt("GameLanguage", 0);
                    break;
            }

            // reload current scene
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void ChangeVolume()
        {
            AudioListener.volume = _volumeChangeSlider.value;
            PlayerPrefs.SetFloat("MusicVolume", _volumeChangeSlider.value);

            if (_volumeChangeSlider.value <= 0)
                _musicImage.sprite = _musicDisabledSprite;
            else
                _musicImage.sprite = _musicEnabledSprite;
        }
    }
}

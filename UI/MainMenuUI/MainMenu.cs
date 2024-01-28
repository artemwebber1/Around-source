using Assets.Scripts;
using Assets.Scripts.AdManagement;
using Assets.Scripts.AdManagement.AdTypes;
using Assets.Scripts.UI.MainMenuEntities;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace Assets.Scripts.UI.MainMenuEntities
{
    public class MainMenu : MonoBehaviour
    {
        [Header("Screens")]
        [SerializeField] private LoadingScreen _loadingScreen;
        [SerializeField] private GameObject _shop;
        [SerializeField] private Options _options;

        [Header("Buttons")]
        [SerializeField] private Button _startButton;
        [SerializeField] private Button _shopButton;
        [SerializeField] private Button _quitButton;


        private void Awake()
        {
            Application.targetFrameRate = 60;
            AudioListener.pause = false;
            AudioListener.volume = PlayerPrefs.GetFloat("MusicVolume");
            Time.timeScale = 1;

            _options.Init();
        }

        public void StartGame()
        {
            _loadingScreen.gameObject.SetActive(true);
            EventManager.OnSceneChanged();
        }
        public void ExitGame()
        {
            Application.Quit();
        }

        public void GoShop() => _shop.SetActive(true);
        public void ExitShop() => _shop.SetActive(false);
    }
}
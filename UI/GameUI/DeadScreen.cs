using Assets.Scripts.AdManagement.AdTypes;
using Assets.Scripts.ItemDropScripts.Items.Shield;
using Assets.Scripts.Localization;
using Assets.Scripts.PlayerScripts;
using Assets.Scripts.Pool;
using Assets.Scripts.UI.Screens.Base;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


namespace Assets.Scripts.UI.Screens
{
    public sealed class DeadScreen : GameScreen
    {
        private Player _player;
        private TranslateableText _totalScores;

        private Button _continueForAdButton;
        private TranslateableText _adLoadedInfoText;

        [Header("If player was respawned, instantiate this shield at his position")]
        [SerializeField] private ShieldSpawnerItem _shield;

        // use this method instead of [Start] or [Awake] because DeadScreen gameObject isn't active by default
        public override void Init()
        {
            base.Init();

            _player = FindObjectOfType<Player>();

            _totalScores = transform
                .Find("Text/TotalScoresInfo")
                .GetComponent<TranslateableText>();
            _totalScores.Init();
        }

        public void AppearDeadScreen()
        {
            _totalScores.AddValue(" " + _player.TotalScores);

            gameObject.SetActive(true);

            Canvas.PauseScreen.gameObject.SetActive(false);
            Canvas.BaseScreen.gameObject.SetActive(false);
            Time.timeScale = 0;
        }


        public void Retry() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        public void ReturnMainMenu()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("MainMenu");
        }
    }
}

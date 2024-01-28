using Assets.Scripts.UI.Screens;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace Assets.Scripts.UI.Screens.Base
{
    /// <summary>
    /// Stores and initializes screens
    /// </summary>
    public sealed class PlayerUIinitializer : MonoBehaviour
    {
        #region Screens

        public GamePlayScreen BaseScreen;

        public PauseScreen PauseScreen;

        public DeadScreen DeadScreen;

        #endregion

        #region Monobehaviour

        private void OnEnable()
        {
            EventManager.onPlayerKilled += DeadScreen.AppearDeadScreen;
        }

        private void OnDisable()
        {
            EventManager.onPlayerKilled -= DeadScreen.AppearDeadScreen;
        }

        private void Start()
        {
            // init screens
            BaseScreen.Init();
            PauseScreen.Init();
            DeadScreen.Init();

            Time.timeScale = 1;
        }

        #endregion
    }

}
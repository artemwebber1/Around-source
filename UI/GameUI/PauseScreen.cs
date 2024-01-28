using Assets.Scripts.UI.Screens.Base;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace Assets.Scripts.UI.Screens
{
    public sealed class PauseScreen : GameScreen
    {
        public void ResumeGame()
        {
            AudioListener.pause = false;
            Canvas.BaseScreen.gameObject.SetActive(true);
            gameObject.SetActive(false);

            Time.timeScale = 1;
        }

        public void ReturnMainMenu()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("MainMenu");
        }
    }
}

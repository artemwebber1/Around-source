using Assets.Scripts.UI.Screens.Base;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace Assets.Scripts.UI.Screens
{
    public sealed class GamePlayScreen : GameScreen
    {
        public void PauseGame()
        {
            AudioListener.pause = true;
            Canvas.PauseScreen.gameObject.SetActive(true);
            gameObject.SetActive(false);

            Time.timeScale = 0;
        }
    }
}

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


namespace Assets.Scripts.UI.MainMenuEntities
{
    public class LoadingScreen : MonoBehaviour
    {
        [SerializeField] private Slider _slider;

        private void OnEnable()
        {
            EventManager.onSceneChanged += Load;
        }

        private void OnDisable()
        {
            EventManager.onSceneChanged -= Load;
        }

        private void Load() => StartCoroutine(LoadSceneAsync());

        private IEnumerator LoadSceneAsync()
        {
            AsyncOperation sceneAsyncLoading = SceneManager.LoadSceneAsync("Game");
            sceneAsyncLoading.allowSceneActivation = false;

            while (!sceneAsyncLoading.isDone)
            {
                _slider.value = sceneAsyncLoading.progress;

                if (sceneAsyncLoading.progress >= 0.9f && !sceneAsyncLoading.allowSceneActivation)
                    sceneAsyncLoading.allowSceneActivation = true;

                yield return null;
            }
        }
    }
}

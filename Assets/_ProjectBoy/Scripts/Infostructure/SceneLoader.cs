using System;
using System.Collections;
using UnityEngine.SceneManagement;

namespace _ProjectBoy.Scripts.Infostructure
{
    public class SceneLoader
    {
        private readonly ICoroutineRunner _coroutineRunner;

        public SceneLoader(ICoroutineRunner coroutineRunner)
        {
            _coroutineRunner = coroutineRunner;
        }

        public void Load(string sceneName, Action onLoaded = null)
        {
            _coroutineRunner.StartCoroutine(LoadScene(sceneName, onLoaded));
        }

        private IEnumerator LoadScene(string nextScene, Action onLoaded)
        {
            if (SceneManager.GetActiveScene().name == nextScene)
            {
                onLoaded?.Invoke();
                yield break;
            }

            var waitNextScene = SceneManager.LoadSceneAsync(nextScene);

            while (waitNextScene is { isDone: false })
                yield return null;

            onLoaded?.Invoke();
        }
    }
}
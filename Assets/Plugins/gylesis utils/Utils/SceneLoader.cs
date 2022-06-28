using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Warlords.Utils
{
    public class SceneLoader : MonoBehaviour //  TO Remake
    { 
        private CurtainService _curtainService;

        [Inject]
        private void Init(CurtainService curtainService)
        {
            _curtainService = curtainService;

            DontDestroyOnLoad(gameObject);
        }

        public async UniTask LoadScene(string sceneName)
        {
            var sceneLoadProgress = new FloatReactiveProperty(0);
            var sceneProgress = new Progress<float>();

            var sceneAsyncOperation = SceneManager.LoadSceneAsync(sceneName);
            var sceneLoadingAsync = sceneAsyncOperation.ToUniTask(sceneProgress);

            sceneAsyncOperation.allowSceneActivation = false;

            sceneProgress.ProgressChanged += SceneLoadingProgress;

            _curtainService.ShowWithProgress(sceneLoadProgress);

            await sceneLoadingAsync;

            await Task.Delay(100);

            var asyncMethodsProgress = new Progress<float>();

            sceneLoadProgress.Value = 0;

            void SceneLoadingProgress(object sender, float progressValue)
            {
                sceneLoadProgress.Value = progressValue;

                if (progressValue >= 0.9f) sceneAsyncOperation.allowSceneActivation = true;
            }

            void AsyncMethodsLoadingProgress(object sender, float progressValue)
            {
                sceneLoadProgress.Value = progressValue;
            }

            asyncMethodsProgress.ProgressChanged += AsyncMethodsLoadingProgress;

            _curtainService.ShowWithProgress(sceneLoadProgress);

           // await _asyncLoadingsDispatcher.LoadAsync(asyncMethodsProgress);  // TO Remake

            _curtainService.Hide();

            asyncMethodsProgress.ProgressChanged -= AsyncMethodsLoadingProgress;
            sceneProgress.ProgressChanged -= SceneLoadingProgress;
        }
    }
}
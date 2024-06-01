using UnityEngine;
using UnityEngine.SceneManagement;

namespace MaidCafe.Utility
{
    [AddComponentMenu("Maid Cafe/Utility/Bootstrapper")]
    public class Boot : MonoBehaviour
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void Init()
        {
            Scene currentScene = SceneManager.GetActiveScene();

#if UNITY_EDITOR
            if (currentScene.IsValid())
                SceneManager.LoadSceneAsync(currentScene.buildIndex == 0 ? 1 : 0, LoadSceneMode.Additive);
#else
            Application.runInBackground = true;
#endif
        }
        
        void UnloadBoot(Scene scene, LoadSceneMode mode)
        {
            if (scene.buildIndex == 0)
            {
                SceneManager.UnloadSceneAsync(0);
                SceneManager.sceneLoaded -= UnloadBoot;
            }
        }
        
        void OnEnable()
        {
            SceneManager.sceneLoaded += UnloadBoot;        
        }
        
        void OnDisable()
        {
            SceneManager.sceneLoaded -= UnloadBoot;
        }
    }
}

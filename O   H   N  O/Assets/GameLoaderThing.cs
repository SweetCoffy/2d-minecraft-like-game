using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameLoaderThing : MonoBehaviour
{
    public GameObject loadingScreen;
    public Image loadingBar;
    

    
    public void StartGame(int scene = 1) {
        if (loadingScreen == null) {
            loadingScreen = LoadingScreen.main;
        }

        if (loadingBar == null) {
            loadingBar = LoadingBar.main;
        }
        
        AsyncOperation op = SceneManager.LoadSceneAsync(scene);
        StartCoroutine(Loading(op));
    }

    IEnumerator Loading(AsyncOperation op) {
        loadingScreen.SetActive(true);
        
        while (!op.isDone) {
            
            loadingBar.fillAmount = Mathf.Lerp(loadingBar.fillAmount, Mathf.Clamp(op.progress / 0.9f * 1, 0, 1), 10 * Time.deltaTime);
            yield return null;
        }

        if (op.isDone) {
            loadingScreen.SetActive(false);
        }
    }

    void OnDisable () {
        if (!loadingScreen)
            return;
        
        loadingScreen.SetActive(false);
    }
}



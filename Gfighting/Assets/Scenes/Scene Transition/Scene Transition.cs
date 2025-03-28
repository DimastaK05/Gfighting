using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransition : MonoBehaviour
{

    public Text LoadingPercentage;

    private Animator componentAnimator;

    private static SceneTransition instance;
    private static bool shouldPlayAnimation = false;

    private AsyncOperation loadingSceneOperation;

    public static void SwitchToScene(string sceneName)
    {
        instance.componentAnimator.SetTrigger("sceneClosing");

        instance.loadingSceneOperation = SceneManager.LoadSceneAsync(sceneName);
        instance.loadingSceneOperation.allowSceneActivation = false;
    }

    void Start()
    {
        instance = this;
        componentAnimator = GetComponent<Animator>();

        if (shouldPlayAnimation) componentAnimator.SetTrigger("sceneOpening");
    }

    // Update is called once per frame
    void Update()
    {
        if (loadingSceneOperation != null) { 
        LoadingPercentage.text = Mathf.RoundToInt(loadingSceneOperation.progress * 100) + "%";
        }
    }


    public void OnAnimationOver()
    {
        shouldPlayAnimation = true;
        loadingSceneOperation.allowSceneActivation = true;
    }
}

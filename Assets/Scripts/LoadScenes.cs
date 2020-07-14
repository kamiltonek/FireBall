using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScenes : MonoBehaviour
{
    [SerializeField] Animator transition;
    [SerializeField] GameObject crossfade;

    private float transitionTime = 1f;
    public void LoadScene(string name)
    {
        crossfade.SetActive(true);
        setNormalTimeScale();
        StartCoroutine(LoadLevel(name));
    }

    public void setNormalTimeScale()
    {
        Time.timeScale = 1f;
    }

    IEnumerator LoadLevel(string name)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        

        if (SceneManager.GetActiveScene().name == "Game")
        {
            Screen.sleepTimeout = SleepTimeout.SystemSetting;
            showVideo();
        }   

        if(name == "Game")
        {
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
            AdController.Instance.destroyBanner();
            Time.timeScale = 0f;
        }
        SceneManager.LoadScene(name);
    }

    private void showVideo()
    {
        int random = Random.Range(1, 4);
        if (random != 0)
        {
            AdController.Instance.showVideo();
        }
    }
}

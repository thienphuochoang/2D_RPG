using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScene_UI : MonoBehaviour
{
    [SerializeField] private string playSceneName = "PlayScene";
    [SerializeField] private GameObject _continueButton;
    [SerializeField] private FadeScreen_UI _fadeScreenUI;

    private void Start()
    {
        if (SaveManager.Instance.HasSavedData() == false)
        {
            _continueButton.SetActive(false);
        }
        else
        {
            _continueButton.SetActive(true);
        }
    }

    public void Continue()
    {
        StartCoroutine(LoadScreenWithFadeEffect(1.5f));
        //SceneManager.LoadScene(playSceneName);
    }

    public void NewGame()
    {
        SaveManager.Instance.DeleteSaveData();
        StartCoroutine(LoadScreenWithFadeEffect(1.5f));
        //SceneManager.LoadScene(playSceneName);
    }

    public void QuitGame()
    {
        Debug.Log("Quit game");
    }

    IEnumerator LoadScreenWithFadeEffect(float delayTime)
    {
        _fadeScreenUI.FadeOut();
        yield return new WaitForSeconds(delayTime);
        SceneManager.LoadScene(playSceneName);
    }
}

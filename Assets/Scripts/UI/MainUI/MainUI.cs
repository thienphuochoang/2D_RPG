using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainUI : MonoBehaviour
{
    public FadeScreen_UI fadeScreenUI;
    public ItemTooltip_UI itemTooltipUI;
    public StatTooltip_UI statTooltipUI;
    public CraftWindow_UI craftWindowUI;
    public InGame_UI inGameUI;
    public GameObject mainMenuUI;
    public GameObject pauseGameUI;

    [Header("Dead Character Screen")]
    [SerializeField]
    private GameObject _deadScreenUI;

    private void Start()
    {
        GameManager.Instance.OnPauseGameChanged += SwitchToPauseGameUI;
    }

    private void Update()
    {
    }

    private void SwitchToPauseGameUI(bool isPause)
    {
        if (isPause)
        {
            pauseGameUI.SetActive(true);
            inGameUI.gameObject.SetActive(false);
            mainMenuUI.SetActive(false);
        }
        else
        {
            pauseGameUI.SetActive(false);
            inGameUI.gameObject.SetActive(true);
            mainMenuUI.SetActive(true);
        }
    }
    
    public void SwitchTo(GameObject menu)
    {
        
        /*for (int i = 0; i < transform.childCount; i++)
        {
            bool fadeScreen = transform.GetChild(i).GetComponent<FadeScreen_UI>() != null;
            if (fadeScreen == false)
                transform.GetChild(i).gameObject.SetActive(false);
        }*/
        if (menu != null)
        {
            if (menu.activeSelf)
                menu.SetActive(false);
            else
                menu.SetActive(true);
        }
    }

    public void SwitchToDeadCharacterUI()
    {
        StartCoroutine(EndScreenCoroutine());
    }

    IEnumerator EndScreenCoroutine()
    {
        yield return new WaitForSeconds(1.5f);
        fadeScreenUI.FadeOut();
        yield return new WaitForSeconds(1f);
        _deadScreenUI.SetActive(true);
    }

    public void RestartGame() => GameManager.Instance.RestartScene();
}

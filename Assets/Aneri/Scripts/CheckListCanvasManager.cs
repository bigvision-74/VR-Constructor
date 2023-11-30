using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CheckListCanvasManager : MonoBehaviour
{
    public static CheckListCanvasManager Instance;

    public GameObject hideChecklistPanel;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AnimationStartandInteractableOff(GameObject button)
    {
        button.GetComponent<Animator>().SetTrigger("Tick");
        button.GetComponent<Button>().interactable = false;
    }

    public void nextButtonActive(GameObject nextButton)
    {
        nextButton.GetComponent<Button>().interactable = true;
    }

    public void nextProcessPanelActive(GameObject nextProcessPanel)
    {
        nextProcessPanel.SetActive(true);
    }

    public void HideChecklistPanel()
    {
        hideChecklistPanel.SetActive(true);
    }

    



}

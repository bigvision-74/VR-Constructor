using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CheckListCanvasManager : MonoBehaviour
{
    
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
        StartCoroutine(Delay());
        nextProcessPanel.SetActive(true);
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(2f);
    }

}

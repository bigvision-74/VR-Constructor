using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject quizCanvas;
    public GameObject congratsPanel;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // this will be called from the Script attached on "fencing" animation, as that is the "StateMachineBehaviour", it doesn't
    // allowed to reference any gameobject from the inspecotr, also it doesn't find the inacitve gameobject in the scene.
    // So we are using this script as a "middle-man" just to call this method from that class.
    public void ActivateQuizCanvas()
    {
        StartCoroutine(ActivationWithDelay());
    }

    private IEnumerator ActivationWithDelay()
    {
        // first we will activate the Congrats panel.
        if (congratsPanel != null)
        {
            congratsPanel.SetActive(true);
        }
        else
        {
            Debug.LogError("CongratsPanel is not assigned in GameManager.");
        }

        yield return new WaitForSeconds(5f); // wait for 5 secs.

        // then activate the quiz panel.
        if (quizCanvas != null)
        {
            quizCanvas.SetActive(true);
        }
        else
        {
            Debug.LogError("QuizCanvas is not assigned in GameManager.");
        }
    }
}

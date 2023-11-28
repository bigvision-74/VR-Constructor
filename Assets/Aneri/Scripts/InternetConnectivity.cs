using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InternetConnectivity : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (GlobalUserData.Instance.UserType == "temporary")
        {
            StartCoroutine(CheckInternetConnectionPeriodically());
        }
    }
    private bool HasInternetConnection()
    {
        return Application.internetReachability != NetworkReachability.NotReachable;
    }

    private IEnumerator CheckInternetConnectionPeriodically()
    {
        while (true)
        {
            if (!HasInternetConnection())
            {
                Debug.LogWarning("No internet connection!");
                PauseGame();
                // Optionally, display a UI element to inform the user
            }
            else
            {
                ResumeGame();
                // Optionally, hide the UI element
            }

            yield return new WaitForSeconds(5); // Wait for 5 seconds before checking again.
        }
    }

    private void PauseGame()
    {
        Time.timeScale = 0;
        // Add code here to show a UI element or popup to inform the user about the lost connection.
    }

    private void ResumeGame()
    {
        Time.timeScale = 1;
        // Add code here to hide the UI element or popup.
    }
}

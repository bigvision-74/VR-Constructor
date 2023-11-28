using UnityEngine;
using System.Collections;

public class GlobalUserData : MonoBehaviour
{
    public static GlobalUserData Instance;

    public string UserType { get; private set; }

    private void Awake()
    {
        // Singleton Pattern
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

    private void Start()
    {
        if (GlobalUserData.Instance.UserType == "temporary")
        {
            Debug.Log(GlobalUserData.Instance.UserType);
            StartCoroutine(CheckInternetConnectionPeriodically());
        }
    }

    public void SetUserType(string userType)
    {
        UserType = userType;
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

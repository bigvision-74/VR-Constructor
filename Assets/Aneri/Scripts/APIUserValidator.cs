using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using TMPro;
using System;
using System.Globalization;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class APIUserValidator : MonoBehaviour
{

    public TMP_InputField username;
    public TMP_InputField password;
    public TextMeshProUGUI outputText;
    public GameObject login_UI;
    public GameObject license_UI;
    public TMP_InputField licenseKey;

    private APIResponse response = new APIResponse();

    // this is called when Login button is clicked.
    public void ValidateUserCredentials()
    {
        if (!HasInternetConnection())
        {
            // Show a popup or message to the user
            Debug.LogWarning("No internet connection!");
            return;
        }

        StartCoroutine(ValidateUser());
    }

    // this is called when OK button is clicked.
    public void ValidateLicenseKey()
    {
        if (!HasInternetConnection())
        {
            // Show a popup or message to the user
            Debug.LogWarning("No internet connection!");
            return;
        }

        StartCoroutine(ValidateLicense());
    }

    private bool HasInternetConnection()
    {
        return Application.internetReachability != NetworkReachability.NotReachable;
    }

    // this validates the user and as per the license type, also validates the temporary license and permanent license.
    private IEnumerator ValidateUser()
    {
        WWWForm form = new WWWForm();
        form.AddField("username", username.text);
        form.AddField("password", password.text);
        /*form.AddField("platform", "Desktop");
        form.AddField("deviceId", "3e5ebf8b2200c6d476d74b53f19fa3ea69319251");*/

        using (UnityWebRequest www = UnityWebRequest.Post("http://eduqwiz.com/user/login", form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.LogError("Error: " + www.error);
            }
            else
            {
                response = JsonUtility.FromJson<APIResponse>(www.downloadHandler.text);
                
                // storing the userType to access it in other scenes for internet checking.
                GlobalUserData.Instance.SetUserType(response.data.UserType);

                if (response.data.Status)
                {
                    if (response.data.license == "temporary" && string.IsNullOrEmpty(response.data.startDate))
                    {
                        // First-time login for a temporary user
                        DateTime currentDate = DateTime.Now;
                        DateTime endDate = currentDate.AddDays(int.Parse(response.data.validity));

                        // Call the new API endpoint to store the dates
                        StartCoroutine(UpdateDatesInDatabase(response.data.userid, currentDate.ToString("yyyy-MM-dd HH:mm:ss"), endDate.ToString("yyyy-MM-dd HH:mm:ss"), licenseKey.text));

                    }
                    /*else if (response.data.license == "temporary")
                    {
                        DateTime endDate = DateTime.ParseExact(response.data.endDate, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
                        if (DateTime.Now > endDate)
                        {
                            Debug.Log("Your license has expired.");
                        }
                        else
                        {
                            outputText.text = "User validated successfully!" + "\n" + " Your license is valid upto " + response.data.endDate;
                            Debug.Log("Your license is still valid upto " + response.data.endDate);
                        }
                    }*/
                    else if (response.data.license == "permanent" && string.IsNullOrEmpty(response.data.startDate))
                    {
                        // Handle permanent license users
                        outputText.text = "User validated Successfully!";
                        login_UI.SetActive(false);
                        license_UI.SetActive(true);
                        // now when the OK button is clicked, the ValidateLicense() coroutine will get called.
                    }
                    else
                    {
                        DateTime endDate = DateTime.ParseExact(response.data.endDate, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
                        if (DateTime.Now > endDate)
                        {
                            outputText.text = "Your license has expired.";
                            Debug.Log("Your license has expired.");
                        }
                        else
                        {
                            outputText.text = "User validated successfully!" + "\n" + " Your license is valid upto " + response.data.endDate;
                            Debug.Log("Your license is still valid upto " + response.data.endDate);

                            // When the license key is validated, take user to the main scene.
                            StartCoroutine(LoadScene());
                        }
                    }

                }
                else
                {
                    Debug.Log("User validation failed. Message: " + response.status.messages);
                    outputText.text = "User validation failed. Message: " + response.status.messages;
                }
            }
        }
    }

    // this is just for validating the license key entered by a permanent user.
    IEnumerator ValidateLicense()
    {
        WWWForm form = new WWWForm();
        form.AddField("licenseKey", licenseKey.text);

        using (UnityWebRequest www = UnityWebRequest.Post("http://eduqwiz.com/user/validateLicense", form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.LogError("Error: " + www.error);
            }
            else
            {
                APIResponseLicense licenseResponse = JsonUtility.FromJson<APIResponseLicense>(www.downloadHandler.text);
                Debug.Log(licenseResponse.licenseData.activated);
                // when the key is valid but not activated, it returns false, i.e 0. So we will insert dates and from backend the "activated" field will change to 1, i.e true.
                // So next time if the user can't use the same key if it is activated.
                if (!licenseResponse.licenseData.activated && licenseResponse.status.messages == "Valid Key") 
                {
                    outputText.text = licenseResponse.status.messages;
                    DateTime currentDate = DateTime.Now;
                    DateTime endDate = currentDate.AddDays(int.Parse(response.data.validity));

                    // Call the new API endpoint to store the dates
                    StartCoroutine(UpdateDatesInDatabase(response.data.userid, currentDate.ToString("yyyy-MM-dd HH:mm:ss"), endDate.ToString("yyyy-MM-dd HH:mm:ss"), licenseKey.text));
                }
                else
                {
                    outputText.text = licenseResponse.status.messages + "\n Please enter valid license key";
                }
            }
        }
    }

    IEnumerator UpdateDatesInDatabase(string userid, string startDate, string endDate, string licenseKey)
    {
        WWWForm form = new WWWForm();
        form.AddField("userid", userid);
        if(response.data.license == "temporary")
            form.AddField("licenseKey", "-");
        else
            form.AddField("licenseKey", licenseKey);
        form.AddField("startDate", startDate);
        form.AddField("endDate", endDate);

        using (UnityWebRequest www = UnityWebRequest.Post("http://eduqwiz.com/user/activateLicense", form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.LogError("Error: " + www.error);
            }
            else
            {
                // Handle the response. Maybe the server sends a confirmation message.

                Debug.Log("Your license activated successfully, valid upto " + endDate);
                outputText.text = "Your license activated successfully, valid upto " + endDate;

                // When the license key is valid and dates are stored, take user to the main scene.
                StartCoroutine(LoadScene());
            }
        }
    }

    IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(1);
    }
}

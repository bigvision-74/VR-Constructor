using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PasswordRevealScript : MonoBehaviour
{
    public TMP_InputField passwordInputField;
    public Button showPasswordButton;
    public Button hidePasswordButton;

    private void Start()
    {
        // Initially, only the Show button should be visible
        hidePasswordButton.gameObject.SetActive(false);

        // Assign the button's click events
        showPasswordButton.onClick.AddListener(ShowPassword);
        hidePasswordButton.onClick.AddListener(HidePassword);
    }

    public void ShowPassword()
    {
        passwordInputField.contentType = TMP_InputField.ContentType.Standard;
        passwordInputField.ForceLabelUpdate();

        // Swap the visibility of the buttons
        showPasswordButton.gameObject.SetActive(false);
        hidePasswordButton.gameObject.SetActive(true);
    }

    public void HidePassword()
    {
        passwordInputField.contentType = TMP_InputField.ContentType.Password;
        passwordInputField.ForceLabelUpdate();

        // Swap the visibility of the buttons back
        hidePasswordButton.gameObject.SetActive(false);
        showPasswordButton.gameObject.SetActive(true);
    }
}

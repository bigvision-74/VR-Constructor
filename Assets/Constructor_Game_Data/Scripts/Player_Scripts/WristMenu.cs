using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WristMenu : MonoBehaviour
{
   // public UIAnimator AnimUi;

    public GameObject wristUI;
    public bool activewristuI = true;
    // Start is called before the first frame update
    void Start()
    {
        DisplaywristUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void MenuPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            DisplaywristUI();
           // AnimUi.OnMainMenuButton();
        }
        
    }
    
    public void DisplaywristUI()
    {
        if (activewristuI)
        {
         wristUI.SetActive(false);
            activewristuI = false;

        }
        else if (!activewristuI)
        {
            wristUI.SetActive(true);
            activewristuI = true;
        }
           
    }
}

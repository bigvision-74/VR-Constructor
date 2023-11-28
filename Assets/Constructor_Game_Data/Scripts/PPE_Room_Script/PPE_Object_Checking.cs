using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PPE_Object_Checking : MonoBehaviour
{
    [SerializeField] GameObject[] RightSine;
    [SerializeField] GameObject CongPanel;
    [SerializeField] GameObject can_vas;
    [SerializeField] GameObject SceneChangePanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Check if all GameObjects in the array are active
        bool allActive = true;

        foreach (var item in RightSine)
        {
            if (!item.activeInHierarchy)
            {
                allActive = false;
                break; // Break out of the loop as soon as an inactive object is found
            }
        }

        // If all objects are active, perform the actions
        if (allActive)
        {
            can_vas.SetActive(true);
            StartCoroutine(congpanelOn());
        }
    }

    IEnumerator congpanelOn()
    {
        CongPanel.SetActive(true);
        yield return new WaitForSeconds(5f);
        Destroy(CongPanel);
        SceneChangePanel.SetActive(true);
    }
}

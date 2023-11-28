using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatedObjects : MonoBehaviour
{
    [SerializeField] GameObject[] icon;
    private void Start()
    {
        foreach (var item in icon)
        {
            item.SetActive(false);
        }
        icon[0].SetActive(true);
       
    }
    public void OnButtonClick(GameObject obj)
    {
       
        for (int i = 0; i < icon.Length; i++)
        {
            icon[i].SetActive(icon[i].Equals(obj));
        }
       
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapHandalling : MonoBehaviour
{
    [SerializeField] GameObject Player;
    [SerializeField] GameObject Canvas;

    public GameObject[] blackdots;
    public GameObject[] whitedots;


    // Start is called before the first frame update

    public void OnePoint()
    {
        Player.transform.position = new Vector3(4.35f, 0, -6.92f);
       
    } 
    public void TwoPoint()
    {
        Player.transform.position = new Vector3(15.59f, 0, -5.204481f);
        Player.transform.rotation = Quaternion.Euler(0, 90, 0);
        Canvas.transform.position = new Vector3(17.12f, 1.25f, -5.204479f);
        Canvas.transform.rotation = Quaternion.Euler(0, 90, 0);

        foreach (var item in blackdots)
        {
            item.SetActive(false);
        }
        foreach (var item in whitedots)
        {
            item.SetActive(true);
        }
    } 
    public void ThreePoint()
    {
        Player.transform.position = new Vector3(14.993f, 0, -10.36f);
        Player.transform.rotation = Quaternion.Euler(0, 90f, 0);
    }
    public void FourPoint()
    {
        Player.transform.position = new Vector3(18.2624f, 0, -0.4221f);
        Player.transform.rotation = Quaternion.Euler(0, 0, 0);
    } 
    public void FivePoint()
    {
        Player.transform.position = new Vector3(22.34f, 0, 32.24f);
        Player.transform.rotation = Quaternion.Euler(0, 0, 0);
    }
    public void SixPoint()
    {
        Player.transform.position = new Vector3(-49.07f, 0, 47.15f);
        Player.transform.rotation = Quaternion.Euler(0, -90f, 0);
    }
    public void SevenPoint()
    {
        Player.transform.position = new Vector3(-41.1f, 0, 38.83f);
        Player.transform.rotation = Quaternion.Euler(0, 90f, 0);
    }
    public void EightPoint()
    {
        Player.transform.position = new Vector3(-48.613f, 0, 38.448f);
        Player.transform.rotation = Quaternion.Euler(0, -90f, 0);
    }
    public void NinePoint()
    {
        Player.transform.position = new Vector3(-43.695f, 0, -23.784f);
        Player.transform.rotation = Quaternion.Euler(0, 180, 0);
    }
    public void TenPoint()
    {
        Player.transform.position = new Vector3(-47.99f, 0, 7.53f);
        Player.transform.rotation = Quaternion.Euler(0, -90f, 0);
    }
    public void ElevenPoint()
    {
        Player.transform.position = new Vector3(-32.065f, 0, -10.008f);
        Player.transform.rotation = Quaternion.Euler(0, 180, 0);
    }
    public void QuestionMode()
    {
        Player.transform.position = new Vector3(14f, 0, -10f);
        Player.transform.rotation = Quaternion.Euler(0, 220, 0);
    }
    public void EnterContamination()
    {
        Player.transform.position = new Vector3(15.59f, 0, -5.204481f);
        Player.transform.rotation = Quaternion.Euler(0, 90, 0);
        Canvas.transform.position = new Vector3(17.12f, 1.25f, -5.204479f);
        Canvas.transform.rotation = Quaternion.Euler(0, 90, 0);

    }
    public void WelcomeSkip()
    {
        Player.transform.position = new Vector3(17.163f, 0, -5.179f);
        Player.transform.rotation = Quaternion.Euler(0, -90, 0);
    }
    public void OtherTwoMode()
    {
        Player.transform.position = new Vector3(15.59f, 0, -5.204481f);
        Player.transform.rotation = Quaternion.Euler(0, 90, 0);
    }
}

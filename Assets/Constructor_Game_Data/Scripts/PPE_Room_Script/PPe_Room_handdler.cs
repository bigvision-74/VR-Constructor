using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PPe_Room_handdler : MonoBehaviour
{
    public Transform Target;
    public Transform Final;
    public float time = 0.80f;

    float speed = 50f;
    public GameObject Model;
    public bool Collide = false;
    private void Update()
    {
        if (Collide == true)
        {
            Model.transform.Rotate(Vector3.up * speed * Time.deltaTime);
        }
    }
    // Start is called before the first frame update
    public void MouseOverFirstPos()
    {
        Vector3 initialPos = transform.position;
        Vector3 TargetPos = Target.transform.position;
        transform.position = Vector3.Lerp(initialPos, TargetPos, time);
        Collide = true;
        
    }
    public void MouseUvOverPos()
    {
        Collide = false;
        this.transform.position = Final.transform.position;
        this.transform.rotation = Final.transform.rotation;
    }
    public void position()
    {
        Collide = false;
        this.transform.position = Final.transform.position;
        this.transform.rotation = Final.transform.rotation;
    }
}

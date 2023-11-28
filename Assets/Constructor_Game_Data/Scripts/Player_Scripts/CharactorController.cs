using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactorController : MonoBehaviour
{
    private Animator animator;
    public Transform target;
    public bool isWalking = false;
    public GameObject Charactor;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (isWalking == true)
        {
            float moveSpeed = 2f;
            Vector3 tragetPosition = new Vector3(target.position.x, transform.position.y, target.position.z);
            transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
        }
    }
    public void WalkingCharactor()
    {
        animator.SetBool("Walk",true );
        isWalking = true;
    }
    public void idleCharactor()
    {
        animator.SetBool("Walk", false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Finish")
        {
            Debug.Log("In");
            animator.SetBool("Walk", false);
            target = null;
            isWalking = false;
            Charactor.SetActive(true);
            this.gameObject.SetActive(false);
        }

    }
}

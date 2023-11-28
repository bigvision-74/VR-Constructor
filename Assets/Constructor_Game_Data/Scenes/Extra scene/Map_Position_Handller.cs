using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map_Position_Handller : MonoBehaviour
{
    public Vector3 targetPosition;
    public Quaternion targetRotation;
    [SerializeField] GameObject Player;

    public void ChangePlayerPositionAndRotation()
    {
        // Set the position and rotation based on the inspector values
        Player.transform.position = targetPosition;
        Player.transform.rotation = targetRotation;
    }
}

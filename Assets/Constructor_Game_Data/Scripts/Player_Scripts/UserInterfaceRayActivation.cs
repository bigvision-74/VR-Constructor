using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UserInterfaceRayActivation : MonoBehaviour
{
    [SerializeField] private Transform linkedhandposition;
    [SerializeField] private LayerMask layerToHit;
    [SerializeField] private float maxDistanceFromCanvas;

    [Header("UI Hover Events")]
    public UnityEvent onUIHoverStart;
    public UnityEvent OnUIHoverEnd;

    enum CurrentInteractorState
    {
        DefaultMode,
        UIMode
    }
    private CurrentInteractorState currentInteractorMode;

    private void Awake() => currentInteractorMode = CurrentInteractorState.DefaultMode;

    private void FixedUpdate()
    {
        RaycastHit hit;

        if (Physics.Raycast(linkedhandposition.position, linkedhandposition.forward, out hit, maxDistanceFromCanvas, layerToHit))
        {
            if (currentInteractorMode != CurrentInteractorState.UIMode)
            {
                onUIHoverStart.Invoke();
                currentInteractorMode = CurrentInteractorState.UIMode;
            }
        }
        else
        {
            if (currentInteractorMode == CurrentInteractorState.UIMode)
            {
                OnUIHoverEnd.Invoke();
                currentInteractorMode = CurrentInteractorState.DefaultMode;
            }

        }

    }
}


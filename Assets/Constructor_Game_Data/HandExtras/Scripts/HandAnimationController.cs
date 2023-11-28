using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class HandAnimationController : MonoBehaviour
{
    [SerializeField] Animator handAnimator;
    [SerializeField] InputActionReference gripAction;
    [SerializeField] InputActionReference pinchAction;
   
    private void OnEnable()
    {
        gripAction.action.performed += GripAnimation;
        pinchAction.action.performed += PinchAnimation;

        gripAction.action.canceled += GripAnimationRelease;
        pinchAction.action.canceled += PinchAnimationRelease;
    }
    private void PinchAnimation(InputAction.CallbackContext obj)
    {
        handAnimator.SetFloat("Trigger", obj.ReadValue<float > ());
    }
    private void GripAnimation(InputAction.CallbackContext obj)
    {
        handAnimator.SetFloat("Grip", obj.ReadValue<float > ());
    }
    private void PinchAnimationRelease(InputAction.CallbackContext obj)
    {
        handAnimator.SetFloat("Trigger", obj.ReadValue<float>());
    }
    private void GripAnimationRelease(InputAction.CallbackContext obj)
    {
        handAnimator.SetFloat("Grip", obj.ReadValue<float>());
    }
}

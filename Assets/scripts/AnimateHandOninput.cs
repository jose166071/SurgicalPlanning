using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class AnimateHandOninput : MonoBehaviour

{
    public InputActionProperty PinchAnimationAction;
    public InputActionProperty GripAnimationAction;
    public Animator handAnimator;

    // Update is called once per frame
    void Update()
    {
        float TriggerValue = PinchAnimationAction.action.ReadValue<float>();
        handAnimator.SetFloat("Trigger", TriggerValue);

        float gripValue = GripAnimationAction.action.ReadValue<float>();
        handAnimator.SetFloat("Grip", gripValue);
    }
}

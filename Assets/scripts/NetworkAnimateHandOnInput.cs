using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Netcode;
public class NetworkAnimateHandOnInput : NetworkBehaviour
{
    public InputActionProperty PinchAnimationAction;
    public InputActionProperty GripAnimationAction;
    public Animator handAnimator;

    // Update is called once per frame
    void Update()
    {
        if (IsOwner)
        {
            float TriggerValue = PinchAnimationAction.action.ReadValue<float>();
            handAnimator.SetFloat("Trigger", TriggerValue);

            float gripValue = GripAnimationAction.action.ReadValue<float>();
            handAnimator.SetFloat("Grip", gripValue);
        }

    }
}

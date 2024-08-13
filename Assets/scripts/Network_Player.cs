using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using Photon.Pun;
using UnityEngine.XR.Interaction.Toolkit;
using Unity.XR.CoreUtils;
public class Network_Player : MonoBehaviour
{
    public Transform head;
    public Transform leftHand;
    public Transform rigthHand;

    public Animator leftHandAnimator;
    public Animator rigthHandAnimator;

    private PhotonView photonView;

    private Transform headrig;
    private Transform lefthandrig;
    private Transform rigthhandrig;
    // Start is called before the first frame update
    void Start()
    {
        photonView = GetComponent<PhotonView>();
        XROrigin origin = FindObjectOfType<XROrigin>();
        headrig = origin.transform.Find("Camera Offset/Main Camera");
        lefthandrig = origin.transform.Find("Camera Offset/izquierda");
        rigthhandrig = origin.transform.Find("Camera Offset/derecha");
        if (photonView.IsMine)
        {
            foreach (var item in GetComponentsInChildren<Renderer>())
            {
                item.enabled = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {

            MapPosition(head, headrig);
            MapPosition(leftHand, lefthandrig);
            MapPosition(rigthHand, rigthhandrig);

            UpdateHandAnimation(InputDevices.GetDeviceAtXRNode(XRNode.LeftHand), leftHandAnimator);
            UpdateHandAnimation(InputDevices.GetDeviceAtXRNode(XRNode.RightHand), rigthHandAnimator);
        }
    }
    void UpdateHandAnimation(InputDevice targetDevice, Animator handAnimator)
    {
        if (targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue))
        {
            handAnimator.SetFloat("Trigger", triggerValue);
        }
        else
        {
            handAnimator.SetFloat("Trigger", 0);
        }
        if (targetDevice.TryGetFeatureValue(CommonUsages.grip, out float gripValue))
        {
            handAnimator.SetFloat("Grip", triggerValue);
        }
        else
        {
            handAnimator.SetFloat("Grip", 0);
        }
    }
    void  MapPosition(Transform target, Transform origintransform)
    {
        
        target.position = origintransform.position;
        target.rotation = origintransform.rotation;

    }
}

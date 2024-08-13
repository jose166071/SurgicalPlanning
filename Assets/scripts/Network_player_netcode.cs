using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.XR.Interaction.Toolkit;

public class Network_player_netcode : NetworkBehaviour
{
    public Transform Root;
    public Transform Head;
    public Transform LeftHand;
    public Transform RigthHand;

    public Renderer[] meshToDisable;
    // Start is called before the first frame update

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        if (IsOwner)
        {
            foreach (var item in meshToDisable)
            {
                item.enabled = false;
            }
        }
    }

    void Start()
    { 

    }

    // Update is called once per frame
    void Update()
    {
        if (IsOwner)
        {
            Root.position = VrRigreferences.Singleton.root.position;
            Root.rotation = VrRigreferences.Singleton.root.rotation;

            Head.position = VrRigreferences.Singleton.Head.position;
            Head.rotation = VrRigreferences.Singleton.Head.rotation;

            LeftHand.position = VrRigreferences.Singleton.Left.position;
            LeftHand.rotation = VrRigreferences.Singleton.Left.rotation;

            RigthHand.position = VrRigreferences.Singleton.Rigth.position;
            RigthHand.rotation = VrRigreferences.Singleton.Rigth.rotation;
        }


    }
    public void OnSelectGrabbable(SelectEnterEventArgs eventArgs)
    {
        if (IsClient && IsOwner)
        {
            NetworkObject networkObject = eventArgs.interactableObject.transform.GetComponent<NetworkObject>();
            if(networkObject != null)
            {
                //Request ownership
                RequestGrabbableOwnershipServerRpc(OwnerClientId, networkObject);
            }
        }
    }
    [ServerRpc]
    public void RequestGrabbableOwnershipServerRpc(ulong newOwnerClientId, NetworkObjectReference networkObjectReference)
    {
        if(networkObjectReference.TryGet(out NetworkObject networkObject))
        {
            networkObject.ChangeOwnership(newOwnerClientId);
            Debug.Log($"Ownership changed {newOwnerClientId}");
        }
        else
        {
            Debug.Log($"Unable to change ownership for clientID {newOwnerClientId}");
        }
    }

}

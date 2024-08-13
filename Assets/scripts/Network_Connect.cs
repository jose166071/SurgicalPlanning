using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Unity.Netcode;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using Unity.Netcode.Transports.UTP;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
public class Network_Connect : MonoBehaviour
{
    public InputActionProperty Y;
    public InputActionProperty PinchAnimationAction;
    public int maxConnection = 20;
    public UnityTransport transport;

    private Lobby currentLobby;
    private float hearBeatTimer;
    
    // Start is called before the first frame update
    public async void Awake()
    {
        await UnityServices.InitializeAsync();
        await AuthenticationService.Instance.SignInAnonymouslyAsync();
        JoinOrCreate();
    }

    // Update is called once per frame
    //void Update()
    //{
    //    bool Leftsecondarybutton = Y.action.WasPressedThisFrame();
    //    bool Pinch_bool = PinchAnimationAction.action.WasPressedThisFrame();
    //    if (estoy_cansado_jefe == false)
    //    {
    //        if (Leftsecondarybutton == true){
    //            Create();
    //            estoy_cansado_jefe = true;
    //        }
    //        else if (Pinch_bool == true)
    //        {
    //            Join();
    //            estoy_cansado_jefe = true;
    //        }
    //    }
    //}

    public async void JoinOrCreate()
    {
        try
        {
            currentLobby = await Lobbies.Instance.QuickJoinLobbyAsync();

            string relayJoinCode = currentLobby.Data["JOIN_CODE"].Value;
            JoinAllocation allocation = await RelayService.Instance.JoinAllocationAsync(relayJoinCode);

            transport.SetClientRelayData(allocation.RelayServer.IpV4, (ushort)allocation.RelayServer.Port,
                allocation.AllocationIdBytes, allocation.Key, allocation.ConnectionData, allocation.HostConnectionData);


            NetworkManager.Singleton.StartClient();
        }
        catch
        {
            Create();
        }
    }
    public async void Create()
    {
        Allocation allocation = await RelayService.Instance.CreateAllocationAsync(maxConnection);
        string newJoinCode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);

        Debug.Log(newJoinCode);

        transport.SetHostRelayData(allocation.RelayServer.IpV4, (ushort)allocation.RelayServer.Port,
            allocation.AllocationIdBytes, allocation.Key, allocation.ConnectionData);

        

        CreateLobbyOptions lobbyOptions = new CreateLobbyOptions();
        lobbyOptions.IsPrivate = false;
        lobbyOptions.Data = new Dictionary<string, DataObject>();
        DataObject dataObject = new DataObject(DataObject.VisibilityOptions.Public,newJoinCode);
        lobbyOptions.Data.Add("JOIN_CODE", dataObject);


        currentLobby = await Lobbies.Instance.CreateLobbyAsync("Lobby Name", maxConnection, lobbyOptions);

        NetworkManager.Singleton.StartHost();
    }
    public async void Join()
    {
        currentLobby = await Lobbies.Instance.QuickJoinLobbyAsync();

        string relayJoinCode = currentLobby.Data["JOIN_CODE"].Value;
        JoinAllocation allocation = await RelayService.Instance.JoinAllocationAsync(relayJoinCode);

        transport.SetClientRelayData(allocation.RelayServer.IpV4, (ushort)allocation.RelayServer.Port,
            allocation.AllocationIdBytes, allocation.Key, allocation.ConnectionData, allocation.HostConnectionData);


        NetworkManager.Singleton.StartClient();
    }
    private void Update()
    {
        if(hearBeatTimer > 15)
        {
            hearBeatTimer -= 15;
            if(currentLobby!= null && currentLobby.HostId == AuthenticationService.Instance.PlayerId)
            {
                LobbyService.Instance.SendHeartbeatPingAsync(currentLobby.Id);
            }
        }

        hearBeatTimer += Time.deltaTime;
    }
}

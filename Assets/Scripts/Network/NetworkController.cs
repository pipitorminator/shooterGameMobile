using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class NetworkController : MonoBehaviourPunCallbacks
{
    [SerializeField] private byte maxPlayersPerRoom = 4;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("conectando no servidor");

        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("conectado no servidor " + PhotonNetwork.CloudRegion);
    }

    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom(null, new RoomOptions {MaxPlayers = maxPlayersPerRoom});
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("conectado na servidor sala");
    }
    
    
    // Update is called once per frame
    void Update()
    {
    }
}
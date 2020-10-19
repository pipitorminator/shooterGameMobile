using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class NetworkController : MonoBehaviourPunCallbacks
{
    [SerializeField] private byte maxPlayersPerRoom = 4;

    public Text playerName;
    public RoomManager roomManager;

    public GameObject alertModal;

    public GameObject loadingServerCanvas;
    public GameObject loadingLevelCanvas;
    public GameObject menuCanvas;
    public GameObject roomCanvas;

    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.GameVersion = "0.0.1";
        Debug.Log("conectando no servidor");
        loadingServerCanvas.SetActive(true);
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        menuCanvas.SetActive(true);
        loadingServerCanvas.SetActive(false);

        PhotonNetwork.JoinLobby();
        PhotonNetwork.AutomaticallySyncScene = true;

        Debug.Log("Lobby: " + PhotonNetwork.InLobby);

        Debug.Log("conectado no servidor " + PhotonNetwork.CloudRegion);
    }

    public void CreateRoom()
    {
        if (playerName.text.Length <= 0)
        {
            alertModal.SetActive(true);
            return;
        }

        PhotonNetwork.CreateRoom("sala de " + playerName.text, new RoomOptions {MaxPlayers = maxPlayersPerRoom},
            TypedLobby.Default);
    }

    public void StartGame()
    {
        PhotonNetwork.LoadLevel(1);
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("conectado na sala");
    }

    public override void OnJoinedRoom()
    {
        if (playerName.text.Length <= 0)
        {
            alertModal.SetActive(true);
            PhotonNetwork.LeaveRoom();
            return;
        }

        roomCanvas.SetActive(true);
        menuCanvas.SetActive(false);

        PhotonNetwork.NickName = playerName.text;

        var room = PhotonNetwork.CurrentRoom;

        roomManager.roomName.text = room.Name;

        Debug.Log("entrou na sala " + room.Name);
    }

    public void LeftRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        menuCanvas.SetActive(true);
        roomCanvas.SetActive(false);
    }
}
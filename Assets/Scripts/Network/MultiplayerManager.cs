using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplayerManager : MonoBehaviour
{
    public NetworkController network;

    public void createRoom()
    {
        network.CreateRoom();
    }
    
}
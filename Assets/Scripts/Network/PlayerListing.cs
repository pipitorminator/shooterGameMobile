using System.Collections;
using System.Collections.Generic;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class PlayerListing : MonoBehaviour
{
    [SerializeField] private Text textField;

    public Player player { get; private set; }

    public void SetPlayerInfo(Player playerInfo)
    {
        player = playerInfo;
        textField.text = playerInfo.NickName;
    }
}
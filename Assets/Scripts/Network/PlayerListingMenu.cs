using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class PlayerListingMenu : MonoBehaviourPunCallbacks
{
    [SerializeField] private Transform content;

    [SerializeField] private PlayerListing playerListing;

    private List<PlayerListing> playerListingList = new List<PlayerListing>();

    private void Start()
    {
        Debug.Log(PhotonNetwork.PlayerList);
        var players = PhotonNetwork.PlayerList;

        foreach (var player in players)
        {
            AddToPlayerList(player);
        }
    }

    private void AddToPlayerList(Player newPlayer)
    {
        Debug.Log("jogador");
        var listing = Instantiate(playerListing, content);

        Debug.Log(listing);
        if (listing == null)
        {
            return;
        }

        listing.SetPlayerInfo(newPlayer);
        playerListingList.Add(listing);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        AddToPlayerList(newPlayer);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        var index = playerListingList.FindIndex(x => x.player.Equals(otherPlayer));

        if (index == -1)
        {
            return;
        }

        Destroy(playerListingList[index].gameObject);
        playerListingList.RemoveAt(index);
    }
}
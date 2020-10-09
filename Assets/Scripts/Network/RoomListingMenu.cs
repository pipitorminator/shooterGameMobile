using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class RoomListingMenu : MonoBehaviourPunCallbacks
{
    [SerializeField] private Transform content;

    [SerializeField] private RoomListing roomListing;

    private List<RoomListing> roomListingList = new List<RoomListing>();

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (var info in roomList)
        {
            if (info.RemovedFromList)
            {
                var index = roomListingList.FindIndex(x => x.roomInfo.Name.Equals(info.Name));

                if (index == -1)
                {
                    return;
                }

                Destroy(roomListingList[index].gameObject);
                roomListingList.RemoveAt(index);
            }
            else
            {
                var listing = Instantiate(roomListing, content);

                if (listing == null)
                {
                    return;
                }

                listing.SetRoomInfo(info);
                roomListingList.Add(listing);
            }
        }
    }
}
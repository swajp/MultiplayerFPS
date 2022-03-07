using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RoomListItem : MonoBehaviour
{
    [SerializeField] TMP_Text text;
    [SerializeField] TMP_Text playersCount;

    public RoomInfo info;

    public void SetUp(RoomInfo _info)
    {
        info = _info;
        playersCount.text = _info.PlayerCount + "/" + _info.MaxPlayers;
        text.text = _info.Name;
    }
    public void OnClick()
    {
        Launcher.Instance.JoinRoom(info);
    }
}

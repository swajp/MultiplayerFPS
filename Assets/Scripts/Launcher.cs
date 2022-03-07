using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;
using System.Linq;

public class Launcher : MonoBehaviourPunCallbacks
{

    public static Launcher Instance;

    [SerializeField] private TMP_InputField roomNameInputField;
    [SerializeField] private TMP_Text errorText;

    [SerializeField] private TMP_Text roomNameText;
    [SerializeField] private Transform roomListContent;
    [SerializeField] private GameObject roomListItemPrefab;
    [SerializeField] private Transform playerListContent;
    [SerializeField] private GameObject playerListItemPrefab;

    [SerializeField] private GameObject startGameButton;
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }
    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master");
        PhotonNetwork.JoinLobby();
        PhotonNetwork.AutomaticallySyncScene = true;
    }
    public override void OnJoinedLobby()
    {
        MenuManager.Instance.OpenMenu("titleMenu");
        Debug.Log("Joined lobby");

        PhotonNetwork.NickName = "Player " + Random.Range(0, 1000).ToString("00000");
    }
    public void CreateRoom()
    {
        if(string.IsNullOrEmpty(roomNameInputField.text))
        {
            return;
        }

        PhotonNetwork.CreateRoom(roomNameInputField.text);
        MenuManager.Instance.OpenMenu("creatingMenu");
    }
    public override void OnJoinedRoom()
    {
        MenuManager.Instance.OpenMenu("roomMenu");
        roomNameText.text = "Room: "+ PhotonNetwork.CurrentRoom.Name;

        Player[] players = PhotonNetwork.PlayerList;

        foreach (Transform child in playerListContent)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < players.Count(); i++)
        {
            Instantiate(playerListItemPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(players[i]);
        }

        startGameButton.SetActive(PhotonNetwork.IsMasterClient);
    }
    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        startGameButton.SetActive(PhotonNetwork.IsMasterClient);
    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        errorText.text = "Something went wrong:" + message;
        MenuManager.Instance.OpenMenu("errorMenu");
    }
    public void StartGame()
    {
        PhotonNetwork.LoadLevel(1);
    }
    public void OnLeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        MenuManager.Instance.OpenMenu("loadingMenu");
    }
    public void JoinRoom(RoomInfo info)
    {
        PhotonNetwork.JoinRoom(info.Name);
        MenuManager.Instance.OpenMenu("loadingMenu");
    }
    public override void OnLeftRoom()
    {
        MenuManager.Instance.OpenMenu("titleMenu");
    }
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (Transform trans in roomListContent)
        {
            Destroy(trans.gameObject);
        }
        for (int i = 0; i < roomList.Count; i++)
        {
            if (roomList[i].RemovedFromList)
                continue;
            Instantiate(roomListItemPrefab, roomListContent).GetComponent<RoomListItem>().SetUp(roomList[i]);
        }
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Instantiate(playerListItemPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(newPlayer);
    }
}

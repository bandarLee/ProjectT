using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_RoomInfo : MonoBehaviourPunCallbacks
{
    public static UI_RoomInfo Instance { get; private set; }

    public TextMeshProUGUI RoomNameTextUI;
    public TextMeshProUGUI PlayerCountTextUI;
    public TextMeshProUGUI LogTextUI;

    private List<string> _logMessages = new List<string>();
    private const int MaxLogLines = 8;

    private void Awake()
    {
        Instance = this;
    }

    public override void OnJoinedRoom()
    {
        Init();
    }

    private void Start()
    {
        if (PhotonNetwork.InRoom)
        {
            Init();
        }
    }

    private void Init()
    {
        RoomNameTextUI.text = PhotonNetwork.CurrentRoom.Name;
        PlayerCountTextUI.text = $"{PhotonNetwork.CurrentRoom.PlayerCount}/{PhotonNetwork.CurrentRoom.MaxPlayers}";

        AddLog("방에 입장했습니다");
    }

    private void Refresh()
    {
        LogTextUI.text = string.Join("\n", _logMessages);
        PlayerCountTextUI.text = $"{PhotonNetwork.CurrentRoom.PlayerCount}/{PhotonNetwork.CurrentRoom.MaxPlayers}";
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        AddLog($"{newPlayer.NickName}님이 입장했습니다");
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        AddLog($"{otherPlayer.NickName}님이 퇴장했습니다");
    }

    public void AddLog(string logMessage)
    {
        if (_logMessages.Count >= MaxLogLines)
        {
            _logMessages.RemoveAt(0);
        }

        _logMessages.Add(logMessage);
        Refresh();

        StartCoroutine(RemoveLogAfterDelay(logMessage));
    }

    private IEnumerator RemoveLogAfterDelay(string logMessage)
    {
        yield return new WaitForSeconds(15);

        _logMessages.Remove(logMessage);
        Refresh();
    }
}

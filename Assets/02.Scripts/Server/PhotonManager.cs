using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Photon API를 사용하기 위한 네임스페이스
using Photon.Pun;
using Photon.Realtime;
using Random = UnityEngine.Random;
using TMPro;
using UnityEngine.UI;

// 역할: 포톤 서버 연결 관리자

public class PhotonManager : MonoBehaviourPunCallbacks // PUN의 다양한 서버 이벤트(콜백 함수)를 받는다.
{
    public TMP_InputField NicknameInput;
    public Button joinButton;
    private void Start()
    {
      
        PhotonNetwork.GameVersion = "0.0.1";
        // <전체를 뒤엎을 변화>, <기능 수정, 추가>, <버그, 내부적 코드 수정>

        // 2. 닉네임을 설정한다.
        // 3. 씬을 설정한다.
        // 4. 연결한다. 
        PhotonNetwork.ConnectUsingSettings();

 

    }



    public override void OnConnectedToMaster()
    {
        joinButton.interactable = true;

        PhotonNetwork.JoinLobby(TypedLobby.Default);
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        // 룸 접속 버튼을 비활성화
        joinButton.interactable = false;
        // 접속 정보 표시

        // 마스터 서버로의 재접속 시도
        PhotonNetwork.ConnectUsingSettings();
    }
    public void Connect()
    {
        PhotonNetwork.LocalPlayer.NickName = NicknameInput.text;
        joinButton.interactable = false;
        if (PhotonNetwork.IsConnected)
        {
            RoomOptions roomOptions = new RoomOptions { MaxPlayers = 20 };
            PhotonNetwork.JoinOrCreateRoom("Server1", roomOptions, TypedLobby.Default);
        }
        else
        {
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Scene1");
    }
}

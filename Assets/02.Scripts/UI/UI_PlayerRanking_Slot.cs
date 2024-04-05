using System;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UI_PlayerRankingSlot : MonoBehaviour
{
    public TextMeshProUGUI RankingTextUI;
    public TextMeshProUGUI NicknameTextUI;
    public TextMeshProUGUI KillCountTextUI;
    public TextMeshProUGUI ScoreTextUI;

    public void Set(Player player)
    {
        RankingTextUI.text = "-";
        NicknameTextUI.text = player.NickName;
        if (player.CustomProperties.ContainsKey("KillCount"))
        {
            KillCountTextUI.text = $"{player.CustomProperties["KillCount"]}";
            ScoreTextUI.text = $"{player.CustomProperties["Score"]}";
        }
        else
        {
            KillCountTextUI.text = "0";
            ScoreTextUI.text = "0";
        }
    }
}
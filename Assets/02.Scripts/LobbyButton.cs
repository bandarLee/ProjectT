using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
public class LobbyButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TextMeshProUGUI StartButton;
    private Color baseColor = new Color(220f / 255f, 222f / 255f, 137f / 255f); // #9C9F2F 색상

    private Color changeColor = new Color(1f, 1f, 0); // #9C9F2F 색상
    void Start()
    {
        StartButton.color = baseColor;

    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        StartButton.color = changeColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        StartButton.color = baseColor;

    }

}

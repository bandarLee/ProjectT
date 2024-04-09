using Photon.Pun;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Scene1Praying : MonoBehaviour
{
    public GameObject PrayingNotificeObject;

    public GameObject PrayingSliderObject;
    public Slider PrayingSlider;
    private bool isPlayerInside = false;
    private Tween prayingTween;

    private void Start()
    {
        PrayingSliderObject.SetActive(false);
        PrayingNotificeObject.SetActive(false);

    }

    private void Update()
    {
        if (isPlayerInside && Input.GetKeyDown(KeyCode.E))
        {
            PrayingNotificeObject.SetActive(false);
            if (!PrayingSliderObject.activeSelf)
            {
                PrayingSliderObject.SetActive(true);
            }

            if (prayingTween == null || !prayingTween.IsActive())
            {
                prayingTween = DOTween.To(() => PrayingSlider.value, x => PrayingSlider.value = x, 1, 3)
                    .SetEase(Ease.Linear)
                    .OnComplete(() =>
                    {
                        Debug.Log("기도 완료");
                    });
            }
        }
        else if (Input.GetKeyUp(KeyCode.E))
        {
            ResetPraying();
        }
    }

    private void ResetPraying()
    {
        if (prayingTween != null && prayingTween.IsActive())
        {
            prayingTween.Kill();
        }
        PrayingSlider.value = 0;
        if (PrayingSliderObject.activeSelf)
        {
            PrayingSliderObject.SetActive(false);
        }
        if (isPlayerInside && !PrayingNotificeObject.activeSelf)
        {
            PrayingNotificeObject.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        PhotonView photonView = other.gameObject.GetComponent<PhotonView>();
        if (photonView != null && photonView.IsMine)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                PrayingNotificeObject.SetActive(true);

                isPlayerInside = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        PhotonView photonView = other.gameObject.GetComponent<PhotonView>();
        if (photonView != null && photonView.IsMine)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                PrayingSliderObject.SetActive(false);
                PrayingSlider.DOKill(); 
                PrayingSlider.value = 0; 
                isPlayerInside = false;
            }
        }
    }
}

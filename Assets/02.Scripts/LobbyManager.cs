using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class LobbyManager : MonoBehaviour
{
    public GameObject UI;
    public CanvasGroup UICanvasGroup;

    public GameObject Plasma;
    public GameObject tower;
    public GameObject[] trashs;

    public GameObject towerRuins;
    private float explosionForce = 15000f;
    private float explosionRadius = 15f; 
    void Awake()
    {
        UI.SetActive(false);
        Plasma.SetActive(false);
    }
    void Start()
    {
        StartCoroutine(Sequence());
    }
    private IEnumerator Sequence()
    {
        yield return new WaitForSeconds(5);
        UI.SetActive(true);
        UICanvasGroup.DOFade(1, 10);

        yield return new WaitForSeconds(2.5f);
        Plasma.SetActive(true);

    
        yield return new WaitForSeconds(0.5f);
        ExplodeTowerRuins();
        yield return new WaitForSeconds(0.5f);

        tower.transform.DOMoveY(-200f, 10f).OnComplete(() =>
        {
            tower.SetActive(false);
        });
        yield return new WaitForSeconds(1f);
   

        Plasma.SetActive(false);
        yield return new WaitForSeconds(5f);
        foreach (GameObject trash in trashs) { 
        trash.SetActive(false);
        }

    }
    void ExplodeTowerRuins()
    {
        towerRuins.SetActive(true);
  
        // 타워 잔해에 있는 모든 Rigidbody 컴포넌트에 폭발력을 적용
        foreach (Transform child in tower.transform)
        {
            Rigidbody rb = child.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(explosionForce, tower.transform.position, explosionRadius);
            }
        }

    }


}

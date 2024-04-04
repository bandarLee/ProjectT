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
    public GameObject Trash;


    public GameObject towerRuins;
    private float explosionForce = 1500f;
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

        yield return new WaitForSeconds(1.5f);
        tower.transform.DOMoveY(-500f, 10f).OnComplete(() =>
        {
            tower.SetActive(false);
        });

        Plasma.SetActive(false);
        yield return new WaitForSeconds(1.5f);

   
        Trash.SetActive(false);
        yield return new WaitForSeconds(4f);

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
                rb.AddExplosionForce(explosionForce, towerRuins.transform.position, explosionRadius);
            }
        }
    }


}

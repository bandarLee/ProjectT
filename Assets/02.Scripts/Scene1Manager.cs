using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene1Manager : MonoBehaviour
{
    void Awake()
    {

    }
    private void Start()
    {
        PhotonNetwork.Instantiate(nameof(Character), Vector3.zero, Quaternion.identity);

    }

    void Update()
    {
        
    }
}

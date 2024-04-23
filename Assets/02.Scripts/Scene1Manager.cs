using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene1Manager : MonoBehaviour
{

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(1);  // 1초 정도 대기 후 데이터 로드

        if (PhotonNetwork.IsConnectedAndReady && PhotonNetwork.InRoom)
        {
            string characterClass = (string)PhotonNetwork.LocalPlayer.CustomProperties["CharacterClass"];
            if (characterClass != null)
            {
                InstantiateCharacterBasedOnClass(characterClass);
            }

        }
    }

    void InstantiateCharacterBasedOnClass(string className)
    {
        GameObject prefab = Resources.Load<GameObject>(className);

        PhotonNetwork.Instantiate(prefab.name, Vector3.zero, Quaternion.identity);
        
    }
}

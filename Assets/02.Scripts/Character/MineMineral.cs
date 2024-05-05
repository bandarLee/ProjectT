using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineMineral : CharacterAbility
{
    public bool IsMining = false;
    public GameObject SmallRockPrefab;
    public int numberOfPieces = 10;
    public GameObject PickAxe;
    private void OnTriggerStay(Collider other)
    {
        if (Owner.State == State.Death || !Owner.PhotonView.IsMine)
        {
            return;
        }

        if (other.CompareTag("Mineral"))
        {
            if (Input.GetKey(KeyCode.E) && !IsMining)
            {
                IsMining = true;
                Owner.PhotonView.RPC(nameof(MineralMotion), RpcTarget.All, 9);
                Invoke(nameof(ResetMining), 2.0f);
                Invoke(nameof(CreateRockFragments), 0.5f);
                Invoke(nameof(CreateRockFragments), 1f);


            }
        }
    }
    private void CreateRockFragments()
    {
        for (int i = 0; i < numberOfPieces; i++)
        {
            GameObject fragment = Instantiate(SmallRockPrefab, PickAxe.transform.position, Random.rotation);
            Rigidbody rb = fragment.GetComponent<Rigidbody>();
            Vector3 forceDirection = Random.insideUnitSphere * Random.Range(5f, 10f);
            rb.AddForce(forceDirection, ForceMode.Impulse);
        }
    }

    private void ResetMining()
    {
        IsMining = false;
    }

    [PunRPC]
    private void MineralMotion(int number)
    {
        GetComponent<Animator>().SetTrigger($"Motion{number}");
    }
}

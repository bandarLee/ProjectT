using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Goblin : MonoBehaviour
{
    public Animator GoblinAnimator;
    public NavMeshAgent Agent;

    private Character _targetCharacter;
    public Stat Stat;


    public float DetectRange = 30f;
    public float AttackRange = 3f;
    public float EscapeDistance = 10f;

    public float patrolRadius = 20f; 
    public float patrolInterval = 5f; 

    private float patrolTimer;
    public enum GoblinState
    {
        Patrol,
        Escape,
        Attack,
        Damaged,
        Death
    }

    private GoblinState _state = GoblinState.Patrol;
    void Start()
    {
        Agent.speed = Stat.MoveSpeed;

    }

    private void Update()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            return;
        }
        Character nearestCharacter = null;
        float nearestDistance = Mathf.Infinity;

        foreach (var player in FindObjectsOfType<Character>())
        {

            float distance = Vector3.Distance(transform.position, player.transform.position);
            if (distance < nearestDistance)
            {
                nearestCharacter = player;
                nearestDistance = distance;
            }
        }

        if (nearestCharacter != null)
        {
            _targetCharacter = nearestCharacter;

            if (nearestDistance <= AttackRange)
            {
                _state = GoblinState.Attack;
            }
            else if (nearestDistance <= DetectRange)
            {
                _state = GoblinState.Escape;
            }
            else
            {
                _state = GoblinState.Patrol;
            }

            GoblinAct();
        }
        else 
        {
            _state = GoblinState.Patrol;
        }
        patrolTimer += Time.deltaTime;

 


    }
   


public void GoblinAct()
    {
        switch (_state)
        {

            case GoblinState.Patrol:
                Agent.speed = Stat.MoveSpeed;
                GoblinAnimator.Play("Move");
                GoblinAnimator.SetBool("IsAttack", false);


                break;

            case GoblinState.Escape:
                Vector3 dirToPlayer = transform.position - _targetCharacter.transform.position;
                Vector3 newPos = transform.position + dirToPlayer.normalized * EscapeDistance;
                Agent.SetDestination(newPos);
                Agent.speed = Stat.RunSpeed;
                GoblinAnimator.SetBool("IsAttack",false);

                break;

            case GoblinState.Attack:
                Agent.speed = 0;
                GoblinAnimator.SetBool("IsAttack", true);

                Vector3 targetDirection = _targetCharacter.transform.position - transform.position;
                targetDirection.y = 0;
                Quaternion lookRotation = Quaternion.LookRotation(targetDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f); // 부드럽게 회전
                break;

            case GoblinState.Damaged:
                GoblinAnimator.Play("Damage");

                break;
            case GoblinState.Death:
                GoblinAnimator.Play("Die");

                break;
            default:
                break;
        }
    }

    void PatrolBehavior()
    {
        
        if (patrolTimer >= patrolInterval)
        {
            patrolTimer = 0f;
            MoveToRandomPosition();
        }
    }
    void MoveToRandomPosition()
    {
        Vector3 randomDirection = Random.insideUnitSphere * patrolRadius;
        randomDirection += transform.position;
        NavMeshHit hit;

        NavMesh.SamplePosition(randomDirection, out hit, patrolRadius, NavMesh.AllAreas);

        // 찾은 위치로 이동
        Agent.SetDestination(hit.position);
    }
}

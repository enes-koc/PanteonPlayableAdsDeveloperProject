using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class OpponentCharacterController : MonoBehaviour
{
    [SerializeField] List<Transform> destionationList;

    Transform destinationPoint;
    NavMeshAgent navMeshAgent;
    Rigidbody rb;
    Animator playerAnimator;

    public bool navMeshAgentEnable { get; set; }

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        playerAnimator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        destinationPoint = destionationList[Random.Range(0, destionationList.Count)];
    }

    private void Update()
    {

        navMeshAgent.destination = destinationPoint.position;
        rb.angularVelocity = Vector3.zero;
        if (navMeshAgent.acceleration > 0.01f)
        {
            playerAnimator.SetFloat("playerSpeed", navMeshAgent.acceleration);
        }

    }
}

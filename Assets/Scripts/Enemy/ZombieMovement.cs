using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieMovement : MonoBehaviour
{
    private NavMeshAgent agent;
    [SerializeField] private float guardRange;
    private Transform playerTransform;
    // Start is called before the first frame update
    void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        GameObject player = GameObject.Find("Player");
        playerTransform = player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        //TODO: Add Attack Logic
        if (playerTransform != null && Vector3.Distance(playerTransform.position,transform.position)<guardRange) 
        { 
        agent.destination = playerTransform.position;
        }
    }

}

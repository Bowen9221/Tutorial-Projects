using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshController : MonoBehaviour
{
    public GameObject target;
    public GameObject target2;
    public GameObject target3;
    private NavMeshAgent agent;

    RotateTowards lookDir;

    private Animator anim;
    bool isWalking = true;

    public float facing;



    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(isWalking);
        if (isWalking)
        {
            RotateTowardsTarget();
            if (target.name == "Dragon Left")
            {
                agent.destination = target2.transform.position;
            }

            if (target.name == "Dragon Right")
            {
                    agent.destination = target3.transform.position;
            }
            
                agent.destination = target.transform.position;
        }
        else
        {
            agent.destination = transform.position;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Dragon")
        {
            Debug.Log("Attacking");
            isWalking = false;
            anim.SetTrigger("ATTACK");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Dragon")
        {
            anim.SetTrigger("WALK");
            Debug.Log("Exit");
            isWalking = true;
        }
    }
    void RotateTowardsTarget()
    {
        float stepSize = facing * Time.deltaTime;

        Vector3 targetDir = target.transform.position - transform.position;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, stepSize, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDir);
    }

}

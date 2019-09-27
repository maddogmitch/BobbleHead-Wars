using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Alien : MonoBehaviour
{
    //where the alien will go
    public Transform target;
    //add NavMeshAgent variable
    private NavMeshAgent agent;
    //this is the time in milliseconds to update the alien
    public float navigationUpdate;
    //this tracks how much time has passed since the previous update
    private float navigationTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        //This is to get the NavMesh code
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            //sets the agents destination as the targets positions.
            //agent.destination = target.position;

            //checks if a certain amount of time has passed than updates the path
            navigationTime += Time.deltaTime;
            if(navigationTime > navigationUpdate)
            {
                agent.destination = target.position;
                navigationTime = 0;
            }
        }
       
    }

    void OnTriggerEnter(Collider other)
    {
        SoundManager.Instance.PlayOneShot(SoundManager.Instance.alienDeath);
        Destroy(gameObject);
    }
}

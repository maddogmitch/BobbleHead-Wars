using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

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
    //Called each time to an alien
    public UnityEvent OnDestroy;
    public Rigidbody head;
    public bool isAlive = true;
    private DeathParticles deathParticles;

    //Destroys the alien
    public void Die()
    {
        isAlive = false;
        head.GetComponent<Animator>().enabled = false;
        head.isKinematic = false;
        head.useGravity = true;
        head.GetComponent<SphereCollider>().enabled = true;
        head.gameObject.transform.parent = null;
        head.velocity = new Vector3(0, 26.0f, 3.0f);
        OnDestroy.Invoke();
        OnDestroy.RemoveAllListeners();
        SoundManager.Instance.PlayOneShot(SoundManager.Instance.alienDeath);
        head.GetComponent<SelfDestruct>().Initiate();

        if(deathParticles)
        {
            deathParticles.transform.parent = null;
            deathParticles.Activate();
        }

        Destroy(gameObject);
    }

    public DeathParticles GetDeathParticles()
    {
        if(deathParticles == null)
        {
            deathParticles = GetComponentInChildren<DeathParticles>();
        }
        return deathParticles;
    }

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
            if (isAlive)
            {
                navigationTime += Time.deltaTime;
                if (navigationTime > navigationUpdate)
                {
                    agent.destination = target.position;
                    navigationTime = 0;
                }
            }
        }
       
    }

    void OnTriggerEnter(Collider other)
    {
        if (isAlive)
        {
            Die();
            SoundManager.Instance.PlayOneShot(SoundManager.Instance.alienDeath);
        }
    }
}

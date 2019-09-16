using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    //Makes the object go invisible when they go off screen
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    //destroys the objects when it collides with something
    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

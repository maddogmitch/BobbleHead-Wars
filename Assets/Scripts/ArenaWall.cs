using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaWall : MonoBehaviour
{

    private Animator arenaAnimator;

    // Start is called before the first frame update
    void Start()
    {
        //Gets the GameObject
        GameObject arena = transform.parent.gameObject;
        //Gets the animation
        arenaAnimator = arena.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Triggers the bool when player enters
    void OnTriggerEnter(Collider other)
    {
        arenaAnimator.SetBool("IsLowered", true);
    }
    //Disables the bool when player exits
    void OnTriggerExit(Collider other)
    {
        arenaAnimator.SetBool("IsLowered", false);
    }


}

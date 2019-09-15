using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //move speed of the character
    public float moveSpeed = 50.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Allows the player model to move
        Vector3 pos = transform.position;

        //Gets the values from the X and Z axis, when a key is pressed it returns ethier 1 or -1 allowing the character to move ethier up on its axis or down
        //Its than multiplied by the movespeed and added to the current X or Z position.
        //Time.deltatime just ensures the movement is in sync with the frame rate.
        pos.x += moveSpeed * Input.GetAxis("Horizontal") * Time.deltaTime;
        pos.z += moveSpeed * Input.GetAxis("Vertical") * Time.deltaTime;

        //Updates the player model to its new position
        transform.position = pos;

    }
}

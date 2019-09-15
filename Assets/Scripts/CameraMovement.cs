using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    //follows the target
    public GameObject followTarget;
    //the speed it will move at while following the target
    public float movespeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Checks if there is a target availible to follow
        if(followTarget != null)
        {
                                //Vector3.lerp is called to calculate the required position
                                //Lerp takes start position, end position and a value between 0 and 1
            transform.position = Vector3.Lerp(transform.position,
                followTarget.transform.position, Time.deltaTime * movespeed);
        }

    }
}

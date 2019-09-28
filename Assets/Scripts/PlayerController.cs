using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //move speed of the character
    public float moveSpeed = 50.0f;
    //An array of force for the camera
    public float[] hitforce;
    //grace peroid between hits
    public float timeBetweenHits = 2.5f;
    //indicates the hero was hit
    private bool isHit = false;
    //tracks amount of time in grace peroid
    private float timeSinceHit = 0;
    //refers to number of time it is hit
    private int hitNumber = -1;

    public Rigidbody marineBody;
    private bool isDead = false;

    public Rigidbody head;

    public Animator bodyAnimator;

    //Lets us decide what layers to hit
    public LayerMask layerMask;
    //this dictates where the marine will look
    private Vector3 currentLookTarget = Vector3.zero;
    
    //creates a instacne to store character controller
    private CharacterController characterController;

    // Start is called before the first frame update
    void Start()
    {
        // gets a reference to current component passed into the script
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        //Allows the player model to move
        //Vector3 pos = transform.position;

        //Gets the values from the X and Z axis, when a key is pressed it returns ethier 1 or -1 allowing the character to move ethier up on its axis or down
        //Its than multiplied by the movespeed and added to the current X or Z position.
        //Time.deltatime just ensures the movement is in sync with the frame rate.
        //pos.x += moveSpeed * Input.GetAxis("Horizontal") * Time.deltaTime;
        //pos.z += moveSpeed * Input.GetAxis("Vertical") * Time.deltaTime;

        //Updates the player model to its new position
        //transform.position = pos;
        
        //Creates a new Vector3 to store the movement, than it calls Simplemove and moves the character in a given direction
        Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"),
            0, Input.GetAxis("Vertical"));
        characterController.SimpleMove(moveDirection * moveSpeed);

       if(isHit)
        {
            timeSinceHit += Time.deltaTime;
            if(timeSinceHit > timeBetweenHits)
            {
                isHit = false;
                timeSinceHit = 0;
            }
        }
    }

    //Calls updates at a fixed interval
    void FixedUpdate()
    {
        //All this code moves the head of the marine including the if statement
        Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"),
            0, Input.GetAxis("Vertical"));

        if(moveDirection == Vector3.zero)
        {
            bodyAnimator.SetBool("IsMoving", false);
        }
        else
        {
            head.AddForce(transform.right * 150, ForceMode.Acceleration);
            bodyAnimator.SetBool("IsMoving", true);
        }
        
        //creates an empty raycast
        RaycastHit hit;
        //cast the ray to the mouse position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //draws the ray in scene view while playing the game
        Debug.DrawRay(ray.origin, ray.direction * 1000, Color.green);
        //This  cast the ray, and indicates the length it is as well as what im trying to hit and tells the physics engine to not activate triggers
        if (Physics.Raycast(ray, out hit, 1000, layerMask,
            QueryTriggerInteraction.Ignore))
        {
            //comprises the coordinates of the raycast hit and updates it with the mouse position
            if(hit.point != currentLookTarget)
            {
                currentLookTarget = hit.point;
            }
        }
        //Gets target position
        Vector3 targetPosition = new Vector3(hit.point.x,
            transform.position.y, hit.point.z);
        //Calculates the current Quaternion which is used to determine roatation
        Quaternion rotation = Quaternion.LookRotation(targetPosition -
            transform.position);
        //Does the actuall turn using lerp
        transform.rotation = Quaternion.Lerp(transform.rotation,
            rotation, Time.deltaTime * 10.0f);
    }

    void OnTriggerEnter(Collider other)
    {
        Alien alien = other.gameObject.GetComponent<Alien>();
        if(alien != null)
        {//Checks if colliding object is an alien
            if(!isHit)
            {
                hitNumber += 1;
                CameraShake cameraShake = Camera.main.GetComponent<CameraShake>();
                if(hitNumber < hitforce.Length) //if hit number is less than force than hero is still alive
                {
                    cameraShake.intensity = hitforce[hitNumber];
                    cameraShake.Shake();
                }
                else
                {
                    Die();
                }
                isHit = true;//sets is hit to true to play the hurt sound
                SoundManager.Instance.PlayOneShot(SoundManager.Instance.hurt);
            }
            alien.Die();
        }
    }

    public void Die()
    {
        bodyAnimator.SetBool("IsMoving", false);
        marineBody.transform.parent = null;
        marineBody.isKinematic = false;
        marineBody.useGravity = true;
        marineBody.gameObject.GetComponent<CapsuleCollider>().enabled = true;
        marineBody.gameObject.GetComponent<Gun>().enabled = false;
        Destroy(head.gameObject.GetComponent<HingeJoint>());
        head.transform.parent = null;
        head.useGravity = true;
        SoundManager.Instance.PlayOneShot(SoundManager.Instance.marineDeath);
        Destroy(gameObject);
    }
}

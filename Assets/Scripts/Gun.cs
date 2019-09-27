using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    //where our bullet prefab will go
    public GameObject bulletPrefab;
    //will be set to the position of the marine's gun
    public Transform launchPosition;
    private AudioSource audioSource;
    public bool isUpgraded;
    public float upgradeTime = 10.0f;
    private float currentTime;
  





    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //checks if the left mouse button is down
        if(Input.GetMouseButtonDown(0))
        {
            //checks if fire bullet is being invoked
            if(!IsInvoking("fireBullet"))
            {
                //repeatedly calls a method till Cancel invoke is called
                InvokeRepeating("fireBullet", 0f, 0.1f);
            }
        }

        //Ensures the guns stops firing once the left mouse button is up
        if(Input.GetMouseButtonUp(0))
        {
            CancelInvoke("fireBullet");
        }

        currentTime += Time.deltaTime;
        if(currentTime > upgradeTime && isUpgraded == true)
        {
            isUpgraded = false;
        }

    }

    private Rigidbody createBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab) as GameObject;
        bullet.transform.position = launchPosition.position;
        return bullet.GetComponent<Rigidbody>();
    }

    public void UpgradeGun()
    {
        isUpgraded = true;
        currentTime = 0;
    }
    void fireBullet()
    {
        //creates a bullet based on the bullet prefab
        //GameObject bullet = Instantiate(bulletPrefab) as GameObject;
        //sets the bullets position as the launchers position
        //bullet.transform.position = launchPosition.position;
        //sets the speed of the bullet as well as makes sure it fires in the direction the player is facing
        //bullet.GetComponent<Rigidbody>().velocity = transform.parent.forward * 100;
        //allows the same sound effect to play multiple times
        //audioSource.PlayOneShot(SoundManager.Instance.gunFire);

        Rigidbody bullet = createBullet();
        bullet.velocity = transform.parent.forward * 100;
        //upgrades the bullets
        if(isUpgraded)
        {
            Rigidbody bullet2 = createBullet();
            bullet2.velocity =
                (transform.right + transform.forward / 0.5f) * 100;
            Rigidbody bullet3 = createBullet();
            bullet3.velocity =
                ((transform.right * -1) + transform.forward / 0.5f) * 100;


        }
        
        if(isUpgraded)
        {
            audioSource.PlayOneShot(SoundManager.Instance.upgradedGunFire);
        }
        else
        {
            audioSource.PlayOneShot(SoundManager.Instance.gunFire);
        }
    }

}

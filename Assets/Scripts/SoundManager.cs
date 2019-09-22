using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    //Stores a static reference
    public static SoundManager Instance = null;
    private AudioSource soundEffectAudio;
    public AudioClip gunFire;
    public AudioClip upgradedGunFire;
    public AudioClip hurt;
    public AudioClip alienDeath;
    public AudioClip marineDeath;
    public AudioClip victory;
    public AudioClip elevatorArrived;
    public AudioClip powerUpPickup;
    public AudioClip powerUpAppear;

    // Start is called before the first frame update
    void Start()
    {
        //Ensures that there is only one copy
        if(Instance = null)
        {
            Instance = this;
        }else if (Instance != this)
        {
            Destroy(gameObject);
        }

        //Ensures that the Audio Source is the empty one
        AudioSource[] sources = GetComponents<AudioSource>();
        foreach (AudioSource source in sources)
        {
            if(source.clip == null)
            {
                soundEffectAudio = source;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

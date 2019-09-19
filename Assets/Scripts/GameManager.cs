using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // this is the player
    public GameObject player;
    //this is spawn points
    public GameObject[] spawnPoints;
    //this is the alien
    public GameObject alien;
    //Max for aliens allowed on the screen
    public int maxAliensOnScreen;
    //Will track the total number of aliens currently displayed
    private int aliensOnScreen = 0;
    //total number of Aliens
    public int totalAliens;
    //How many will spawn per peroid
    public int aliensPerSpawn;
    //Both control the rate they spawn
    public float minSpawnTime;
    public float maxSpawnTime;
    //tracks time between spawns
    private float generatedSpawnTime = 0;
    //track the millisecond since the last spawn
    private float currentSpawnTime = 0;

    public 



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //accumulates time that passed between each frame
        currentSpawnTime += Time.deltaTime;

        if(currentSpawnTime > generatedSpawnTime)
        {
            currentSpawnTime = 0;

            generatedSpawnTime = Random.Range(minSpawnTime, maxSpawnTime);

            if(aliensPerSpawn > 0 && aliensOnScreen < totalAliens)
            {
                List<int> previousSpawnLocations = new List<int>();

                if(aliensPerSpawn > spawnPoints.Length)
                {
                    aliensPerSpawn = spawnPoints.Length - 1;
                }
                aliensPerSpawn = (aliensPerSpawn > totalAliens) ?
                      aliensPerSpawn - totalAliens : aliensPerSpawn;
                //iterates once for each spawned alien
                for (int i = 0; i < aliensPerSpawn; i++)
                {
                    //checks if aliens on screen is less than the max
                    if (aliensOnScreen < maxAliensOnScreen)
                    {
                        aliensOnScreen += 1;
                        //generated spawn point number
                        int spawnPoint = -1;
                        //loop runs until it finds a spawn point
                        while (spawnPoint == -1)
                        {
                            //produces a random number
                            int randomNumber = Random.Range(0, spawnPoints.Length - 1);
                            //checks the array to see if that random number is an active spawn point
                            if (!previousSpawnLocations.Contains(randomNumber))
                            {
                                previousSpawnLocations.Add(randomNumber);
                                spawnPoint = randomNumber;
                            }
                        }
                        //grabs the spawnpoints from the inspector
                        GameObject spawnLocation = spawnPoints[spawnPoint];
                        //creates and instacne of any prefab put into it
                        GameObject newAlien = Instantiate(alien) as GameObject;
                        //positions the alien at the spawn point
                        newAlien.transform.position = spawnLocation.transform.position;
                        //Reference to get the alien script
                        Alien alienScript = newAlien.GetComponent<Alien>();
                        //this sets the target as the players current position
                        alienScript.target = player.transform;

                        Vector3 targetRoatation = new Vector3(player.transform.position.x,
                            newAlien.transform.position.y, player.transform.position.z);
                        newAlien.transform.LookAt(targetRoatation);
                    }
                }
            }
        }
       


    }
}

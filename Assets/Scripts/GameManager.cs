using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject obstacleToSpawn;
    

    private void Awake()
    {
        instance = this;
    }

    //spawning in spikes after player jumps over them
    public void SpawnObject()
    {
        GameObject newObject = Instantiate(obstacleToSpawn, gameObject.transform.position, Quaternion.identity) as GameObject;
        newObject.transform.parent = GameObject.Find("Floor Obstacles").transform;
    }
}

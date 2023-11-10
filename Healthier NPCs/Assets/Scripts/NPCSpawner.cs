using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpawner : MonoBehaviour
{
    public List<GameObject> npcs = new();
    public float randomSpawnTime = 5f;
    private float lastSpawnTime = 0f;

    private void Start()
    {
        SpawnRandomNPC();
        lastSpawnTime = Time.time;
    }

    private void Update()
    {
        if (Time.time - lastSpawnTime > randomSpawnTime)
        {
            lastSpawnTime = Time.time;
            randomSpawnTime = Random.Range(1f, 5f);
            SpawnRandomNPC();
        }
    }

    public void SpawnRandomNPC()
    {
        int randIndex = Random.Range(0, npcs.Count);
        Transform newTransform = Instantiate(npcs[randIndex]).transform;

        newTransform.position += new Vector3(Random.Range(-2f, 2f), 0, 0);
    }
 }

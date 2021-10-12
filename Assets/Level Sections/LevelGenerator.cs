using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public List<GameObject> levelSections;
    private LinkedList<GameObject> spawnedSections = new LinkedList<GameObject>();

    private Transform playerPosition;

    private void Start()
    {
        // spawns 3 level sections
        GameObject newSection = Instantiate(levelSections[Random.Range(0, levelSections.Count)], transform);
        newSection.transform.position = Vector2.zero;
        spawnedSections.AddLast(newSection);
        AddNewSection();
        AddNewSection();

        playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        print(playerPosition);
        if(spawnedSections.Last.Value.transform.localPosition.x - playerPosition.position.x < 10f)
        {
            AddNewSection();
            DestroyOldestSection();
        }
    }

    private void AddNewSection()
    {
        GameObject newSection = Instantiate(levelSections[Random.Range(0, levelSections.Count)], transform);               // spawn a random section from the list
        newSection.transform.localPosition = new Vector2(spawnedSections.Last.Value.transform.localPosition.x + 20, 0);    // change position to 20 units in front of the last one
        spawnedSections.AddLast(newSection);
    }

    private void DestroyOldestSection()
    {
        GameObject oldestSection = spawnedSections.First.Value;
        spawnedSections.RemoveFirst();
        Destroy(oldestSection);
    }
}

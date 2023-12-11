// Importing necessary libraries
using System.Collections;
using UnityEngine;

// Define a class named SpawnManager
public class SpawnManager : MonoBehaviour
{
    // Public variables accessible in the Unity Editor
    public GameObject goblinPrefab; // Prefab of the goblin to be spawned
    public Transform spawnPlane; // Reference to the spawn plane or area
    public int maxGoblins = 5; // Maximum number of goblins that can be spawned
    public float spawnInterval = 10f; // Time interval between goblin spawns
    public float spawnRange = 10f; // Range within which goblins can be spawned

    // Private variables
    private int currentGoblins = 0; // Current number of active goblins

    // Start is called before the first frame update
    private void Start()
    {
        // Start the coroutine for spawning goblins
        StartCoroutine(SpawnGoblinRoutine());
    }

    // Coroutine for spawning goblins
    IEnumerator SpawnGoblinRoutine()
    {
        while (true)
        {
            // Check if the maximum number of goblins hasn't been reached
            if (currentGoblins < maxGoblins)
            {
                // Wait for the specified spawn interval
                yield return new WaitForSeconds(spawnInterval);

                // Generate a random position within the spawn range and offset by spawnPlane position
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnRange, spawnRange), 0, Random.Range(-spawnRange, spawnRange)) + spawnPlane.position;

                // Instantiate a goblin at the calculated position with no rotation
                Instantiate(goblinPrefab, spawnPosition, Quaternion.identity);

                // Increment the current number of goblins
                currentGoblins++;
            }
            else
            {
                yield return null; // Wait without spawning if maxGoblins is reached
            }
        }
    }

    // Function to handle goblin destruction and decrement the currentGoblins count
    public void GoblinDestroyed()
    {
        currentGoblins--;
    }
}

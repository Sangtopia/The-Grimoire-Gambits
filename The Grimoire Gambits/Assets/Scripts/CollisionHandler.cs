using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider belongs to the Player
        if (other.CompareTag("Player"))
        {
            // Check the tag of the other collider and switch scenes accordingly
            if (gameObject.CompareTag("Goblin"))
            {
                SwitchScene("GoblinScene");
            }
            else if (gameObject.CompareTag("Skeleton"))
            {
                SwitchScene("SkeletonScene");
            }
            else if (gameObject.CompareTag("Mushroom"))
            {
                SwitchScene("MushroomScene");
            }
        }
    }

    private void SwitchScene(string sceneName)
    {
        // Load the specified scene
        SceneManager.LoadScene(sceneName);
    }
}

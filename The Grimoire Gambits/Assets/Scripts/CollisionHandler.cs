using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collision involves the Player
        if (collision.gameObject.CompareTag("Player"))
        {
            // Check the tag of the other collider and switch scenes accordingly
            if (collision.gameObject.CompareTag("Goblin"))
            {
                SwitchScene("GoblinScene");
            }
            else if (collision.gameObject.CompareTag("Skeleton"))
            {
                SwitchScene("SkeletonScene");
            }
            else if (collision.gameObject.CompareTag("Mushroom"))
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

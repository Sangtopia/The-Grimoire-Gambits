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
                SwitchScene("Village Battle");
            }
            else if (gameObject.CompareTag("Skeleton"))
            {
                SwitchScene("Dungeon Battle");
            }
            else if (gameObject.CompareTag("Mushroom"))
            {
                SwitchScene("Forest Battle");
            }
        }
    }

    private void SwitchScene(string sceneName)
    {
        // Load the specified scene
        SceneManager.LoadScene(sceneName);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Add this line

public class MainStory : MonoBehaviour
{
    // Start is called before the first frame update
    void OnEnable() // Corrected method name
    {
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }
}

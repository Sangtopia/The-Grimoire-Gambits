using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Add this line

public class LoadForest : MonoBehaviour
{
    // Start is called before the first frame update
    void OnEnable() // Corrected method name
    {
        SceneManager.LoadScene("Withered Forest", LoadSceneMode.Single);
    }
}

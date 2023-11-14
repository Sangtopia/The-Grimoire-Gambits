using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Add this method to handle the "Play" button click
    public void OnPlayButtonClicked()
    {
        SceneManager.LoadScene("First Cut Scene", LoadSceneMode.Single);
    }
}

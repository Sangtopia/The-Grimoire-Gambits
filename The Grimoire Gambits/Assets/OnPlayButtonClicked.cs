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

    // Add this method to handle the "Quit" button click
    public void OnQuitButtonClicked()
    {
        // This will quit the application. Note that this may not work in the Unity Editor.
        // In the Unity Editor, you might need to stop the play mode manually.
        Application.Quit();
    }
}

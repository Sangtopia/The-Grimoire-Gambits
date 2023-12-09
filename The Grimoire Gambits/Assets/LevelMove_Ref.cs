using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMove_Ref : MonoBehaviour
{
    public int sceneBuildIndex;

    private void OnTriggerEnter(Collider other)
    {
        print("Trigger Entered");

        if(other.tag == "Player")
        {
            print("Switching SCene to" + sceneBuildIndex);

            SceneManager.LoadScene(sceneBuildIndex, LoadSceneMode.Single);
        }
    }
}

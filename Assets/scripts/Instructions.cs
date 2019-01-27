using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Instructions : MonoBehaviour
{
    public string gameLevelName = "JonD_Test";

    void Update()
    {
        if (Input.GetMouseButtonUp(0) || Input.GetButtonUp("Submit"))
        {
            SceneManager.LoadScene(gameLevelName);
        }

        if (Input.GetMouseButtonUp(1) || Input.GetButtonUp("Cancel"))
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}

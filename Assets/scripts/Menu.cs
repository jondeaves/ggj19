using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetMouseButtonUp(0) || Input.GetButtonUp("Submit"))
        {
            SceneManager.LoadScene("Instructions");
        }

        if (Input.GetMouseButtonUp(1) || Input.GetButtonUp("Cancel"))
        {
            Application.Quit();
        }
    }
}

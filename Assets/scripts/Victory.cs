using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Victory : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponentInChildren<Text>().text = "Such a Hackin Good Boy, you've kept the house safe from thiefs.";
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0) || Input.GetButtonUp("Submit"))
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}

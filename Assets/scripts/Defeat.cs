using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Defeat : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponentInChildren<Text>().text = "Bad boy! Someone has gotten away with " + GameData.valueOfStolenGoods + " coins worth of our family memories!";
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0) || Input.GetButtonUp("Submit"))
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}

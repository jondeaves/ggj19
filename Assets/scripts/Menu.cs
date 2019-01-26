using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public Button m_SubmitBtn;
    public Button m_QuitBtn;

    // Start is called before the first frame update
    void Start()
    {
        m_SubmitBtn.onClick.AddListener(OnSubmit);
        m_QuitBtn.onClick.AddListener(OnQuit);
    }

    private void OnSubmit()
    {
        Debug.Log("Clicked");
        SceneManager.LoadScene("Instructions");
    }

    private void OnQuit()
    {
        Application.Quit();
    }
}

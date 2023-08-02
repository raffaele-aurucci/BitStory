using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneController : MonoBehaviour
{
    public static ChangeSceneController current { get; private set; }

    public string tagCharacter { get; set; }

    private void Awake()
    {
        if (current == null)
            current = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        // aggiungere funzione per il caricamento dei collezionabili all'interno di World
    }

    // Update is called once per frame
    void Update()
    {
        Quit();
    }

    void Quit()
    {
        // da aggingere la scena menu
        if (SceneManager.GetActiveScene().name == "World")
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                PlayerPrefs.Save();
                SceneManager.LoadScene("Menu");
            }
        }
    }

    public void LoadScene()
    {
        if (SceneManager.GetActiveScene().name == "World")
        {
            if (tagCharacter == "Ada")
            {
                PlayerPrefs.Save();
                SceneManager.LoadScene("AdaGame");
            }
        }
        else if (SceneManager.GetActiveScene().name == "Menu")
        {
            SceneManager.LoadScene("World");
        }
        else if (SceneManager.GetActiveScene().name == "AdaGame")
        {
            PlayerPrefs.Save();
            SceneManager.LoadScene("World");
        }
    }
}

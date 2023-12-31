using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneController : MonoBehaviour
{
    #region fields
    
    public static ChangeSceneController current { get; private set; }

    public string tagCharacter { get; set; }

    #endregion
    
    private void Awake()
    {
        if (current == null)
            current = this;
    }
    
    void Start()
    {
        // metodo per il caricamento delle note raccolte/non raccolte all'interno di World
        LoadNotesInWorld();
    }
    
    void Update()
    {
        Quit();
    }

    void Quit()
    {
        if (SceneManager.GetActiveScene().name == "World")
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                PlayerPrefs.Save();
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
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
                AudioManager.current.PlayButtonSound();
                AudioManager.current.PauseMainMusic();
                AudioManager.current.PlayMusicAdaGame();
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
            Time.timeScale = 1;
            AudioManager.current.PlayButtonSound();
            AudioManager.current.StopGameOverSound();
            AudioManager.current.StopWinGameSound();
            AudioManager.current.StopMusicAdaGame();
            AudioManager.current.ResumeMainMusic();
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            SceneManager.LoadScene("World");
        }
    }

    void LoadNotesInWorld()
    {
        if (SceneManager.GetActiveScene().name == "World" || SceneManager.GetActiveScene().name == "Menu")
        {
            if(PlayerPrefs.HasKey("Algorithm") && PlayerPrefs.GetInt("Algorithm") == 0)
                Destroy(GameObject.FindWithTag("Algorithm"));
            
            if(PlayerPrefs.HasKey("Hardware") && PlayerPrefs.GetInt("Hardware") == 0)
                Destroy(GameObject.FindWithTag("Hardware"));
            
            if(PlayerPrefs.HasKey("Software") && PlayerPrefs.GetInt("Software") == 0)
                Destroy(GameObject.FindWithTag("Software"));
            
            if(PlayerPrefs.HasKey("Malware") && PlayerPrefs.GetInt("Malware") == 0)
                Destroy(GameObject.FindWithTag("Malware"));
            
            if(PlayerPrefs.HasKey("OS") && PlayerPrefs.GetInt("OS") == 0)
                Destroy(GameObject.FindWithTag("OS"));
            
            if(PlayerPrefs.HasKey("C") && PlayerPrefs.GetInt("C") == 0)
                Destroy(GameObject.FindWithTag("C"));
            
            if(PlayerPrefs.HasKey("Dos") && PlayerPrefs.GetInt("Dos") == 0)
                Destroy(GameObject.FindWithTag("Dos"));
            
            if(PlayerPrefs.HasKey("IA") && PlayerPrefs.GetInt("IA") == 0)
                Destroy(GameObject.FindWithTag("IA"));
            
            if(PlayerPrefs.HasKey("Bit") && PlayerPrefs.GetInt("Bit") == 0)
                Destroy(GameObject.FindWithTag("Bit"));
            
            if(PlayerPrefs.HasKey("Qwerty") && PlayerPrefs.GetInt("Qwerty") == 0)
                Destroy(GameObject.FindWithTag("Qwerty"));
        }
    }
}

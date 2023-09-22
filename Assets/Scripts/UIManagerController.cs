using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class UIManagerController : MonoBehaviour
{
    #region fields
    
    [Header("The text that sign the bits (points)")]
    [SerializeField] 
    private TextMeshProUGUI textPoints;

    [Header("The text that signes the collected notes")]
    [SerializeField] 
    private TextMeshProUGUI textNotes;

    [Header("The text that signes the medals obtained")]
    [SerializeField] 
    private TextMeshProUGUI textMedals;

    [Header("The control info panel in game menu")]
    [SerializeField]
    private GameObject controlInfoPanel;

    [SerializeField] 
    private Texture2D cursor;

    #endregion
    
    void Start()
    {
        Cursor.SetCursor(cursor, Vector2.zero, CursorMode.ForceSoftware);
        
        if (!PlayerPrefs.HasKey("medalsWorld"))
            PlayerPrefs.SetInt("medalsWorld", 0);
        
        if (!PlayerPrefs.HasKey("points"))
            PlayerPrefs.SetInt("points", 0);
        
        if (!PlayerPrefs.HasKey("notes"))
            PlayerPrefs.SetInt("notes", 0);
    }
    
    void Update()
    {
        CountProgressElement();
        QuitGame();
    }

    void CountProgressElement()
    {
        if (SceneManager.GetActiveScene().name == "World" || SceneManager.GetActiveScene().name == "Menu")
        {
            if (PlayerPrefs.HasKey("points"))
            {
                int points = PlayerPrefs.GetInt("points");
                textPoints.text = "Bits: " + points;
            }
            else
            {
                textPoints.text = "Bits: 0";    
            }

            if (PlayerPrefs.HasKey("notes"))
            {
                int notes = PlayerPrefs.GetInt("notes");
                textNotes.text = "Notes: " + notes + "/10";
            }
            else
            {
                textNotes.text = "Notes: 0/10";    
            }

            if (PlayerPrefs.HasKey("medalsWorld"))
            {
                int medals = PlayerPrefs.GetInt("medalsWorld");
                textMedals.text = "Medals: " + medals + "/8";
            }
            else
            {
                textMedals.text = "Medals: 0/8";    
            }
        }
    }

    public void DeleteData()
    {
        if (SceneManager.GetActiveScene().name == "Menu")
        {
            AudioManager.current.PlayButtonSound();
            PlayerPrefs.DeleteAll();
            SceneManager.LoadScene("Menu");
        }
    }

    public void ControlInfoPanelAppear()
    {
        if (SceneManager.GetActiveScene().name == "Menu")
        {
            AudioManager.current.PlayButtonSound();
            if (!controlInfoPanel.activeSelf)   
                controlInfoPanel.SetActive(true);
        }
    }

    public void ControlInfoPanelDisappear()
    {
        if (SceneManager.GetActiveScene().name == "Menu")
        {
            AudioManager.current.PlayButtonSound();
            if (controlInfoPanel.activeSelf)   
                controlInfoPanel.SetActive(false);
        }
    }

    public void NoButtonDialog()
    {
        // Forza l'uscita del cursore dal bottone
        AudioManager.current.PlayButtonSound();
        StartDialogueController.current.EnablePlayerMovementAndDisablePanel();
        EventSystem.current.SetSelectedGameObject(null);
    }

    void QuitGame()
    {
        if (SceneManager.GetActiveScene().name == "Menu" && Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();    
        }
    }
}

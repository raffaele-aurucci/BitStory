using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManagerController : MonoBehaviour
{
    [Header("The text that sign the bits (points)")]
    [SerializeField] 
    private TextMeshProUGUI textPoints;

    [Header("The text that signes the collected notes")]
    [SerializeField] 
    private TextMeshProUGUI textNotes;
    
    // [Header("The text that signes the number of complete task")]
    // [SerializeField] 
    // private TextMeshProUGUI textTasks;
    
    [Header("The text that signes the medals obtained")]
    [SerializeField] 
    private TextMeshProUGUI textMedals;

    [Header("The control info panel in game menu")]
    [SerializeField]
    private GameObject controlInfoPanel;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CountProgressElement();
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
            
            // aggiungere altri controlli per gli altri elementi
        }
    }

    public void DeleteData()
    {
        if (SceneManager.GetActiveScene().name == "Menu")
        {
            PlayerPrefs.DeleteAll();
            SceneManager.LoadScene("Menu");
        }
    }

    public void ControlInfoPanelAppear()
    {
        if (SceneManager.GetActiveScene().name == "Menu")
        {
            if (!controlInfoPanel.activeSelf)   
                controlInfoPanel.SetActive(true);
        }
    }

    public void ControlInfoPanelDisappear()
    {
        if (SceneManager.GetActiveScene().name == "Menu")
        {
            if (controlInfoPanel.activeSelf)   
                controlInfoPanel.SetActive(false);
        }
    }
}

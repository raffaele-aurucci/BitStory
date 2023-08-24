using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class StartDialogueController : MonoBehaviour
{
    #region fields
    
    [SerializeField]
    private GameObject cameraController;
        
    [SerializeField]
    private Animator playerAnimator;
    
    [SerializeField] 
    private float interactionRange;
    
    [Header("Dialgoue panel for the lines of dialogue")]
    [SerializeField]
    private GameObject dialoguePanel;
    
    [Header("Name of character in dialogue")]
    [SerializeField]
    private TextMeshProUGUI textName;
    
    [Header("Button panel for change scene")]
    [SerializeField] 
    private GameObject buttonPanel;
    
    // tiene traccia della presenza del player nell'area di interazione con i personaggi
    private bool isInRange;
    
    // verifica se è stato premuto il tasto per interagire con il personaggio
    private bool isKeyPressed;
    
    private GameObject character; 
    
    [Space(10)]
    [Header("Here the lines of dialogue characters")]
    [SerializeField]
    private string[] linesAda;
    [SerializeField]
    private string[] linesHorst;
    [SerializeField] 
    private string[] linesDennis;
    [SerializeField] 
    private string[] linesAlan;
    [SerializeField] 
    private string[] linesTim;
    [SerializeField] 
    private string[] linesGeorge;
    [SerializeField] 
    private string[] linesClaude;
    [SerializeField]
    private string[] linesJohn;
    
    public static StartDialogueController current { get; private set; }
    
    #endregion
    
    private void Awake()
    {
        if (current == null)
            current = this;
    }

    private void Update()
    {
        // Controlla se il giocatore è nell'area di interazione e preme il tasto E
        if (isInRange && Input.GetKeyDown(KeyCode.E) && !isKeyPressed)
        {
            isKeyPressed = true;
            AudioManager.current.StopStepSound();   // ferma l'esecuzione del suono dei passi
            StartDialogueWithCharacter();
        }
        
        // Controlla se il personaggio con cui interagire è nell'area di interazione
        CheckInteractionRange();
    }

    private void CheckInteractionRange()
    {
        // verifica la presenza di collisioni con altri oggetti nel layer Characters, attraverso una sfera di raggio 
        // interactionRange
        Collider[] characterColliders = Physics.OverlapSphere(transform.position, interactionRange, LayerMask.GetMask("Characters"));

        if (characterColliders.Length > 0)
        {
            // da qui decido come attivare il dialogPanel rispetto al personaggio
            character = characterColliders[0].gameObject;
            isInRange = true;
        }
        else
        {
            isInRange = false;
        }
    }

    private void StartDialogueWithCharacter()
    {
        // Attiva il pannello del dialogo e avvia il dialogo con il personaggio con cui si sta interagendo
        gameObject.GetComponent<PlayerController>().enabled = false;
        cameraController.GetComponent<CameraController>().enabled = false;
        playerAnimator.SetFloat("horizontal", 0);
        playerAnimator.SetFloat("vertical", 0);

        switch (character.tag)
        {
            case "Ada":
                AssignLinesDialogue(linesAda, "Ada Lovelace");
                break;
            case "Horst":
                AssignLinesDialogue(linesHorst, "Horst Feistel");
                break;
            case "Dennis":
                AssignLinesDialogue(linesDennis, "Dennis Ritchie");
                break;
            case "Alan":
                AssignLinesDialogue(linesAlan, "Alan Turing");
                break;
            case "Tim":
                AssignLinesDialogue(linesTim, "Tim Berners-Lee");
                break;
            case "George":
                AssignLinesDialogue(linesGeorge, "George Boole");
                break;
            case "Claude":
                AssignLinesDialogue(linesClaude, "Claude Shannon");
                break;
            case "John":
                AssignLinesDialogue(linesJohn, "John McCarthy");
                break;
        }

    }

    void AssignLinesDialogue(string[] linesCharacter, string textNameCharacter)
    {
        if (linesCharacter != null && linesCharacter.Length > 0)
        {
            dialoguePanel.SetActive(true);
            AudioManager.current.PlayButtonSound();
            DialogueController.current.lines = linesCharacter;
            DialogueController.current.tagCharacter = character.tag;
            textName.text = textNameCharacter;
            DialogueController.current.StartDialogue();
            // tag del personaggio in caso si voglia accedere al corrispettivo minigame
            ChangeSceneController.current.tagCharacter = character.tag;
        }
        else
        {
            EnablePlayerMovementAndDisablePanel();
        }
    }
    
    public void EnablePlayerMovementAndDisablePanel()
    {
        gameObject.GetComponent<PlayerController>().enabled = true;
        cameraController.GetComponent<CameraController>().enabled = true;
        
        if (dialoguePanel.activeSelf)
            dialoguePanel.SetActive(false);
        
        if (buttonPanel.activeSelf)
            buttonPanel.SetActive(false);

        isKeyPressed = false;
    }
}

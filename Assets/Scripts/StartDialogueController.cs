using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class StartDialogueController : MonoBehaviour
{
    [SerializeField]
    private GameObject dialoguePanel;
    [SerializeField]
    private GameObject cameraController;
    [SerializeField]
    private Animator playerAnimator;
    [SerializeField] 
    private float interactionRange;
    
    // tiene traccia della presenza del player nell'area di interazione con i personaggi
    private bool isInRange;
    private GameObject character; 
    
    [SerializeField]
    private string[] linesAda;
    [SerializeField]
    private string[] linesHorst;
    
    // aggiungere altri characters ...
    
    public static StartDialogueController current { get; private set; }
    
    private void Awake()
    {
        if (current == null)
            current = this;
    }

    private void Update()
    {
        // Controlla se il giocatore è nell'area di interazione e preme il tasto desiderato
        if (isInRange && Input.GetKeyDown(KeyCode.E))
        {
            StartDialogWithCharacter();
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

    private void StartDialogWithCharacter()
    {
        // Attiva il pannello del dialogo e avvia il dialogo con il personaggio con cui si sta interagendo
        gameObject.GetComponent<PlayerController>().enabled = false;
        cameraController.GetComponent<CameraController>().enabled = false;
        playerAnimator.SetFloat("horizontal", 0);
        playerAnimator.SetFloat("vertical", 0);
        
        dialoguePanel.SetActive(true);

        switch (character.tag)
        {
            case "Ada":
                DialogueController.current.lines = linesAda;
                break;
            case "Horst":
                DialogueController.current.lines = linesHorst;
                break;
            
            // aggiungere altri case
        }

        DialogueController.current.StartDialogue();
    }

    public void EnablePlayerMovementAndDisablePanel()
    {
        gameObject.GetComponent<PlayerController>().enabled = true;
        cameraController.GetComponent<CameraController>().enabled = true;
        
        dialoguePanel.SetActive(false);
    }
}

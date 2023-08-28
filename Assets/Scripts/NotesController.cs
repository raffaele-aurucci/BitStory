using System.Collections;
using TMPro;
using UnityEngine;

public class NotesController : MonoBehaviour
{
    #region fields
    
    private bool isInRange;
    private bool isKeyPressed;
    private GameObject note; 
    private bool delay;
    
    [SerializeField] 
    private float interactionRange;

    [SerializeField] 
    private GameObject cameraController;

    [SerializeField] 
    private Animator playerAnimator;

    [SerializeField]
    [Header("Notes Panel for appear note")]
    private GameObject notesPanel;
    
    [SerializeField] 
    [Header("The title of note")]
    private TextMeshProUGUI textNameNote;

    [SerializeField] 
    [Header("The text of note")]
    private TextMeshProUGUI textNote;
    
    [Space(10)]
    [Header("Here the text of notes")]
    [SerializeField]
    private string linesAlgorithm;
    [SerializeField]
    private string linesSoftware;
    [SerializeField]
    private string linesHardware;
    [SerializeField]
    private string linesOS;
    [SerializeField]
    private string linesMalware;
    [SerializeField]
    private string linesC;
    [SerializeField]
    private string linesDos;
    [SerializeField]
    private string linesBit;
    [SerializeField]
    private string linesQwerty;
    [SerializeField]
    private string linesIA;
    
    #endregion
    
    void Update()
    {
        // Controlla se il giocatore è nell'area di interazione e preme il tasto C
        if (isInRange && Input.GetKeyDown(KeyCode.C) && !isKeyPressed)
        {
            isKeyPressed = true;
            TakeNote();
            StartCoroutine("DelayedAction");
        }

        // Controlla se la nota con con cui interagire è nell'area di interazione
        CheckInteractionRange();
    }

    void LateUpdate()
    {
        if (delay && Input.GetKeyDown(KeyCode.C))
        {
            EnablePlayerMovementAndDisablePanel();
            StopAllCoroutines();
        }    
    }
    
    // couroutine utilizzata per ritardare l'esecuzione del metodo LateUpdate()
    private IEnumerator DelayedAction()
    {
        if (!delay)
        {
            yield return new WaitForSeconds(1.0f);
            delay = true;
        }
    }
    
    private void CheckInteractionRange()
    {
        // verifica la presenza di collisioni con altri oggetti nel layer Notes, attraverso una sfera di raggio 
        // interactionRange
        Collider[] notesColliders = Physics.OverlapSphere(transform.position, interactionRange, LayerMask.GetMask("Notes"));

        if (notesColliders.Length > 0)
        {
            // da qui decido come attivare il notesPanel rispetto alla nota
            note = notesColliders[0].gameObject;
            isInRange = true;
        }
        else
        {
            isInRange = false;
        }
    }
    
    private void TakeNote()
    {
        // verifico se sono stati già salvati dei punti in precedenza, in alternativa creo la key points
        if (!PlayerPrefs.HasKey("points"))
            PlayerPrefs.SetInt("points", 0);
        
        // verifico se sono stati già salvate delle note in precedenza, in alternativa creo la key notes
        if (!PlayerPrefs.HasKey("notes"))
            PlayerPrefs.SetInt("notes", 0);
        
        int points = PlayerPrefs.GetInt("points");
        points += 10;
        
        int notes = PlayerPrefs.GetInt("notes");
        notes += 1;
            
        PlayerPrefs.SetInt("points", points);
        PlayerPrefs.SetInt("notes", notes);
        
        // necessario per memorizzare lo stato della nota (raccolto/non raccolto)
        PlayerPrefs.SetInt(note.tag, 0);
            
        // disabilito temporaneamente il movimento del personaggio e della camera
        gameObject.GetComponent<PlayerController>().enabled = false;
        cameraController.GetComponent<CameraController>().enabled = false;
        playerAnimator.SetFloat("horizontal", 0);
        playerAnimator.SetFloat("vertical", 0);

        switch (note.tag)
        {
            case "Algorithm":
                WriteNote(linesAlgorithm, "ALGORITMO");
                break;
            case "Hardware":
                WriteNote(linesHardware, "HARDWARE");
                break;
            case "Software":
                WriteNote(linesSoftware, "SOFTWARE");
                break;
            case "OS":
                WriteNote(linesOS, "SISTEMA OPERATIVO (OS)");
                break;
            case "Malware":
                WriteNote(linesMalware, "MALWARE");
                break;
            case "C":
                WriteNote(linesC, "IL LINGUAGGIO C");
                break;
            case "Dos":
                WriteNote(linesDos, "DOS");
                break;
            case "Bit":
                WriteNote(linesBit, "BIT");
                break;
            case "Qwerty":
                WriteNote(linesQwerty, "QWERTY");
                break;
            case "IA":
                WriteNote(linesIA, "ARTIFICIAL INTELLIGENCE");
                break;
        }
        
    }

    private void WriteNote(string linesNote, string nameNote)
    {
        if (linesNote != null && linesNote.Length > 0)
        {
            notesPanel.SetActive(true);
            AudioManager.current.PlayNotesSound();
            textNameNote.text = nameNote;
            textNote.text = linesNote;
        }
        else
        {
            EnablePlayerMovementAndDisablePanel();
        }
    }
    
    private void EnablePlayerMovementAndDisablePanel()
    {
        gameObject.GetComponent<PlayerController>().enabled = true;
        cameraController.GetComponent<CameraController>().enabled = true;
        
        if (notesPanel.activeSelf)
            notesPanel.SetActive(false);
        
        notesPanel.GetComponent<Animator>().Rebind();

        isKeyPressed = false;

        delay = false;

        Destroy(this.note);
    }
}

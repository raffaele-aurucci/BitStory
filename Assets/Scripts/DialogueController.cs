using System.Collections;
using TMPro;
using UnityEngine;

public class DialogueController : MonoBehaviour
{
    #region fields
    
    [Header("Text TMP when the lines of dialogues appear")]
    [SerializeField]
    private TextMeshProUGUI textDialogue;
    
    [Header("Button panel for change scene")]
    [SerializeField] 
    private GameObject buttonPanel;
     
    [Header("Velocity of text, low is better")]
    [SerializeField]
    private float textSpeed;
    
    private int index;
    public string[] lines { get; set; }
    public static DialogueController current { get; private set; }
    
    // Necessario per attivare il buttonPanel al termine del dialogo con determinati personaggi
    public string tagCharacter { get; set; }
    
    #endregion
    
    void Awake()
    {
        if (current == null)
            current = this;
    }
    
    // velocizza il dialogo quando clicco il pulsante E della tastiera, oppure passa alla linea di dialogo successiva
    void Update()
    {
        if (lines != null && lines.Length > 0)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                AudioManager.current.PlayButtonSound();
                
                if (textDialogue.text == lines[index])
                {
                    // necessario per non far scomparire il dialogPanel all'utlima battuta di dialogo, in modo da far apparire
                    // il pannello per scegliere se iniziare o meno il minigioco (al momento solo per Ada)
                    if (!(tagCharacter == "Ada" && index == lines.Length - 1))
                        NextLine();
                }
                else
                {
                    StopAllCoroutines();
                    textDialogue.text = lines[index];
                }
            } // attiva il buttonPanel per Ada
            else if (index == lines.Length - 1 && textDialogue.text == lines[index] && tagCharacter == "Ada")
                buttonPanel.SetActive(true);
        }
        
    }

    public void StartDialogue()
    {
        if (lines != null && lines.Length > 0)
        {
            index = 0;
            textDialogue.text = string.Empty;
            StartCoroutine(WriteLine());
        }
    }

    IEnumerator WriteLine()
    {
        // scrive un carattere alla volta
        foreach (char c in lines[index].ToCharArray())
        {
            textDialogue.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textDialogue.text = string.Empty;
            StartCoroutine(WriteLine());
        }
        else
        {
            StartDialogueController.current.EnablePlayerMovementAndDisablePanel();
        }
    }
}

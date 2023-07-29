using System.Collections;
using TMPro;
using UnityEngine;

public class DialogueController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textDialogue;
    [SerializeField]
    private float textSpeed;

    private int index;
    public string[] lines { get; set; }
    public static DialogueController current { get; private set; }

    void Awake()
    {
        if (current == null)
            current = this;
    }
    
    // velocizza il dialogo quando clicco il pulsante sx del mouse, oppure passa alla linea di dialogo successiva
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && lines != null && lines.Length > 0)
        {
            if (textDialogue.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textDialogue.text = lines[index];
            }
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

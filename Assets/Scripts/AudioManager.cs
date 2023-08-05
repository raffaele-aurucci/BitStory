using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager current { get; set; }
    
    private AudioSource audioSourceMusic;
    private AudioSource audioSourceButton;
    private AudioSource audioSourceStartButton;
    private AudioSource audioSourceNotes;
    private AudioSource audioSourceStep;
    
    [SerializeField]
    private AudioClip audioClipMusic;
    [SerializeField]
    private AudioClip audioClipPressButton;
    [SerializeField]
    private AudioClip audioClipStartButton;
    [SerializeField] 
    private AudioClip audioClipNotes;
    [SerializeField] 
    private AudioClip audioClipStep;
    
    private void Awake()
    {
        if (current == null)
        {
            current = this;
            
            // mantengo il riferimento alla stessa istanza e allo stesso oggetto
            DontDestroyOnLoad(gameObject);
            
            audioSourceMusic = gameObject.AddComponent<AudioSource>();
            audioSourceButton = gameObject.AddComponent<AudioSource>();
            audioSourceStartButton = gameObject.AddComponent<AudioSource>();
            audioSourceNotes = gameObject.AddComponent<AudioSource>();
            
            audioSourceStep = gameObject.AddComponent<AudioSource>();
            ConfigureStepSound();

            if (SceneManager.GetActiveScene().name == "Menu")
            {
                // attivo la musica solo la prima volta, in modo che non si avverta il distacco tra le diverse scene
                PlayMusic();
            }
        }
        else
        {
            // distruggo la nuova istanza che viene creata durante i cambi scena
            Destroy(gameObject);
        }
    }

    private void PlayMusic()
    {
        audioSourceMusic.loop = true;
        audioSourceMusic.clip = audioClipMusic;
        audioSourceMusic.volume = 0.3F;
        audioSourceMusic.Play();
    }

    public void PlayButtonSound()
    {
        audioSourceButton.clip = audioClipPressButton;
        audioSourceButton.volume = 0.5F;
        audioSourceButton.Play();
    }

    public void PlayStartGameSound()
    {
        audioSourceStartButton.clip = audioClipStartButton;
        audioSourceStartButton.volume = 0.3F;
        audioSourceStartButton.Play();
    }

    public void PauseMusic()
    {
        audioSourceMusic.Pause();
    }

    public void ResumeMusic()
    {
        audioSourceMusic.Play();
    }

    public void PlayNotesSound()
    {
        audioSourceNotes.clip = audioClipNotes;
        audioSourceNotes.volume = 0.5F;
        audioSourceNotes.Play();    
    }

    public void PlayStepSound()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)
            || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.UpArrow) 
            || Input.GetKey(KeyCode.DownArrow))
            audioSourceStep.enabled = true;
        else
            audioSourceStep.enabled = false;
    }

    private void ConfigureStepSound()
    {
        audioSourceStep.loop = true;
        audioSourceStep.clip = audioClipStep;
        audioSourceStep.pitch = 0.5F;
        audioSourceStep.volume = 0.3F;
        audioSourceStep.enabled = false;    
    }
}

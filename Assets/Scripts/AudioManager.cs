using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager current { get; set; }
    
    private AudioSource audioSourceMusic;
    private AudioSource audioSourceButton;
    private AudioSource audioSourceStartButton;
    
    [SerializeField]
    private AudioClip audioClipMusic;
    [SerializeField]
    private AudioClip audioClipPressButton;
    [SerializeField]
    private AudioClip audioClipStartButton;
    
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

    public void PlayMusic()
    {
        audioSourceMusic.loop = true;
        audioSourceMusic.clip = audioClipMusic;
        audioSourceMusic.volume = 0.5F;
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
        audioSourceStartButton.volume = 0.5F;
        audioSourceStartButton.PlayDelayed(0.3F);
    }

    public void PauseMusic()
    {
        audioSourceMusic.Pause();
    }

    public void ResumeMusic()
    {
        audioSourceMusic.Play();
    }
}

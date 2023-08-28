using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    #region fields
    public static AudioManager current { get; private set; }

    private AudioSource audioSourceMainMusic;
    private AudioSource audioSourceButton;
    private AudioSource audioSourceStartButton;
    private AudioSource audioSourceNotes;
    private AudioSource audioSourceStep;
    private AudioSource audioSourceAdaGameMusic;
    private AudioSource audioSourceGameOver;
    private AudioSource audioSourceWinGame;
    private AudioSource audioSourceBonus;

    [SerializeField] private AudioClip audioClipMainMusic;
    [SerializeField] private AudioClip audioClipPressButton;
    [SerializeField] private AudioClip audioClipStartButton;
    [SerializeField] private AudioClip audioClipNotes;
    [SerializeField] private AudioClip audioClipStep;
    [SerializeField] private AudioClip audioClipAdaGameMusic;
    [SerializeField] private AudioClip audioClipGameOver;
    [SerializeField] private AudioClip audioClipWinGame;
    [SerializeField] private AudioClip audioClipBonus;

    #endregion
    
    private void Awake()
    {
        if (current == null)
        {
            current = this;

            // mantengo il riferimento alla stessa istanza e allo stesso oggetto
            DontDestroyOnLoad(gameObject);

            audioSourceMainMusic = gameObject.AddComponent<AudioSource>();
            audioSourceButton = gameObject.AddComponent<AudioSource>();
            audioSourceStartButton = gameObject.AddComponent<AudioSource>();
            audioSourceNotes = gameObject.AddComponent<AudioSource>();

            audioSourceStep = gameObject.AddComponent<AudioSource>();
            ConfigureStepSound();

            audioSourceAdaGameMusic = gameObject.AddComponent<AudioSource>();
            audioSourceGameOver = gameObject.AddComponent<AudioSource>();
            audioSourceWinGame = gameObject.AddComponent<AudioSource>();
            audioSourceBonus = gameObject.AddComponent<AudioSource>();

            if (SceneManager.GetActiveScene().name == "Menu")
            {
                // attivo la musica solo la prima volta, in modo che non si avverta il distacco tra le diverse scene
                PlayMainMusic();
            }
        }
        else
        {
            // distruggo la nuova istanza che viene creata durante i cambi scena
            Destroy(gameObject);
        }
    }

    private void PlayMainMusic()
    {
        audioSourceMainMusic.loop = true;
        audioSourceMainMusic.clip = audioClipMainMusic;
        audioSourceMainMusic.volume = 0.3F;
        audioSourceMainMusic.Play();
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

    public void PauseMainMusic()
    {
        audioSourceMainMusic.Pause();
    }

    public void ResumeMainMusic()
    {
        audioSourceMainMusic.Play();
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

    public void StopStepSound()
    {
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

    public void PlayMusicAdaGame()
    {
        audioSourceAdaGameMusic.loop = true;
        audioSourceAdaGameMusic.clip = audioClipAdaGameMusic;
        audioSourceAdaGameMusic.volume = 0.3F;
        audioSourceAdaGameMusic.Play();
    }

    public void StopMusicAdaGame()
    {
        audioSourceAdaGameMusic.Stop();
    }

    public void PlayGameOverSound()
    {
        audioSourceGameOver.clip = audioClipGameOver;
        audioSourceGameOver.volume = 0.5F;
        audioSourceGameOver.Play();
    }

    public void StopGameOverSound()
    {
        if (audioSourceGameOver.clip != null)
            audioSourceGameOver.Stop();
    }

    public void PlayWinGameSound()
    {
        audioSourceWinGame.clip = audioClipWinGame;
        audioSourceWinGame.volume = 0.5F;
        audioSourceWinGame.Play();
    }

    public void StopWinGameSound()
    {
        if (audioSourceWinGame.clip != null)
            audioSourceWinGame.Stop();
    }

    public void PlayBonusSound()
    {
        audioSourceBonus.clip = audioClipBonus;
        audioSourceBonus.volume = 0.2F;
        audioSourceBonus.PlayDelayed(0.3F);
    }

}

using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AdaGame
{
    public class UIManagerController : MonoBehaviour
    {
        # region fields 
    
        [SerializeField]
        [Header("The text that signs the flow of time")]
        private TextMeshProUGUI timeText;

        [SerializeField] 
        [Header("The text that signs the steps of sphere")]
        private TextMeshProUGUI stepsText;
    
        [SerializeField] 
        [Header("The text that signs the best score of player")]
        private TextMeshProUGUI highScoreText;

        [SerializeField] 
        [Header("The panel appear when the time is 0 or sphere falls")]
        private GameObject gameOverPanel;

        [SerializeField] 
        [Header("The panel appear when starts the game")]
        private GameObject startPanel;
        
        [SerializeField]
        [Header("The panel appear when finish the game")]
        private GameObject finishPanel;

        [SerializeField]
        [Header("The medal empthy shows in progress panel")]
        private GameObject medalEmpthy;
        
        [SerializeField]
        [Header("The colored medal shows in progress panel")]
        private GameObject medalColored;
        
        [SerializeField] 
        [Header("The text appear when finish game")]
        private TextMeshProUGUI textFinish;

        [SerializeField] 
        [Header("The text appear when finish game and show the collected bits")]
        private TextMeshProUGUI textBits;
        [Space(10)]
        
        [SerializeField] 
        private string linesWinWithMedal;
        [SerializeField] 
        private string linesWithoutMedal;
        [SerializeField] 
        private string linesWinWithMedalObtained;
        
        [Space(10)]
        [SerializeField] 
        private GameObject sphere;

        
        private float currentTime;
        public int steps { get; set; }
        private bool startGame = true;

        public readonly int limitSteps = 25;
        public readonly int minSteps = 16;

        public static UIManagerController current { get; private set; }

        #endregion
    
        private void Awake()
        {
            if (current == null)
                current = this;
        }

        // Start is called before the first frame update
        void Start()
        {
            currentTime = 60F;
            steps = 0;
            
            if (!PlayerPrefs.HasKey("medal"))
                PlayerPrefs.SetInt("medal", 0);
            
            if (!PlayerPrefs.HasKey("highScore"))
                PlayerPrefs.SetInt("highScore", 0);

            if (PlayerPrefs.HasKey("medal") && PlayerPrefs.GetInt("medal") == 1)
            {
                if (medalEmpthy.activeSelf)
                    medalEmpthy.SetActive(false);
                
                medalColored.SetActive(true);
            }

            if (PlayerPrefs.HasKey("highScore"))
                highScoreText.text = "High Score: " + PlayerPrefs.GetInt("highScore");
            
            Time.timeScale = 0;
        }

        // Update is called once per frame
        void Update()
        {
            timeText.text = "Time: " + (int)currentTime;
            stepsText.text = "Steps: " + steps;
    
            // decrementa il tempo poco dopo l'inizio del gioco con startGame
            if (currentTime > 0 && !startGame)
                currentTime -= Time.deltaTime;
        
            // tempo scaduto o limite steps raggiunto
            if ( (currentTime <= 0 && !startGame) || steps == limitSteps)
            {
                sphere.GetComponent<Rigidbody>().velocity = Vector3.zero;
                sphere.GetComponent<MoveSphereController>().enabled = false;

                if(!gameOverPanel.activeSelf)
                    gameOverPanel.SetActive(true);
            
                Time.timeScale = 0;
            }
        }

        // fa partire il gioco
        public void StartGame()
        {
            Time.timeScale = 1;
        
            if(startPanel.activeSelf) 
                startPanel.SetActive(false);

            startGame = false;
        }

        // fa ripartire il gioco
        public void RestartGame()
        {
            PlayerPrefs.Save();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void GameOverPanelAppearNextFallSphere()
        {
            sphere.GetComponent<MoveSphereController>().enabled = false;

            if(!gameOverPanel.activeSelf)
                gameOverPanel.SetActive(true);
        
            Time.timeScale = 0;        
        }

        public void FinishGame() // da completare con i bit ottenuti
        {
            if (steps < limitSteps)
            {
                sphere.GetComponent<Rigidbody>().velocity = Vector3.zero;
                sphere.GetComponent<MoveSphereController>().enabled = false;    
                
                if(!finishPanel.activeSelf)
                    finishPanel.SetActive(true);

                if (steps == minSteps && PlayerPrefs.HasKey("medal") && PlayerPrefs.GetInt("medal") == 0
                    && PlayerPrefs.HasKey("highScore")) 
                {
                    PlayerPrefs.SetInt("medal", 1);
                    PlayerPrefs.SetInt("highScore", steps);
                    textFinish.text = linesWinWithMedal;
                    
                    if (medalEmpthy.activeSelf)
                        medalEmpthy.SetActive(false);
                    
                    medalColored.SetActive(true);
                    highScoreText.text = "High Score: " + PlayerPrefs.GetInt("highScore");
                }

                else if (steps == minSteps && PlayerPrefs.HasKey("medal") && PlayerPrefs.GetInt("medal") == 1
                    && PlayerPrefs.HasKey("highScore"))
                {
                    textFinish.text = linesWinWithMedalObtained;
                    highScoreText.text = "High Score: " + PlayerPrefs.GetInt("highScore");
                }
                // mostro lo stesso messaggio sia nel caso in cui si ha già una medaglia che non
                else if (steps > minSteps && PlayerPrefs.HasKey("highScore"))
                {
                    if (steps < PlayerPrefs.GetInt("highScore") && PlayerPrefs.GetInt("highScore")!=0)
                        PlayerPrefs.SetInt("highScore", steps);
                    else if (PlayerPrefs.GetInt("highScore")==0)
                        PlayerPrefs.SetInt("highScore", steps);
                    
                    textFinish.text = linesWithoutMedal;
                    highScoreText.text = "High Score: " + PlayerPrefs.GetInt("highScore");
                }

                Time.timeScale = 0;  
            }
        }
    }
}

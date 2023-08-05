using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AdaGame
{
    public class GameMangerController : MonoBehaviour
    {
        public static GameMangerController current { get; set; }
        
        private bool startGame = true;
        
        [SerializeField] 
        private GameObject sphere;
        
        public readonly int limitSteps = 25;
        public readonly int minSteps = 16;

        private void Awake()
        {
            if (current == null)
                current = this;
        }

        // Start is called before the first frame update
        void Start()
        {

            if (!PlayerPrefs.HasKey("medal"))
                PlayerPrefs.SetInt("medal", 0);
            
            if (!PlayerPrefs.HasKey("highScore"))
                PlayerPrefs.SetInt("highScore", 0);

            if (PlayerPrefs.HasKey("medal") && PlayerPrefs.GetInt("medal") == 1)
            {
                if (UIManagerController.current.medalEmpthy.activeSelf)
                    UIManagerController.current.medalEmpthy.SetActive(false);
                
                UIManagerController.current.medalColored.SetActive(true);
            }

            if (PlayerPrefs.HasKey("highScore"))
                UIManagerController.current.highScoreText.text = "High Score: " + PlayerPrefs.GetInt("highScore");
            
            Time.timeScale = 0;
        }

        private void Update()
        {
            // consente anche in caso di bonus di non superare i 60s inizialmente definiti
            if (UIManagerController.current.currentTime > 60)
                UIManagerController.current.currentTime = 60F;

            // decrementa il tempo poco dopo l'inizio del gioco con startGame
            if (UIManagerController.current.currentTime > 0 && !startGame)
                UIManagerController.current.currentTime -= Time.deltaTime;
        
            // tempo scaduto o limite steps raggiunto
            if ( (UIManagerController.current.currentTime <= 0 && !startGame) 
                 || UIManagerController.current.steps == limitSteps)
            {
                sphere.GetComponent<Rigidbody>().velocity = Vector3.zero;
                sphere.GetComponent<MoveSphereController>().enabled = false;

                if(!UIManagerController.current.gameOverPanel.activeSelf)
                    UIManagerController.current.gameOverPanel.SetActive(true);
            
                Time.timeScale = 0;
            }
        }
        
        // fa partire il gioco
        public void StartGame()
        {
            Time.timeScale = 1;
        
            if(UIManagerController.current.startPanel.activeSelf) 
                UIManagerController.current.startPanel.SetActive(false);

            startGame = false;
        }
        
        // fa ripartire il gioco
        public void RestartGame()
        {
            PlayerPrefs.Save();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        
        public void FinishGame() // da completare con i bit ottenuti
        {
            if (UIManagerController.current.steps < limitSteps)
            {
                sphere.GetComponent<Rigidbody>().velocity = Vector3.zero;
                sphere.GetComponent<MoveSphereController>().enabled = false;    
                
                if(!UIManagerController.current.finishPanel.activeSelf)
                    UIManagerController.current.finishPanel.SetActive(true);
                
                int bits = UIManagerController.current.bitsBonus;

                // caso in cui viene per la prima volta vinta la medaglia
                if (UIManagerController.current.steps == minSteps && PlayerPrefs.HasKey("medal") && PlayerPrefs.GetInt("medal") == 0
                    && PlayerPrefs.HasKey("highScore"))
                {
                    PlayerPrefs.SetInt("medal", 1);
                    PlayerPrefs.SetInt("highScore", UIManagerController.current.steps);
                    UIManagerController.current.textFinish.text = UIManagerController.current.linesWinWithMedal;

                    if (UIManagerController.current.medalEmpthy.activeSelf)
                        UIManagerController.current.medalEmpthy.SetActive(false);

                    UIManagerController.current.medalColored.SetActive(true);
                    UIManagerController.current.highScoreText.text = "High Score: " + PlayerPrefs.GetInt("highScore");

                    if (PlayerPrefs.HasKey("medalsWorld"))
                        PlayerPrefs.SetInt("medalsWorld", PlayerPrefs.GetInt("medalsWorld")+1);

                    bits += 100;
                }
                
                // caso in cui viene vinta per una seconda volta la medaglia
                else if (UIManagerController.current.steps == minSteps && PlayerPrefs.HasKey("medal") && PlayerPrefs.GetInt("medal") == 1
                         && PlayerPrefs.HasKey("highScore"))
                {
                    UIManagerController.current.textFinish.text = UIManagerController.current.linesWinWithMedalObtained;
                    UIManagerController.current.highScoreText.text = "High Score: " + PlayerPrefs.GetInt("highScore");

                    bits += 50;
                }
                
                // casi in cui si vince senza trovare la soluzione ottima
                else if (UIManagerController.current.steps > minSteps && PlayerPrefs.HasKey("highScore"))
                {
                    if (UIManagerController.current.steps < PlayerPrefs.GetInt("highScore") && PlayerPrefs.GetInt("highScore")!=0)
                        PlayerPrefs.SetInt("highScore", UIManagerController.current.steps);
                    else if (PlayerPrefs.GetInt("highScore")==0)
                        PlayerPrefs.SetInt("highScore", UIManagerController.current.steps);
                    
                    UIManagerController.current.textFinish.text = UIManagerController.current.linesWithoutMedal;
                    UIManagerController.current.highScoreText.text = "High Score: " + PlayerPrefs.GetInt("highScore");
                }

                UIManagerController.current.textBitsFinish.text = "HAI GUADAGNATO " + bits + " BITS";
                
                // salvo i bits ottenuti durante il gioco
                if (!PlayerPrefs.HasKey("points"))
                    PlayerPrefs.SetInt("points", 0);
                
                if (PlayerPrefs.HasKey("points"))
                    PlayerPrefs.SetInt("points", PlayerPrefs.GetInt("points") + bits);

                Time.timeScale = 0;  
            }
        }
        
        public void GameOverNextFallSphere()
        {
            sphere.GetComponent<MoveSphereController>().enabled = false;

            if(!UIManagerController.current.gameOverPanel.activeSelf)
                UIManagerController.current.gameOverPanel.SetActive(true);
        
            Time.timeScale = 0;        
        }
    }
}

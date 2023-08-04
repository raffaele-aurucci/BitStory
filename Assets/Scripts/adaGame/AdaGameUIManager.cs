using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class AdaGameUIManager : MonoBehaviour
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
    
    [Space(10)]
    [SerializeField] 
    private GameObject sphere;
    
    private float currentTime;
    public int steps { get; set; }
    private bool startGame = true;

    public static AdaGameUIManager current { get; private set; }

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
        else if (currentTime <= 0 && !startGame) // tempo scaduto
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GameOverPanelAppearNextFallSphere()
    {
        sphere.GetComponent<MoveSphereController>().enabled = false;

        if(!gameOverPanel.activeSelf)
            gameOverPanel.SetActive(true);
        
        Time.timeScale = 0;        
    }
}

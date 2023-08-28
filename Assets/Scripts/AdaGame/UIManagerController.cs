using TMPro;
using UnityEngine;

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
        public TextMeshProUGUI highScoreText;

        [SerializeField] 
        [Header("The panel appear when the time is 0 or sphere falls")]
        public GameObject gameOverPanel;

        [SerializeField] 
        [Header("The panel appear when starts the game")]
        public GameObject startPanel;
        
        [SerializeField]
        [Header("The panel appear when finish the game")]
        public GameObject finishPanel;

        [SerializeField]
        [Header("The medal empthy shows in progress panel")]
        public GameObject medalEmpthy;
        
        [SerializeField]
        [Header("The colored medal shows in progress panel")]
        public GameObject medalColored;
        
        [SerializeField] 
        [Header("The text appear when finish game")]
        public TextMeshProUGUI textFinish;
        
        [SerializeField] 
        [Header("The text appear when the sphere enter on collision with node")]
        private TextMeshProUGUI textBonus;
        
        [SerializeField] 
        [Header("The text appear when finish game and show the collected bits")]
        public TextMeshProUGUI textBitsFinish;
        [Space(10)]
        
        [SerializeField] 
        public string linesWinWithMedal;
        [SerializeField] 
        public string linesWithoutMedal;
        [SerializeField] 
        public string linesWinWithMedalObtained;
        
        public float currentTime { get; set; }
        public int steps { get; set; }
        public int bitsBonus { get; set; }
        
        public static UIManagerController current { get; private set; }

        #endregion
    
        private void Awake()
        {
            if (current == null)
                current = this;
        }
        
        void Start()
        {
            currentTime = 60F;
            steps = 0;
            bitsBonus = 0;
        }
        
        void Update()
        {
            timeText.text = "Time: " + (int)currentTime;
            stepsText.text = "Steps: " + steps;
        }

        public void TimeBonusAppear(int value)
        {
            if (!textBonus.gameObject.activeSelf)
            {
                textBonus.gameObject.SetActive(true);
                textBonus.text = "Bonus time +" + value;
                Invoke("DisableBonusText", 2F);
            }
                
        }

        public void BitsBonusAppear(int value)
        {
            if (!textBonus.gameObject.activeSelf)
            {
                textBonus.gameObject.SetActive(true);
                textBonus.text = "Bonus bits +" + value;
                Invoke("DisableBonusText", 2F);
            }
        }

        void DisableBonusText()
        {
            if (textBonus.gameObject.activeSelf)
                textBonus.gameObject.SetActive(false);
        }
    }
}

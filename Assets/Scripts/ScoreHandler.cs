using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreHandler : MonoBehaviour
{

    [SerializeField] TMPro.TextMeshProUGUI scoreText;
    [Tooltip("Gives the player points over time for still being alive")]   
    [SerializeField] int scorePointsOverTime = 10;
    [SerializeField] float timeBetweenPoints = 5f;
    [Tooltip("the score increases over the set amount of time")]
    [SerializeField] float scoreUpdateTime = .2f;
    [SerializeField] Slider gunHeatSlider;

    private float playerScore = 0;
    private float displayScore = 0;


    PlayerController player;

    private void Awake()
    {
        int numOfScoreHandlers = FindObjectsOfType<ScoreHandler>().Length;
        if (numOfScoreHandlers > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
        
    }
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        displayScore = playerScore;
        scoreText.text = playerScore.ToString();
        InvokeRepeating("PointsOverTime", timeBetweenPoints, timeBetweenPoints);
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(ScoreIncreament());
        DisplayGunHeat();
    }

    private void PointsOverTime() // this method is called using a string ref
    {
        playerScore += scorePointsOverTime;
    }

    private IEnumerator ScoreIncreament()
    {

        if(displayScore < playerScore)
        {
            displayScore++;            
            scoreText.text = displayScore.ToString();
        }
        yield return new WaitForSeconds(scoreUpdateTime);
    }

    public void AddToScore(int score)
    {
        playerScore += score;
    }

    private void DisplayGunHeat()
    {
        gunHeatSlider.maxValue = player.GetMaxGunHeat();
        gunHeatSlider.value = player.GetCurrentGunHeat();
    }

}

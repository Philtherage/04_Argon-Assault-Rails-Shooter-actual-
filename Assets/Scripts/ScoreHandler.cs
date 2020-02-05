using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreHandler : MonoBehaviour
{

    [SerializeField] TMPro.TextMeshProUGUI scoreText;
    private int playerScore = 0;

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
        scoreText.text = playerScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = playerScore.ToString();
    }

    public void AddToScore(int score)
    {
        playerScore += score;
    }

    

}

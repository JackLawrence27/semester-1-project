using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreScript : MonoBehaviour
{
    public static int scoreValue = 0;
    public static int scoreValueMax = 3;
    Text score;

    // Start is called before the first frame update
    void Start()
    {
        score = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        score.text = "BOARS SLAYED: " + scoreValue + " / " + scoreValueMax;

        if(scoreValue == scoreValueMax)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            scoreValue = 0;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int score = 0;

    int targetScore = 5;

    int currentLevel = 0;
    
    const string DIR_LOGS = "/Logs";
    const string FILE_SCORES = DIR_LOGS + "/highScore.txt";
    string FILE_PATH_HIGH_SCORES;
    
    const string PREF_KEY_HIGH_SCORE = "HighScoreKey";
    int highScore = -1;

    
    public int Score
    {
        get { return score; }
        set
        {
            score = value;
            
            if (score > HighScore)
            {
                HighScore = score;
            }
            
        }
    }
    
    public int HighScore
    {
        get
        {
            if (highScore < 0)
            {
                if (File.Exists(FILE_PATH_HIGH_SCORES))
                {
                    string fileContents = File.ReadAllText(FILE_PATH_HIGH_SCORES);
                    highScore = Int32.Parse(fileContents);
                }
                else
                {
                    highScore = 0;
                }
            }

            return highScore;
        }
        set
        {
            highScore = value;

            if (!File.Exists(FILE_PATH_HIGH_SCORES))
            {
                Directory.CreateDirectory(Application.dataPath + DIR_LOGS);
            }

            File.WriteAllText(FILE_PATH_HIGH_SCORES, highScore + "");
        }
    }

    void Awake()
    {
        if (instance == null) 
        {
            DontDestroyOnLoad(gameObject);  
            instance = this; 
        }
        else  
        {
            Destroy(gameObject); 
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        FILE_PATH_HIGH_SCORES = Application.dataPath + FILE_SCORES;
    }

    // Update is called once per frame
    void Update()
    {
        print("Score: " + Score);
        print("HighScore: " + HighScore);
        if (Score == targetScore)  //if the current score == the targetScore
        {
            currentLevel++; //increase the level number
            SceneManager.LoadScene(currentLevel); //go to the next level
            targetScore += 5;
        }
    }
}

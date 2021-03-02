using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // to create Singleton

    public int score = 0;

    int targetScore = 5; // targetScore equals 5 for level1

    public static int currentLevel = 0; 
    
    const string FILE_SCORES = "/highScores.txt";
    string FILE_PATH_HIGH_SCORES;
    

    public Text scoreText;
    
    bool isGame = true;
    
    List<int> highScores;

    
    public int Score    // make score a property
    {
        get { return score; }
        set
        {
            score = value;
        }
    }

    void Awake()   //make GameManager a singleton
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
        if (isGame)  //while in the game (level1 & level2 scenes)
        {
            scoreText.text = "Score: " + Score + "\nTarget Score: " + targetScore; // show the current score on the screen
            if (Score == targetScore)  //if the current score == the targetScore
            {
                currentLevel++; //increase the level number
                SceneManager.LoadScene(currentLevel); //go to the next level
                if (currentLevel == 2) // if now is the GameEnd scene
                {
                    UpdateHighScores();
                    isGame = false;
                }
                targetScore += 5; // when go to the next level, add 5 to the targetScore
            }
        }
        else // while in the gameEnd scene
        {
            string highScoreString = "High Scores\n\n";

            for (var i = 0; i < highScores.Count; i++)
            {
                highScoreString += highScores[i] + "\n";
            }

            scoreText.text = highScoreString;
        }
       
        
    }
    
    void UpdateHighScores() 
    {
        if (highScores == null) //if we don't have the high scores yet
        {
            highScores = new List<int>();
            
            highScores.Add(2); // TODO
            highScores.Add(1);// TODO

            string fileContents = File.ReadAllText(FILE_PATH_HIGH_SCORES);

            string[] fileScores = fileContents.Split(',');
            
            print(fileScores.Length);

            for (var i = 0; i < fileScores.Length - 1; i++)
            {
                highScores.Add(Int32.Parse(fileScores[i]));
            }
        }

        for (var i = 0; i < highScores.Count; i++)
        {
            if (score > highScores[i])
            {
                highScores.Insert(i, score);
                break;
            }
        }

        string saveHighScoreString = "";

        for (var i = 0; i < highScores.Count; i++)
        {
            saveHighScoreString += highScores[i] + ",";
        }

        File.WriteAllText(FILE_PATH_HIGH_SCORES, saveHighScoreString);
    }
}


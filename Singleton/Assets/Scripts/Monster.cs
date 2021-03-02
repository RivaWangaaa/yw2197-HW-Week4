using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;

public class Monster : MonoBehaviour
{
    public static Monster instance;
    private float timer = 0;
    
    void Awake() // create Singleton
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
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime; // timer count the time
        if (timer >= 3) //every 3 seconds, the monster appears at a different spot
        {
            transform.position = new Vector2( //teleport to a random location
                Random.Range(-5,5),
                Random.Range(-5,5));

            timer = 0; 
            
            GameManager.instance.Score++; // if the player survives, score + 1;
            
        }

        print(timer);

        if (GameManager.currentLevel == 2)
        {
            Destroy(gameObject);
        }
        
    }


// Sorry i couldn't get this function to work, I will seek help and fix this.
// It supposed to be a function that detects monster and player collision.
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player") //if collide with "Player" object, player loses.
        {
            print("End");
            SceneManager.LoadScene("EndScene");
        }
    }
}

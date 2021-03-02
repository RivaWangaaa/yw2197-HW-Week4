using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{
    public float forceAmount = 5;  //set forceAmount to determine how fast player move.
    
    Rigidbody2D rb2D;
    public static PlayerControl instance;
    
    void Awake() //create singleton
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
        rb2D = GetComponent<Rigidbody2D>();  
    }

    // Update is called once per frame
    void Update()
    {
        //WASD Controller
        if (Input.GetKey(KeyCode.W)) //if W is pressed
        {
            rb2D.AddForce(Vector2.up * forceAmount); 
        }

        if (Input.GetKey(KeyCode.S)) //if S is pressed
        {
            rb2D.AddForce(Vector2.down * forceAmount); 
        }

        if (Input.GetKey(KeyCode.A)) //if A is pressed
        {
            rb2D.AddForce(Vector2.left * forceAmount); 
        }

        if (Input.GetKey(KeyCode.D)) //if D is pressed
        {
            rb2D.AddForce(Vector2.right * forceAmount);
        }
        
        if (GameManager.currentLevel == 2) //destroy player object in the gameEnd scene
        {
            Destroy(gameObject);
        }
    }
    
   
}

using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
[RequireComponent(typeof(MazeConstructor))]
public class GameController : MonoBehaviour
{
    private MazeConstructor generator;
    [SerializeField] private FpsMovement player;
    [SerializeField] private Enemy_Tracking enemy;
    public Text scoreLabel;

    public FpsMovement Player { get => player; set => player = value; }
    public Enemy_Tracking Enemy { get => enemy; set => enemy = value; }
    //public float lives = 3;
    public  int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        generator = GetComponent<MazeConstructor>();
        StartNewGame();//Generates the maze on start
        score = 0;
        //scoreLabel.GetComponent<Text>().text = score.ToString("F0");
        scoreLabel.text = score.ToString();
    }

    void StartNewGame()
    {
        player.enabled = true;
        generator.GenerateNewMaze(35, 35, AtEndTrigger);
        float x = generator.startCol * generator.hallWidth;
        float y = 1;
        float z = generator.startRow * generator.hallWidth;
        float a = generator.middleCol * generator.hallWidth;
        float b = 1;
        float c = generator.middleRow * generator.hallWidth;
        Enemy.transform.position = new Vector3(a, b, c);
        Player.transform.position = new Vector3(x, y, z);
        
        
       
    }

    private void AtEndTrigger(GameObject trigger, GameObject other)
    {
        Debug.Log("End Reached");

        
        Destroy(trigger);
        
        Debug.Log("Finish");
        Player.enabled = false;
        Invoke("StartNewGame", 1);
        float x = generator.startCol * generator.hallWidth;
        float y = 1;
        float z = generator.startRow * generator.hallWidth;
        Player.transform.position = new Vector3(x, y, z);
        score += 1;
        scoreLabel.GetComponent<Text>().text = score.ToString();


    }

 
    
    // Update is called once per frame
    void Update()
    {
        if (enemy.lives <= 0)
        {
            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadScene(3);
        }

    }
}

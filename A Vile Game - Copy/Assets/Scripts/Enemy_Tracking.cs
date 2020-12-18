using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
[RequireComponent(typeof(GameController))]
public class Enemy_Tracking : MonoBehaviour
{
    float Speed = 4f;
    float rotSpeed = 4f;
    private GameController life;
    private bool isPatroling = false;
    private bool isRotatingLeft = false;
    private bool isRotatingRight = false;
    private bool isWalking = false;
    private bool follow = false;
    Vector3 turn;
    public float lives = 25;
    float distance = 10f;
    private Transform target;

    Collision collision;
    // Start is called before the first frame update
    void Start()
    {
        life = GetComponent<GameController>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        Patroling();
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(target.position, transform.position);
        if (dist <= 10)
        {
            follow = true;
        }
        if (dist > 10)
        {
            follow = false;
            
        }
        int turnValue = Random.Range(0, 2);
        
        if (isPatroling == false)
        {
            StartCoroutine(Patroling());
        }

        if (isRotatingRight == true && (turnValue == 0 || turnValue == 1))
        {
            if (turnValue == 0)
            {
                turn = new Vector3(0, 90, 0);
                transform.Rotate(turn * Time.deltaTime * rotSpeed);
            }
            if (turnValue == 1)
            {
                turn = new Vector3(0, 180, 0);
                transform.Rotate(turn * Time.deltaTime * rotSpeed);
            }
        }
        if (isRotatingLeft == true && turnValue == 2)
        {
            turn = new Vector3(0, -90, 0);
            transform.Rotate(turn * Time.deltaTime * -rotSpeed);
        }
        if (isWalking == true)
        {
           
            transform.position += transform.forward * Speed * Time.deltaTime;
        }
        if (follow)
        {
            StopCoroutine(Patroling());
            // enemy will now only follow if the boolean enemyShouldFollow is true
            transform.LookAt(target.position);
            transform.position = Vector3.MoveTowards(transform.position, target.position, Speed * Time.deltaTime);

        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Player")
        {
            lives = lives - 1;
        }
        
        
       
    }
    IEnumerator Patroling()
    {
        int rotTime = 1;//Random.Range(1, 3);
        int rotWait = Random.Range(1, 4);
        int rotLorR = Random.Range(1, 2);

        int walkWait = Random.Range(1, 4);
        int walkTime = 10; //Random.Range(1, 5);

        isPatroling = true;

        yield return new WaitForSeconds(walkWait);
        isWalking = true;
        yield return new WaitForSeconds(walkTime);
        isWalking = false;
        yield return new WaitForSeconds(rotWait);
        if (rotLorR == 1)
        {
            isRotatingRight = true;
            yield return new WaitForSeconds(rotTime);
            isRotatingRight = false;
        }
        if (rotLorR == 2)
        {
            isRotatingLeft = true;
            yield return new WaitForSeconds(rotTime);
            isRotatingLeft = false;
        }
        isPatroling = false;
    }


}

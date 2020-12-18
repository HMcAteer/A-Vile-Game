using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public bool isStart;
    public bool isInstructions;
    public bool isCredits;


    private void OnMouseUp()
    {
        if(isStart)
        {
            SceneManager.LoadScene(1);
        }
        if (isInstructions)
        {
            SceneManager.LoadScene(2);
        }
        if (isCredits)
        {
            SceneManager.LoadScene(4);
        }
    }
}

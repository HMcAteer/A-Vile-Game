using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gameovercont : MonoBehaviour
{
    public bool isMenu;
    public bool isQuit;


    private void OnMouseUp()
    {

        if (isMenu)
        {
            SceneManager.LoadScene(0);
        }

        if(isQuit)
        {
            Application.Quit();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class InstructionsCont : MonoBehaviour
{
    public bool isMenu;



    private void OnMouseUp()
    {

        if (isMenu)
        {
            SceneManager.LoadScene(0);
        }
    }
}

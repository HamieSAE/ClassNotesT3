using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BTNManager : MonoBehaviour
{
    public void NewGameBtn(string newGameLevel)
    {
        SceneManager.LoadScene(newGameLevel);//This will load the next scene when called upon
    }

    public void ExitGameBtn()
    {
        Application.Quit(); //To Exit Game
    }
}
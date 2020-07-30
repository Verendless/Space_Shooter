using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private bool _gameOver;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R) && _gameOver == true)
        {
            SceneManager.LoadScene(1);
        }

        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }

    public void GameOver()
    {
        _gameOver = true;
    }
}

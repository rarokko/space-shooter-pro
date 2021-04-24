using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private bool _gameOver = false;

    void Update()
    {
        GetInput();
    }

    void GetInput() {
        if (Input.GetKeyDown(KeyCode.R) && _gameOver)
        {
            SceneManager.LoadScene(0);
        }
    }

    public void SetGameOver()
    {
        _gameOver = true;
    }
}

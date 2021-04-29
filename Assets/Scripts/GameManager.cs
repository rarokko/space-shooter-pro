using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    private bool _gameOver = false;

    public void GetInput(InputAction.CallbackContext value) {
        if (value.started && _gameOver)
        {
            SceneManager.LoadScene(1);
        }
    }

    public void SetGameOver()
    {
        _gameOver = true;
    }
}

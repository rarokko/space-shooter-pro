using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]   
    private Text _score;
    private string _scorePrefix = "Score: ";
    [SerializeField]
    private Sprite[] _sprites;
    [SerializeField]
    private Image _livesImage;
    [SerializeField]
    private Text _gameOverText;
    [SerializeField]
    private Text _restartText;
    [SerializeField]
    private GameManager _gameManager;

    void Start()
    {
        _gameOverText.gameObject.SetActive(false);
        _restartText.gameObject.SetActive(false);
        _score.text = _scorePrefix + "0";
    }

    public void UpdateScore(int score)
    {
        _score.text = _scorePrefix + score;
    }

    public void UpdateLives(int lives)
    {
        Debug.Log(lives);
        _livesImage.sprite = _sprites[lives];
    }

    public void SetGameOver()
    {
        _gameOverText.gameObject.SetActive(true);
        _restartText.gameObject.SetActive(true);
        _gameManager.SetGameOver();
        StartCoroutine(GameOverAnimation());
    }

    IEnumerator GameOverAnimation() {
        while(true)
        {
            _gameOverText.text = "GAME OVER";
            yield return new WaitForSeconds(0.5f);
            _gameOverText.text = "";
            yield return new WaitForSeconds(0.5f);
        }
    }
}

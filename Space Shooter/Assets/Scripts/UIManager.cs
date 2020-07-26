using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _score;
    [SerializeField]
    private Text _gameOverText;
    [SerializeField]
    private Text _restartText;
    [SerializeField]
    private Sprite [] _playerLifeArray;
    [SerializeField]
    private Image _playerLifeIcon;
    private GameManager _gameManager;

    // Start is called before the first frame update
    void Start()
    {
        _score.text = "Score: 0";
        _gameOverText.gameObject.SetActive(false);
        _restartText.gameObject.SetActive(false);

        _gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        if(_gameManager == null)
        {
            Debug.LogError("Game Manager is Null");
        }

        if(_playerLifeIcon == null)
        {
            Debug.LogError("Player Life Image is Null");
        }
    }

    public void UpdateScore(int playerScore)
    {
        _score.text = "Score: " + playerScore.ToString();
    }

    public void UpdatePlayerLifeUI(int currentPlayerLife)
    {
        _playerLifeIcon.sprite = _playerLifeArray[currentPlayerLife];

        if(currentPlayerLife <= 0)
        {
            GameOverSequence();
        }
    }

    void GameOverSequence()
    {
        _gameManager.GameOver();
        _gameOverText.gameObject.SetActive(true);
        _restartText.gameObject.SetActive(true);
        StartCoroutine(GameOverTextFlickerRoutine());
    }

    IEnumerator GameOverTextFlickerRoutine()
    {
        while(true)
        {
            _gameOverText.text = "Game Over";
            yield return new WaitForSeconds(.5f);
            _gameOverText.text = "";
            yield return new WaitForSeconds(.5f);
        }
    }
}

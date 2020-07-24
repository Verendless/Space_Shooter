using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private Text _score;
    [SerializeField]
    private Sprite [] _playerLifeArray;
    private Image _playerLifeIcon;
    private Player _player;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _score = GameObject.Find("Canvas").GetComponentInChildren<Text>();
        _playerLifeIcon = GameObject.Find("Player Life").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        _score.text = "Score: " + _player.ShowScore();
        
        switch(_player.ShowPlayerLife())
        {
            case 3:
                _playerLifeIcon.sprite = _playerLifeArray[3];
                break;
            case 2:
                _playerLifeIcon.sprite = _playerLifeArray[2];
                break;
            case 1:
                _playerLifeIcon.sprite = _playerLifeArray[1];
                break;
            case 0:
                _playerLifeIcon.sprite = _playerLifeArray[0];
                break;
        }

    }
}

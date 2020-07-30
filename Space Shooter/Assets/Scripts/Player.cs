using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float _speed = 5.0f;
    private float _speedBoost = 2.0f;
    [SerializeField] 
    private int _playerLife = 3;
    [SerializeField]
    private int _score;
    [SerializeField] 
    private GameObject _leserPrefab;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    [SerializeField]
    private GameObject _shield;
    [SerializeField]    
    private GameObject _explosionPrefab;
    [SerializeField]
    private GameObject _playerThruster;
    [SerializeField]
    private GameObject[] _playerHurtAnim;
    private bool _tripleShotActive = false;
    private bool _shieldActive = false;
    private bool _speedBoostActive = false;
    private bool _isPlayerInvicibleActive;
    private float _fireRate = 0.15f;
    private float _nextFire = 0.0f;
    private SpawnManager _spawnManager;
    private UIManager _uiManager;
    private AudioSource _audioSource;
    private Animator _animator;
    
    // Start is called before the first frame update
    void Start()
    {
        
        transform.position = new Vector2(0, 0);
        _spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _animator = GetComponent<Animator>(); 
        _audioSource = GetComponent<AudioSource>();
    
        if(_spawnManager == null)
        {
            Debug.LogError("Spawn Manager is Null");
        }

        if(_uiManager == null)
        {
            Debug.LogError("UI Manager is Null");
        }
        
        if(_audioSource == null)
        {
            Debug.LogError("Audio Source on the Player is Null");
        }

        if(_animator == null)
        {
            Debug.LogError("Animtor on the Player is Null");
        }
    }

    // Update is called once per frame
    void Update()
    {
        CalculatePlayerMovement();
        MovementAnimation();

        if(Input.GetKeyDown(KeyCode.Space) && Time.time > _nextFire)
        {
            FireLeser();
        }

  
    }
 
    void CalculatePlayerMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector2 direction = new Vector2(horizontalInput, verticalInput);

        if(_speedBoostActive == false)
        {
            transform.Translate(direction * _speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(direction * (_speed * _speedBoost) * Time.deltaTime);
        }
        
        
        transform.position = new Vector2(transform.position.x, Mathf.Clamp(transform.position.y, -3.0f, 0));

        if(transform.position.x <= -11.0f)
        {
            transform.position = new Vector2(11.0f, transform.position.y);
        }
        else if(transform.position.x >= 11.0f)
        {
            transform.position = new Vector2(-11.0f, transform.position.y);
        }
    }

    void MovementAnimation()
    {
        // for to add later (Player Movement Animtions)
        if(Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            _animator.SetBool("PlayerTurnRightBool", true);
            _animator.SetBool("PlayerTurnLeftBool", false);
        }

        else if(Input.GetKeyUp(KeyCode.D) == true || Input.GetKeyUp(KeyCode.RightArrow))
        {
            _animator.SetBool("PlayerTurnRightBool", false);
            _animator.SetBool("PlayerTurnLeftBool", false);
        }
        
        if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _animator.SetBool("PlayerTurnRightBool", false);
            _animator.SetBool("PlayerTurnLeftBool", true);
        }

        else if(Input.GetKeyUp(KeyCode.A) == true || Input.GetKeyUp(KeyCode.LeftArrow))
        {
            _animator.SetBool("PlayerTurnRightBool", false);
            _animator.SetBool("PlayerTurnLeftBool", false);
        }  
    }

    void FireLeser()
    {
        _nextFire = Time.time + _fireRate;
        if(_tripleShotActive == false)
        {
            Instantiate(_leserPrefab, transform.position + new Vector3(0, 1.33f, 0), Quaternion.identity);
        } 
        else
        {
            Instantiate(_tripleShotPrefab, transform.position + new Vector3(0, -0.5f, 0), Quaternion.identity);
        }
        _audioSource.Play();                  
    }

    public void AddScore()
    {
        _score += 10;
        _uiManager.UpdateScore(_score);
    }

    public void Demage()
    {
        if(_playerLife != 0)
        {
            _playerLife -= 1;
            _uiManager.UpdatePlayerLifeUI(_playerLife);
        }

        if(_playerLife < 3 && _playerLife > 0)
        {
            WhenPlayerGetDamage();
        }

        if(_playerLife <= 0)
        {
            _spawnManager.onPlayerDeath();
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
        
    }

    public void WhenPlayerGetDamage()
    {
        _playerHurtAnim[Random.Range(0, 2)].SetActive(true);
        if(_playerHurtAnim[0].gameObject.activeInHierarchy == true && _playerLife < 2)
        {
            _playerHurtAnim[1].SetActive(true);
        }
        if (_playerHurtAnim[1].gameObject.activeInHierarchy == true && _playerLife < 2)
        {
            _playerHurtAnim[0].SetActive(true);
        }
    }

    public void TripleShotIsActive()
    {
        _tripleShotActive = true;
        StartCoroutine(TripleShotIsDownRoutine());
    }

    public void SpeedPowerUpActive()
    {
        _speedBoostActive = true;
        _playerThruster.transform.localScale  += new Vector3(.5f,0,0);
        StartCoroutine(SpeedPowerUpDownRoutine());

    }

    public void ShieldPowerUpActive()
    {
        _shield.SetActive(true);
        _shieldActive = true;
        StartCoroutine(ShieldPowerUpDownRoutine());
    }

    IEnumerator TripleShotIsDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _tripleShotActive = false;
    }

    IEnumerator SpeedPowerUpDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _speedBoostActive = false;
        _playerThruster.transform.localScale  -= new Vector3(.5f,0,0);
    }

    IEnumerator ShieldPowerUpDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _shield.SetActive(false);
        _shieldActive = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "EnemyLeser" || other.tag == "Enemy")
        {
            Destroy(other.gameObject);
            if(_shieldActive == false && _isPlayerInvicibleActive == false)
            {
                Demage();   
                StartCoroutine(PlayerInvincibleRoutine());
            }
            else
            {
                _shield.SetActive(false);
                _shieldActive = false;
            }
                
        }
    }

    IEnumerator PlayerInvincibleRoutine()
    {
        if(_playerLife > 0)
        {
            _animator.SetTrigger("IsPlayerGetHit");
            _animator.ResetTrigger("PlayerInvicibleDown");
            _isPlayerInvicibleActive = true;
            yield return new WaitForSeconds(2.0f);
            _isPlayerInvicibleActive = false;
            _animator.SetTrigger("PlayerInvicibleDown");
            _animator.ResetTrigger("IsPlayerGetHit");
        }

    }
}

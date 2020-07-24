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

    private bool _tripleShotActive = false;
    private bool _shieldActive = false;
    private bool _speedBoostActive = false;

    private float _fireRate = 0.15f;
    private float _nextFire = 0.0f;

    private SpawnManager _spawnManager;
    
    // Start is called before the first frame update
    void Start()
    {
        
        transform.position = new Vector2(0, 0);
        _spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();

        if(_spawnManager == null)
        {
            Debug.LogError("Spawn Manager is Null");
        }
    }

    // Update is called once per frame
    void Update()
    {
        CalculatePlayerMovement();

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

    void FireLeser()
    {
        _nextFire = Time.time + _fireRate;
        if(_tripleShotActive == false)
        {
            Instantiate(_leserPrefab, transform.position + new Vector3(0, 1f, 0), Quaternion.identity);
        } 
        else
        {
            Instantiate(_tripleShotPrefab, transform.position + new Vector3(0, -0.88f, 0), Quaternion.identity);
        }
                       
    }

    public void AddScore()
    {
        _score += 10;
    }

    public int ShowScore()
    {
        return _score;
    }

    public void Demage()
    {
        if(_shieldActive == false)
        {
            _playerLife --;
        }
        else 
        {
            transform.Find("Shield").gameObject.SetActive(false);
        }
        

        if(_playerLife == 0)
        {
            _spawnManager.onPlayerDeath();
            Destroy(this.gameObject);
        }
        
    }

    public int ShowPlayerLife()
    {
        return _playerLife;
    }

    public void TripleShotIsActive()
    {
        _tripleShotActive = true;
        StartCoroutine(TripleShotIsDownRoutine());
    }

    public void SpeedPowerUpActive()
    {
        _speedBoostActive = true;
        StartCoroutine(SpeedPowerUpDownRoutine());

    }

    public void ShieldPowerUpActive()
    {
        transform.Find("Shield").gameObject.SetActive(true);
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
    }

    IEnumerator ShieldPowerUpDownRoutine()
    {
        if(_shieldActive == true)
        {
        yield return new WaitForSeconds(5.0f);
        transform.Find("Shield").gameObject.SetActive(false);
        _shieldActive = false;
        }
    }


}

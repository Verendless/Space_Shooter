using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float _speed = 5.0f;
    [SerializeField] private GameObject _leserPrefab;
    private float _fireRate = 0.15f;
    private float _nextFire = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector2(0, 0);

        if(_leserPrefab == null)
        {
            Debug.Log ("Leser is Null");
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
        transform.Translate(direction * _speed * Time.deltaTime);
        
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
            Instantiate(_leserPrefab, transform.position + new Vector3(0, 0.8f, 0), Quaternion.identity);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float _speed = 5.0f;
    private float _verticalInput;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector2(0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector2 direction = new Vector2(horizontalInput, verticalInput);
        transform.Translate(direction * _speed * Time.deltaTime);
    }
}

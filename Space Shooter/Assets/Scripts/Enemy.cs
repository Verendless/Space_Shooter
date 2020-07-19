using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float _speed = 4.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.down * _speed * Time.deltaTime);

        if(transform.position.y <= -4.4)
        {
            float randomX = Random.Range(-9.4f, 9.4f);
            transform.position = new Vector2(randomX, 8.0f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            if(other != null)
            {
               player.Demage();
            }          
            Destroy(this.gameObject);
        }

        if(other.tag == "Leser")
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }
}

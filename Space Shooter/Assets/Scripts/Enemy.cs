using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float _speed = 4.0f;
    private Player _player;
    private Animator _enemyDestroyedAnim;

    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        if(_player == null)
        {
            Debug.LogError("Player is Null");
        }

        _enemyDestroyedAnim = gameObject.GetComponent<Animator>();
        if(_enemyDestroyedAnim == null)
        {
            Debug.LogError("Player is Null");
        }
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if(other.tag == "Player")
        { 
            if(other != null)
            {
               _player.Demage();
            }          
            OnEnemyDeath();
        }

        if(other.tag == "Leser")
        {
            Destroy(other.gameObject);
            if(_player != null)
            {
                _player.AddScore();
            }
            OnEnemyDeath();
        }
    }

    void OnEnemyDeath()
    {
        _enemyDestroyedAnim.SetTrigger("EnemyDestroyTrigger");
        Destroy(GetComponent<PolygonCollider2D>());
        Destroy(this.gameObject, 1.0f);
        

    }
}

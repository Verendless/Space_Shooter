using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float _speed = 4.0f;
    [SerializeField]
    private GameObject _enemyLeserPrefab;
    private float _fireRate = 2f;
    private float _nextFire = 0.0f;
    private bool _isEnemyDestroyed = false;
    // private bool _isEnemyLeser = false;
    private Player _player;
    private Animator _enemyDestroyedAnim;
    private AudioSource _enemyExplosionSoundSource;

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
            Debug.LogError("Enemy Destroyed anim on the Enemy is Null");
        }

        _enemyExplosionSoundSource = GetComponent<AudioSource>();
        if(_enemyExplosionSoundSource == null)
        {
            Debug.LogError("Explosion Sound on the Enemy is Null");
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

        ShootLeser();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Leser")
        {
            Destroy(other.gameObject);
            if(_player != null)
            {
                _player.AddScore();
            }
            OnEnemyDeath();
        }
        else if(other.tag == "Player")
        {
            OnEnemyDeath();
        }
        
    }

    void ShootLeser()
    {
        if(Time.time > _nextFire && _isEnemyDestroyed == false)
        {
            _nextFire = Time.time + _fireRate;
           GameObject _newEnemyLeser = Instantiate(_enemyLeserPrefab, transform.position + new Vector3(0, -1.05f, 0), Quaternion.identity);
           _newEnemyLeser.transform.parent = gameObject.transform.parent;
           EnemyLeser leser = _newEnemyLeser.GetComponentInChildren<EnemyLeser>();

        //    for(int i = 0; i < lesers.Length; i++)
        //    {
        //        lesers[i].WhenEnemyShot();
        //    }
        }
    }

    void OnEnemyDeath()
    {
        _enemyDestroyedAnim.SetTrigger("EnemyDestroyTrigger");
        _enemyExplosionSoundSource.Play();
        _isEnemyDestroyed = true;
        Destroy(GetComponent<PolygonCollider2D>());
        Destroy(this.gameObject, 3.0f);
    }
}

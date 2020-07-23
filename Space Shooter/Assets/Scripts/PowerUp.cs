using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    private float _speed = 3.0f;

    // Update is called once per frame
    void Update()
    {
        PowerUpBehaviour();
    }

    void PowerUpBehaviour()
    {
        transform.Translate(Vector3.down * _speed *Time.deltaTime);

        if(transform.position.y <= -4.35f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();

            if(player != null)
            {
                player.TripleShotIsActive();
            }

            Destroy(this.gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    private float _speed = 3.0f;
    [SerializeField]
    private int _powerUpID;
    [SerializeField]
    private AudioClip _audioClip;
    
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
            AudioSource.PlayClipAtPoint(_audioClip, transform.position);
            if(player != null)
            {
                switch(_powerUpID)
                {
                    case 0:
                        player.TripleShotIsActive();
                        break;
                    case 1:
                        player.SpeedPowerUpActive();
                        break;
                    case 2:
                        player.ShieldPowerUpActive();
                        break;
                    default:
                        Debug.LogError("Invalid Power Up ID ");
                        break;
                }
            }
            Destroy(this.gameObject);
        }
    }
}

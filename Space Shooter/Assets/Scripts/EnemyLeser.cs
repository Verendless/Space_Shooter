using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLeser : MonoBehaviour
{
    private float _speed = 8.0f;

    // Update is called once per frame
    void Update()
    {
        EnemyLeserBehaviour();
    }

    void EnemyLeserBehaviour()
    {
        transform.Translate((Vector2.down) * _speed * Time.deltaTime);

        if(transform.position.y <= -4.1f)
        {
            Destroy(this.gameObject);
        }
    }
}

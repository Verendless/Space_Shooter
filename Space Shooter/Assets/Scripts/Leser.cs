using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leser : MonoBehaviour
{
    private float _speed = 8.0f;
   
    // Update is called once per frame
    void Update()
    {
        LeserBehaviour();
    }

    void LeserBehaviour()
    {
        transform.Translate((Vector2.up) * _speed * Time.deltaTime);

        if(transform.position.y >= 7.9f)
        {
            if(transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(this.gameObject);
            
        }
    }
}

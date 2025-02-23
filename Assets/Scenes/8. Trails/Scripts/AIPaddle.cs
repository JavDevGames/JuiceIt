using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class AIPaddle : MonoBehaviour
{
    public GameObject ball;
    public float velocity;
    
    private float m_Velocity;
    void Start()
    {
        m_Velocity = 0;
    }

    // Update is called once per frame
    void Update()
    {
        var bounds = GetComponent<SpriteRenderer>().bounds;

        if (bounds.center.y > ball.transform.position.y)
        {
            transform.Translate(0, -m_Velocity * Time.deltaTime, 0);
            m_Velocity = Mathf.Clamp(m_Velocity + 1f, -velocity, velocity);
        }
        else if (bounds.center.y < ball.transform.position.y)
        {
            transform.Translate(0, m_Velocity * Time.deltaTime, 0);
            m_Velocity = Mathf.Clamp(m_Velocity + 1f, -velocity, velocity);
        }
    }
}

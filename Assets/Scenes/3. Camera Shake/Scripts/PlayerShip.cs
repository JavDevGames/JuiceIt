using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;

public class PlayerShip : MonoBehaviour
{
    public GameObject pirateShip;
    public GameObject bulletPrefab;
    private GameObject m_CannonBall;
    public GameObject cannon;
    public Camera m_Camera;

    public float m_Speed;
    public float m_Movement;
    
    private CameraShake m_CameraShake;
    enum PlayerState
    {
        Idle,
        Shooting,
        TakingDamage
    };

    private PlayerState m_State;

    public float shotDuration;
    private float m_StartTime;

    public GameObject explosion;
    public float m_ExplosionDuration;
    private GameObject m_Explosion;

    private static Vector3 POSITION_HELPER;

    // Start is called before the first frame update
    void Start()
    {
        m_State = PlayerState.Idle;
        POSITION_HELPER = new Vector3 (0, 0, 0);
        m_CameraShake = m_Camera.GetComponent<CameraShake>();
    }

    void UpdatePosition()
    {
        if (Input.GetKey(KeyCode.W))
        {
            var position = transform.position;
            position.y += m_Movement * Time.deltaTime;
            transform.position = position;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            var position = transform.position;
            position.y -= m_Movement * Time.deltaTime;
            transform.position = position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (m_State == PlayerState.Idle)
        {
            if (Input.GetKeyDown(KeyCode.Space)) // Example input to trigger shooting
            {
                ShootAtPirateShip();
                m_State = PlayerState.Shooting;
                POSITION_HELPER = cannon.transform.position;
            }

            UpdatePosition();
        }
        else if(m_State == PlayerState.Shooting)
        {
            if(Time.realtimeSinceStartup - m_StartTime > shotDuration)
            {
                Destroy(m_CannonBall);
                m_State = PlayerState.Idle;
            }

            UpdatePosition();
        }

        UpdateCannonball();
    }

    void ShootAtPirateShip()
    {
        if(m_CannonBall != null) 
            Destroy(m_CannonBall);

        m_CannonBall = Instantiate(bulletPrefab, cannon.transform.position, Quaternion.identity, cannon.transform);
        m_StartTime = Time.realtimeSinceStartup;        
    }

    void UpdateCannonball()
    {
        if (m_CannonBall == null)
            return;
     
        POSITION_HELPER.x += m_Speed * Time.deltaTime;
        m_CannonBall.transform.position = POSITION_HELPER;
    }

    // Player ship has:
    // Rigid Body 2D - Kinematic Type
    // Box Collider 2D - Trigger
    // CannonBall has:
    // Rigid Body 2D - Kinematic Type
    // Box Collider 2D - Trigger
    // That allows this to be called
    private void OnTriggerEnter2D(Collider2D collision)
    {
        m_CameraShake.BeginCameraShake();
        ApplyDamage();
    }

    public void ApplyDamage()
    {
        m_Explosion = Instantiate(explosion, transform.position, Quaternion.identity);
        m_State = PlayerState.TakingDamage;
        StartCoroutine(FadeExplosionInOut());
    }

    private IEnumerator FadeExplosionInOut()
    {
        float time = 0;
        Color originalColor = m_Explosion.GetComponent<SpriteRenderer>().color;

        while (time < (m_ExplosionDuration / 2.0f))
        {
            float alpha = Mathf.Lerp(0f, 1f, time / (m_ExplosionDuration / 2.0f));
            m_Explosion.GetComponent<SpriteRenderer>().color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            time += Time.deltaTime;
            yield return null;
        }

        while (time < m_ExplosionDuration)
        {
            float alpha = Mathf.Lerp(1f, 0f, time / (m_ExplosionDuration / 2.0f));
            m_Explosion.GetComponent<SpriteRenderer>().color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            time += Time.deltaTime;
            yield return null;
        }

        m_State = PlayerState.Idle;
        Destroy(m_Explosion);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class PirateShip : MonoBehaviour
{
    public Sprite[] sprites;
    public float fadeDuration;
    public float explosionDuration;
    private int damage;
    private int m_CurDamage;
    public GameObject explosion;
    
    public GameObject m_Cannon;    
    public GameObject m_PlayerShip;
    public float m_Speed;
    public float m_CannonBallDuration;

    public GameObject bulletPrefab;
    private GameObject m_CannonBall;

    private SpriteRenderer m_Renderer;
    private GameObject m_Explosion;

    private float m_CannonBallStartTime;

    private static Vector3 POSITION_HELPER;

    enum PirateShipState
    {
        idle,
        taking_damage,
        disappearing
    };
    private PirateShipState m_State;

    // Start is called before the first frame update
    void Start()
    {
        damage = 0;
        m_Renderer = GetComponent<SpriteRenderer>();
        m_Renderer.sprite = sprites[damage];
        POSITION_HELPER = new Vector3(0,0, 0);
    }

    public void ApplyDamage()
    {
        damage++;
        m_Explosion = Instantiate(explosion, transform.position, Quaternion.identity);
        m_State = PirateShipState.taking_damage;
        StartCoroutine(FadeExplosionInOut());
    }

    private IEnumerator FadeExplosionInOut()
    {
        float time = 0;
        Color originalColor = m_Explosion.GetComponent<SpriteRenderer>().color;

        while (time < (explosionDuration / 2.0f))
        {
            float alpha = Mathf.Lerp(0f, 1f, time / (explosionDuration / 2.0f));
            m_Explosion.GetComponent<SpriteRenderer>().color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            time += Time.deltaTime;
            yield return null;
        }
        
        while (time < explosionDuration)
        {
            float alpha = Mathf.Lerp(1f, 0f, time / (explosionDuration / 2.0f));
            m_Explosion.GetComponent<SpriteRenderer>().color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            time += Time.deltaTime;
            yield return null;
        }

        m_State = PirateShipState.idle;
        Destroy(m_Explosion);
    }

    // Update is called once per frame
    void Update()
    {
        if (m_State == PirateShipState.idle)
        {
            var playerPosition = m_PlayerShip.transform.position;
            var currentPosition = transform.position;

            currentPosition.y += Mathf.Clamp((playerPosition.y - currentPosition.y) * 0.01f, -0.01f, 0.01f);

            transform.position = currentPosition;

            if (m_CurDamage != damage)
            {
                if (damage < sprites.Length)
                {
                    m_Renderer.sprite = sprites[damage];
                }
                else
                {
                    DestroyEnemy();
                }
            }

            if(m_CannonBall == null)
            {
                ShootAtPlayerShip();
            }
        }

        UpdateCannonball();
    }

    // Call this function to start the fade out and destroy process
    public void DestroyEnemy()
    {
        m_State = PirateShipState.disappearing;
        StartCoroutine(FadeOutAndDestroy());
    }

    private IEnumerator FadeOutAndDestroy()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        Color originalColor = spriteRenderer.color;
        float time = 0;

        while (time < fadeDuration)
        {
            float alpha = Mathf.Lerp(1f, 0f, time / fadeDuration);
            spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            time += Time.deltaTime;
            yield return null;
        }

        damage = 0;
        spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, 1);
        m_Renderer.sprite = sprites[damage];
        m_State = PirateShipState.idle;
    }

    // Pirate ship has:
    // Rigid Body 2D - Kinematic Type
    // Box Collider 2D - Trigger
    // CannonBall has:
    // Rigid Body 2D - Kinematic Type
    // Box Collider 2D - Trigger
    // That allows this to be called
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ApplyDamage();
    }

    void ShootAtPlayerShip()
    {
        m_CannonBall = Instantiate(bulletPrefab, m_Cannon.transform.position, Quaternion.identity, m_Cannon.transform);
        m_CannonBallStartTime = Time.realtimeSinceStartup;
        POSITION_HELPER = m_Cannon.transform.position;
    }

    void UpdateCannonball()
    {
        if (m_CannonBall == null)
            return;

        POSITION_HELPER.x += m_Speed * Time.deltaTime;
        m_CannonBall.transform.position = POSITION_HELPER;

        if((Time.realtimeSinceStartup - m_CannonBallStartTime) > m_CannonBallDuration)
            Destroy(m_CannonBall);
        
    }
}

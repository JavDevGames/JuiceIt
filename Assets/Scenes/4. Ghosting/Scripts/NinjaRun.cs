using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class NinjaRun : MonoBehaviour
{
    public Sprite [] sprites;
    public float m_Duration;
    public float m_Speed;
    public float m_DashSpeed;
    public float m_DashDuration;

    public ParticleSystem m_GhostParticleSystem;
    public float m_EmitDelay;
    private float m_LastEmit;

    private int m_CurIndex;
    private float m_LastTime;
    private SpriteRenderer m_SpriteRenderer;
    private float m_StartDash;

    enum PlayerState
    {
        StateIdle,
        StateRunning,
        StateDash
    }

    private PlayerState m_State;

    enum Direction
    {
        Left,
        Right
    }

    private Direction m_Direction;

    // Start is called before the first frame update
    void Start()
    {
        m_LastTime = Time.realtimeSinceStartup;
        m_SpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        m_State = PlayerState.StateIdle;
    }

    // Update is called once per frame
    void Update()
    {
        if(m_State == PlayerState.StateIdle)
        {
            if(Input.GetKeyDown(KeyCode.D))
            {
                m_State = PlayerState.StateRunning;

                Vector2 lookRight = new Vector2(1, 1);
                transform.localScale = lookRight;
                m_Direction = Direction.Right;            
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                m_State = PlayerState.StateRunning;

                // flip the direction
                Vector2 lookLeft = new Vector2(-1, 1);
                transform.localScale = lookLeft;
                m_Direction = Direction.Left;
            }
        }
        else if (m_State == PlayerState.StateRunning)
        {
            var diff = Time.realtimeSinceStartup - m_LastTime;

            if (diff > m_Duration)
            {
                m_CurIndex = (m_CurIndex + 1) % sprites.Length;
                m_LastTime = Time.realtimeSinceStartup;
                m_SpriteRenderer.sprite = sprites[m_CurIndex];
            }

            if(Input.GetKey(KeyCode.D))
            {
                Vector2 pos = transform.position;
                pos.x += m_Speed * Time.deltaTime;
                transform.position = pos;
            }
            else if(Input.GetKeyUp(KeyCode.D))
            {
                m_State = PlayerState.StateIdle;
            }
            else if (Input.GetKey(KeyCode.A))
            {
                Vector2 pos = transform.position;
                pos.x -= m_Speed * Time.deltaTime;
                transform.position = pos;
            }
            else if (Input.GetKeyUp(KeyCode.A))
            {
                m_State = PlayerState.StateIdle;
            }

            if(Input.GetKeyDown(KeyCode.Space))
            {
                m_State = PlayerState.StateDash;
                m_StartDash = Time.realtimeSinceStartup;

                var particleRenderer = m_GhostParticleSystem.GetComponent<ParticleSystemRenderer>();

                particleRenderer.material.mainTexture = m_SpriteRenderer.sprite.texture;

                EmitParams emitParams = new EmitParams();
                emitParams.position = transform.position;

                float direction = (m_Direction == Direction.Right) ? 0 : 1.0f;
                particleRenderer.flip = new Vector3 (direction, 0, 0);
                m_GhostParticleSystem.Emit(emitParams, 1);
                m_LastEmit = Time.realtimeSinceStartup;
            }
        }
        else if(m_State == PlayerState.StateDash)
        {
            // Do effect
            float directionMultiplier = (m_Direction == Direction.Right) ? 1.0f : -1.0f;
            Vector2 pos = transform.position;
            pos.x += (m_DashSpeed * directionMultiplier) * Time.deltaTime;
            transform.position = pos;

            if (Time.realtimeSinceStartup - m_LastEmit > m_EmitDelay)
            {
                EmitParams emitParams = new EmitParams();
                emitParams.position = transform.position;
                m_GhostParticleSystem.Emit(emitParams, 1);
                m_LastEmit = Time.realtimeSinceStartup;
            }

            if (Time.realtimeSinceStartup - m_StartDash > m_DashDuration)
            {
                m_State = PlayerState.StateRunning;
            }
        }
    }
}

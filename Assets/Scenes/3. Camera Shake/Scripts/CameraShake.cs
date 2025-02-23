using UnityEngine;

public class CameraShake : MonoBehaviour
{
    enum CameraShakeState
    {
        Idle,
        Shaking
    }

    public float m_ShakeDuration;
    public float m_ShakeAmount;

    private CameraShakeState m_State;
    private Vector3 m_OriginalPosition;
    private float m_StartTime;
    private static Vector3 POSITION_HELPER;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_OriginalPosition = transform.position;
        POSITION_HELPER = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(m_State == CameraShakeState.Shaking)
        {
            POSITION_HELPER.x = Random.insideUnitSphere.x * m_ShakeAmount;
            POSITION_HELPER.y = Random.insideUnitSphere.y * m_ShakeAmount;

            transform.position = POSITION_HELPER;

            if (Time.realtimeSinceStartup -  m_StartTime > m_ShakeDuration)
            {
                transform.position = m_OriginalPosition;
                m_State = CameraShakeState.Idle;

            }
        }
    }

    public void BeginCameraShake()
    {
        m_State = CameraShakeState.Shaking;
        m_StartTime = Time.realtimeSinceStartup;
    }
}

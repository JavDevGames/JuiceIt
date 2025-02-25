using UnityEngine;

public class TimeController : MonoBehaviour
{
    public AnimationCurve m_Curve;
    public float m_Duration;
    private bool m_BulletTime;
    private float m_StartTime;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_BulletTime = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(m_BulletTime)
        {
            float progress = (Time.realtimeSinceStartup - m_StartTime) / m_Duration;
            Time.timeScale = m_Curve.Evaluate(progress);
            Time.fixedDeltaTime = Time.timeScale * 0.02f;

            if (progress >= 1.0f)
            {
                m_BulletTime = false;
                Time.timeScale = 1.0f;
                //Time.fixedDeltaTime = 1.0f;
            }
        }
    }

    public void OnClick()
    {
        m_BulletTime = true;
        m_StartTime = Time.realtimeSinceStartup;
    }
}

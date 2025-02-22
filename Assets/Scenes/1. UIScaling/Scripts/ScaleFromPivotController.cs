using UnityEngine;
using UnityEngine.UI;

public class ScaleFromPivotController : MonoBehaviour
{
    public Canvas ScaleFromPivotUI;
    public float m_Duration;

    private float m_StartTime;

    private readonly Vector3 kStartScale = new(0, 0, 0);
    private readonly Vector3 kEndScale = Vector3.one;

    enum ScaleDirection
    {
        None,
        ScaleUp,
        ScaleDown
    };

    private ScaleDirection m_Direction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ScaleFromPivotUI.enabled = false;
        m_Direction = ScaleDirection.None;
    }

    // Update is called once per frame
    void Update()
    {
        if(m_Direction == ScaleDirection.ScaleUp)
        {
            float progress = Mathf.Clamp((Time.realtimeSinceStartup - m_StartTime) / m_Duration, 0, 1);
            ScaleFromPivotUI.transform.localScale = Vector3.Lerp(kStartScale, kEndScale, progress);
            
            if(progress >= 1.0f)
                m_Direction = ScaleDirection.None;
        }
        else if (m_Direction == ScaleDirection.ScaleDown)
        {
            float progress = Mathf.Clamp((Time.realtimeSinceStartup - m_StartTime) / m_Duration, 0, 1);
            ScaleFromPivotUI.transform.localScale = Vector3.Lerp(kEndScale, kStartScale, progress);

            if (progress >= 1.0f)
            {
                ScaleFromPivotUI.enabled = false;
                m_Direction = ScaleDirection.None;
            }
        }
    }

    public void OnScaleUpClicked()
    {
        ScaleFromPivotUI.enabled = true;
        m_Direction = ScaleDirection.ScaleUp;
        m_StartTime = Time.realtimeSinceStartup;
    }

    public void OnMinimizeClicked()
    {
        m_StartTime = Time.realtimeSinceStartup;
        m_Direction = ScaleDirection.ScaleDown;
    }
}

using UnityEngine;
using UnityEngine.UI;

public class ScaleController : MonoBehaviour
{
    public Image ScaleTarget;
    public float m_Duration;

    private float m_StartTime;

    private readonly Vector3 kStartScale = new(0, 0, 0);
    private readonly Vector3 kEndScale = Vector3.one;

    enum ScaleDirection
    {
        None,
        Scale
    };

    private ScaleDirection m_Direction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ScaleTarget.enabled = false;
        m_Direction = ScaleDirection.None;
    }

    // Update is called once per frame
    void Update()
    {
        if(m_Direction == ScaleDirection.Scale)
        {
            float progress = Mathf.Clamp((Time.realtimeSinceStartup - m_StartTime) / m_Duration, 0, 1);
            ScaleTarget.transform.localScale = Vector3.Lerp(kStartScale, kEndScale, progress);
            
            if(progress >= 1.0f)
                m_Direction = ScaleDirection.None;
        }
    }

    public void OnScaleClicked()
    {
        ScaleTarget.enabled = true;
        m_Direction = ScaleDirection.Scale;
        m_StartTime = Time.realtimeSinceStartup;
    }
}

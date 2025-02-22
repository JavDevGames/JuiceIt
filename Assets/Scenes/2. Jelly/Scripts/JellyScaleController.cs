using System;
using UnityEngine;
using UnityEngine.UI;

public class JellyScaleController: MonoBehaviour
{
    public Image ScaleTarget;
    public float m_Duration;

    [SerializeField]
    public AnimationCurve m_ScaleXCurve;

    [SerializeField]
    public AnimationCurve m_ScaleYCurve;

    private float m_StartTime;

    private readonly Vector3 kStartScale = new(0, 0, 0);
    private readonly Vector3 kEndScale = Vector3.one;

    private static Vector3 SCALE_HELPER = new Vector3(0, 0, 0);
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
            
            float scaleX = m_ScaleXCurve.Evaluate(progress);
            float scaleY = m_ScaleYCurve.Evaluate(progress);
            SCALE_HELPER.x = scaleX;
            SCALE_HELPER.y = scaleY;

            ScaleTarget.transform.localScale = SCALE_HELPER;
            
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

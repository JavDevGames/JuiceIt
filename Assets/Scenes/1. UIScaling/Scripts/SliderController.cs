using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    public Canvas SliderUI;
    public Slider AnimatedSlider;
    public float m_Duration;

    private float m_StartTime;
    private float m_CurrentValue;
    private float m_TargetValue;

    bool m_Animate;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SliderUI.enabled = false;
        m_Animate = false;
        m_CurrentValue = 0; 
        m_TargetValue = 0; 
    }

    // Update is called once per frame
    void Update()
    {
        if(m_Animate)
        {
            float progress = Mathf.Clamp((Time.realtimeSinceStartup - m_StartTime) / m_Duration, 0, 1);
            AnimatedSlider.value = Mathf.Lerp(m_CurrentValue, m_TargetValue, progress);

            if (progress >= 1.0f)
            {
                m_Animate = false;
                m_CurrentValue = m_TargetValue;
            }
        }
    }

    public void OnSliderClicked()
    {
        SliderUI.enabled = true;
        m_StartTime = Time.realtimeSinceStartup;
        m_CurrentValue = AnimatedSlider.value;
    }

    public void OnSliderValueChanged()
    {
        if (!m_Animate)
        {
            m_StartTime = Time.realtimeSinceStartup;
            m_TargetValue = AnimatedSlider.value;
            m_Animate = true;
        }
    }
}

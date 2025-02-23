using UnityEngine;
using UnityEngine.UI;

public class TrailsController : MonoBehaviour
{
    public GameObject m_Ball;
    public Toggle m_Toggle;

    private TrailRenderer m_TrialRenderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_TrialRenderer = m_Ball.GetComponent<TrailRenderer>();
        m_TrialRenderer.emitting = m_Toggle.isOn;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnToggleValueChanged()
    {
        m_TrialRenderer.emitting = m_Toggle.isOn;
    }
}

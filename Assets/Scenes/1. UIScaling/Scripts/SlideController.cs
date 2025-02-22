using UnityEngine;
using UnityEngine.UI;

public class SlideController : MonoBehaviour
{
    public Canvas SlideUI;
    public float m_Duration;

    private float m_StartTime;

    private Vector3 StartPosition = new(0, 0, 0);
    private Vector3 CenterPosition = new(0, 0, 0);
    private Vector3 EndPosition = Vector3.one;

    enum SlideDirection
    {
        None,
        SlideIn,
        SlideOut
    };

    private SlideDirection m_Direction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SlideUI.enabled = false;
        m_Direction = SlideDirection.None;

        StartPosition = new Vector3(SlideUI.transform.position.x,
                                    0,
                                    0);
        
        CenterPosition = new Vector3(SlideUI.transform.position.x,
                                    SlideUI.transform.position.y,
                                    0);
        
        EndPosition = new Vector3(SlideUI.transform.position.x,
                                    Screen.height + SlideUI.pixelRect.height,
                                    0);
    }

    // Update is called once per frame
    void Update()
    {
        if(m_Direction == SlideDirection.SlideIn)
        {
            float progress = Mathf.Clamp((Time.realtimeSinceStartup - m_StartTime) / m_Duration, 0, 1);
            SlideUI.transform.position = Vector3.Lerp(StartPosition, CenterPosition, progress);
            
            if(progress >= 1.0f)
                m_Direction = SlideDirection.None;
        }
        else if (m_Direction == SlideDirection.SlideOut)
        {
            float progress = Mathf.Clamp((Time.realtimeSinceStartup - m_StartTime) / m_Duration, 0, 1);
            SlideUI.transform.position = Vector3.Lerp(CenterPosition, EndPosition, progress);

            if (progress >= 1.0f)
            {
                SlideUI.enabled = false;
                m_Direction = SlideDirection.None;
            }
        }
    }

    public void OnSlideInClicked()
    {
        SlideUI.enabled = true;
        m_Direction = SlideDirection.SlideIn;
        m_StartTime = Time.realtimeSinceStartup;
    }

    public void OnSlideOutClicked()
    {
        m_StartTime = Time.realtimeSinceStartup;
        m_Direction = SlideDirection.SlideOut;
    }
}

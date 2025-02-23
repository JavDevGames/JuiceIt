using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CountUpController: MonoBehaviour
{
    public Image CountUpUI;
    public GameObject m_TargetUI;

    public float m_TargetScore;
    public float m_Duration;

    private TextMeshProUGUI m_TextMeshPro;
    private float m_StartTime;

    bool m_CountUp;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CountUpUI.gameObject.SetActive(false);
        m_TextMeshPro = m_TargetUI.GetComponent<TextMeshProUGUI>();
        m_CountUp = false;
    }

    // Update is called once per frame
    void Update()
    {
       if(m_CountUp)
       { 
            float progress = Mathf.Clamp((Time.realtimeSinceStartup - m_StartTime) / m_Duration, 0, 1);
            int score = (int) Mathf.Lerp(0, m_TargetScore, progress);
            m_TextMeshPro.text = score.ToString();

            if (progress >= 1.0f)
            {
                m_CountUp = false;
            }
        }
    }

    public void OnShowUIClicked()
    {
        CountUpUI.gameObject.SetActive(true);
        m_CountUp = true;
        m_StartTime = Time.realtimeSinceStartup;
    }

    public void OnHideUIClicked()
    {
        CountUpUI.gameObject.SetActive(false);
    }
}

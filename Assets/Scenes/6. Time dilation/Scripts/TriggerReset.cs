using UnityEngine;

public class TriggerReset : MonoBehaviour
{
    public GameObject m_Ball;
    public Vector2 m_Area;

    private Vector3 m_BallStart;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_BallStart = m_Ball.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Reset the ball position
        Vector3 randomPos = new Vector3(m_BallStart.x + Random.Range(-m_Area.x, m_Area.x), m_BallStart.y + Random.Range(-m_Area.y, m_Area.y), 0);
        m_Ball.transform.position = randomPos;
    }
}

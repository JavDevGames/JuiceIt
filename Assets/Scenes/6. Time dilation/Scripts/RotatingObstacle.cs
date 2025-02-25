using UnityEngine;

public class RotatingObstacle : MonoBehaviour
{
    public GameObject m_Target;
    public float m_Speed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var curRotation = m_Target.transform.rotation.eulerAngles;
        curRotation.z = m_Speed * Time.deltaTime;
        m_Target.transform.Rotate(curRotation);
    }
}

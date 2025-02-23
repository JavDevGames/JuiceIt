using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterAnimation : MonoBehaviour
{
    public float scrollSpeedY;
    public float scrollSpeedX;

    private Vector2 m_ScrollSpeed;
    private Material m_Material;
    // Start is called before the first frame update
    void Start()
    {
        m_ScrollSpeed = new Vector2(scrollSpeedX,scrollSpeedY);
        m_Material = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        m_ScrollSpeed.x = (m_ScrollSpeed.x + (scrollSpeedX * Time.deltaTime)) % 1.0f;
        m_ScrollSpeed.y = (m_ScrollSpeed.y + (scrollSpeedY * Time.deltaTime)) % 1.0f;

        m_Material.SetTextureOffset("_MainTex", m_ScrollSpeed);
    }
}

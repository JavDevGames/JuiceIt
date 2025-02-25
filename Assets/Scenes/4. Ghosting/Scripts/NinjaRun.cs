using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaRun : MonoBehaviour
{
    public Sprite [] sprites;
    public float duration;
    private int m_CurIndex;
    private float m_LastTime;
    private SpriteRenderer m_SpriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        m_LastTime = Time.realtimeSinceStartup;
        m_SpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        var diff = Time.realtimeSinceStartup - m_LastTime;

        if (diff > duration)
        {
            m_CurIndex = (m_CurIndex + 1) % sprites.Length;
            m_LastTime = Time.realtimeSinceStartup;
            m_SpriteRenderer.sprite = sprites[m_CurIndex];
        }
    }
}

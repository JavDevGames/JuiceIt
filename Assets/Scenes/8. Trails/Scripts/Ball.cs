using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public Vector2 velocity;
    public Bounds bounds;
    public Vector2 m_StartPos;
    public GameObject humanPaddle;
    public GameObject aiPaddle;
    public GameObject levelBounds;
    public float radius;

    void Start()
    {
        m_StartPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        var displacementX = velocity.x * Time.deltaTime;
        var displacementY = velocity.y * Time.deltaTime;

        // Check if we're out of bounds
        if (transform.position.y + displacementY > bounds.upperBound)
            velocity.y = -velocity.y;
        else if(transform.position.y + displacementY < bounds.lowerBound)
            velocity.y = -velocity.y;

        // Check if we hit the player's paddle
        var humanPaddleBounds = humanPaddle.GetComponent<SpriteRenderer>().bounds;
        float targetX = (transform.position.x + displacementX) - radius;

        if (targetX < humanPaddleBounds.max.x 
            && (transform.position.y) < humanPaddleBounds.max.y
            && (transform.position.y) > humanPaddleBounds.min.y)
            velocity.x = -velocity.x;

        // Check if we hit the AI's paddle
        var aiPaddleBounds = aiPaddle.GetComponent<SpriteRenderer>().bounds;
        targetX = (transform.position.x + displacementX) + radius;

        if (targetX > aiPaddleBounds.min.x
            && (transform.position.y) < aiPaddleBounds.max.y
            && (transform.position.y) > aiPaddleBounds.min.y)
            velocity.x = -velocity.x;

        transform.Translate(
            velocity.x * Time.deltaTime
            , velocity.y * Time.deltaTime
            , 0);

        var finalBounds = levelBounds.GetComponent<SpriteRenderer>().bounds;

        // Out of bounds
        if (!finalBounds.Intersects(GetComponent<SpriteRenderer>().bounds))
        {
            transform.position = m_StartPos;

        }

    }
}

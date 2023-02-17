using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollBackground : MonoBehaviour
{
    private BoxCollider2D m_Background1;

    private Rigidbody2D m_RB1;

    private float m_Width;

    [SerializeField]
    private float m_speed = -3;

    private void Start()
    {
        m_Background1 = GetComponent<BoxCollider2D>();
        m_RB1 = GetComponent<Rigidbody2D>();

        m_Width = m_Background1.size.x;
        m_RB1.velocity = new Vector2(m_speed, 0);
    }

    private void Update()
    {
        if (transform.position.x <- m_Width)
        {
            MoveBackground();
        }
    }

    private void MoveBackground()
    {
        Vector2 vector = new Vector2(m_Width * 2f, 0);
        transform.position = (Vector2)transform.position + vector;
    }

}

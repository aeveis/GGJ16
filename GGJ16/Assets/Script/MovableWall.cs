using UnityEngine;
using System.Collections;

public class MovableWall : MonoBehaviour
{
    public float m_Speed = 1.0f;
    public float m_Amount = 2.0f;
    public Vector3 m_Direction;
    private Vector3 m_InitialPosition;

    // Use this for initialization
    void Start()
    {
        m_InitialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = m_InitialPosition + m_Direction.normalized * Mathf.PingPong(Time.time * m_Speed, m_Amount);
    }
}

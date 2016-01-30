using UnityEngine;
using System.Collections;

public class Civilian : MonoBehaviour
{

    public Ritual m_Ritual;

    public float m_Speed = 1.0f;
    public Vector3 m_MoveDir;

    //movement att
    private bool m_IsMoving;
    private float m_ElapsedTimeMoving;
    private float m_ElapsedTimeStopped;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        m_Ritual.Action(transform);

        Move();
    }

    void Move()
    {
        if (!m_IsMoving)
        {
            m_ElapsedTimeStopped += Time.deltaTime;
            if (m_ElapsedTimeStopped > 1.0f)
            {
                m_MoveDir = new Vector3(Random.Range(-1.0f, 1.0f), 0.0f, Random.Range(-1.0f, 1.0f));

                if (m_MoveDir.x < -0.5f)
                    m_MoveDir.x = -1.0f;
                else if (m_MoveDir.x > 0.5f)
                    m_MoveDir.x = 1.0f;
                else
                    m_MoveDir.x = 0.0f;

                if (m_MoveDir.z < -0.5f)
                    m_MoveDir.z = -1.0f;
                else if (m_MoveDir.z > 0.5f)
                    m_MoveDir.z = 1.0f;
                else
                    m_MoveDir.z = 0.0f;

                m_ElapsedTimeMoving = 0.0f;
                m_IsMoving = true;
            }
        }

        if (m_IsMoving)
        {
            transform.position += m_MoveDir.normalized * m_Speed * Time.deltaTime;

            if (m_MoveDir != Vector3.zero)
                transform.rotation = Quaternion.LookRotation(m_MoveDir);

            m_ElapsedTimeMoving += Time.deltaTime;
            if (m_ElapsedTimeMoving > 1.0f)
            {
                m_IsMoving = false;
                m_ElapsedTimeStopped = 0.0f;
            }
        }
    }

    bool CheckObstacle()
    {
        return false;
    }
}

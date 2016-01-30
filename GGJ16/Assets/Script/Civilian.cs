using UnityEngine;
using System.Collections;

public class Civilian : MonoBehaviour
{

    public Ritual m_Ritual;

    public float m_Speed = 1.0f;
    public Vector3 m_MoveDir;

    public bool m_EnableMovement;

    //movement att
    private bool m_IsMoving;
    private float m_ElapsedTimeMoving;
    private float m_ElapsedTimeStopped;
    public float m_WaitTime = 1.0f;
    public float m_MoveTime = 1.0f;
    public float m_HitDistance = 1.0f;

    public float m_GroundLevel = 0.6f;

    // Use this for initialization
    void Start()
    {
        m_Ritual = Instantiate(m_Ritual);
    }

    // Update is called once per frame
    void Update()
    {
        m_Ritual.Action(transform);

        if(m_EnableMovement)
            Move();
    }

    void Move()
    {
        if (!m_IsMoving)
        {
            m_ElapsedTimeStopped += Time.deltaTime;
            if (m_ElapsedTimeStopped > m_WaitTime)
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
            if (m_ElapsedTimeMoving > m_MoveTime || CheckObstacle())
            {
                m_IsMoving = false;
                m_ElapsedTimeStopped = 0.0f;
            }
        }
    }

    bool CheckObstacle()
    {
        RaycastHit hit;
        //Debug.DrawRay(transform.position, transform.forward, Color.red);
        return Physics.Raycast(transform.position, transform.forward, out hit, m_HitDistance);
    }
}

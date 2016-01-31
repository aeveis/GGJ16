using UnityEngine;
using System.Collections;

public class Civilian : MonoBehaviour
{

    public Ritual m_Ritual;

    public float m_Speed = 1.0f;
    public float m_PatrolSpeed = 2.0f;
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

    //patrolling
    private float m_TimeOfArrival;
    private bool m_Reversed;
    private int m_CurrentPathIndex;
    private int m_LastPathIndex;
    public PatrolPoint[] m_PatrolPath;

    public enum MovementType
    {
        Random,
        Loop,
        PingPong
    }
    public MovementType m_CurrentMovementType = MovementType.Random;

    // Use this for initialization
    void Start()
    {
        m_Ritual = Instantiate(m_Ritual);


        if (m_CurrentMovementType != MovementType.Random)
            m_IsMoving = m_EnableMovement;
    }

    // Update is called once per frame
    void Update()
    {
        m_Ritual.Action(transform);

        if (m_EnableMovement)
        {
            if (m_CurrentMovementType == MovementType.Random)
                Move();
            else
                MovePatrol();
        }
    }

    void MovePatrol()
    {
        if (m_IsMoving)
        {
            Vector3 dir = m_PatrolPath[m_CurrentPathIndex].transform.position - transform.position;

            if (Time.time - m_TimeOfArrival > m_PatrolPath[m_LastPathIndex].m_WaitTime)
            {
                transform.position += dir.normalized * m_PatrolSpeed * Time.deltaTime;
                if (dir != Vector3.zero)
                    transform.rotation = Quaternion.LookRotation(dir);
            }

            if (dir.sqrMagnitude < 0.2f * 0.2f)
            {
                //snap pos and rotation
                transform.position = m_PatrolPath[m_CurrentPathIndex].transform.position;
                transform.forward = m_PatrolPath[m_CurrentPathIndex].transform.forward;
                m_TimeOfArrival = Time.time;
                GetNextPath();
            }
        }
    }

    float GetCurrentWaitTime()
    {
        int indexWait = m_CurrentPathIndex - 1;
        if (indexWait < 0)
            indexWait = m_PatrolPath.Length - 1;

        return m_PatrolPath[indexWait].m_WaitTime;
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

    void GetNextPath()
    {
        m_LastPathIndex = m_CurrentPathIndex;

        if (m_CurrentMovementType == MovementType.Loop)
        {
            m_CurrentPathIndex++;
            if (m_CurrentPathIndex >= m_PatrolPath.Length)
            {
                m_CurrentPathIndex = 0;
            }
        }
        else if (m_CurrentMovementType == MovementType.PingPong)
        {
            if (!m_Reversed)
            {
                m_CurrentPathIndex++;
                if (m_CurrentPathIndex >= m_PatrolPath.Length)
                {
                    m_CurrentPathIndex = m_PatrolPath.Length - 2;
                    m_Reversed = !m_Reversed;
                }
            }
            else
            {
                m_CurrentPathIndex--;
                if (m_CurrentPathIndex < 0)
                {
                    m_CurrentPathIndex = 1;
                    m_Reversed = !m_Reversed;
                }
            }
        }
        else
        {
            //do nothing
        }
    }
}

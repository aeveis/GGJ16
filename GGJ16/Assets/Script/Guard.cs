using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Guard : MonoBehaviour
{
    public Ritual m_Ritual;

    public float m_Dist = 10.0f;
    public float m_Angle = 90.0f;

    public GameObject m_ExclamationMark;

    //movement att
    private float m_TimeOfArrival;
    public float m_Speed = 5.0f;
    public bool m_IsMoving;
    private int m_CurrentPathIndex;
    private int m_LastPathIndex;
    public PatrolPoint[] m_PatrolPath;

    private bool m_IsPlayerOnSight;

    private bool m_Reversed;

    private bool m_IsCaught;
    public AudioClip[] m_CaughtClips;

    public enum MovementType
    {
        Loop,
        PingPong
    }
    public MovementType m_CurrentMovementType = MovementType.Loop;


    // Use this for initialization
    void Start()
    {
        m_LastPathIndex = 0;
        m_CurrentPathIndex = 0;

        foreach (PatrolPoint p in m_PatrolPath)
            p.GetComponent<Renderer>().enabled = false;

        m_Ritual = Instantiate(m_Ritual);
    }

    // Update is called once per frame
    void Update()
    {
        bool isOnSight = CheckPlayerIsOnSight();

        if (isOnSight && !m_IsPlayerOnSight)
            m_Ritual.OnVisionConeEnter();
        if (!isOnSight && m_IsPlayerOnSight)
            m_Ritual.OnVisionConeExit();

        m_IsPlayerOnSight = isOnSight;

        m_ExclamationMark.SetActive(isOnSight);
        if (m_IsPlayerOnSight && !m_Ritual.Check(transform))
        {
            //Debug.Log("RESET!!!");
            if (!m_IsCaught)
            {
                m_IsCaught = true;
                StartCoroutine(Caught());
            }
            
            //StartCoroutine(ReloadLevel());
        }

        if(m_PatrolPath.Length > 0)
            Move();
    }

    IEnumerator Caught()
    {
        //Time.timeScale = 0.0f;

        Civilian[] allcivs = GameObject.FindObjectsOfType<Civilian>();
        foreach (Civilian c in allcivs)
            c.Stop();

        yield return null;

       

        float timeToWait = 1.0f;
        if (m_CaughtClips.Length > 0)
        {
            AudioClip clip = m_CaughtClips[UnityEngine.Random.Range(0, m_CaughtClips.Length - 1)];
            timeToWait = clip.length;
            SoundManager.instance.PlaySingle(clip);
        }

        float time = 0.0f;
        while (time < timeToWait)
        {
            time += Time.deltaTime;

            float val = Camera.main.orthographicSize - Time.deltaTime * 20.0f;
            if (val < 5.0f)
                val = 5.0f;

            Camera.main.orthographicSize = val;
            yield return null;
        }


        m_IsCaught = false;
        Application.LoadLevel(Application.loadedLevel);
        //SceneController.Instance.ReloadLevel();
    }

    private bool CheckPlayerIsOnSight()
    {
        float dist = (PlayerController.Instance.transform.position - transform.position).magnitude;
        //Debug.Log("Dist ["+ dist + "] MaxDist ["+ m_Dist + "]");
        if (dist < m_Dist)
        {
            Vector3 project = new Vector3(PlayerController.Instance.transform.position.x, transform.position.y, PlayerController.Instance.transform.position.z);
            Vector3 dirToPlayer = (PlayerController.Instance.transform.position - transform.position).normalized;
            Vector3 dirToPlayerProj = (project - transform.position).normalized;
            float angle = Vector3.Angle(transform.forward, dirToPlayerProj);
            //Debug.Log(angle);
            if (angle < m_Angle) 
            {
                RaycastHit hit;
                //Debug.DrawRay(transform.position + Vector3.up, dirToPlayer * m_Dist, Color.red);
                if (Physics.Raycast(transform.position, dirToPlayer, out hit, m_Dist, 1 << LayerMask.NameToLayer("Default")))
                {
                    //Debug.Log(hit.transform.name);
                    return (hit.transform.tag == "Player") ? true : false;
                }
            }
        }
        return false;
    }

    void Move()
    {
        if (m_IsMoving)
        {
            Vector3 dir = m_PatrolPath[m_CurrentPathIndex].transform.position - transform.position;

            if (Time.time - m_TimeOfArrival > m_PatrolPath[m_LastPathIndex].m_WaitTime)
            {
                transform.position += dir.normalized * m_Speed * Time.deltaTime;
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
        else
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
    }
}

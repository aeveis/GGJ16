using UnityEngine;
using System.Collections;
using System;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }

    public Vector3 m_MoveDir;
    public float m_Speed = 0.1f;
    public float m_Acceleration = 10.0f;
    private float m_CurrentSpeed;

    public float m_JumpForce = 50.0f;
    private bool m_IsJumping;
    public float m_GroundLevel = 0.6f;
    public float m_FallSpeed = 10.0f;

    public float m_HitDistance = 1.0f;

    private Rigidbody m_Rigidbody;

    public float m_SpinningSpeed = 2.0f;
    private bool m_Spinning;

	//keyboard control tweaks
	private float keyb_timer = .2f;

    public Transform[] m_SpawnPoints;

    private Quaternion m_InitialRotation;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //Object.DontDestroyOnLoad(gameObject);
        }
        else
        {
            Debug.LogWarning("PlayerController Instance already exists.");
            Destroy(gameObject);
        }
    }

    public bool IsJumping()
    {
        return m_IsJumping;
    }

    public bool IsMoving()
    {
        return (m_MoveDir != Vector3.zero && m_CurrentSpeed > 0.0f);
    }

    public bool IsSpinning()
    {
        return m_Spinning;
    }

    // Use this for initialization
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        if (!m_Rigidbody)
            m_Rigidbody = gameObject.AddComponent<Rigidbody>();

        m_InitialRotation = transform.rotation;

        Reset();
    }

    // Update is called once per frame
    void Update()
    {
        m_MoveDir = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));

		float moveThreshold = 0.5f;

		//make keyboard less sensitive
		if (Input.anyKey || keyb_timer>0) {
			moveThreshold = 0.8f;
			keyb_timer -= Time.deltaTime;
			if (Input.anyKey)
				keyb_timer = .2f;
		}
		
		if (m_MoveDir.x < -moveThreshold)
            m_MoveDir.x = -1.0f;
		else if (m_MoveDir.x > moveThreshold)
            m_MoveDir.x = 1.0f;
        else
            m_MoveDir.x = 0.0f;

		if (m_MoveDir.z < -moveThreshold)
            m_MoveDir.z = -1.0f;
		else if (m_MoveDir.z > moveThreshold)
            m_MoveDir.z = 1.0f;
        else
            m_MoveDir.z = 0.0f;

        if (m_MoveDir != Vector3.zero && !m_Spinning)
            transform.rotation = Quaternion.LookRotation(m_MoveDir);
        else if (m_MoveDir == Vector3.zero)
            m_CurrentSpeed = 0.0f;

        if (!CheckObstacle())
        {
            m_CurrentSpeed += Time.deltaTime * Time.deltaTime * m_Acceleration;
            if (m_CurrentSpeed > m_Speed)
                m_CurrentSpeed = m_Speed;

            transform.position += m_MoveDir.normalized * m_CurrentSpeed * Time.deltaTime;
        }




		if (transform.position.y > m_GroundLevel)
        {
            m_IsJumping = true;
            m_Rigidbody.velocity += Vector3.down * Time.deltaTime * m_FallSpeed;
        }
        else if (Input.GetButtonDown("Jump") && transform.position.y <= m_GroundLevel)
        {
            m_Rigidbody.velocity = Vector3.up * m_JumpForce;
            //m_Rigidbody.AddForce(Vector3.up * m_JumpForce);
            m_IsJumping = true;
        }
        else
        {
            m_IsJumping = false;
        }


        if (Input.GetButtonDown("Fire1") && !m_Spinning)
            StartCoroutine(Spin());
    }

    bool CheckObstacle()
    {
        RaycastHit hit;
        //Debug.DrawRay(transform.position, transform.forward, Color.red);
        return Physics.Raycast(transform.position, transform.forward, out hit, m_HitDistance);
    }



    IEnumerator Spin()
    {
        m_Spinning = true;

        float time = 0.0f;
        while (time < 1.0f)
        {
            time += Time.deltaTime * m_SpinningSpeed;
            transform.Rotate(Vector3.up, 20.0f);
            yield return null;
        }


        transform.rotation = m_InitialRotation;
        m_Spinning = false;
    }

    public bool CheckGuardIsOnSight(Guard p_Guard)
    {
        float dist = (p_Guard.transform.position - transform.position).magnitude;
        //Debug.Log("Dist ["+ dist + "] MaxDist ["+ m_Dist + "]");
        if (dist < p_Guard.m_Dist)
        {
            Vector3 project = new Vector3(p_Guard.transform.position.x, transform.position.y, p_Guard.transform.position.z);
            Vector3 dirToGuard = (p_Guard.transform.position - transform.position).normalized;
            Vector3 dirToGuardProj = (project - transform.position).normalized;
            float angle = Vector3.Angle(transform.forward, dirToGuardProj);
            //Debug.Log(angle);
            if (angle < p_Guard.m_Angle)
            {
                RaycastHit hit;
                //Debug.DrawRay(transform.position + Vector3.up, dirToGuard * p_Guard.m_Dist, Color.red);
                if (Physics.Raycast(transform.position, dirToGuard, out hit, p_Guard.m_Dist))
                    return (hit.transform.tag == "Guard") ? true : false;
            }
        }
        return false;
    }

    //public bool CheckGuardIsOnSight(Guard p_Guard)
    //{
    //    //m_Flashlight.spotAngle
    //    float dist = (p_Guard.transform.position - transform.position).magnitude;
    //    if (dist < p_Guard.m_Dist)
    //    {
    //        Vector3 dirToPlayer = (p_Guard.transform.position - transform.position).normalized;
    //        float angle = Vector3.Angle(transform.forward, dirToPlayer);
    //        if (angle < p_Guard.m_Angle)
    //        {
    //            RaycastHit hit;
    //            //Debug.DrawRay(transform.position, dirToPlayer, Color.red);
    //            if (Physics.Raycast(transform.position, dirToPlayer, out hit, p_Guard.m_Dist))
    //                return (hit.transform.tag == "Guard") ? true : false;
    //        }
    //    }
    //    return false;
    //}

    public void Reset()
    {
        if (m_SpawnPoints.Length > 0)
            transform.position = m_SpawnPoints[UnityEngine.Random.Range(0, m_SpawnPoints.Length)].position;
    }
}


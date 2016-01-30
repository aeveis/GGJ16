using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }

    public Vector3 m_MoveDir;
    public float m_Speed = 0.1f;


    public float m_JumpForce = 50.0f;
    private bool m_IsJumping;
    public float m_GroundLevel = 0.6f;
    public float m_FallSpeed = 10.0f;


    private Rigidbody m_Rigidbody;

    public float m_SpinningSpeed = 2.0f;
    private bool m_Spinning;

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

        transform.position += m_MoveDir.normalized * m_Speed * Time.deltaTime;

        if(m_MoveDir != Vector3.zero && !m_Spinning)
            transform.rotation = Quaternion.LookRotation(m_MoveDir);


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

    public void Reset()
    {
        if (m_SpawnPoints.Length > 0)
            transform.position = m_SpawnPoints[Random.Range(0, m_SpawnPoints.Length)].position;
    }
}


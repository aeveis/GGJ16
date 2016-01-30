using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }

    public Vector3 m_MoveDir;
    public float m_Speed = 0.1f;
    public float m_JumpForce = 50.0f;
    public float m_GroundLevel = 0.6f;

    private Rigidbody m_Rigidbody;

    public float m_SpinningSpeed = 2.0f;
    private bool m_Spinning;

    private Quaternion m_LastRotation;

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

    // Use this for initialization
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        if (!m_Rigidbody)
            m_Rigidbody = gameObject.AddComponent<Rigidbody>();

        m_InitialRotation = transform.rotation;
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

        transform.position += m_MoveDir.normalized * m_Speed;

        if(m_MoveDir != Vector3.zero && !m_Spinning)
            transform.rotation = Quaternion.LookRotation(m_MoveDir);

        m_LastRotation = transform.rotation;


        if (Input.GetButton("Jump") && transform.position.y < m_GroundLevel)
            m_Rigidbody.AddForce(Vector3.up * m_JumpForce);

        if (Input.GetButton("Fire1") && !m_Spinning)
            StartCoroutine(Spin());
    }

    Vector3 SnapTo(Vector3 v3, float snapAngle)
    {
        float angle = Vector3.Angle(v3, Vector3.up);
        if (angle < snapAngle / 2.0f)          // Cannot do cross product 
            return Vector3.up * v3.magnitude;  //   with angles 0 & 180
        if (angle > 180.0f - snapAngle / 2.0f)
            return Vector3.down * v3.magnitude;

        float t = Mathf.Round(angle / snapAngle);
        float deltaAngle = (t * snapAngle) - angle;

        Vector3 axis = Vector3.Cross(Vector3.up, v3);
        Quaternion q = Quaternion.AngleAxis(deltaAngle, axis);
        return q * v3;
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

}


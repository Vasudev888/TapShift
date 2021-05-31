using UnityEngine;


[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(ParticleSystem))]
public class PlayerController : MonoBehaviour
{
    public float smooth;
    public float jumpForce;
    public float gravityAccelaration;
    public float maxGravity;
    public float tresholdValue;

    private SpriteRenderer m_Sprite;
    private ParticleSystem m_Particle;
    private Vector3 m_TargetPosition;
    private float m_DownwardVelocity;
    private bool m_Pause;


    #region public Functions

    public void OnInit()
    {
        m_TargetPosition = transform.position;
        m_Sprite = GetComponent<SpriteRenderer>();
        m_Particle = GetComponent<ParticleSystem>();
    }

    public void OnUpdate()
    {
        if (m_Pause) return;
        Jump();
        Fall();
        Move();
    }

    public void Reset()
    {
        m_TargetPosition = Vector3.up * -3;
        transform.position = m_TargetPosition;
        m_Pause = false;
        m_Sprite.enabled = true;
    }

    public void Pause()
    {
        m_Sprite.enabled = false;
        m_Particle.Play();
        m_Pause = true;
    }

    #endregion

    #region private Functions

    private void Jump()
    {
        if (Input.GetMouseButtonUp(0))
        {
            m_TargetPosition.y = transform.position.y + jumpForce;
            m_DownwardVelocity = 0;
        }
    }

    private void Fall()
    {
        m_DownwardVelocity += gravityAccelaration;
        m_DownwardVelocity = Mathf.Clamp(m_DownwardVelocity, 0, maxGravity);
        m_TargetPosition.y -= m_DownwardVelocity * Time.deltaTime;
        if(m_TargetPosition.y < tresholdValue)
        {
            m_TargetPosition.y = tresholdValue;
        }
    }

    private void Move()
    {
        transform.position = Vector3.Lerp(transform.position, m_TargetPosition, smooth * Time.deltaTime);
    }

    #endregion
}

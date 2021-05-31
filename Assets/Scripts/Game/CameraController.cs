using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour
{

    public Transform player;
    public float smooth;
    
    
    private Camera m_Camera;
    private Vector3 m_TargetPosition;
    private Vector3 m_InitialPosition;


    #region public Functions

    public void OnInit()
    {
        m_Camera = GetComponent<Camera>();
        m_TargetPosition = transform.position;
        m_InitialPosition = m_TargetPosition;
    }

    public void OnUpdate()
    {
        FollowPlayer();
    }

    public void Reset()
    {
        m_TargetPosition = m_InitialPosition;
        transform.position = m_TargetPosition;
    }
    #endregion


    #region Private Functions
    private void FollowPlayer()
    {
        if(!player)
        {
            Debug.Log("Camera could not find the reference to the player");
            return;
        }

        if(player.transform.position.y > transform.position.y)
        {
            m_TargetPosition.y = player.position.y;
        }

        transform.position = Vector3.Lerp(transform.position, m_TargetPosition, smooth * Time.deltaTime);
    }

    #endregion

}

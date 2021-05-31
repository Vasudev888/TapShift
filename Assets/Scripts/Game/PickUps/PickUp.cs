using UnityEngine;

public class PickUp : MonoBehaviour
{
    private GameController m_Game;
    private bool m_DidCollect;

    // get GameController with integrity
    protected GameController game
    {
        get
        {
            if (!m_Game)
            {
                m_Game = GameController.instance;
            }
            if (!m_Game)
            {
                Debug.LogWarning("Pick is trying to access the game, but no instance of GameController was found");
            }
            return m_Game;
        }
    }

    #region Unity Functions
    private void OnTriggerEnter2D(Collider2D _col)
    {
        if (!game) return;
        if (m_DidCollect) return;
        if (_col.gameObject.tag.Equals("Player"))
        {
            m_DidCollect = true;
            OnPlayerCollect();
            Destroy(gameObject);
        }
    }
    #endregion

    #region Override Functions
    protected virtual void OnPlayerCollect()
    {
        Debug.Log("Player picked up ["+gameObject.name+"].");
    }
    #endregion
}

using UnityEngine;

public class Obstacle : MonoBehaviour
{


    #region Unity Functions
    private void OnTriggerEnter2D(Collider2D _col)
    {
        if (_col.gameObject.tag.Equals("Player"))
        {
            GameController.instance.OnPlayerHitObstacle();
        }
    }

    #endregion
}

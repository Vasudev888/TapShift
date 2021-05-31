using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    public Transform gameMap;
    public GameObject[] obstacles;
    public GameObject[] pickUps;
    public float interval = 5.0f;

    private List<GameObject> m_Obstacles;
    private List<GameObject> m_PickUps;

    #region Unity Functions
    private void Awake()
    {
        m_Obstacles = new List<GameObject>();
        m_PickUps = new List<GameObject>();
    }
    #endregion

    #region public Functions
    public void AddObstacle(int _progress)
    {
        GameObject _prefab = GetRandomObstacle(obstacles);
        if (!_prefab)
        {
            return;
        }

        GameObject _new = Instantiate(_prefab);
        _new.transform.parent = gameMap;
        float _y = interval * (_progress + 1);
        _new.transform.position = Vector3.up * _y;

        m_Obstacles.Insert(0, _new);

        if(m_Obstacles.Count > 4)
        {
            Destroy(m_Obstacles[m_Obstacles.Count - 1]);
            m_Obstacles.RemoveAt(m_Obstacles.Count - 1);
        }

    }

    public void AddPickup(int _progress)
    {
        GameObject _prefab = GetRandomObstacle(pickUps);
        if (!_prefab)
        {
            return;
        }

        GameObject _new = Instantiate(_prefab);
        _new.transform.parent = gameMap;
        float _y = interval * (_progress + 1) + interval * 0.5f;
        _new.transform.position = Vector3.up * _y;

        m_PickUps.Insert(0, _new);

        if (m_PickUps.Count > 4)
        {
            Destroy(m_PickUps[m_PickUps.Count - 1]);
            m_PickUps.RemoveAt(m_PickUps.Count - 1);
        }

    }

    public void Reset()
    {
        for(int i = m_Obstacles.Count - 1; i >= 0; i-- )
        {
            Destroy(m_Obstacles[i]);
            m_Obstacles.RemoveAt(i);
        }


        for (int i = m_PickUps.Count - 1; i >= 0; i--)
        {
            if (m_PickUps[i] != null)
            {
                Destroy(m_PickUps[i]);
            }
                m_PickUps.RemoveAt(i);
            
        }
    }
    #endregion

    #region private Functions
    private GameObject GetRandomObstacle(GameObject[] _arr)
    {
        if(_arr.Length == 0)
        {
            Debug.LogWarning("Tring to get a random obstacle, but none were found");
            return null;
        }

        int _random = Random.Range(0, _arr.Length);
        return _arr[_random];
    }
    #endregion
}

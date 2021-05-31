using System.Threading.Tasks;
using UnityEngine;
using UnityCore.Session;
using UnityCore.Menu;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    public PageController pages;
    public PlayerController player;
    public CameraController camera;
    public ObstacleController obstacles;
    public int pickupDropRate = 3;
    public int score { get; private set; }
    public Text scoreText;

    private SessionController m_Session;
    private int m_Progress = -1;
    private int m_ScoreMultiplier = 1;
    private bool m_Invincible;
    private float m_ScoreMultiplierDuration;
    private float m_InvincibilityDuration;
    private bool m_DidDropPickup;
    private bool m_GameOver;

    //get session with integrity
    private SessionController session
    {
        get
        {
            if (!m_Session)
            {
                m_Session = SessionController.instance;
            }

            if (!m_Session)
            {
                Debug.LogWarning("Game is trying ti access the session, but no instance of SessionController was found");
            }

            return m_Session;
        }
    }

    #region Unity Functions
    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
    }

    private void Start()
    {
        if (!session) return;
        session.InitializeGame(this);
    }

    #endregion


    #region public Functions
    public void OnInit()
    {
        //initialize all of our games
        player.OnInit();
        camera.OnInit();
        obstacles.AddObstacle(m_Progress);
    }

    public void OnUpdate()
    {
        //update all of our game system
        player.OnUpdate();
        camera.OnUpdate();
        CheckPlayerProgress();
        DetectPlayerFall();
    }

    public void HandleInvincibilityPickup(float _duration)
    {
        m_InvincibilityDuration = _duration;
        m_Invincible = true;

        //cancel score Pickup
        m_ScoreMultiplier = 1;
        m_ScoreMultiplierDuration = 0;
    }

    public void HandleScorePickup(int _multiplier, float _duration)
    {
        m_ScoreMultiplier = _multiplier;
        m_ScoreMultiplierDuration = _duration;

        // cancel invincibility pickup
        m_Invincible = false;
        m_InvincibilityDuration = 0;
    }

    public void OnPlayerHitObstacle()
    {
        if (m_Invincible) return;
        EndGame();
    }

    public void TryAgain()
    {
        Reset();
    }

    #endregion

    #region private Functions
    private void CheckPlayerProgress()
    {
        if(player.transform.position.y / obstacles.interval > (m_Progress + 1))
        {
            m_Progress++;
            score += m_ScoreMultiplier;
            scoreText.text = score.ToString();
            obstacles.AddObstacle(m_Progress);
        }

        if(m_Progress > 0 && m_Progress % pickupDropRate == 0)
        {
            if (!m_DidDropPickup)
            {
                m_DidDropPickup = true;
                obstacles.AddPickup(m_Progress);
            }

        }
        else
        {
            m_DidDropPickup = false;
        }
    }

    private void EnforcePickupRules()
    {
        float _dt = Time.deltaTime;
        m_InvincibilityDuration -= _dt;
        m_ScoreMultiplierDuration -= _dt;

        if(m_ScoreMultiplierDuration <= 0 && m_ScoreMultiplier != 1)
        {
            m_ScoreMultiplier = 1;
        }

        if(m_InvincibilityDuration <= 0 && m_Invincible)
        {
            m_Invincible = false;
            m_InvincibilityDuration = 0;    
        }
    }

    private void DetectPlayerFall()
    {
        if(camera.transform.position.y - player.transform.position.y > 5)
        {
            EndGame();
        }
    }

    private async void EndGame()
    {
        if (m_GameOver) return;
        m_GameOver = true;

        // pause the player
        player.Pause();
        // play animation for the player
        // turn n gameover scene
        await Task.Delay(1000);
        pages.TurnPageOn(PageType.GameOver);
        /*Reset();*/
    }

    private void Reset()
    {
        // reset the score 0
        score = 0;
        scoreText.text = score.ToString();
        m_Progress = -1;
        // reset obstacles
        obstacles.Reset();
        obstacles.AddObstacle(m_Progress);
        // reset camera
        camera.Reset();
        // reset player
        player.Reset();
        m_GameOver = false;
    }
    #endregion

}

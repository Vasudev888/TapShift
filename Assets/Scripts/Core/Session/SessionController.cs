using System.Threading.Tasks;
using UnityEngine;
using UnityCore.Menu;
using UnityCore.Scene;


namespace UnityCore {

    namespace Session {

        public class SessionController : MonoBehaviour
        {
            public static SessionController instance;

            private SceneController m_Scene;
            private long m_SessionStartTime;
            private bool m_IsPaused;
            private GameController m_Game;
            private float m_FPS;

            public long sessionStartTime {
                get {
                    return m_SessionStartTime;
                }
            }

            public float fps {
                get {
                    return m_FPS;
                }
            }

            //get data with integrity
            private SceneController scene
            {
                get
                {
                    if (!m_Scene)
                    {
                        m_Scene = SceneController.instance;
                    }

                    if (!m_Scene)
                    {
                        Debug.LogWarning("Trying to access scenes, but no instance of Scenecontroller was found");
                    }

                    return m_Scene;
                }
            }
            
#region Unity Functions
            private void Awake() {
                Configure();
            }

            private void OnApplicationFocus(bool _focus) {
                if (_focus) {
                    // Open a window to unpause the game
                    /*PageController.instance.TurnPageOn(PageType.PausePopup);*/
                } else {
                    // Flag the game paused
                    m_IsPaused = true;
                }
            }

            private void Update() {
                if (m_IsPaused) return;
                if (!m_Game) return;
                m_Game.OnUpdate();
                m_FPS = Time.frameCount / Time.time;
            }
            #endregion

            #region Public Functions
            public void InitializeGame(GameController _game)
            {
                m_Game = _game;
                m_Game.OnInit();
            }

            public void UnPause() {
                m_IsPaused = false;
            }
#endregion

#region Private Functions
            /// <summary>
            /// Initialize the singleton pattern!
            /// </summary>
            private void Configure() {
                if (!instance) {
                    instance = this;
                    StartSession();
                    DontDestroyOnLoad(gameObject);
                } else {
                    Destroy(gameObject);
                }
            }

            private  async void StartSession() {
                m_SessionStartTime = EpochSeconds();
                await Task.Delay(1000);
                if (scene)
                {
                    scene.Load(SceneType.Game, null, false, PageType.Loading);
                }
            }

            private long EpochSeconds() {
                var _epoch = new System.DateTimeOffset(System.DateTime.UtcNow);
                return _epoch.ToUnixTimeSeconds();
            }
#endregion
        }
    }
}

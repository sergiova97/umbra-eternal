using UnityEngine;
using UnityEngine.Events;

namespace UmbraEternal.Core
{
    /// <summary>
    /// GameManager central - Singleton pattern para manejar el estado global del juego
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        #region Singleton
        private static GameManager _instance;
        public static GameManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<GameManager>();
                    if (_instance == null)
                    {
                        GameObject go = new GameObject("GameManager");
                        _instance = go.AddComponent<GameManager>();
                        DontDestroyOnLoad(go);
                    }
                }
                return _instance;
            }
        }

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
                InitializeGame();
            }
            else if (_instance != this)
            {
                Destroy(gameObject);
            }
        }
        #endregion

        #region Game State
        public enum GameState
        {
            MainMenu,
            HeroSelection,
            RunInProgress,
            Combat,
            Shop,
            Sanctuary,
            Event,
            GameOver,
            Victory
        }

        [SerializeField] private GameState _currentState;
        public GameState CurrentState
        {
            get => _currentState;
            private set
            {
                if (_currentState != value)
                {
                    GameState previousState = _currentState;
                    _currentState = value;
                    OnGameStateChanged?.Invoke(previousState, value);
                }
            }
        }
        #endregion

        #region Events
        public UnityEvent<GameState, GameState> OnGameStateChanged;
        public UnityEvent OnGameInitialized;
        public UnityEvent OnRunStarted;
        public UnityEvent OnRunEnded;
        #endregion

        #region Core Systems
        [Header("Core Systems")]
        [SerializeField] private EventSystem _eventSystem;
        [SerializeField] private SceneController _sceneController;
        [SerializeField] private SaveSystem _saveSystem;
        #endregion

        #region Properties
        public EventSystem EventSystem => _eventSystem;
        public SceneController SceneController => _sceneController;
        public SaveSystem SaveSystem => _saveSystem;
        #endregion

        #region Initialization
        private void InitializeGame()
        {
            Debug.Log("[GameManager] Initializing game...");
            
            // Initialize core systems
            InitializeCoreSystems();
            
            // Load saved data
            LoadGameData();
            
            // Initialize event system
            if (_eventSystem == null)
                _eventSystem = GetComponent<EventSystem>() ?? gameObject.AddComponent<EventSystem>();
            
            if (_sceneController == null)
                _sceneController = GetComponent<SceneController>() ?? gameObject.AddComponent<SceneController>();
            
            if (_saveSystem == null)
                _saveSystem = GetComponent<SaveSystem>() ?? gameObject.AddComponent<SaveSystem>();
            
            Debug.Log("[GameManager] Game initialized successfully!");
            OnGameInitialized?.Invoke();
        }

        private void InitializeCoreSystems()
        {
            // Initialize random seed
            Random.InitState(System.DateTime.Now.Millisecond);
            
            // Set target framerate
            Application.targetFrameRate = 60;
            
            // Configure quality settings for pixel art
            QualitySettings.vSyncCount = 1;
        }

        private void LoadGameData()
        {
            // Load meta progression and settings
            SaveSystem.LoadMetaProgression();
        }
        #endregion

        #region State Management
        public void ChangeGameState(GameState newState)
        {
            if (!IsValidStateTransition(CurrentState, newState))
            {
                Debug.LogWarning($"[GameManager] Invalid state transition: {CurrentState} -> {newState}");
                return;
            }

            Debug.Log($"[GameManager] Changing state: {CurrentState} -> {newState}");
            CurrentState = newState;
            
            // Handle state-specific logic
            HandleStateChange(newState);
        }

        private bool IsValidStateTransition(GameState from, GameState to)
        {
            // Define valid state transitions
            switch (from)
            {
                case GameState.MainMenu:
                    return to == GameState.HeroSelection || to == GameState.GameOver;
                case GameState.HeroSelection:
                    return to == GameState.MainMenu || to == GameState.RunInProgress;
                case GameState.RunInProgress:
                    return to == GameState.Combat || to == GameState.Shop || to == GameState.Sanctuary || 
                           to == GameState.Event || to == GameState.GameOver || to == GameState.Victory;
                case GameState.Combat:
                    return to == GameState.RunInProgress || to == GameState.GameOver;
                case GameState.Shop:
                case GameState.Sanctuary:
                case GameState.Event:
                    return to == GameState.RunInProgress;
                case GameState.GameOver:
                case GameState.Victory:
                    return to == GameState.MainMenu;
                default:
                    return false;
            }
        }

        private void HandleStateChange(GameState newState)
        {
            switch (newState)
            {
                case GameState.MainMenu:
                    // Load main menu scene
                    break;
                case GameState.RunInProgress:
                    OnRunStarted?.Invoke();
                    break;
                case GameState.GameOver:
                case GameState.Victory:
                    OnRunEnded?.Invoke();
                    break;
            }
        }
        #endregion

        #region Utility Methods
        public void PauseGame()
        {
            Time.timeScale = 0f;
            Debug.Log("[GameManager] Game paused");
        }

        public void ResumeGame()
        {
            Time.timeScale = 1f;
            Debug.Log("[GameManager] Game resumed");
        }

        public void QuitGame()
        {
            Debug.Log("[GameManager] Quitting game...");
            SaveSystem.SaveMetaProgression();
            Application.Quit();
        }

        public void RestartGame()
        {
            Debug.Log("[GameManager] Restarting game...");
            SceneController.LoadScene("MainMenu");
        }
        #endregion

        #region Debug
        [Header("Debug")]
        [SerializeField] private bool _enableDebugLogs = true;

        private void Log(string message)
        {
            if (_enableDebugLogs)
                Debug.Log($"[GameManager] {message}");
        }
        #endregion
    }
}
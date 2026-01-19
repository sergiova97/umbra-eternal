using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.Events;

namespace UmbraEternal.Core
{
    /// <summary>
    Controlador de escenas con sistema de loading y transiciones
    </summary>
    public class SceneController : MonoBehaviour
    {
        #region Singleton
        private static SceneController _instance;
        public static SceneController Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<SceneController>();
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
            }
            else if (_instance != this)
            {
                Destroy(gameObject);
            }
        }
        #endregion

        #region Events
        [Header("Events")]
        public UnityEvent<string> OnSceneLoadStart;
        public UnityEvent<string> OnSceneLoadComplete;
        public UnityEvent<float> OnLoadProgress;
        #endregion

        #region Scene Management
        [Header("Scene Settings")]
        [SerializeField] private float _minimumLoadTime = 1.0f;
        [SerializeField] private bool _showLoadingScreen = true;

        // Scene names
        public const string MAIN_MENU_SCENE = "MainMenu";
        public const string HERO_SELECTION_SCENE = "HeroSelection";
        public const string RUN_SCENE = "Run";
        public const string COMBAT_SCENE = "Combat";
        public const string SHOP_SCENE = "Shop";
        public const string SANCTUARY_SCENE = "Sanctuary";
        public const string EVENT_SCENE = "Event";
        public const string GAME_OVER_SCENE = "GameOver";
        public const string VICTORY_SCENE = "Victory";

        private AsyncOperation _loadOperation;
        private string _currentSceneName;

        public string CurrentSceneName => _currentSceneName;
        public bool IsLoading => _loadOperation != null && !_loadOperation.isDone;
        #endregion

        #region Public Methods
        public void LoadScene(string sceneName)
        {
            if (IsLoading)
            {
                Debug.LogWarning("[SceneController] Scene loading already in progress");
                return;
            }

            StartCoroutine(LoadSceneRoutine(sceneName));
        }

        public void LoadMainMenu()
        {
            LoadScene(MAIN_MENU_SCENE);
        }

        public void LoadHeroSelection()
        {
            LoadScene(HERO_SELECTION_SCENE);
        }

        public void LoadRun()
        {
            LoadScene(RUN_SCENE);
        }

        public void LoadCombat()
        {
            LoadScene(COMBAT_SCENE);
        }

        public void LoadShop()
        {
            LoadScene(SHOP_SCENE);
        }

        public void LoadSanctuary()
        {
            LoadScene(SANCTUARY_SCENE);
        }

        public void LoadEvent()
        {
            LoadScene(EVENT_SCENE);
        }

        public void LoadGameOver()
        {
            LoadScene(GAME_OVER_SCENE);
        }

        public void LoadVictory()
        {
            LoadScene(VICTORY_SCENE);
        }

        public void RestartCurrentScene()
        {
            if (!string.IsNullOrEmpty(_currentSceneName))
            {
                LoadScene(_currentSceneName);
            }
            else
            {
                Debug.LogError("[SceneController] No current scene to restart");
            }
        }

        public void QuitToMainMenu()
        {
            // Save current state before quitting
            if (GameManager.Instance != null)
            {
                GameManager.Instance.SaveSystem.SaveCurrentRun();
            }
            
            LoadMainMenu();
        }
        #endregion

        #region Private Methods
        private IEnumerator LoadSceneRoutine(string sceneName)
        {
            Debug.Log($"[SceneController] Starting to load scene: {sceneName}");

            // Trigger events
            OnSceneLoadStart?.Invoke(sceneName);
            
            if (EventSystem.Instance != null)
            {
                EventSystem.Instance.TriggerSceneLoadStarted(sceneName);
            }

            // Show loading screen if enabled
            if (_showLoadingScreen)
            {
                // TODO: Show loading screen UI
                yield return new WaitForSeconds(0.1f);
            }

            // Start loading the scene
            float startTime = Time.time;
            _loadOperation = SceneManager.LoadSceneAsync(sceneName);
            _loadOperation.allowSceneActivation = false;

            // Wait for loading to complete or minimum time
            while (!_loadOperation.isDone || (Time.time - startTime) < _minimumLoadTime)
            {
                float progress = Mathf.Clamp01(_loadOperation.progress / 0.9f);
                OnLoadProgress?.Invoke(progress);
                
                yield return null;
            }

            // Activate the scene
            _loadOperation.allowSceneActivation = true;

            // Wait for scene to fully load
            while (_loadOperation.progress < 1f)
            {
                yield return null;
            }

            _currentSceneName = sceneName;
            _loadOperation = null;

            Debug.Log($"[SceneController] Scene loaded successfully: {sceneName}");

            // Trigger completion events
            OnLoadProgress?.Invoke(1f);
            OnSceneLoadComplete?.Invoke(sceneName);

            if (EventSystem.Instance != null)
            {
                EventSystem.Instance.TriggerSceneLoadCompleted(sceneName);
            }

            // Update game state based on scene
            UpdateGameStateFromScene(sceneName);
        }

        private void UpdateGameStateFromScene(string sceneName)
        {
            if (GameManager.Instance == null) return;

            switch (sceneName)
            {
                case MAIN_MENU_SCENE:
                    GameManager.Instance.ChangeGameState(GameManager.GameState.MainMenu);
                    break;
                case HERO_SELECTION_SCENE:
                    GameManager.Instance.ChangeGameState(GameManager.GameState.HeroSelection);
                    break;
                case RUN_SCENE:
                    GameManager.Instance.ChangeGameState(GameManager.GameState.RunInProgress);
                    break;
                case COMBAT_SCENE:
                    GameManager.Instance.ChangeGameState(GameManager.GameState.Combat);
                    break;
                case SHOP_SCENE:
                    GameManager.Instance.ChangeGameState(GameManager.GameState.Shop);
                    break;
                case SANCTUARY_SCENE:
                    GameManager.Instance.ChangeGameState(GameManager.GameState.Sanctuary);
                    break;
                case EVENT_SCENE:
                    GameManager.Instance.ChangeGameState(GameManager.GameState.Event);
                    break;
                case GAME_OVER_SCENE:
                    GameManager.Instance.ChangeGameState(GameManager.GameState.GameOver);
                    break;
                case VICTORY_SCENE:
                    GameManager.Instance.ChangeGameState(GameManager.GameState.Victory);
                    break;
            }
        }
        #endregion

        #region Utility Methods
        public bool IsSceneValid(string sceneName)
        {
            if (string.IsNullOrEmpty(sceneName)) return false;

            // Check if scene exists in build settings
            for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
            {
                string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
                string sceneFileName = System.IO.Path.GetFileNameWithoutExtension(scenePath);
                if (sceneFileName == sceneName)
                {
                    return true;
                }
            }

            return false;
        }

        public string GetNextCombatScene()
        {
            return COMBAT_SCENE;
        }

        public string GetNextNodeScene(NodeType nodeType)
        {
            switch (nodeType)
            {
                case NodeType.Combat:
                case NodeType.EliteCombat:
                    return COMBAT_SCENE;
                case NodeType.Shop:
                    return SHOP_SCENE;
                case NodeType.Sanctuary:
                    return SANCTUARY_SCENE;
                case NodeType.Event:
                    return EVENT_SCENE;
                default:
                    return RUN_SCENE;
            }
        }
        #endregion

        #region Unity Lifecycle
        private void Start()
        {
            // Initialize current scene
            Scene currentScene = SceneManager.GetActiveScene();
            _currentSceneName = currentScene.name;
            
            Debug.Log($"[SceneController] Initialized with scene: {_currentSceneName}");
        }

        private void OnDestroy()
        {
            // Clean up events
            OnSceneLoadStart?.RemoveAllListeners();
            OnSceneLoadComplete?.RemoveAllListeners();
            OnLoadProgress?.RemoveAllListeners();
        }
        #endregion
    }

    #region Supporting Enums
    public enum NodeType
    {
        Combat,
        EliteCombat,
        Shop,
        Sanctuary,
        Event,
        Boss
    }
    #endregion
}
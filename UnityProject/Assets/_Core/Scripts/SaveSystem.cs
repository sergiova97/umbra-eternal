using UnityEngine;
using System.IO;
using System.Collections.Generic;
using System;

namespace UmbraEternal.Core
{
    /// <summary>
    Sistema de guardado/carga para meta-progresión y runs
    </summary>
    public class SaveSystem : MonoBehaviour
    {
        #region Singleton
        private static SaveSystem _instance;
        public static SaveSystem Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<SaveSystem>();
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

        #region File Paths
        private const string META_PROGRESS_FILE = "meta_progression.json";
        private const string CURRENT_RUN_FILE = "current_run.json";
        private const string SETTINGS_FILE = "settings.json";
        private const string SAVE_FOLDER = "SaveData";

        private string _saveFolderPath;
        private string _metaProgressPath;
        private string _currentRunPath;
        private string _settingsPath;
        #endregion

        #region Data Classes
        [System.Serializable]
        public class MetaProgressionData
        {
            public int totalRunsCompleted;
            public int totalGoldEarned;
            public int essenceAmount;
            public int memoryFragments;
            public int archiveEternalLevel;
            
            public List<string> unlockedSpecies = new List<string>();
            public List<string> unlockedClasses = new List<string>();
            public List<string> unlockedAbilities = new List<string>();
            
            public List<string> completedAchievements = new List<string>();
            public List<string> unlockedEndings = new List<string>();
            
            // Permanent stat boosts
            public int globalHPBoost = 0;
            public int globalATKBoost = 0;
            public int globalDEFBoost = 0;
            public float globalEconomyBoost = 1.0f;
            public float globalExperienceBoost = 1.0f;
            
            public DateTime lastSaveTime;
            public float totalPlaytime;
        }

        [System.Serializable]
        public class CurrentRunData
        {
            public bool runInProgress;
            public string runId;
            public int currentGold;
            public int currentHealthPotions;
            public int corruptionLevel;
            public int currentMapIndex;
            
            public List<string> selectedHeroes = new List<string>();
            public List<string> collectedItems = new List<string>();
            public List<string> defeatedEnemies = new List<string>();
            public List<string> selectedRunes = new List<string>();
            
            public RunMapData mapData;
            public DateTime runStartTime;
            public float currentRunTime;
        }

        [System.Serializable]
        public class RunMapData
        {
            public int currentX;
            public int currentY;
            public List<MapNodeData> nodes = new List<MapNodeData>();
            public List<string> visitedNodes = new List<string>();
            public List<string> availableNodes = new List<string>();
        }

        [System.Serializable]
        public class MapNodeData
        {
            public string nodeId;
            public int x;
            public int y;
            public NodeType nodeType;
            public bool isVisited;
            public bool isCorrupted;
            public int corruptionLevel;
            public List<string> connectedNodes = new List<string>();
        }

        [System.Serializable]
        public class SettingsData
        {
            public float masterVolume = 1.0f;
            public float musicVolume = 0.8f;
            public float sfxVolume = 0.9f;
            public bool enableVSync = true;
            public bool fullscreen = true;
            public int qualityLevel = 2;
            public string language = "ES";
            public bool showNotifications = true;
            public bool autoSave = true;
            public int autoSaveInterval = 300; // seconds
        }
        #endregion

        #region Private Fields
        [Header("Save Settings")]
        [SerializeField] private bool _enableAutoSave = true;
        [SerializeField] private int _autoSaveIntervalSeconds = 300;
        [SerializeField] private bool _enableCloudSave = false;
        
        private MetaProgressionData _metaProgression;
        private CurrentRunData _currentRun;
        private SettingsData _settings;
        
        private float _lastAutoSaveTime;
        private bool _isInitialized = false;
        #endregion

        #region Properties
        public MetaProgressionData MetaProgression => _metaProgression;
        public CurrentRunData CurrentRun => _currentRun;
        public SettingsData Settings => _settings;
        public bool IsInitialized => _isInitialized;
        #endregion

        #region Initialization
        private void Start()
        {
            InitializeSaveSystem();
        }

        private void InitializeSaveSystem()
        {
            // Create save folder
            _saveFolderPath = Path.Combine(Application.persistentDataPath, SAVE_FOLDER);
            if (!Directory.Exists(_saveFolderPath))
            {
                Directory.CreateDirectory(_saveFolderPath);
            }

            // Set file paths
            _metaProgressPath = Path.Combine(_saveFolderPath, META_PROGRESS_FILE);
            _currentRunPath = Path.Combine(_saveFolderPath, CURRENT_RUN_FILE);
            _settingsPath = Path.Combine(_saveFolderPath, SETTINGS_FILE);

            // Load data
            LoadMetaProgression();
            LoadSettings();

            _isInitialized = true;
            
            Debug.Log("[SaveSystem] Initialized successfully");
            Debug.Log($"[SaveSystem] Save folder: {_saveFolderPath}");

            // Start auto-save timer
            if (_enableAutoSave)
            {
                InvokeRepeating(nameof(AutoSave), _autoSaveIntervalSeconds, _autoSaveIntervalSeconds);
            }
        }
        #endregion

        #region Meta Progression
        public void LoadMetaProgression()
        {
            try
            {
                if (File.Exists(_metaProgressPath))
                {
                    string json = File.ReadAllText(_metaProgressPath);
                    _metaProgression = JsonUtility.FromJson<MetaProgressionData>(json);
                    Debug.Log("[SaveSystem] Meta progression loaded successfully");
                }
                else
                {
                    _metaProgression = CreateDefaultMetaProgression();
                    SaveMetaProgression();
                    Debug.Log("[SaveSystem] Created default meta progression");
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"[SaveSystem] Error loading meta progression: {e.Message}");
                _metaProgression = CreateDefaultMetaProgression();
            }
        }

        public void SaveMetaProgression()
        {
            try
            {
                if (_metaProgression != null)
                {
                    _metaProgression.lastSaveTime = DateTime.Now;
                    string json = JsonUtility.ToJson(_metaProgression, true);
                    File.WriteAllText(_metaProgressPath, json);
                    
                    if (EventSystem.Instance != null)
                    {
                        EventSystem.Instance.GameEvents.OnGameDataSaved?.Invoke();
                    }
                    
                    Debug.Log("[SaveSystem] Meta progression saved successfully");
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"[SaveSystem] Error saving meta progression: {e.Message}");
            }
        }

        private MetaProgressionData CreateDefaultMetaProgression()
        {
            return new MetaProgressionData
            {
                totalRunsCompleted = 0,
                totalGoldEarned = 0,
                essenceAmount = 0,
                memoryFragments = 0,
                archiveEternalLevel = 1,
                unlockedSpecies = new List<string> { "Human" }, // Start with one species
                unlockedClasses = new List<string> { "Warrior" }, // Start with one class
                lastSaveTime = DateTime.Now,
                totalPlaytime = 0f
            };
        }

        public void AddEssence(int amount)
        {
            _metaProgression.essenceAmount = Mathf.Max(0, _metaProgression.essenceAmount + amount);
            SaveMetaProgression();
        }

        public void UnlockSpecies(string speciesId)
        {
            if (!_metaProgression.unlockedSpecies.Contains(speciesId))
            {
                _metaProgression.unlockedSpecies.Add(speciesId);
                SaveMetaProgression();
                
                if (EventSystem.Instance != null)
                {
                    // Trigger species unlocked event
                }
            }
        }

        public void UnlockClass(string classId)
        {
            if (!_metaProgression.unlockedClasses.Contains(classId))
            {
                _metaProgression.unlockedClasses.Add(classId);
                SaveMetaProgression();
                
                if (EventSystem.Instance != null)
                {
                    // Trigger class unlocked event
                }
            }
        }

        public void CompleteRun(bool victory, int goldEarned, float playtime)
        {
            _metaProgression.totalRunsCompleted++;
            _metaProgression.totalGoldEarned += goldEarned;
            _metaProgression.totalPlaytime += playtime;
            
            if (victory)
            {
                _metaProgression.essenceAmount += 50; // Victory reward
            }
            
            SaveMetaProgression();
        }
        #endregion

        #region Current Run
        public void StartNewRun(List<string> selectedHeroes)
        {
            _currentRun = new CurrentRunData
            {
                runInProgress = true,
                runId = System.Guid.NewGuid().ToString(),
                currentGold = 100, // Starting gold
                currentHealthPotions = 3,
                corruptionLevel = 0,
                currentMapIndex = 0,
                selectedHeroes = selectedHeroes,
                runStartTime = DateTime.Now,
                currentRunTime = 0f,
                mapData = GenerateNewMap()
            };
            
            SaveCurrentRun();
            Debug.Log($"[SaveSystem] New run started with ID: {_currentRun.runId}");
        }

        public void LoadCurrentRun()
        {
            try
            {
                if (File.Exists(_currentRunPath))
                {
                    string json = File.ReadAllText(_currentRunPath);
                    _currentRun = JsonUtility.FromJson<CurrentRunData>(json);
                    Debug.Log("[SaveSystem] Current run loaded successfully");
                }
                else
                {
                    _currentRun = null;
                    Debug.Log("[SaveSystem] No current run found");
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"[SaveSystem] Error loading current run: {e.Message}");
                _currentRun = null;
            }
        }

        public void SaveCurrentRun()
        {
            try
            {
                if (_currentRun != null && _currentRun.runInProgress)
                {
                    if (_currentRun.mapData == null)
                    {
                        _currentRun.mapData = new RunMapData();
                    }
                    
                    string json = JsonUtility.ToJson(_currentRun, true);
                    File.WriteAllText(_currentRunPath, json);
                    Debug.Log("[SaveSystem] Current run saved successfully");
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"[SaveSystem] Error saving current run: {e.Message}");
            }
        }

        public void EndRun()
        {
            if (_currentRun != null)
            {
                _currentRun.runInProgress = false;
                SaveCurrentRun();
                
                // Clean up run file
                if (File.Exists(_currentRunPath))
                {
                    File.Delete(_currentRunPath);
                }
                
                _currentRun = null;
                Debug.Log("[SaveSystem] Run ended and data cleaned up");
            }
        }

        private RunMapData GenerateNewMap()
        {
            // TODO: Implement map generation logic
            return new RunMapData
            {
                currentX = 0,
                currentY = 0,
                nodes = new List<MapNodeData>(),
                visitedNodes = new List<string>(),
                availableNodes = new List<string>()
            };
        }
        #endregion

        #region Settings
        public void LoadSettings()
        {
            try
            {
                if (File.Exists(_settingsPath))
                {
                    string json = File.ReadAllText(_settingsPath);
                    _settings = JsonUtility.FromJson<SettingsData>(json);
                    ApplySettings();
                    Debug.Log("[SaveSystem] Settings loaded successfully");
                }
                else
                {
                    _settings = CreateDefaultSettings();
                    SaveSettings();
                    Debug.Log("[SaveSystem] Created default settings");
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"[SaveSystem] Error loading settings: {e.Message}");
                _settings = CreateDefaultSettings();
            }
        }

        public void SaveSettings()
        {
            try
            {
                if (_settings != null)
                {
                    string json = JsonUtility.ToJson(_settings, true);
                    File.WriteAllText(_settingsPath, json);
                    ApplySettings();
                    Debug.Log("[SaveSystem] Settings saved successfully");
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"[SaveSystem] Error saving settings: {e.Message}");
            }
        }

        private SettingsData CreateDefaultSettings()
        {
            return new SettingsData();
        }

        private void ApplySettings()
        {
            if (_settings == null) return;

            // Apply audio settings
            AudioListener.volume = _settings.masterVolume;

            // Apply quality settings
            QualitySettings.SetQualityLevel(_settings.qualityLevel);

            // Apply fullscreen
            Screen.fullScreen = _settings.fullscreen;
        }

        public void UpdateSetting(string settingName, object value)
        {
            if (_settings == null) return;

            switch (settingName)
            {
                case "masterVolume":
                    _settings.masterVolume = (float)value;
                    break;
                case "musicVolume":
                    _settings.musicVolume = (float)value;
                    break;
                case "sfxVolume":
                    _settings.sfxVolume = (float)value;
                    break;
                case "fullscreen":
                    _settings.fullscreen = (bool)value;
                    break;
                case "qualityLevel":
                    _settings.qualityLevel = (int)value;
                    break;
                case "language":
                    _settings.language = (string)value;
                    break;
            }
            
            SaveSettings();
        }
        #endregion

        #region Utility Methods
        public void AutoSave()
        {
            if (_currentRun != null && _currentRun.runInProgress)
            {
                SaveCurrentRun();
                Debug.Log("[SaveSystem] Auto-save completed");
            }
        }

        public void ClearAllData()
        {
            try
            {
                // Clear meta progression
                _metaProgression = CreateDefaultMetaProgression();
                SaveMetaProgression();

                // Clear current run
                _currentRun = null;
                if (File.Exists(_currentRunPath))
                {
                    File.Delete(_currentRunPath);
                }

                // Clear settings
                _settings = CreateDefaultSettings();
                SaveSettings();

                Debug.Log("[SaveSystem] All data cleared successfully");
            }
            catch (Exception e)
            {
                Debug.LogError($"[SaveSystem] Error clearing data: {e.Message}");
            }
        }

        public bool HasValidSaveData()
        {
            return File.Exists(_metaProgressPath) && _metaProgression != null;
        }

        public void ExportSaveData()
        {
            // TODO: Implement save export functionality
            Debug.Log("[SaveSystem] Export save data not implemented yet");
        }

        public void ImportSaveData(string importData)
        {
            // TODO: Implement save import functionality
            Debug.Log("[SaveSystem] Import save data not implemented yet");
        }
        #endregion

        #region Unity Lifecycle
        private void Update()
        {
            if (_currentRun != null && _currentRun.runInProgress)
            {
                _currentRun.currentRunTime += Time.deltaTime;
            }
        }

        private void OnDestroy()
        {
            // Final save before destruction
            if (_isInitialized)
            {
                SaveMetaProgression();
                SaveCurrentRun();
                SaveSettings();
            }
        }
        #endregion
    }
}
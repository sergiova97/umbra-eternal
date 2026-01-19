using UnityEngine;
using UnityEngine.Events;

namespace UmbraEternal.Core
{
    /// <summary>
    Sistema de eventos centralizado para comunicación entre sistemas
    </summary>
    public class EventSystem : MonoBehaviour
    {
        #region Singleton
        private static EventSystem _instance;
        public static EventSystem Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<EventSystem>();
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

        #region Game Events
        public static class GameEvents
        {
            // Hero Events
            public static UnityEvent<Hero> OnHeroCreated = new UnityEvent<Hero>();
            public static UnityEvent<Hero> OnHeroLevelUp = new UnityEvent<Hero>();
            public static UnityEvent<Hero, int> OnHeroDamaged = new UnityEvent<Hero, int>();
            public static UnityEvent<Hero> OnHeroHealed = new UnityEvent<Hero>();
            public static UnityEvent<Hero> OnHeroDied = new UnityEvent<Hero>();

            // Combat Events
            public static UnityEvent OnCombatStarted = new UnityEvent();
            public static UnityEvent<CombatResult> OnCombatEnded = new UnityEvent<CombatResult>();
            public static UnityEvent<Hero, Enemy> OnAttackStarted = new UnityEvent<Hero, Enemy>();
            public static UnityEvent<Hero, Enemy, int> OnDamageDealt = new UnityEvent<Hero, Enemy, int>();
            public static UnityEvent<Ability> OnAbilityUsed = new UnityEvent<Ability>();
            public static UnityEvent<StatusEffect> OnStatusEffectApplied = new UnityEvent<StatusEffect>();

            // Turn Events
            public static UnityEvent<Character> OnTurnStarted = new UnityEvent<Character>();
            public static UnityEvent<Character> OnTurnEnded = new UnityEvent<Character>();
            public static UnityEvent OnRoundStarted = new UnityEvent();
            public static UnityEvent OnRoundEnded = new UnityEvent();

            // Run Events
            public static UnityEvent OnRunStarted = new UnityEvent();
            public static UnityEvent<RunResult> OnRunEnded = new UnityEvent<RunResult>();
            public static UnityEvent<RuneCard> OnRuneSelected = new UnityEvent<RuneCard>();
            public static UnityEvent<Node> OnNodeCompleted = new UnityEvent<Node>();
            public static UnityEvent<MapNode> OnMapNodeSelected = new UnityEvent<MapNode>();

            // UI Events
            public static UnityEvent<string> OnShowNotification = new UnityEvent<string>();
            public static UnityEvent OnHideNotification = new UnityEvent();
            public static UnityEvent<string> OnSceneLoadStarted = new UnityEvent<string>();
            public static UnityEvent<string> OnSceneLoadCompleted = new UnityEvent<string>();

            // Economy Events
            public static UnityEvent<int> OnGoldChanged = new UnityEvent<int>();
            public static UnityEvent<Item> OnItemPurchased = new UnityEvent<Item>();
            public static UnityEvent<Pact> OnPactMade = new UnityEvent<Pact>();

            // Meta Progression Events
            public static UnityEvent<SpeciesSO> OnSpeciesUnlocked = new UnityEvent<SpeciesSO>();
            public static UnityEvent<ClassSO> OnClassUnlocked = new UnityEvent<ClassSO>();
            public static UnityEvent<Achievement> OnAchievementUnlocked = new UnityEvent<Achievement>();

            // System Events
            public static UnityEvent OnGameDataLoaded = new UnityEvent();
            public static UnityEvent OnGameDataSaved = new UnityEvent();
            public static UnityEvent OnApplicationPaused = new UnityEvent();
            public static UnityEvent OnApplicationResumed = new UnityEvent();
        }
        #endregion

        #region Utility Methods
        public void TriggerHeroCreated(Hero hero)
        {
            GameEvents.OnHeroCreated?.Invoke(hero);
            Debug.Log($"[EventSystem] Hero created: {hero.name}");
        }

        public void TriggerCombatStarted()
        {
            GameEvents.OnCombatStarted?.Invoke();
            Debug.Log("[EventSystem] Combat started");
        }

        public void TriggerCombatEnded(CombatResult result)
        {
            GameEvents.OnCombatEnded?.Invoke(result);
            Debug.Log($"[EventSystem] Combat ended: {result}");
        }

        public void TriggerRunStarted()
        {
            GameEvents.OnRunStarted?.Invoke();
            Debug.Log("[EventSystem] Run started");
        }

        public void TriggerRuneSelected(RuneCard card)
        {
            GameEvents.OnRuneSelected?.Invoke(card);
            Debug.Log($"[EventSystem] Rune selected: {card.name}");
        }

        public void TriggerGoldChanged(int newAmount)
        {
            GameEvents.OnGoldChanged?.Invoke(newAmount);
            Debug.Log($"[EventSystem] Gold changed: {newAmount}");
        }

        public void TriggerNotification(string message)
        {
            GameEvents.OnShowNotification?.Invoke(message);
            Debug.Log($"[EventSystem] Notification: {message}");
        }

        public void TriggerSceneLoadStarted(string sceneName)
        {
            GameEvents.OnSceneLoadStarted?.Invoke(sceneName);
            Debug.Log($"[EventSystem] Scene load started: {sceneName}");
        }

        public void TriggerSceneLoadCompleted(string sceneName)
        {
            GameEvents.OnSceneLoadCompleted?.Invoke(sceneName);
            Debug.Log($"[EventSystem] Scene load completed: {sceneName}");
        }

        public void TriggerApplicationPaused()
        {
            GameEvents.OnApplicationPaused?.Invoke();
            SaveAutoState();
            Debug.Log("[EventSystem] Application paused");
        }

        public void TriggerApplicationResumed()
        {
            GameEvents.OnApplicationResumed?.Invoke();
            Debug.Log("[EventSystem] Application resumed");
        }
        #endregion

        #region Save System Integration
        private void SaveAutoState()
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.SaveSystem.SaveCurrentRun();
            }
        }
        #endregion

        #region Cleanup
        private void OnApplicationPause(bool pauseStatus)
        {
            if (pauseStatus)
            {
                TriggerApplicationPaused();
            }
            else
            {
                TriggerApplicationResumed();
            }
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            if (!hasFocus)
            {
                TriggerApplicationPaused();
            }
            else
            {
                TriggerApplicationResumed();
            }
        }
        #endregion

        #region Debug
        [Header("Debug")]
        [SerializeField] private bool _enableEventLogs = true;

        private void LogEvent(string eventName)
        {
            if (_enableEventLogs)
                Debug.Log($"[EventSystem] Event triggered: {eventName}");
        }
        #endregion
    }

    #region Event Data Types
    public enum CombatResult
    {
        Victory,
        Defeat,
        Retreat
    }

    public enum RunResult
    {
        Victory,
        Defeat,
        Abandoned
    }

    // Forward declarations - these will be implemented in their respective systems
    public class Hero { }
    public class Enemy { }
    public class Character { }
    public class Ability { }
    public class StatusEffect { }
    public class RuneCard { }
    public class Node { }
    public class MapNode { }
    public class Item { }
    public class Pact { }
    public class SpeciesSO { }
    public class ClassSO { }
    public class Achievement { }
    #endregion
}
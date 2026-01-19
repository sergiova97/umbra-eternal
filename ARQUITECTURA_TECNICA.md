# Arquitectura Técnica - Umbra Eternal

## Plataforma y Tecnología

### Engine: Unity 2022.3 LTS
- **Target Platform**: PC (Windows/Linux/Mac)
- **Rendering**: 2DRenderer con soporte Detailed Pixel Art
- **Scripting**: C# (.NET Standard 2.1)
- **Architecture**: Scriptable Objects + ECS (selectivo)

### Estructura del Proyecto

#### 1. Core Systems
```
Assets/
├── _Core/
│   ├── GameManager.cs
│   ├── SceneController.cs
│   └── EventSystem.cs
├── _Data/
│   ├── Heroes/
│   ├── Enemies/
│   ├── Abilities/
│   └── Runes/
└── _UI/
    ├── MainMenu/
    ├── Combat/
    └── Meta/
```

#### 2. Game Systems
```
Assets/
├── _Systems/
│   ├── Combat/
│   │   ├── CombatManager.cs
│   │   ├── TurnManager.cs
│   │   └── DamageCalculator.cs
│   ├── Heroes/
│   │   ├── HeroSystem.cs
│   │   └── AbilityManager.cs
│   ├── Run/
│   │   ├── RunManager.cs
│   │   ├── VoidDeck.cs
│   │   └── NodeSystem.cs
│   └── Enemies/
│       ├── EnemyAI.cs
│       └── BossManager.cs
```

## Patrones de Diseño

### 1. Scriptable Objects System
- **Heroes**: HeroSO, SpeciesSO, ClassSO
- **Abilities**: AbilitySO, EffectSO, ModifierSO
- **Enemies**: EnemySO, AIPatternSO, BossPhaseSO
- **Runes**: RuneSO, CorruptEffectSO, NodeSO

### 2. Event-Driven Architecture
```csharp
public static class GameEvents
{
    public static UnityEvent<Hero> OnHeroCreated;
    public static UnityEvent<CombatResult> OnCombatEnd;
    public static UnityEvent<Rune> OnRuneSelected;
    // ...
}
```

### 3. Component Pattern
```csharp
public interface IHeroComponent
{
    void Initialize(Hero hero);
    void Update();
}

public class SpeciesComponent : IHeroComponent { }
public class ClassComponent : IHeroComponent { }
public class AbilityComponent : IHeroComponent { }
```

## Sistema de Datos

### 1. Scriptable Objects Structure
```csharp
[CreateAssetMenu(fileName = "New Hero", menuName = "Heroes/Hero")]
public class HeroSO : ScriptableObject
{
    public string heroName;
    public SpeciesSO species;
    public ClassSO heroClass;
    public int baseHP, baseATK, baseDEF;
    public AbilityData[] abilities;
}
```

### 2. Runtime Data Management
```csharp
public class Hero : MonoBehaviour
{
    private HeroSO heroData;
    private HeroStats stats;
    private List<Ability> abilities;
    private List<StatusEffect> statusEffects;
}
```

## Sistema de Persistencia

### 1. Save System Structure
```csharp
[System.Serializable]
public class SaveData
{
    public MetaProgressionData metaProgression;
    public CurrentRunData currentRun;
    public SettingsData settings;
}
```

### 2. Local Storage
- **Meta Progression**: PlayerPrefs + JSON serialization
- **Run Data**: Temporal durante la run actual
- **Settings**: PlayerPrefs para configuración

## Performance Considerations

### 1. Object Pooling
```csharp
public class ObjectPoolManager : MonoBehaviour
{
    [SerializeField] private GameObject[] poolPrefabs;
    private Dictionary<string, Queue<GameObject>> pools;
}
```

### 2. Optimización 2D
- **Sprite Atlas**: Agrupar sprites por tema
- **Sprite Renderer**: Batch render optimization
- **UI System**: Canvas optimizado para pixel art

### 3. Memory Management
- **Asset Bundles**: Carga dinámica de recursos
- **Unload Unused**: Limpieza de assets no utilizados
- **Garbage Collection**: Minimizar allocations en runtime

## Herramientas de Desarrollo

### 1. Custom Editors
```csharp
[CustomEditor(typeof(HeroSO))]
public class HeroEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Custom UI para editar heroes
    }
}
```

### 2. Validation Tools
- **Balance Testing**: Herramientas para testear balance
- **Data Validation**: Validación de datos en Scriptable Objects
- **Performance Profiler**: Integration con Unity Profiler

## Flujo de Desarrollo

### 1. Iterative Development
1. **Core Systems** (GameManager, Events)
2. **Hero System** (Stats, Abilities)
3. **Combat System** (Turn Management, Damage)
4. **Run System** (Void Deck, Nodes)
5. **Enemy AI** (Behavior, Corruption)
6. **UI/UX** (Menús, HUD)

### 2. Testing Strategy
- **Unit Tests**: Para lógica de cálculos (stats, damage)
- **Integration Tests**: Para sistemas complejos (combate completo)
- **Play Tests**: Para balance y UX

### 3. Version Control
- **Git Flow**: Main/Develop/Feature branches
- **Commit Convention**: Mensajes estandarizados
- **Documentation**: README en cada sistema principal

## Asset Pipeline

### 1. Pixel Art Workflow
- **Resolution**: 64x64 base, upscale a 128x128/256x256
- **Animation**: Spritesheets con padding
- **Compression**: No compression para pixel art
- **Format**: PNG 32-bit con alpha channel

### 2. Audio Pipeline
- **Format**: WAV para desarrollo,compressed para release
- **System**: AudioMixer de Unity
- **Categories**: SFX, Music, UI Sounds

### 3. Localization
- **Textos**: Scriptable Objects multi-idioma
- **System**: Unity Localization package
- **Idiomas**: Español, Inglés (inicial)

## Deployment Strategy

### 1. Build Pipeline
- **Automated**: Unity Cloud Build o GitHub Actions
- **Platforms**: Windows (Standalone), Linux, Mac
- **Compression**: LZ4 para runtime

### 2. Quality Assurance
- **Automated Tests**: CI/CD integration
- **Performance Benchmarks**: FPS targets, memory usage
- **Cross-Platform Testing**: Validación en múltiples sistemas

## Herramientas Externas

### 1. Pixel Art
- **Aseprite**: Para animaciones y sprites
- **TexturePacker**: Para sprite atlases
- **Unity Pixel Perfect**: Para rendering

### 2. Audio
- **Audacity**: Para edición básica
- **FMod/Wwise**: Si se necesita audio avanzado

### 3. Analytics
- **Unity Analytics**: Para métricas de jugador
- **Custom Events**: Tracking de balance y gameplay

## Roadmap Técnico

### Phase 1: Foundation (2-3 semanas)
- [ ] Project setup with Unity
- [ ] Core architecture (GameManager, Events)
- [ ] Data structure (Scriptable Objects)
- [ ] Basic UI framework

### Phase 2: Core Systems (3-4 semanas)
- [ ] Hero system implementation
- [ ] Combat system foundation
- [ ] Basic enemy AI
- [ ] Turn management

### Phase 3: Content Systems (4-5 semanas)
- [ ] Void Deck implementation
- [ ] Run system and nodes
- [ ] Meta progression
- [ ] Save system

### Phase 4: Polish & Content (6-8 semanas)
- [ ] UI/UX complete
- [ ] Balance and testing
- [ ] Asset integration
- [ ] Performance optimization

## Próximos Pasos Inmediatos

1. **Crear proyecto Unity** con estructura base
2. **Implementar ScriptableObject templates** para heroes/enemies
3. **Desarrollar GameManager básico** con sistema de eventos
4. **Prototipar sistema de combate** simple (sin UI final)
5. **Validar technical choices** con prototype funcional
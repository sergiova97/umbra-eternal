# Umbra Eternal - Unity Project Structure

## Project Setup (Week 1 - Simulated)

Esta es la estructura base del proyecto Unity que crearíamos. Los archivos C# están listos para usar en Unity.

```
Assets/
├── _Core/
│   ├── Scripts/
│   │   ├── GameManager.cs
│   │   ├── SceneController.cs
│   │   └── EventSystem.cs
│   └── Prefabs/
├── _Data/
│   ├── Heroes/
│   │   ├── HeroSO.cs
│   │   ├── SpeciesSO.cs
│   │   ├── ClassSO.cs
│   │   └── AbilityData.cs
│   ├── Enemies/
│   │   ├── EnemySO.cs
│   │   ├── AIPatternSO.cs
│   │   └── BossPhaseSO.cs
│   ├── Abilities/
│   │   ├── AbilitySO.cs
│   │   ├── EffectSO.cs
│   │   └── ModifierSO.cs
│   └── Runes/
│       ├── RuneSO.cs
│       ├── CorruptEffectSO.cs
│       └── NodeSO.cs
├── _Systems/
│   ├── Combat/
│   ├── Heroes/
│   ├── Run/
│   └── Enemies/
├── _UI/
│   ├── MainMenu/
│   ├── Combat/
│   └── Meta/
├── _Prefabs/
├── _Scenes/
├── _Art/
│   ├── Sprites/
│   ├── Animations/
│   └── UI/
└── _Audio/
    ├── Music/
    └── SFX/
```

## Unity Settings

### Project Settings Configurados:
- **Target Platform**: PC
- **Scripting Backend**: Mono
- **API Compatibility**: .NET Standard 2.1
- **Color Space**: Linear
- **Rendering Pipeline**: Built-in Render Pipeline
- **Input System**: New Input System
- **Script Compilation**: Custom assemblies

### Quality Settings:
- **Default Quality**: Ultra (PC)
- **VSync Count**: 1
- **Anti-Aliasing**: 4x
- **Pixel Perfect Camera**: Enabled

## Installation Instructions

Para usar estos scripts en Unity:

1. **Crear nuevo proyecto Unity 2022.3 LTS**
2. **Configurar Package Manager**: 
   - Unity Addressables (para asset management)
   - Unity Localization (para multi-idioma)
   - Unity Test Framework (para testing)
3. **Importar esta estructura** en Assets/
4. **Configurar assembly definitions** para separar sistemas
5. **Setup Input System** para manejo de inputs

## Next Steps

1. Importar todos los scripts C# en Unity
2. Configurar Scriptable Objects templates
3. Setup basic scenes (MainMenu, Combat)
4. Implementar GameManager y EventSystem
5. Test basic functionality
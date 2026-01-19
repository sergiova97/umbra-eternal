# Sistema de Enemigos

## Definición Base

Sistema de enemigos dinámico basado en arquetipos tácticos, con IA adaptativa, mutaciones por corrupción y patrones de comportamiento estratégicos que evolucionan según la progresión.

## Arquetipos Base

### Tanque (Protector)
- **Stats**: HP alto, DEF alta, SPD baja, ATK moderado
- **Rol**: Proteger aliados, aggro control, interceptar daño
- **Habilidades típicas**: Provocación, escudos, counter-attacks
- **IA Priority**: Proteger al DPS más débil, interceptar ataques directed

### DPS (Daño por Segundo)
- **Subtipos**: Melee DPS, Ranged DPS, Burst DPS
- **Stats**: ATK alto, CRIT alto, DEF baja, SPD media-alta
- **Rol**: Infligir daño máximo, eliminar threats rápidamente
- **Habilidades**: Single target powerful attacks, damage buffs
- **IA Priority**: Focusear enemy más débil o más threateneo

### Control (CC)
- **Stats**: SPD alta, stats de impacto high, survival baja
- **Rol**: Apply status effects, disrupt enemy patterns
- **Habilidades**: Stun, silence, debuffs, positional control
- **IA Priority**: Neutralizar threats más peligrosos primero

### Soporte (Support)
- **Stats**: HP moderados, abilities de utility alta
- **Rol**: Curar allies, buffear equipo, remove debuffs
- **Habilidades**: Healing, protection, buff application
- **IA Priority**: Mantener vivo al tanque, respond to debuffs priority

### Híbrido (Flexible)
- **Stats**: Balanceados con especialización media
- **Rol**: Adaptar rol según situación del combate
- **Habilidades**: Mix de roles dependiendo de needs
- **IA Priority**: Evaluar battlefield y adjust strategy dinámicamente

## Sistema de Rasgos (Traits)

### Pasivas Innatas
- **Resistencia Elemental**: Reducción 25% damage de un tipo específico
- **Regeneration**: 5% HP máximo por turno
- **Vengeance**: +50% daño cuando por debajo de 30% HP
- **Adaptation**: Immunity creciente a repeated status effects
- **Pack Tactics**: +10% stats por cada aliado vivo

### Reactivas
- **Retribution**: Counter-attack cuando reciben daño crítico
- **Last Stand**: No puede morir por encima de 1 HP una vez por combate
- **Rage**: +25% ATK cuando un aliado muere
- **Focus**: Reduce movement pero aumenta precisión

### Situacionales
- **Day/Night Cycle**: Bonus diferentes según hora del mapa
- **Terrain Advantage**: Stats modificados en terrenos específicos
- **Environmental Interaction**: Interactúan con objetos del escenario

## Sistema de Comportamiento (AI Logic)

### Sistema de Prioridad
```python
# Pseudocode de AI decision making
def calculate_action_priority(enemy, battle_state):
    threats = assess_threat_level(enemy, battle_state.allies)
    tactical_value = evaluate_tactical_opportunities(enemy, battle_state)
    survival_need = check_survival_priority(enemy, battle_state)
    
    return (threats * 0.4) + (tactical_value * 0.4) + (survival_need * 0.2)
```

### Patrones de Comportamiento

#### Aggressive Pattern
- **Trigger**: HP > 70%, enemy team weak
- **Behavior**: Prioritize damage over defense
- **Target Selection**: Weakest enemy first
- **Skill Usage**: Damage abilities on cooldown

#### Defensive Pattern
- **Trigger**: HP < 40%, important ally threatened
- **Behavior**: Protect and heal over damage
- **Target Selection**: Self-protection or endangered allies
- **Skill Usage**: Defensive and healing abilities

#### Tactical Pattern
- **Trigger**: Mid-health, favorable position
- **Behavior**: Use battlefield control
- **Target Selection**: Disrupt enemy formations
- **Skill Usage**: CC and positioning abilities

#### Adaptive Pattern
- **Trigger**: Combat duration > X turns, multiple defeats
- **Behavior**: Change strategy based on enemy success
- **Target Selection**: Target successful enemy patterns
- **Skill Usage**: Adapt to counter enemy strategy

### Sistema de Aprendizaje
- **Pattern Recognition**: Learn player strategies over time
- **Counter-development**: Adjust behavior to counter player success
- **Difficulty Scaling**: Become more effective with repeated failures

## Sistema de Mutaciones por Corrupción

### Niveles de Corrupción
#### Corrupción Nivel 1 (Sutil)
- **Visual Changes**: Ligera oscurecimiento, eyes glowing
- **Stat Changes**: +10% HP, +5% ATK
- **New Abilities**: Minor corruption abilities
- **Behavior**: Slightly more aggressive

#### Corrupción Nivel 2 (Moderada)
- **Visual Changes**: Mutaciones físicas notables, aura oscura
- **Stat Changes**: +20% HP, +15% ATK, resistencia a daño holy
- **New Abilities**: Void-touched abilities
- **Behavior**: Unpredictable patterns, team disruption

#### Corrupción Nivel 3 (Severa)
- **Visual Changes**: Forma monstruosa, mutations múltiples
- **Stat Changes**: +30% all stats, immunities múltiples
- **New Abilities**: Powerful void abilities, transformations
- **Behavior**: Erratic, dangerous to both teams

### Tipo de Mutaciones
- **Físicas**: Armadura adicional, weapon mutations, extra limbs
- **Elementales**: Nuevo tipo de daño, resistencia adicional
- **Behavioral**: Cambios en AI patterns, new targeting priorities
- **Abilities**: Nuevas habilidades o versiones corrompidas

## Sistema de Jefes (Bosses)

### Estructura de Fases
- **Phase 1 (100-70% HP)**: Introduction mechanics, base abilities
- **Phase 2 (70-40% HP)**: Enhanced abilities, additional mechanics
- **Phase 3 (40-15% HP)**: Desesperate phase, berserk patterns
- **Phase 4 (<15% HP)**: Final stand, ultimate abilities

### Boss Archetypes

#### Guardian Boss
- **Characteristics**: Massive HP, strong defenses, limited offense
- **Mechanics**: Environmental control, summons, damage reduction
- **Strategy**: Whittle down while avoiding damage amplification
- **Example**: Ancient Golem, Void Guardian

#### DPS Boss
- **Characteristics**: High damage output, multiple attacks, low defenses
- **Mechanics**: Burst damage patterns, area attacks, damage reflection
- **Strategy**: Survive burst windows, maximize healing uptime
- **Example**: Void Dragon, Archangel Vindicator

#### Controller Boss
- **Characteristics**: Extensive CC abilities, tactical advantages
- **Mechanics**: Battlefield manipulation, status effect combos
- **Strategy**: Break CC chains, target priority management
- **Example**: Void Summoner, Mind Flayer Lord

#### Support Boss
- **Characteristics**: Heals and buffs allies, indirect damage
- **Mechanics**: Ally resurrection, buff stacks, healing denial
- **Strategy**: Focus on support elements, interrupt healing
- **Example**: Void Priestess, Demon Commander

### Unique Boss Mechanics
- **Environmental Interaction**: Use terrain to advantage
- **Phase Transformations**: Drastically change patterns
- **Summon Patterns**: Call reinforcements strategically
- **Enrage Timers**: Become more powerful over time

## Sistema de Grupos de Enemigos

### Formations Tácticas
- **Line Formation**: Front tanks, back DPS/support
- **Flank Formation**: Sides protected, center vulnerable
- **Scatter Formation**: Distributed positioning
- **Stack Formation**: Clumped for AoE vulnerability

### Coordination Patterns
- **Focus Fire**: Multiple enemies target same hero
- **Cover Systems**: Protect key members
- **Chain Combos**: Enemies set up each other's attacks
- **Rescue Protocol**: Prioritize saving fallen allies

## Visual Design Guidelines

### Enemy Categories
- **Bestiales**: Animalistic, primal, instinctive behavior
- **Humanoides**: Structured, tactical, tool-using
- **Aberraciones**: Unpredictable, chaotic, reality-warping
- **Constructos**: Methodical, relentless, mechanical
- **Spectral**: Ethereal, phasing, psychological warfare

### Corruption Visual Progression
- **Stage 1**: Slight color changes, minor mutations
- **Stage 2**: Physical deformations, void energy manifestations
- **Stage 3**: Complete transformation, otherworldly appearance
- **Stage 4**: Unrecognizable horror, pure void entity

## Balance y Escalado

### Difficulty Scaling
- **Stat Scaling**: Base stats increase with corruption level
- **AI Complexity**: More sophisticated behavior at higher levels
- **Mechanic Count**: Additional mechanics introduced progressively
- **Team Composition**: More diverse enemy types in advanced runs

### Player Counter-play
- **Weakness Systems**: Each enemy type has exploitable weaknesses
- **Counter Mechanics**: Specific abilities work better against certain types
- **Pattern Learning**: Observant players can predict and counter patterns
- **Build Optimization**: Different team compositions excel vs different enemies

## Próximos Pasos

1. Implementar sistema base de arquetipos
2. Desarrollar IA patterns fundamentales
3. Crear sistema de progresión de corrupción
4. Diseñar jefes iniciales con multi-phase
5. Balancear stat scaling y difficulty progression
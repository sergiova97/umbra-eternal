# Roadmap de Desarrollo - Umbra Eternal

## Visión General

Roadmap detallado para el desarrollo completo del juego, dividido en fases claras con objetivos específicos y milestones alcanzables.

## Phase 0: Pre-Production (Completado)
- [x] Definición de conceptos fundamentales
- [x] Documentación de sistemas base
- [x] Selección de tecnología (Unity + C#)
- [x] Creación de repositorio GitHub
- [x] Arquitectura técnica definida

## Phase 1: Foundation (Semanas 1-3)

### Objetivo: Establecer las bases técnicas del proyecto

#### Week 1: Project Setup
- [ ] Crear proyecto Unity 2022.3 LTS
- [ ] Configurar estructura de carpetas estándar
- [ ] Implementar GameManager base
- [ ] Setup sistema de eventos (UnityEvents)
- [ ] Configurar versión control (gitignore, branches)

#### Week 2: Data Architecture
- [ ] Implementar ScriptableObject templates:
  - [ ] HeroSO, SpeciesSO, ClassSO
  - [ ] AbilitySO, EffectSO
  - [ ] EnemySO, AIPatternSO
  - [ ] RuneSO, NodeSO
- [ ] Crear validadores de datos
- [ ] Implementar save system básico (JSON)

#### Week 3: Core Framework
- [ ] Implementar Hero Component System
- [ ] Sistema de Stats base
- [ ] Sistema de Abilities framework
- [ ] Debug tools para testing
- [ ] Basic UI framework (buttons, panels)

**Deliverable**: Prototipo técnico con datos editables pero sin gameplay funcional

---

## Phase 2: Core Gameplay (Semanas 4-7)

### Objetivo: Implementar mecánicas fundamentales de juego

#### Week 4: Combat Foundation
- [ ] Sistema de turnos básico
- [ ] Grid positioning (2x3 formation)
- [ ] Sistema de iniciativa (SPD-based)
- [ ] Damage calculation básica
- [ ] Debug UI para combate

#### Week 5: Abilities & Effects
- [ ] Implementar sistema de habilidades
- [ ] Status effects framework
- [ ] Cooldown system
- [ ] Targeting system (single, AoE, positional)
- [ ] Visual feedback básico

#### Week 6: Enemy AI Base
- [ ] Implementar arquetipos básicos (Tank, DPS, Support)
- [ ] AI decision making básico
- [ ] Target selection logic
- [ ] Behavioral patterns
- [ ] Testing framework para AI

#### Week 7: Combat Integration
- [ ] Integrar heroes + enemies en combate
- [ ] Sistema de victoria/derrota
- [ ] Combat rewards básicas
- [ ] Post-combat flow
- [ ] Combat balancing inicial

**Deliverable**: Sistema de combate funcional con heroes vs enemies básicos

---

## Phase 3: Roguelike Systems (Semanas 8-12)

### Objetivo: Implementar sistemas de progresión roguelike

#### Week 8: Void Deck System
- [ ] Implementar carta mechanics
- [ ] Draft system (3 cartas A/B/C)
- [ ] Corrupción por exposición
- [ ] Visual deck interface
- [ ] Card variety inicial (10-15 cartas)

#### Week 9: Node System
- [ ] Generación procedural de mapa
- [ ] Implementar tipos de nodos:
  - [ ] Combat (Común/Élite)
  - [ ] Santuario
  - [ ] Tienda básica
  - [ ] Evento simple
- [ ] Map navigation system

#### Week 10: Run Management
- [ ] Run state management
- [ ] Node progression logic
- [ ] Run rewards system
- [ ] Corruption escalation
- [ ] Run restart functionality

#### Week 11: Economy & Shops
- [ ] Implementar sistema de oro
- [ ] Shop interface functionality
- [ ] Item generation system
- [ ] Price balance inicial
- [ ] Shop inventory management

#### Week 12: Meta-Progression Foundation
- [ ] Archivo Eterno structure
- [ ] Permanent unlock system
- [ ] Currency meta (Essence/Memory)
- [ ] Achievement framework
- [ ] Save/Load meta progress

**Deliverable**: Ciclo completo de run con meta-progresión básica

---

## Phase 4: Content & Polish (Semanas 13-20)

### Objetivo: Crear contenido jugable y pulir experiencia

#### Weeks 13-14: Hero Content
- [ ] Implementar 3 especies iniciales:
  - [ ] Humanos (balanced)
  - [ ] Elfos Oscuros (stealth/speed)
  - [ ] Demonios Menores (high risk/reward)
- [ ] Implementar 3 clases iniciales:
  - [ ] Guerrero (tank)
  - [ ] Mago Arcano (DPS)
  - [ ] Pícaro (control)
- [ ] Balance de synergies especies-clase

#### Weeks 15-16: Enemy Content
- [ ] Implementar 10+ enemy types variados
- [ ] 2-3 bosses con multi-phase
- [ ] Corruption mutations system
- [ ] Enemy variety por biome
- [ ] AI pattern diversification

#### Weeks 17-18: Systems Expansion
- [ ] Pact system (Ángeles, Demonios, Vacío)
- [ ] Advanced corruption mechanics
- [ ] Elite enemies con unique abilities
- [ ] Environmental effects
- [ ] Advanced rune cards (20+ total)

#### Weeks 19-20: UI/UX Polish
- [ ] Complete UI para todos los sistemas
- [ ] Animations y visual feedback
- [ ] Sound effects básicos
- [ ] Music integration
- [ ] Performance optimization

**Deliverable**: Versión jugable completa con contenido variado

---

## Phase 5: Balance & Launch Preparation (Semanas 21-24)

### Objetivo: Balance final y preparación para release

#### Week 21: Game Balance
- [ ] Statistical balance analysis
- [ ] Player feedback incorporation
- [ ] Difficulty curve adjustment
- [ ] Meta-economy balance
- [ ] Risk/reward optimization

#### Week 22: Content Polish
- [ ] Additional variety (abilities, enemies)
- [ ] Achievement completion
- [ ] Endings implementation
- [ ] Secrets y easter eggs
- [ ] Lore integration

#### Week 23: Technical Polish
- [ ] Performance profiling
- [ ] Memory optimization
- [ ] Bug fixes critical
- [ ] Save system validation
- [ ] Build pipeline testing

#### Week 24: Launch Preparation
- [ ] Store page preparation
- [ ] Marketing materials
- [ ] Launch trailer
- [ ] Community setup
- [ ] Release candidate testing

**Deliverable**: Juego listo para lanzamiento

---

## Phase 6: Post-Launch Support (Ongoing)

### Content Updates
- [ ] New heroes/classes/species
- [ ] Additional enemies/bosses
- [ ] New rune cards
- [ ] Alternative endings
- [ ] Challenge modes

### Community Features
- [ ] Steam integration
- [ ] Achievement system
- [ ] Trading cards
- [ ] Cloud saves
- [ ] Mod support consideration

## Milestones Key

### Alpha v0.1 (Week 7)
- Combat system funcional
- Basic heroes/enemies
- Technical foundation complete

### Alpha v0.2 (Week 12)
- Full run cycle
- Meta-progression
- Roguelike systems

### Beta v0.8 (Week 20)
- Complete game content
- Polish and optimization
- Feature complete

### Release v1.0 (Week 24)
- Balanced and optimized
- Launch ready
- Documentation complete

## Risk Management

### Technical Risks
- **Complexity creep**: Regular scope review meetings
- **Performance issues**: Early profiling and optimization
- **Save corruption**: Robust testing and validation

### Design Risks
- **Balance issues**: Continuous playtesting and analytics
- **Content burnout**: Modular content creation
- **Player retention**: Meta-progression depth

### Timeline Risks
- **Scope creep**: Strict feature prioritization
- **Delays**: Buffer time built into schedule
- **Quality issues**: Regular QA checkpoints

## Success Metrics

### Technical Metrics
- FPS: 60+ en target hardware
- Load times: <3 seconds
- Memory usage: <2GB RAM
- Bug count: <50 critical issues

### Player Metrics (Post-Launch)
- Retention: 70% Day 1, 40% Day 7, 20% Day 30
- Playtime: Average 2+ hours per session
- Completion: 25% reach first ending
- Engagement: 3+ runs per active player

## Next Immediate Actions

1. **Week 1 Tasks**: Unity project setup y GameManager
2. **Resource Planning**: Confirm developer time allocation
3. **Asset Pipeline**: Setup herramientas pixel art workflow
4. **Testing Strategy**: Define QA process y tools
5. **Documentation**: Maintain developer docs同步
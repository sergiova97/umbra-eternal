# Sistema de Run (The Void Deck System)

## Definición Base

Sistema de progresión roguelike basado en draft de cartas que define el recorrido del jugador a través del mapa de la run, con mecánicas de corrupción dinámica que aumentan el riesgo y la recompensa.

## Estructura del Mazo

### Tres Mazos Temáticos
- **Mazo A (Seguridad)**: Cartas de bajo riesgo, recompensas moderadas
- **Mazo B (Equilibrio)**: Cartas de riesgo medio, recompensas variables
- **Mazo C (Corrupción)**: Cartas de alto riesgo, recompensas masivas

### Sistema de Draft
- **Porción 3 cartas**: Una de cada mazo (A, B, C)
- **Elección del jugador**: Selecciona UNA carta
- **Descarte automático**: Las otras dos cartas van al "Void Deck"

## Corrupción por Exposición

### Mecánica Principal
Las cartas no elegidas acumulan niveles de corrupción, afectando futuros nodos:

#### Nivel 1 de Corrupción
- **Efecto**: Incremento de dificultad (+25% stats enemigos)
- **Beneficio**: +50% botín y experiencia
- **Visual**: Corrupción sutil en el entorno

#### Nivel 2 de Corrupción
- **Efecto**: Inserción de rasgos negativos de entorno
- **Ejemplos**: Niebla (reducida visibilidad), terreno inestable, trampas aleatorias
- **Beneficio**: +100% botín, chance de items corruptos

#### Nivel 3 de Corrupción
- **Efecto**: El nodo muta a versión "Vacío"
- **Características**: Enemigos mutados, mechanics invertidas, recompensas épicas
- **Riesgo**: High chance de game over si no se está preparado

### Contador de Corrupción Global
- **Acumulación**: Cada carta nivel 3 incrementa corrupción global
- **Efectos**: Modifica al jefe final, desbloquea endings alternativos
- **Umbral**: 5+ cartas nivel 3 = "Ending del Vacío"

## Tipos de Nodos

### Nodos de Combate
#### Combate Común
- **Enemigos**: 3-4 enemigos estándar
- **Recompensa**: Oro, experiencia, chance de equipo común
- **Corrupción**: Puede aparecer en cualquier nivel

#### Combate Élite
- **Enemigos**: 2-3 enemigos élite + minions
- **Recompensa**: Oro abundante, equipo raro, artefactos
- **Corrupción**: Generalmente nivel 2 o 3

#### Jefe de Área
- **Enemigos**: Boss único con múltiples fases
- **Recompensa**: Equipo legendario, habilidades únicas
- **Corrupción**: Siempre nivel 3 si está presente

### Nodos de Santuario
- **Función**: Recuperación y mejora
- **Opciones**: Curación gratuita, mejora de una habilidad, comprar consumibles
- **Corrupción**: Reduce temporalmente los efectos de corrupción

### Nodos de Tienda
- **Sistema**: Comerciar oro por equipo y servicios
- **Inventario**: Rotación de items según progreso de la run
- **Servicios**: Mejorar rango de habilidades, reroll stats, comprar info de próximos nodos

### Nodos de Evento Narrativo
- **Formato**: Elección con consecuencias
- **Resultados**: Beneficios, penalizaciones, o mutations permanentes
- **Variación**: Events específicos por facción (Ángeles, Demonios, Vacío)

### Nodos de Pacto
- **Mecánica**: Encuentros con facciones poderosas
- **Propuesta**: Ventajas masivas a cambio de sacrificios permanentes
- **Facciones**: 
  - **Ángeles**: Pureza a cambio de restricciones
  - **Demonios**: Poder a cambio de corrupción
  - **Vacío**: Mutación a cambio de humanity

## Sistema de Mapa y Navegación

### Estructura del Mapa
- **Generación procedural**: Mapa diferente cada run
- **Conexiones**: Líneas que conectan nodos, algunas requieren requisitos
- **Rutas múltiples**: Siempre hay al menos 2-3 caminos posibles
- **Nodos especiales**: Ocultos, requieren descubrimiento o llaves

### Visibilidad y Exploración
- **Fog of War**: Nodos no visitados ocultos
- **Scouting**: Habilidades o items para revelar áreas
- **Pistas**: Indicadores de corrupción futura en rutas cercanas

## Sistema de Riesgo y Recompensa

### Cálculo de Valor
```
Valor = (Recompensa base × Multiplicador) / (Riesgo estimado × Coste oportunidad)
```

### Factores de Decisión
- **Estado actual del equipo**: HP, consumibles, cooldowns
- **Progreso de la run**: Distancia al jefe, corrupción acumulada
- **Build del equipo**: Sinergias actuales y potenciales
- **Meta-progresión**: ¿Vale la pena arriesgar para desbloqueos?

### Ejemplos de Decisiones
- **Carta Mazo C (Nivel 3)**: "Frente al Jefe Final" vs "Tienda con Descuento"
- **Pacto Demoníaco**: "+50% daño toda la run" vs "Pierdes un héroe aleatorio"
- **Evento de Vacío**: "Mutación aleatoria poderosa" vs "Corrupción global +1"

## Interfaz y UX

### Visualización de Cartas
- **Arte único** para cada carta
- **Indicadores claros** de riesgo/recompensa
- **Historial** de cartas descartadas y su corrupción

### Mapa Interactivo
- **Tooltips** detallados de cada nodo
- **Predictor** de ruta y corrupción acumulada
- **Comparador** de posibles paths

## Balance y Escalado

### Dificultad Adaptativa
- **Performance tracking**: El sistema se adapta al éxito del jugador
- **Dynamic scaling**: Corrupción aumenta más rápido si domina el combate
- **Catch-up mechanics**: Oportunidades de recuperación si va mal

### Meta Elements
- **Run goals**: Objetivos específicos para desbloqueos
- **Challenges**: Modificadores opcionales para recompensas extra
- **Achievements**: Hitos específicos paraArchivo Eterno

## Próximos Pasos

1. Diseñar pool inicial de cartas para cada mazo
2. Implementar sistema de corrupción progresiva
3. Prototipar generación de mapas
4. Balancear riesgo/recompensa de diferentes nodos
5. Desarrollar eventos y pactos específicos
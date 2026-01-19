# Sistema de Combate

## Definición Base

Sistema de combate táctico 5v5 por turnos con iniciativa dinámica basada en SPD, enfocado en posicionamiento estratégico y sinergias de equipo.

## Formación de Batalla

### Formación de Pirámide
```
[PT1] [PT2]  ← Protectores (Frente)
[HB1] [HB2] [HB3]  ← Héroes de Batalla (Retaguardia)
```

### Lógica de Cobertura
- **Protector Izquierdo (PT1)**: Cubre a HB1 y HB2
- **Protector Derecho (PT2)**: Cubre a HB2 y HB3
- **Héroes de Retaguardia**: Protegidos contra ataques de corto alcance
- **Line of Sight**: Requiere visión clara para ataques a distancia

### Reglas de Cobertura
1. Los protectores reciben 50% del daño destinado a los héroes que cubren
2. Los ataques de largo alcance pueden ignorar cobertura con penalización de precisión
3. Los ataques de área afectan a múltiples objetivos independientemente de la cobertura
4. Los protectores pueden usar habilidades para mejorar cobertura (Fortaleza, Escudo)

## Sistema de Iniciativa

### Cálculo de Turno
- **Base SPD**: Velocidad del héroe
- **Modificadores**: Bonificaciones/malus por estados, habilidades, terreno
- **Random Factor**: ±10% variación para evitar patrones predecibles

### Barra de Iniciativa
- Cada héroe acumula puntos cada "tick"
- Al alcanzar 100 puntos, el héroe puede actuar
- SPD determina cuántos puntos acumula por tick

## Jerarquía de Daño

### Tipos de Daño Físico
- **Cortante**: Espadas, cuchillas, garras
  - Efectivo contra: Armadura ligera, tejidos
  - Resistido por: Armadura pesada, escudos
- **Perforante**: Lanzas, flechas, proyectiles
  - Efectivo contra: Armadura pesada, estructuras
  - Resistido por: Armadura flexible, agilidad
- **Contundente**: Martillos, mazas, cuerpo a cuerpo
  - Efectivo contra: Estructuras, armaduras
  - Resistido por: Flexibilidad, absorción

### Tipos de Daño Mágico
- **Fuego**: Daño constante, puede causar "Quemado"
- **Hielo**: Daño variable, puede causar "Congelado"
- **Rayo**: Daño instantáneo, puede causar "Aturdido"
- **Viento**: Daño penetrante, ignora parte de la armadura
- **Arcano**: Daño puro, difícil de resistir
- **Psíquico**: Daño mental, ignora defensas físicas
- **Natural**: Veneno, enfermedades, puede causar "Envenenado"

### Tipos de Daño Primordial
- **Celestial**: Daño sagrado, efectivo contra seres oscuros
- **Infernal**: Daño corrupto, efectivo contra seres santos
- **Vacío**: Daño entrópico, corrompe y muta objetivos

## Sistema de Estados

### Estados de Ventaja/Desventaja
Modifican temporalmente las estadísticas base:

#### Estados Positivos
- **Fuerza**: +25% ATK
- **Fortaleza**: +25% DEF
- **Agilidad**: +25% SPD/EVA
- **Foco**: +25% CRIT/precisión
- **Bendición**: +15% todos los stats

#### Estados Negativos
- **Debilidad**: -25% ATK
- **Vulnerabilidad**: -25% DEF
- **Lentitud**: -25% SPD/EVA
- **Confusión**: -50% precisión
- **Maldición**: -15% todos los stats

### Efectos Alterados (Status Effects)

#### Daño por Tiempo (DoT)
- **Sangrado**: 10% HP máximos por turno, físico
- **Quemado**: 8% HP máximos por turno, mágico fuego
- **Envenenado**: 6% HP máximos por turno, natural
- **Corrupción**: 5% HP máximos por turno, vacío, muta

#### Control
- **Aturdido**: Pierde turno actual + siguiente
- **Congelado**: Inmovilizado, DEF +50%, ATK -50%
- **Dormido**: No actúa, daño lo despierta
- **Paralizado**: 50% de fallar acciones
- **Silenciado**: No puede usar habilidades mágicas
- **Desarmado**: No puede usar ataques físicos

#### Otros
- **Ceguera**: -75% precisión
- **Miedo**: Puede huir o no actuar
- **Borracho**: Acciones aleatorias
- **Mutación**: Stats modificados aleatoriamente

## Mecánicas Avanzadas

### Combos y Sinergias
- **Chain Attacks**: Personajes con SPD similar pueden atacar en secuencia
- **Elemental Reactions**: Combinar tipos de daño para efectos especiales
- **Positional Bonuses**: Bonificaciones por ataque por espalda/flanco

### Sistema de Stamina
- Cada acción consume stamina
- Las habilidades poderosas consumen más
- Se recupera gradualmente cada turno
- Stamina baja reduce efectividad

### Environmental Factors
- **Terreno**: Afecta movimiento y cobertura
- **Condiciones**: Lluvia, niebla, oscuridad modifican visibilidad
- **Interactivos**: Objetos destructibles, trampas, pozos

## Balance y Progresión

### Escalado de Dificultad
- Enemigos más fuertes en nodos corruptos
- Jefes con múltiples fases y patrones cambiantes
- Modificadores de entorno en runs avanzadas

### Feedback Visual
- Indicadores claros de daño y estados
- Animaciones diferenciadas por tipo de daño
- Efectos visuales para combos y sinergias

## Próximos Pasos

1. Implementar sistema de iniciativa dinámica
2. Desarrollar mecánicas de cobertura
3. Balancear tipos de daño y resistencias
4. Diseñar sistema de combos visuales
5. Prototipar IA enemiga básica
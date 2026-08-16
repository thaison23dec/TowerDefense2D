# Tower Defense 2D

A playable 2D tower-defense prototype built with **Unity** and **C#**. The project focuses on reusable gameplay systems: data-driven content, enemy waves, combat, tower economy, level progression, and pooled runtime objects.

> Built as a Unity Developer portfolio project by [Thai Son](https://github.com/thaison23dec).

## Play the game

**[Play Tower Defense 2D in your browser](https://thaison23dec.github.io/TowerDefense2D/)** — no installation required.

The project is published as a Unity WebGL build through GitHub Pages. For the best experience, use a current desktop browser and allow the game a moment to load.

## At a glance

| | |
| --- | --- |
| **Engine** | Unity `2022.3.47f1` (LTS) |
| **Language** | C# |
| **Genre** | 2D Tower Defense |
| **Playable content** | Level-select menu and 3 playable levels |
| **Platform** | Web browser (WebGL) and Unity Editor |

## Gameplay

Players spend coins to place Archer, Gun, or Spawn towers on build spots, then start enemy waves. Defeat enemies to earn coins, upgrade or sell towers to adapt their defense, and protect the main tower until every wave is cleared.

### Implemented features

- **Wave-based encounters** with configurable enemy groups, quantities, spawn cadence, and multiple waypoint paths.
- **Three tower roles:** ranged Archer, Gun, and Spawn tower that deploys allied units with patrol positions.
- **Tower economy:** purchase, upgrade, sell, coin feedback, and range-based target selection.
- **Enemy and ally combat:** health bars, movement, nearest-target detection, attack cooldowns, damage, death animation, rewards, and base damage on path completion.
- **Level flow:** level-select screen, win/lose states, retry/next-level actions, and saved completion/unlock progress through `PlayerPrefs`.
- **2D Tilemap map logic:** walkability data is used to find valid patrol cells for spawned allies.
- **Audio, animation, and VFX** for attacks, deaths, explosions, and coin feedback.

## Engineering highlights

This project is deliberately organized around gameplay systems rather than placing all logic in scene scripts:

- **Data-driven configuration:** `ScriptableObject` assets define characters, towers, projectiles, tiles, prefabs, and levels. New content can be tuned in the Inspector without changing core gameplay code.
- **Object pooling:** a central `ObjectPoolManager` uses `UnityEngine.Pool.ObjectPool<T>` to reuse enemies, allies, projectiles, and VFX, reducing runtime `Instantiate`/`Destroy` churn during combat.
- **Factory-based spawning:** `EnemyFactory` resolves an enemy type through `PrefabData`, then requests it from the pool.
- **Reusable character base:** `CharacterBase` centralizes states, movement, targeting, combat, animation calls, health, and pooling lifecycle; enemy and ally controllers extend it for role-specific behavior.
- **Separation of responsibilities:** managers own game, wave, map, audio, level, and UI concerns; tower classes encapsulate placement/upgrade/combat behavior.

## Project structure

```text
Assets/_GAME/
├── Config/       # ScriptableObject assets for levels, units, towers, tiles, etc.
├── Pool/         # Generic runtime object-pooling manager
├── Prefabs/      # Gameplay prefabs (towers, enemies, allies, VFX)
├── Scenes/       # LevelMenu, Level1, Level2, Level3
├── Scripts/
│   ├── Character/  # Shared character behaviour plus enemy/ally controllers
│   ├── Config/     # ScriptableObject data definitions
│   ├── Factory/    # Enemy creation
│   ├── Manager/    # Game, wave, level, UI, map, and audio coordination
│   ├── Projectile/ # Projectile behaviours
│   ├── Tower/      # Tower base classes and tower implementations
│   └── Effects/    # Combat feedback VFX
└── Tiles/        # Tilemap assets

docs/             # Deployable Unity WebGL build for GitHub Pages
```

## Run locally

1. Install **Unity Hub** and Unity Editor **2022.3.47f1**.
2. Clone the repository:

   ```bash
   git clone https://github.com/thaison23dec/TowerDefense2D.git
   ```

3. In Unity Hub, choose **Open** and select the cloned project folder.
4. Open `Assets/_GAME/Scenes/LevelMenu.unity`.
5. Press **Play**. The Build Settings already includes the menu and Levels 1–3.

## Reviewer quick route

If you are reviewing the code, these files provide the fastest overview of the project’s architecture:

- `Assets/_GAME/Pool/ObjectPoolManager.cs` — pooled object lifecycle and type-safe spawning.
- `Assets/_GAME/Scripts/Character/CharacterBase.cs` — shared unit state, targeting, combat, and animation handling.
- `Assets/_GAME/Scripts/Manager/WaveManager.cs` — wave sequencing and end-of-wave flow.
- `Assets/_GAME/Scripts/Manager/MapManager.cs` — Tilemap walkability and patrol-cell search.
- `Assets/_GAME/Scripts/Tower/TowerBase.cs` — shared tower economy and upgrade/sell actions.

## What I learned

- Designing extensible gameplay around **composition, inheritance, and ScriptableObjects**.
- Managing active combat objects responsibly with **pooling** and explicit spawn/despawn hooks.
- Coordinating gameplay state across UI, progression, economy, waves, and scene loading.
- Building and debugging 2D gameplay with **Physics2D**, Tilemaps, Animator, and Unity UI.

## Roadmap

- Add a short gameplay video/GIF and development notes for an even faster portfolio review.
- Add automated play-mode tests for wave completion, economy, and level progression.
- Expand targeting priorities and tower/enemy variety.
- Add save-data versioning and volume/settings UI.

## Assets and attribution

This repository includes Unity/TextMesh Pro resources and third-party art assets. Their original licenses apply. Before commercial distribution, ensure every external asset has a documented source and license.

## Contact

[GitHub — thaison23dec](https://github.com/thaison23dec)

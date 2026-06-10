# Cartograph update for Satisfactory 1.2 — status & remaining steps

This fork updates Cartograph (originally by yeshjho) from Satisfactory 1.1 / SML 3.11.1
to Satisfactory 1.2 / SML 3.12.0 / Unreal Engine 5.6.1-CSS.

## Already done (in this branch)

- Merged `satisfactorymodding/SatisfactoryModLoader` master (SML 3.12.0, FactoryGame
  headers/stubs for CL 491125, engine association 5.6.1-CSS).
- Moved the mod to the new Game Features layout: `Mods/GameFeatures/Cartograph/`.
- `Cartograph.uplugin`: SemVersion 1.3.0, SML dependency `^3.12.0`,
  `GameVersion >=491125`, added `BuiltInInitialFeatureState: Active`.
- Converted bare includes to full include paths (required by the new build settings),
  e.g. `Buildables/FGBuildable.h`, `Components/PanelWidget.h`, `Net/UnrealNetwork.h`.
- Static API audit against the new headers found no removed/renamed symbols used by the
  mod (hooked functions, AccessTransformers entries, and FG classes all still exist).

## Remaining steps (require accounts / the Unreal editor)

1. **Install the modding engine (UE 5.6.1-CSS).** Link your Epic Games account to
   GitHub, run the linker at <https://linker.ficsit.app/link>, then download and run the
   installer from the `satisfactorymodding/UnrealEngine` releases page. Full directions:
   <https://docs.ficsit.app/satisfactory-modding/latest/Development/BeginnersGuide/dependencies.html>
2. **Visual Studio 2022** with "Desktop development with C++" and "Game development with
   C++" workloads, MSVC v14.38 build tools, .NET 8 runtime, .NET Framework 4.8.1 SDK.
3. **Wwise.** Create an Audiokinetic account, install the Audiokinetic Launcher, and
   integrate the *exact* Wwise version listed on the Dependencies page into
   `FactoryGame.uproject` (Authoring + SDK (C++); Windows VS2019/VS2022/GameCore +
   Linux platforms). Do not accept Wwise update prompts inside the editor.
4. **Generate VS project files** (right-click `FactoryGame.uproject` → Generate Visual
   Studio project files), open `FactoryGame.sln`, build `FactoryGame` with the
   **Development Editor** configuration.
5. **Create the Game Feature data asset** (new requirement in 1.2): in the editor, in
   `Mods/GameFeatures/Cartograph/Content`, create a Data Asset of class
   `FGGameFeatureData` named exactly `Cartograph` in the mod's content root. Compare
   with ExampleMod's asset for the "Primary Asset Types to Scan" rules.
   See: <https://docs.ficsit.app/satisfactory-modding/latest/Development/UpdatingFromSml311.html>
6. **Package with Alpakit** (Alpakit Release → Windows) and test against Satisfactory 1.2.

## Known risks to watch at compile time

- The vendored UE5Coro copy under `Source/Cartograph/{Public,Private}/UE5Coro/` was
  written for UE 5.3. If it fails to compile on 5.6.1, update the vendored files from
  a UE5Coro release that supports UE 5.6 (2.2+): <https://github.com/landelare/ue5coro>
- Hooked engine/game methods (`AFGLightweightBuildableSubsystem::AddFromBuildableInstanceData`,
  `AFGBuildableSubsystem::AddBuildable`/`RemoveBuildable`, `AFGHUD::CloseRespawnUI`,
  `FCanvas::GetBatchedElements`) still exist, but verify their parameter lists match the
  lambda signatures in `CartographGameInstanceModule.cpp` if the build complains.
- Blueprint hook asset `Content/Hook/Hook_MapMenu_Cartograph.uasset` targets the game's
  map menu widget; if the map UI changed in 1.2, it may need re-pointing in the editor.

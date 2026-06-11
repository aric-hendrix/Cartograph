using UnrealBuildTool;
using System.IO;
using System;

public class Cartograph : ModuleRules
{
	public Cartograph(ReadOnlyTargetRules Target) : base(Target)
	{
		PCHUsage = PCHUsageMode.UseExplicitOrSharedPCHs;
		CppStandard = CppStandardVersion.Cpp20;

		// FactoryGame transitive dependencies
		// Not all of these are required, but including the extra ones saves you from having to add them later.
		// Some entries are commented out to avoid compile-time warnings about depending on a module that you don't explicitly depend on.
		// You can uncomment these as necessary when your code actually needs to use them.
		PublicDependencyModuleNames.AddRange(new string[] {
			"Core", "CoreUObject",
			"Engine",
			"DeveloperSettings",
			"PhysicsCore",
			"InputCore",
			//"OnlineSubsystem", "OnlineSubsystemUtils", "OnlineSubsystemNull",
			//"SignificanceManager",
			"GeometryCollectionEngine",
			//"ChaosVehiclesCore", "ChaosVehicles", "ChaosSolverEngine",
			"AnimGraphRuntime",
			//"AkAudio",
			"AssetRegistry",
			"NavigationSystem",
			//"ReplicationGraph",
			"AIModule",
			"GameplayTasks",
			"SlateCore", "Slate", "UMG",
			//"InstancedSplines",
			"RenderCore",
			"CinematicCamera",
			"Foliage",
			//"Niagara",
			//"EnhancedInput",
			//"GameplayCameras",
			//"TemplateSequence",
			"NetCore",
			"GameplayTags",
			"Json", "JsonUtilities"
		});

		// FactoryGame plugins
		PublicDependencyModuleNames.AddRange(new string[] {
			//"AbstractInstance",
			//"InstancedSplinesComponent",
			//"SignificanceISPC"
		});

		// Header stubs
		PublicDependencyModuleNames.AddRange(new string[] {
			"DummyHeaders",
		});

		if (Target.Type == TargetRules.TargetType.Editor) {
			PublicDependencyModuleNames.AddRange(new string[] {/*"OnlineBlueprintSupport",*/ "AnimGraph"});
		}
		PublicDependencyModuleNames.AddRange(new string[] {"FactoryGame", "SML"});
		
		PublicIncludePaths.AddRange(new string[] {
			// Vendored UE5Coro lives under Public/UE5Coro/UE5Coro/, included as "UE5Coro/...".
			// UE 5.6 no longer auto-adds Public subdirectories to the include search path.
			Path.Combine(ModuleDirectory, "Public", "UE5Coro"),
		});

		PrivateIncludePaths.AddRange(new string[] {
			Path.Combine(ModuleDirectory, "Private", "UE5Coro"),
			// UE5Coro private sources include its own headers without the UE5Coro/ prefix
			Path.Combine(ModuleDirectory, "Public", "UE5Coro", "UE5Coro"),
		});
		
		PublicDependencyModuleNames.AddRange(new string[] {
			// ... add public dependencies that you statically link with here ...
		});
		
		PrivateDependencyModuleNames.AddRange(new string[] {
			"RHI"
		});
		
		DynamicallyLoadedModuleNames.AddRange(new string[] {
			// ... add any modules that your module loads dynamically here ...
		});
	}
}

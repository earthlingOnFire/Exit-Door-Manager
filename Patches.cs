using UnityEngine;
using HarmonyLib;
using System;
using Random = System.Random;

namespace ExitDoorManager;

[HarmonyPatch]
public static class Patches {

  private static Random rand = new Random();

  [HarmonyPrefix]
  [HarmonyPatch(typeof(SceneHelper), nameof(SceneHelper.LoadScene))]
  private static bool SceneHelper_LoadScene_Prefix(string sceneName, bool noBlocker) {
    Exit exit = new Exit(SceneHelper.CurrentScene, sceneName);
    string nextScene = GetNextScene(exit);

    if (nextScene == null || nextScene == sceneName) return true;

    // hack to make sure you can actually go back to the main menu from 1-E
    if (SceneHelper.CurrentScene == "Level 1-E" && GameObject.Find("Canvas/PauseMenu") != null) return true;

    switch (nextScene) {
      case "Random Scene":
        string[] scenes = Array.FindAll(ConfigManager.Levels, lvl => 
            !lvl.StartsWith("Random") && !lvl.StartsWith("Close")
        );
        int randomScene = rand.Next(0, scenes.Length);
        nextScene = scenes[randomScene];
        break;
      case "Random Level":
        string[] levels = Array.FindAll(ConfigManager.Levels, lvl => lvl.StartsWith("Level"));
        int randomLevel = rand.Next(0, levels.Length);
        nextScene = levels[randomLevel];
        break;
      case "The Cyber Grind":
        nextScene = "Endless";
        break;
      case "Sandbox":
        nextScene = "uk_construct";
        break;
      case "Credits Museum":
        nextScene = "CreditsMuseum2";
        break;
      case "Close Game":
        Application.Quit();
        return false;
    }

    SceneHelper.Instance.StartCoroutine(MonoSingleton<SceneHelper>.Instance.LoadSceneAsync(nextScene, noBlocker));
    return false;
  }

  [HarmonyPrefix]
  [HarmonyPatch(typeof(GameConsole.Commands.Scene), nameof(GameConsole.Commands.Scene.Execute))]
  private static bool Scene_Execute_Prefix(GameConsole.Console con, string[] args) {
		if (con.CheatBlocker() || args.Length == 0) {
			return true;
		}

		string sceneName = string.Join(" ", args);
		if (!UnityEngine.Debug.isDebugBuild && MonoSingleton<SceneHelper>.Instance.IsSceneSpecial(sceneName)) {
      return true;
		}

    MonoSingleton<SceneHelper>.Instance.StartCoroutine(MonoSingleton<SceneHelper>.Instance.LoadSceneAsync(sceneName));
    return false;
  }

  private class Exit {
    public string from { get; }
    public string to { get; }

    public Exit(string fromLevel, string toLevel) {
      from = fromLevel;
      to = toLevel;
    }
  }

  private static string GetNextScene(Exit exit) => exit switch {
    {from: "Bootstrap", to: "Intro"} => ConfigManager.intro.value,
    {from: "Intro", to: "Main Menu"} => ConfigManager.postintro.value,
    {from: "Level 0-1", to: "Level 0-2"} => ConfigManager.exit01.value,
    {from: "Level 0-2", to: "Level 0-3"} => ConfigManager.exit02.value,
    {from: "Level 0-2", to: "Level 0-S"} => ConfigManager.exit02s.value,
    {from: "Level 0-3", to: "Level 0-4"} => ConfigManager.exit03.value,
    {from: "Level 0-4", to: "Level 0-5"} => ConfigManager.exit04.value,
    {from: "Level 0-5", to: "Level 1-1"} => ConfigManager.exit05.value,
    {from: "Level 0-S", to: "Level 0-3"} => ConfigManager.exit0S.value,
    {from: "Level 1-1", to: "Level 1-2"} => ConfigManager.exit11.value,
    {from: "Level 1-1", to: "Level 1-S"} => ConfigManager.exit11s.value,
    {from: "Level 1-2", to: "Level 1-3"} => ConfigManager.exit12.value,
    {from: "Level 1-3", to: "Level 1-4"} => ConfigManager.exit13.value,
    {from: "Level 1-4", to: "Level 2-1"} => ConfigManager.exit14.value,
    {from: "Level 1-S", to: "Level 1-1"} => ConfigManager.exit1S.value,
    {from: "Level 2-1", to: "Level 2-2"} => ConfigManager.exit21.value,
    {from: "Level 2-2", to: "Level 2-3"} => ConfigManager.exit22.value,
    {from: "Level 2-3", to: "Level 2-4"} => ConfigManager.exit23.value,
    {from: "Level 2-3", to: "Level 2-S"} => ConfigManager.exit23s.value,
    {from: "Level 2-4", to: "Level 3-1"} => ConfigManager.exit24.value,
    {from: "Level 2-S", to: "Level 2-4"} => ConfigManager.exit2S.value,
    {from: "Level 3-1", to: "Level 3-2"} => ConfigManager.exit31.value,
    {from: "Level 3-1", to: "Level P-1"} => ConfigManager.exit31s.value,
    {from: "Level 3-2", to: "Intermission1"} => ConfigManager.exit32.value,
    {from: "Intermission1", to: "Level 4-1"} => ConfigManager.exitI1.value,
    {from: "Level P-1", to: "Level 3-2"} => ConfigManager.exitP1.value,
    {from: "Level 4-1", to: "Level 4-2"} => ConfigManager.exit41.value,
    {from: "Level 4-2", to: "Level 4-3"} => ConfigManager.exit42.value,
    {from: "Level 4-2", to: "Level 4-S"} => ConfigManager.exit42s.value,
    {from: "Level 4-3", to: "Level 4-4"} => ConfigManager.exit43.value,
    {from: "Level 4-4", to: "Level 5-1"} => ConfigManager.exit44.value,
    {from: "Level 4-S", to: "Level 4-3"} => ConfigManager.exit4S.value,
    {from: "Level 5-1", to: "Level 5-2"} => ConfigManager.exit51.value,
    {from: "Level 5-1", to: "Level 5-S"} => ConfigManager.exit51s.value,
    {from: "Level 5-2", to: "Level 5-3"} => ConfigManager.exit52.value,
    {from: "Level 5-3", to: "Level 5-4"} => ConfigManager.exit53.value,
    {from: "Level 5-4", to: "Level 6-1"} => ConfigManager.exit54.value,
    {from: "Level 5-S", to: "Level 5-2"} => ConfigManager.exit5S.value,
    {from: "Level 6-1", to: "Level 6-2"} => ConfigManager.exit61.value,
    {from: "Level 6-2", to: "Intermission2"} => ConfigManager.exit62.value,
    {from: "Intermission2", to: "Level 7-1"} => ConfigManager.exitI2.value,
    {from: "Level 6-2", to: "Level P-2"} => ConfigManager.exit62s.value,
    {from: "Level P-2", to: "Level 6-2"} => ConfigManager.exitP2.value,
    {from: "Level 7-1", to: "Level 7-2"} => ConfigManager.exit71.value,
    {from: "Level 7-2", to: "Level 7-3"} => ConfigManager.exit72.value,
    {from: "Level 7-3", to: "Level 7-4"} => ConfigManager.exit73.value,
    {from: "Level 7-3", to: "Level 7-S"} => ConfigManager.exit73s.value,
    {from: "Level 7-4", to: "Level 8-1"} => ConfigManager.exit74.value,
    {from: "Level 7-S", to: "Level 7-4"} => ConfigManager.exit7s.value,
    {from: "Level 8-1", to: "Level 8-2"} => ConfigManager.exit81.value,
    {from: "Level 8-2", to: "Level 8-3"} => ConfigManager.exit82.value,
    {from: "Level 8-3", to: "Level 8-4"} => ConfigManager.exit83.value,
    {from: "Level 8-4", to: "EarlyAccessEnd"} => ConfigManager.exit84.value,
    {from: "Level 0-E", to: "Level 1-E"} => ConfigManager.exit0e.value,
    {from: "Level 1-E", to: "Main Menu"} => ConfigManager.exit1e.value,
    _ => null,
  };
  
}

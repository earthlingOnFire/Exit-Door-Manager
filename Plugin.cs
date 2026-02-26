using UnityEngine;
using BepInEx;
using BepInEx.Logging;
using HarmonyLib;

namespace ExitDoorManager;

[BepInPlugin(PLUGIN_GUID, PLUGIN_NAME, PLUGIN_VERSION)]
public class Plugin : BaseUnityPlugin {	
  public const string PLUGIN_GUID = "com.earthlingOnFire.ExitDoorManager";
  public const string PLUGIN_NAME = "Exit Door Manager";
  public const string PLUGIN_VERSION = "1.2.0";
  public static ManualLogSource logger;

  private void Awake() {
    gameObject.hideFlags = HideFlags.HideAndDontSave;
    logger = Logger;
  }

  private void Start() {
    ConfigManager.Initialize();
    new Harmony(PLUGIN_GUID).PatchAll();
    logger.LogInfo($"Plugin {PLUGIN_GUID} is loaded!");
  }
}




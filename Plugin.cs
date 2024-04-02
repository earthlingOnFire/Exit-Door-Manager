﻿using BepInEx;
using HarmonyLib;

namespace ExitDoorManager;

[BepInPlugin(PLUGIN_GUID, PLUGIN_NAME, PLUGIN_VERSION)]
public class Plugin : BaseUnityPlugin {	
  public const string PLUGIN_GUID = "com.earthlingOnFire.ExitDoorManager";
  public const string PLUGIN_NAME = "Exit Door Manager";
  public const string PLUGIN_VERSION = "1.0.0";

  private void Start() {
    new Harmony(PLUGIN_GUID).PatchAll();
  }

  private void Awake() {
    ConfigManager.Initialize();
  }
}



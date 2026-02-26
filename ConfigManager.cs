using PluginConfig.API;
using PluginConfig.API.Fields;
using PluginConfig.API.Functionals;
using System.IO;
using System.Reflection;
using System;

namespace ExitDoorManager;

public static class ConfigManager {

  public static string[] Levels = {
    "Random Scene",
    "Random Level",
    "Level 0-1",
    "Level 0-2",
    "Level 0-3",
    "Level 0-4",
    "Level 0-5",
    "Level 0-S",
    "Level 1-1",
    "Level 1-2",
    "Level 1-3",
    "Level 1-4",
    "Level 1-S",
    "Level 2-1",
    "Level 2-2",
    "Level 2-3",
    "Level 2-4",
    "Level 2-S",
    "Level 3-1",
    "Level 3-2",
    "Intermission1",
    "Level P-1",
    "Level 4-1",
    "Level 4-2",
    "Level 4-3",
    "Level 4-4",
    "Level 4-S",
    "Level 5-1",
    "Level 5-2",
    "Level 5-3",
    "Level 5-4",
    "Level 5-S",
    "Level 6-1",
    "Level 6-2",
    "Intermission2",
    "Level P-2",
    "Level 7-1",
    "Level 7-2",
    "Level 7-3",
    "Level 7-4",
    "Level 7-S",
    "Level 8-1",
    "Level 8-2",
    "Level 8-3",
    "Level 8-4",
    "Level 0-E",
    "Level 1-E",
    "Intro",
    "Main Menu",
    "The Cyber Grind",
    "Sandbox",
    "Credits Museum",
    "EarlyAccessEnd",
    "Close Game",
  };

  public static PluginConfig.API.Fields.StringListField postintro;
  public static PluginConfig.API.Fields.StringListField intro;
  public static PluginConfig.API.Fields.StringListField exit01; 
  public static PluginConfig.API.Fields.StringListField exit02; 
  public static PluginConfig.API.Fields.StringListField exit02s; 
  public static PluginConfig.API.Fields.StringListField exit03; 
  public static PluginConfig.API.Fields.StringListField exit04; 
  public static PluginConfig.API.Fields.StringListField exit05; 
  public static PluginConfig.API.Fields.StringListField exit0S; 
  public static PluginConfig.API.Fields.StringListField exit11; 
  public static PluginConfig.API.Fields.StringListField exit11s; 
  public static PluginConfig.API.Fields.StringListField exit12; 
  public static PluginConfig.API.Fields.StringListField exit13; 
  public static PluginConfig.API.Fields.StringListField exit14; 
  public static PluginConfig.API.Fields.StringListField exit1S; 
  public static PluginConfig.API.Fields.StringListField exit21; 
  public static PluginConfig.API.Fields.StringListField exit22; 
  public static PluginConfig.API.Fields.StringListField exit23; 
  public static PluginConfig.API.Fields.StringListField exit23s; 
  public static PluginConfig.API.Fields.StringListField exit24; 
  public static PluginConfig.API.Fields.StringListField exit2S; 
  public static PluginConfig.API.Fields.StringListField exit31; 
  public static PluginConfig.API.Fields.StringListField exit31s; 
  public static PluginConfig.API.Fields.StringListField exit32; 
  public static PluginConfig.API.Fields.StringListField exitI1; 
  public static PluginConfig.API.Fields.StringListField exitP1; 
  public static PluginConfig.API.Fields.StringListField exit41; 
  public static PluginConfig.API.Fields.StringListField exit42; 
  public static PluginConfig.API.Fields.StringListField exit42s; 
  public static PluginConfig.API.Fields.StringListField exit43; 
  public static PluginConfig.API.Fields.StringListField exit44; 
  public static PluginConfig.API.Fields.StringListField exit4S; 
  public static PluginConfig.API.Fields.StringListField exit51; 
  public static PluginConfig.API.Fields.StringListField exit51s; 
  public static PluginConfig.API.Fields.StringListField exit52; 
  public static PluginConfig.API.Fields.StringListField exit53; 
  public static PluginConfig.API.Fields.StringListField exit54; 
  public static PluginConfig.API.Fields.StringListField exit5S; 
  public static PluginConfig.API.Fields.StringListField exit61; 
  public static PluginConfig.API.Fields.StringListField exit62; 
  public static PluginConfig.API.Fields.StringListField exit62s; 
  public static PluginConfig.API.Fields.StringListField exitI2; 
  public static PluginConfig.API.Fields.StringListField exitP2; 
  public static PluginConfig.API.Fields.StringListField exit71; 
  public static PluginConfig.API.Fields.StringListField exit72; 
  public static PluginConfig.API.Fields.StringListField exit73; 
  public static PluginConfig.API.Fields.StringListField exit73s; 
  public static PluginConfig.API.Fields.StringListField exit74; 
  public static PluginConfig.API.Fields.StringListField exit81; 
  public static PluginConfig.API.Fields.StringListField exit82; 
  public static PluginConfig.API.Fields.StringListField exit83; 
  public static PluginConfig.API.Fields.StringListField exit84; 
  public static PluginConfig.API.Fields.StringListField exit7s; 
  public static PluginConfig.API.Fields.StringListField exit0e; 
  public static PluginConfig.API.Fields.StringListField exit1e; 

  public static PluginConfigurator config;

  /*public static ButtonArrayField randomizeButtons;*/
  public static ButtonField randomizeButton;
  public static ButtonField resetButton;

  public static ConfigPanel randoSettings;
  public static BoolField allowSecretLevels;
  public static BoolField allowEncoreLevels;
  public static BoolField allowPrimeSanctums;
  public static StringListField finalScene;

  public static StringListField[] GetExits() {
    ConfigField[] exitFields = Array.FindAll(config.rootPanel.GetAllFields(), field => (
          field.guid.StartsWith("exit") || field.guid.EndsWith("intro")
    ));
    StringListField[] exits = new StringListField[exitFields.Length];

    for (int i = 0; i < exitFields.Length; i++) {
      exits[i] = exitFields[i] as StringListField;
    }
    return exits;
  }

  public static void Initialize() {
    config = PluginConfigurator.Create(Plugin.PLUGIN_NAME, Plugin.PLUGIN_GUID);

    string workingPath = Assembly.GetExecutingAssembly().Location;
    string workingDir = Path.GetDirectoryName(workingPath);
    config.SetIconWithURL(Path.Combine(workingDir, "icon.png"));

    /*randomizeButtons = new ButtonArrayField(config.rootPanel, "randomizeButtons", 2, */
    /*    new float[] { 0.5f, 0.5f }, new string[] { "Reset", "Randomize" });*/
    /*randomizeButtons.OnClickEventHandler(0).onClick += () => {*/
    /*  foreach (var exit in GetExits()) {*/
    /*    exit.value = exit.defaultValue;*/
    /*  }*/
    /*};*/
    /*randomizeButtons.OnClickEventHandler(1).onClick += () => Randomizer.RandomizeExits();*/
    resetButton = new ButtonField(config.rootPanel, "Reset All", "resetButton");
    resetButton.onClick += () => {
      foreach (var exit in GetExits()) {
        exit.value = exit.defaultValue;
      }
    };

    randoSettings = new ConfigPanel(config.rootPanel, "Randomizer", "randoSettings");
    randomizeButton = new ButtonField(randoSettings, "Randomize", "randomizeButton");
    randomizeButton.onClick += () => Randomizer.RandomizeExits();
    allowSecretLevels = new BoolField(randoSettings, "Allow Secret Levels", "randoAllowSecret", false);
    allowEncoreLevels = new BoolField(randoSettings, "Allow Encore Levels", "randoAllowEncore", false);
    allowPrimeSanctums = new BoolField(randoSettings, "Allow Prime Sanctums", "randoAllowPrime", false);
    finalScene = new StringListField(randoSettings, "Final Scene", "randoFinalScene",
        Array.FindAll(Levels, l => !l.StartsWith("Random")), "EarlyAccessEnd");

    intro = new PluginConfig.API.Fields.StringListField(config.rootPanel, "Initial Scene", "intro", Levels, "Intro");
    postintro = new PluginConfig.API.Fields.StringListField(config.rootPanel, "Post-Intro", "postintro", Levels, "Main Menu");
    exit01 = new PluginConfig.API.Fields.StringListField(config.rootPanel, "0-1 Exit", "exit01", Levels, "Level 0-2");
    exit02 = new PluginConfig.API.Fields.StringListField(config.rootPanel, "0-2 Exit", "exit02", Levels, "Level 0-3");
    exit02s = new PluginConfig.API.Fields.StringListField(config.rootPanel, "0-2 Secret Exit", "exit02s", Levels, "Level 0-S");
    exit03 = new PluginConfig.API.Fields.StringListField(config.rootPanel, "0-3 Exit", "exit03", Levels, "Level 0-4");
    exit04 = new PluginConfig.API.Fields.StringListField(config.rootPanel, "0-4 Exit", "exit04", Levels, "Level 0-5");
    exit05 = new PluginConfig.API.Fields.StringListField(config.rootPanel, "0-5 Exit", "exit05", Levels, "Level 1-1");
    exit0S = new PluginConfig.API.Fields.StringListField(config.rootPanel, "0-S Exit", "exit0S", Levels, "Level 0-3");
    exit11 = new PluginConfig.API.Fields.StringListField(config.rootPanel, "1-1 Exit", "exit11", Levels, "Level 1-2");
    exit11s = new PluginConfig.API.Fields.StringListField(config.rootPanel, "1-1 Secret Exit", "exit11s", Levels, "Level 1-S");
    exit12 = new PluginConfig.API.Fields.StringListField(config.rootPanel, "1-2 Exit", "exit12", Levels, "Level 1-3");
    exit13 = new PluginConfig.API.Fields.StringListField(config.rootPanel, "1-3 Exit", "exit13", Levels, "Level 1-4");
    exit14 = new PluginConfig.API.Fields.StringListField(config.rootPanel, "1-4 Exit", "exit14", Levels, "Level 2-1");
    exit1S = new PluginConfig.API.Fields.StringListField(config.rootPanel, "1-S Exit", "exit1S", Levels, "Level 1-1");
    exit21 = new PluginConfig.API.Fields.StringListField(config.rootPanel, "2-1 Exit", "exit21", Levels, "Level 2-2");
    exit22 = new PluginConfig.API.Fields.StringListField(config.rootPanel, "2-2 Exit", "exit22", Levels, "Level 2-3");
    exit23 = new PluginConfig.API.Fields.StringListField(config.rootPanel, "2-3 Exit", "exit23", Levels, "Level 2-4");
    exit23s = new PluginConfig.API.Fields.StringListField(config.rootPanel, "2-3 Secret Exit", "exit23s", Levels, "Level 2-S");
    exit24 = new PluginConfig.API.Fields.StringListField(config.rootPanel, "2-4 Exit", "exit24", Levels, "Level 3-1");
    exit2S = new PluginConfig.API.Fields.StringListField(config.rootPanel, "2-S Exit", "exit2S", Levels, "Level 2-4");
    exit31 = new PluginConfig.API.Fields.StringListField(config.rootPanel, "3-1 Exit", "exit31", Levels, "Level 3-2");
    exit31s = new PluginConfig.API.Fields.StringListField(config.rootPanel, "3-1 Secret Exit", "exit31s", Levels, "Level P-1");
    exit32 = new PluginConfig.API.Fields.StringListField(config.rootPanel, "3-2 Exit", "exit32", Levels, "Intermission1");
    exitI1 = new PluginConfig.API.Fields.StringListField(config.rootPanel, "Intermission 1 Exit", "exitI1", Levels, "Level 4-1");
    exitP1 = new PluginConfig.API.Fields.StringListField(config.rootPanel, "P-1 Exit", "exitP1", Levels, "Level 3-2");
    exit41 = new PluginConfig.API.Fields.StringListField(config.rootPanel, "4-1 Exit", "exit41", Levels, "Level 4-2");
    exit42 = new PluginConfig.API.Fields.StringListField(config.rootPanel, "4-2 Exit", "exit42", Levels, "Level 4-3");
    exit42s = new PluginConfig.API.Fields.StringListField(config.rootPanel, "4-2 Secret Exit", "exit42s", Levels, "Level 4-S");
    exit43 = new PluginConfig.API.Fields.StringListField(config.rootPanel, "4-3 Exit", "exit43", Levels, "Level 4-4");
    exit44 = new PluginConfig.API.Fields.StringListField(config.rootPanel, "4-4 Exit", "exit44", Levels, "Level 5-1");
    exit4S = new PluginConfig.API.Fields.StringListField(config.rootPanel, "4-S Exit", "exit4S", Levels, "Level 4-3");
    exit51 = new PluginConfig.API.Fields.StringListField(config.rootPanel, "5-1 Exit", "exit51", Levels, "Level 5-2");
    exit51s = new PluginConfig.API.Fields.StringListField(config.rootPanel, "5-1 Secret Exit", "exit51s", Levels, "Level 5-S");
    exit52 = new PluginConfig.API.Fields.StringListField(config.rootPanel, "5-2 Exit", "exit52", Levels, "Level 5-3");
    exit53 = new PluginConfig.API.Fields.StringListField(config.rootPanel, "5-3 Exit", "exit53", Levels, "Level 5-4");
    exit54 = new PluginConfig.API.Fields.StringListField(config.rootPanel, "5-4 Exit", "exit54", Levels, "Level 6-1");
    exit5S = new PluginConfig.API.Fields.StringListField(config.rootPanel, "5-S Exit", "exit5S", Levels, "Level 5-2");
    exit61 = new PluginConfig.API.Fields.StringListField(config.rootPanel, "6-1 Exit", "exit61", Levels, "Level 6-2");
    exit62 = new PluginConfig.API.Fields.StringListField(config.rootPanel, "6-2 Exit", "exit", Levels, "Intermission2");
    exit62s = new PluginConfig.API.Fields.StringListField(config.rootPanel, "6-2 Secret Exit", "exit62s", Levels, "Level P-2");
    exitI2 = new PluginConfig.API.Fields.StringListField(config.rootPanel, "Intermission 2 Exit", "exitI2", Levels, "Level 7-1");
    exitP2 = new PluginConfig.API.Fields.StringListField(config.rootPanel, "P-2 Exit", "exitP2", Levels, "Level 6-2");
    exit71 = new PluginConfig.API.Fields.StringListField(config.rootPanel, "7-1 Exit", "exit71", Levels, "Level 7-2");
    exit72 = new PluginConfig.API.Fields.StringListField(config.rootPanel, "7-2 Exit", "exit72", Levels, "Level 7-3");
    exit73 = new PluginConfig.API.Fields.StringListField(config.rootPanel, "7-3 Exit", "exit73", Levels, "Level 7-4");
    exit73s = new PluginConfig.API.Fields.StringListField(config.rootPanel, "7-3 Secret Exit", "exit73s", Levels, "Level 7-S");
    exit74 = new PluginConfig.API.Fields.StringListField(config.rootPanel, "7-4 Exit", "exit74", Levels, "Level 8-1");
    exit7s = new PluginConfig.API.Fields.StringListField(config.rootPanel, "7-S Exit", "exit7s", Levels, "Level 7-4");
    exit81 = new PluginConfig.API.Fields.StringListField(config.rootPanel, "8-1 Exit", "exit81", Levels, "Level 8-2");
    exit82 = new PluginConfig.API.Fields.StringListField(config.rootPanel, "8-2 Exit", "exit82", Levels, "Level 8-3");
    exit83 = new PluginConfig.API.Fields.StringListField(config.rootPanel, "8-3 Exit", "exit83", Levels, "Level 8-4");
    exit84 = new PluginConfig.API.Fields.StringListField(config.rootPanel, "8-4 Exit", "exit84", Levels, "EarlyAccessEnd");
    exit0e = new PluginConfig.API.Fields.StringListField(config.rootPanel, "0-E Exit", "exit0e", Levels, "Level 1-E");
    exit1e = new PluginConfig.API.Fields.StringListField(config.rootPanel, "1-E Exit", "exit1e", Levels, "Main Menu");
  }
}

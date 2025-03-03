using PluginConfig.API;
using PluginConfig.API.Fields;
using System.IO;
using System.Reflection;

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
    "Level 0-E",
    "Level 1-E",
    "Main Menu",
    "The Cyber Grind",
    "Sandbox",
    "Credits Museum",
    "EarlyAccessEnd",
    "Close Game",
  };

  public static StringListField exit01; 
  public static StringListField exit02; 
  public static StringListField exit02s; 
  public static StringListField exit03; 
  public static StringListField exit04; 
  public static StringListField exit05; 
  public static StringListField exit0S; 
  public static StringListField exit11; 
  public static StringListField exit11s; 
  public static StringListField exit12; 
  public static StringListField exit13; 
  public static StringListField exit14; 
  public static StringListField exit1S; 
  public static StringListField exit21; 
  public static StringListField exit22; 
  public static StringListField exit23; 
  public static StringListField exit23s; 
  public static StringListField exit24; 
  public static StringListField exit2S; 
  public static StringListField exit31; 
  public static StringListField exit31s; 
  public static StringListField exit32; 
  public static StringListField exitI1; 
  public static StringListField exitP1; 
  public static StringListField exit41; 
  public static StringListField exit42; 
  public static StringListField exit42s; 
  public static StringListField exit43; 
  public static StringListField exit44; 
  public static StringListField exit4S; 
  public static StringListField exit51; 
  public static StringListField exit51s; 
  public static StringListField exit52; 
  public static StringListField exit53; 
  public static StringListField exit54; 
  public static StringListField exit5S; 
  public static StringListField exit61; 
  public static StringListField exit62; 
  public static StringListField exit62s; 
  public static StringListField exitI2; 
  public static StringListField exitP2; 
  public static StringListField exit71; 
  public static StringListField exit72; 
  public static StringListField exit73; 
  public static StringListField exit73s; 
  public static StringListField exit74; 
  public static StringListField exit7s; 
  public static StringListField exit0e; 
  public static StringListField exit1e; 

  public static PluginConfigurator config;

  public static void Initialize() {
    config = PluginConfigurator.Create(Plugin.PLUGIN_NAME, Plugin.PLUGIN_GUID);

    string workingPath = Assembly.GetExecutingAssembly().Location;
    string workingDir = Path.GetDirectoryName(workingPath);
    config.SetIconWithURL(Path.Combine(workingDir, "icon.png"));

    exit01 = new StringListField(config.rootPanel, "0-1 Exit", "exit01", Levels, "Level 0-2");
    exit02 = new StringListField(config.rootPanel, "0-2 Exit", "exit02", Levels, "Level 0-3");
    exit02s = new StringListField(config.rootPanel, "0-2 Secret Exit", "exit02s", Levels, "Level 0-S");
    exit03 = new StringListField(config.rootPanel, "0-3 Exit", "exit03", Levels, "Level 0-4");
    exit04 = new StringListField(config.rootPanel, "0-4 Exit", "exit04", Levels, "Level 0-5");
    exit05 = new StringListField(config.rootPanel, "0-5 Exit", "exit05", Levels, "Level 1-1");
    exit0S = new StringListField(config.rootPanel, "0-S Exit", "exit0S", Levels, "Level 0-3");
    exit11 = new StringListField(config.rootPanel, "1-1 Exit", "exit11", Levels, "Level 1-2");
    exit11s = new StringListField(config.rootPanel, "1-1 Secret Exit", "exit11s", Levels, "Level 1-S");
    exit12 = new StringListField(config.rootPanel, "1-2 Exit", "exit12", Levels, "Level 1-3");
    exit13 = new StringListField(config.rootPanel, "1-3 Exit", "exit13", Levels, "Level 1-4");
    exit14 = new StringListField(config.rootPanel, "1-4 Exit", "exit14", Levels, "Level 2-1");
    exit1S = new StringListField(config.rootPanel, "1-S Exit", "exit1S", Levels, "Level 1-1");
    exit21 = new StringListField(config.rootPanel, "2-1 Exit", "exit21", Levels, "Level 2-2");
    exit22 = new StringListField(config.rootPanel, "2-2 Exit", "exit22", Levels, "Level 2-3");
    exit23 = new StringListField(config.rootPanel, "2-3 Exit", "exit23", Levels, "Level 2-4");
    exit23s = new StringListField(config.rootPanel, "2-3 Secret Exit", "exit23s", Levels, "Level 2-S");
    exit24 = new StringListField(config.rootPanel, "2-4 Exit", "exit24", Levels, "Level 3-1");
    exit2S = new StringListField(config.rootPanel, "2-S Exit", "exit2S", Levels, "Level 2-4");
    exit31 = new StringListField(config.rootPanel, "3-1 Exit", "exit31", Levels, "Level 3-2");
    exit31s = new StringListField(config.rootPanel, "3-1 Secret Exit", "exit31s", Levels, "Level P-1");
    exit32 = new StringListField(config.rootPanel, "3-2 Exit", "exit32", Levels, "Intermission1");
    exitI1 = new StringListField(config.rootPanel, "Intermission 1 Exit", "exitI1", Levels, "Level 4-1");
    exitP1 = new StringListField(config.rootPanel, "P-1 Exit", "exitP1", Levels, "Level 3-2");
    exit41 = new StringListField(config.rootPanel, "4-1 Exit", "exit41", Levels, "Level 4-2");
    exit42 = new StringListField(config.rootPanel, "4-2 Exit", "exit42", Levels, "Level 4-3");
    exit42s = new StringListField(config.rootPanel, "4-2 Secret Exit", "exit42s", Levels, "Level 4-S");
    exit43 = new StringListField(config.rootPanel, "4-3 Exit", "exit43", Levels, "Level 4-4");
    exit44 = new StringListField(config.rootPanel, "4-4 Exit", "exit44", Levels, "Level 5-1");
    exit4S = new StringListField(config.rootPanel, "4-S Exit", "exit4S", Levels, "Level 4-3");
    exit51 = new StringListField(config.rootPanel, "5-1 Exit", "exit51", Levels, "Level 5-2");
    exit51s = new StringListField(config.rootPanel, "5-1 Secret Exit", "exit51s", Levels, "Level 5-S");
    exit52 = new StringListField(config.rootPanel, "5-2 Exit", "exit52", Levels, "Level 5-3");
    exit53 = new StringListField(config.rootPanel, "5-3 Exit", "exit53", Levels, "Level 5-4");
    exit54 = new StringListField(config.rootPanel, "5-4 Exit", "exit54", Levels, "Level 6-1");
    exit5S = new StringListField(config.rootPanel, "5-S Exit", "exit5S", Levels, "Level 5-2");
    exit61 = new StringListField(config.rootPanel, "6-1 Exit", "exit61", Levels, "Level 6-2");
    exit62 = new StringListField(config.rootPanel, "6-2 Exit", "exit", Levels, "Intermission2");
    exit62s = new StringListField(config.rootPanel, "6-2 Secret Exit", "exit62s", Levels, "Level P-2");
    exitI2 = new StringListField(config.rootPanel, "Intermission 2 Exit", "exitI2", Levels, "Level 7-1");
    exitP2 = new StringListField(config.rootPanel, "P-2 Exit", "exitP2", Levels, "Level 6-2");
    exit71 = new StringListField(config.rootPanel, "7-1 Exit", "exit71", Levels, "Level 7-2");
    exit72 = new StringListField(config.rootPanel, "7-2 Exit", "exit72", Levels, "Level 7-3");
    exit73 = new StringListField(config.rootPanel, "7-3 Exit", "exit73", Levels, "Level 7-4");
    exit73s = new StringListField(config.rootPanel, "7-3 Secret Exit", "exit73s", Levels, "Level 7-S");
    exit74 = new StringListField(config.rootPanel, "7-4 Exit", "exit74", Levels, "EarlyAccessEnd");
    exit7s = new StringListField(config.rootPanel, "7-S Exit", "exit7s", Levels, "Level 7-4");
    exit0e = new StringListField(config.rootPanel, "0-E Exit", "exit0e", Levels, "Level 1-E");
    exit1e = new StringListField(config.rootPanel, "1-E Exit", "exit1e", Levels, "Main Menu");
  }
}

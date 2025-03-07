using System;
using PluginConfig.API.Fields;

namespace ExitDoorManager;

public static class Randomizer {

  private static T[] Shuffle<T>(T[] array) {
    Random rand = new Random();
    for (int i = 0; i < array.Length - 1; i++) {
      int x = rand.Next(0, array.Length);
      if (x != i) {
        T temp = array[i];
        array[i] = array[x];
        array[x] = temp;
      }
    }
    return array;
  }
  
  private static string[] GetShuffledLevels() {
    string[] levels = ConfigManager.Levels;
    string endingLevel = ConfigManager.finalScene.value;
    bool enableSecret = ConfigManager.allowSecretLevels.value;
    bool enablePrime = ConfigManager.allowPrimeSanctums.value;
    bool enableEncore = ConfigManager.allowEncoreLevels.value;

    Random rand = new Random();
    string[] shuffled = Shuffle(Array.FindAll(levels, l => (
        (l.StartsWith("Level")) && (!l.Equals("Level 0-1")) && (!l.Equals(endingLevel)) &&
        (enableSecret || !l.EndsWith("S")) &&
        (enablePrime || !l.StartsWith("Level P-")) &&
        (enableEncore || !l.EndsWith("E")) 
    )));

    return shuffled;
  }

  private static StringListField GetLevelExit(string level) {
    string levelNumber = level.Substring(6);
    StringListField[] exits = ConfigManager.GetExits();
    StringListField exit = Array.Find(exits, e => (
          e.displayName.Contains(levelNumber) && !e.displayName.Contains("Secret")
    ));
    return exit;
  }

  private static StringListField GetLevelSecretExit(string level) {
    string levelNumber = level.Substring(6);
    StringListField[] exits = ConfigManager.GetExits();
    StringListField exit = Array.Find(exits, e => (
          e.displayName.Contains(levelNumber) && e.displayName.Contains("Secret")
    ));
    return exit;
  }

  public static void RandomizeExits() {
    string[] shuffledLevels = GetShuffledLevels();
    string finalScene = ConfigManager.finalScene.value;

    int k = 0;
    string currentLevel = "Level 0-1";
    while (k < shuffledLevels.Length) {
      string nextLevel = shuffledLevels[k];
      /*Console.WriteLine($"{currentLevel} --> {nextLevel}");*/
      GetLevelExit(currentLevel).value = nextLevel;
      if (GetLevelSecretExit(currentLevel) != null) {
        GetLevelSecretExit(currentLevel).value = nextLevel;
      }
      currentLevel = nextLevel;
      k++;
    }
    /*Console.WriteLine($"{currentLevel} --> {ConfigManager.finalScene.value}");*/
    GetLevelExit(currentLevel).value = finalScene;
    if (finalScene.StartsWith("Level")) {
      GetLevelExit(finalScene).value = "Main Menu";
    }
  }

}

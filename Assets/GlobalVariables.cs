using System;

public static class GlobalVariables
{
    public static int MeditationPractice = 0;
    public static string SceneToPlay = "frozenforest.mp4";
    public static int TimeMinutes = 7;
    public static int SoundOn = 1;

    // Last used/played.
    public static DateTime FrozenLakeTime = DateTime.MinValue;
    public static DateTime FrozenForestTime = DateTime.MinValue;
    public static DateTime FrozenIslandTime = DateTime.MinValue;
    public static DateTime ClearMindTime = DateTime.MinValue;
    public static DateTime RelaxTime = DateTime.MinValue;
    public static DateTime CalmingTime = DateTime.MinValue;
    public static DateTime PowerTime = DateTime.MinValue;
    public static DateTime HarmonyTime = DateTime.MinValue;
    public static DateTime AntiStressTime = DateTime.MinValue;
    public static DateTime AntiAppetiteTime = DateTime.MinValue;

    public static bool IsVideoScenePaused = false;
}
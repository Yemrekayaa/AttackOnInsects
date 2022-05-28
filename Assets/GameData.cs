using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameData
{
    private static int gold;
    private static int highScore;
    private static int totalKill;
    private static int selectedClass;

    private static int selectedLevel;

    public static int HighScore { get => highScore; set => highScore = value; }
    public static int Gold { get => gold; set => gold = value; }
    public static int TotalKill { get => totalKill; set => totalKill = value; }
    public static int SelectedClass { get => selectedClass; set => selectedClass = value; }
    public static int SelectedLevel { get => selectedLevel; set => selectedLevel = value; }
}

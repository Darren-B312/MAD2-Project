using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverMenu : MonoBehaviour
{
    public GameObject gameOverMenuUI;
    public static bool GameIsOver = false;

    public void ShowMenu()
    {
        gameOverMenuUI.SetActive(true);
    }
}

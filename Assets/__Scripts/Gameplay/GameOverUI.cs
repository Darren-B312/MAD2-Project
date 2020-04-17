using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // restart the game scene
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0); // load the main menu scene
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // Brackeys - https://youtu.be/zc8ac_qUXQY?t=543
    }

    public void Quit()
    {
        //Debug.Log("Quit");
        Application.Quit();
    }
}

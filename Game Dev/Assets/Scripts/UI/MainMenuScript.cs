using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public void newGame()
    {
        SceneManager.LoadScene("Assets/Scenes/Level1.unity");
    }

    public void loadGame()
    {
        if (!DataManager.LoadJsonData())
        {
            newGame();
        }
    }

    public void quitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}

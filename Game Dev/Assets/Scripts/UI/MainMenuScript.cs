using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public void newGame()
    {
        SceneManager.LoadScene(1);
    }

    public void loadGame()
    {
        //to be continued when save game feature is implemented
        SceneManager.LoadScene(1);
    }

    public void quitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}

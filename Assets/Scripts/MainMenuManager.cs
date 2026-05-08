using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void PlayButton ()
    {
        SceneManager.LoadScene("FinalScene");
    }

    public void QuitButton ()
    {
        Application.Quit();
    }   
}

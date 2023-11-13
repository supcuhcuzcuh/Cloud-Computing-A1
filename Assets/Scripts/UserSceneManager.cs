using UnityEngine;
using UnityEngine.SceneManagement;

public class UserSceneManager : MonoBehaviour
{
    public void OnStartClick()
    {
        SceneManager.LoadScene("Login");
    }

    public void OnStartExitClick()
    {
        Application.Quit();
    }

    public void OnLoginClickRegister()
    {
        SceneManager.LoadScene("Register");
    }

    public void OnLoginClickForgetPassword()
    {
        SceneManager.LoadScene("ForgetPassword");
    }

    public void OnLoginClickUseUsername()
    {   
        SceneManager.LoadScene("LoginUsername");
    }

    public void OnUserClickClose()
    {
        int activeSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int num = 1;
        if (activeSceneIndex == 5)
        {
            num = 2;
        }
        else if (activeSceneIndex == 6)
        {
            num = 3;
        }
        SceneManager.LoadScene(activeSceneIndex - num);
    }
}

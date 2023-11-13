using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UserLogoutManagment : MonoBehaviour
{
    [SerializeField] private GameObject confirmLogout;
    public void OnPressLogout()
    {
        confirmLogout.SetActive(true);
    }
    public void OnClickYes()
    {
        SceneManager.LoadScene(2);
    }
    public void OnClickNo()
    {
        confirmLogout.SetActive(false);
    }
}

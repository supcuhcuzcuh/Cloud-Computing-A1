using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using UnityEngine.SceneManagement;
public class PFUserMgt : MonoBehaviour
{
    [SerializeField] TMP_Text msgbox;
    [SerializeField] TMP_InputField if_username, if_email, if_password, if_currentScore, if_displayName;

    void UpdateMsg(string msg) // to display in console and messagebox
    {
        Debug.Log(msg);
        msgbox.text = msg;
    }
    void OnError(PlayFabError e) // handle error
    {
        UpdateMsg("Error" + e.GenerateErrorReport());
    }
    public void OnButtonRegUser()
    {
        var regReq = new RegisterPlayFabUserRequest
        {
            Email = if_email.text,
            Password = if_password.text,
            Username = if_username.text
        };
        // execute request by calling playfab api
        PlayFabClientAPI.RegisterPlayFabUser(regReq, OnRegSucc, OnError);
    }
    public void OnButtonLogin()
    {
        var loginReq = new LoginWithEmailAddressRequest
        {
            Email = if_email.text,
            Password = if_password.text
        };
        PlayFabClientAPI.LoginWithEmailAddress(loginReq, OnLoginSucc, OnError);
    }
    public void OnButtonLogout()
    {
        PlayFabClientAPI.ForgetAllCredentials();
        msgbox.text = "Logout Success";
    }
    void OnLoginSucc(LoginResult r)
    {
        msgbox.text = "Login Success" + r.PlayFabId;
        SceneManager.LoadScene(1);
    }
    void OnRegSucc(RegisterPlayFabUserResult r) // handle success
    {
        msgbox.text = "Register Success" + r.PlayFabId;

        // to create a player display name
        var req = new UpdateUserTitleDisplayNameRequest
        {
            DisplayName = if_displayName.text,
        };
        PlayFabClientAPI.UpdateUserTitleDisplayName(req, OnDisplayNameUpdate, OnError);
    }
    void OnDisplayNameUpdate(UpdateUserTitleDisplayNameResult r)
    {
        UpdateMsg("display name updated! " + r.DisplayName);
    }
    public void OnButtonLoginEmail() // login using email + password
    {
        var loginRequest = new LoginWithEmailAddressRequest
        {
            Email = if_email.text,
            Password = if_password.text,
            //to get player profile, to get display name
            InfoRequestParameters = new GetPlayerCombinedInfoRequestParams
            {
                GetPlayerProfile = true
            }
        };
        PlayFabClientAPI.LoginWithEmailAddress(loginRequest, OnLoginSucc, OnError);
    }
    public void OnButtonLoginUsername()
    {
        var loginRequest = new LoginWithPlayFabRequest
        {
            Username = if_username.text,
            Password = if_password.text,
            // to get player profile, including displyName
            InfoRequestParameters = new GetPlayerCombinedInfoRequestParams
            {
                GetPlayerProfile = true
            }
        };
        PlayFabClientAPI.LoginWithPlayFab(loginRequest, OnLoginSucc, OnError);
    }
    public void OnButtonGetLeaderboard()
    {
        var lbreq = new GetLeaderboardRequest
        {
            StatisticName = "highscore",//playfab leaderboard statistic name
            StartPosition = 0,
            MaxResultsCount = 10
        };
        PlayFabClientAPI.GetLeaderboard(lbreq, OnLeaderboardGet, OnError);
    }
    void OnLeaderboardGet(GetLeaderboardResult r)
    {
        string LeaderBoardStr = "Leaderboard\n";
        foreach (var item in r.Leaderboard)
        {
            string onerow = item.Position + "/" + item.PlayFabId + "/" + item.DisplayName + "/" + item.StatValue + "\n";
            LeaderBoardStr += onerow; // combine all display into my string
        }
        UpdateMsg(LeaderBoardStr);
    }
    public void OnButtonSendLeaderboard()
    {
        var req = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate> // playfab leaderboard statistic name
            {
                new StatisticUpdate
                {
                    StatisticName =  "highscore",
                    Value = int.Parse(if_currentScore.text)
                }
            }
        };
        UpdateMsg("Submitting score:  " + if_currentScore.text);
        PlayFabClientAPI.UpdatePlayerStatistics(req, OnLeaderboardUpdate, OnError);
    }
    void OnLeaderboardUpdate(UpdatePlayerStatisticsResult r)
    {
        UpdateMsg("Successful leaderboard sent: " + r.ToString());
    }
}

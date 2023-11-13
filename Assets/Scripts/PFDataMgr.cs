using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

public class PFDataMgr : MonoBehaviour
{
    [SerializeField] TMP_Text XPDisplay;
    [SerializeField] TMP_InputField XPInput;
    public void SetUserData()
    {
        PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest()
        {
            Data = new Dictionary<string, string>()
            {
                {"XP", XPInput.text.ToString()}
            }
        },
        result => Debug.Log("Sucessfully updated user Data"),
        error =>
        {
            Debug.Log("Got error setting user data XP");
            Debug.Log(error.GenerateErrorReport());
        });
    }
    public void GetUserData()
    {
        PlayFabClientAPI.GetUserData(new GetUserDataRequest()
        {

        },
        result =>
        {
            Debug.Log("Got user data: ");
            if (result.Data == null || !result.Data.ContainsKey("XP"))
                Debug.Log("NO XP");
            else
            {
                Debug.Log("XP: " + result.Data["XP"].Value);
                XPDisplay.text = "XP: " + result.Data["XP"].Value;
            }
        },
        (error) =>
        {
            Debug.Log("Got error retrieving user data:  ");
            Debug.Log(error.GenerateErrorReport());
        });
    }
}

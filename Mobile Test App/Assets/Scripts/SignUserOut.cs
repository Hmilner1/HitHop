using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;
using UnityEngine.UI;

public class SignUserOut : MonoBehaviour
{
    private void Update()
    {
        var currentUser = FirebaseAuth.DefaultInstance.CurrentUser;
        if (currentUser != null)
        {
            Button LogOutButton = this.gameObject.GetComponent<Button>();
            LogOutButton.interactable = true;
        }
        else
        {
            Button LogOutButton = this.gameObject.GetComponent<Button>();
            LogOutButton.interactable = false;
        }
    }
    public void OnClickSignOut()
    {
        FirebaseAuth.DefaultInstance.SignOut();

    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Firebase.Auth;

public class LoginMan : MonoBehaviour
{
    GameObject m_RegisterPanel;
    GameObject m_RegErrorText;
    GameObject m_LogInErrorText;

    TMP_InputField m_EmailRegister;
    TMP_InputField m_PasswordRegister;
    TMP_InputField m_ConfirmPasswordRegister;
    Button m_ConfirmRegButton;

    TMP_InputField m_EmailLogIn;
    TMP_InputField m_PasswordLogIn;
    Button m_LogInButton;

    private void Start()
    {
        //register object gathering 
        m_EmailRegister = GameObject.Find("Register Email Text Field").GetComponent<TMP_InputField>();
        m_PasswordRegister = GameObject.Find("Register Pass Text Field").GetComponent<TMP_InputField>();
        m_ConfirmPasswordRegister = GameObject.Find("Confirm Pass Text Field").GetComponent<TMP_InputField>();
        m_ConfirmRegButton = GameObject.Find("Confirm Reg Button").GetComponent<Button>();
        m_RegErrorText = GameObject.Find("Register Error Text");
        m_RegErrorText.SetActive(false);
        m_RegisterPanel = GameObject.Find("Register Panel");
        m_RegisterPanel.SetActive(false);

        //Login object gathering
        m_LogInErrorText = GameObject.Find("LogIn Error Text");
        m_LogInErrorText.SetActive(false);
        m_EmailLogIn = GameObject.Find("Email Text Field").GetComponent<TMP_InputField>();
        m_PasswordLogIn = GameObject.Find("Pass Text Field").GetComponent<TMP_InputField>();
        m_LogInButton = GameObject.Find("Login Button").GetComponent<Button>();
        m_LogInButton.interactable = false;
    }

    private void Update()
    {
        if (!string.IsNullOrEmpty(m_EmailLogIn.text))
        {
            if (!string.IsNullOrEmpty(m_PasswordLogIn.text))
            {
                m_LogInButton.interactable = true;
            }
        }

        if (m_RegisterPanel.activeInHierarchy == true)
        {
            if (!string.IsNullOrEmpty(m_EmailRegister.text))
            {
                if (!string.IsNullOrEmpty(m_PasswordRegister.text))
                {
                    if (!string.IsNullOrEmpty(m_ConfirmPasswordRegister.text) && m_PasswordRegister.text == m_ConfirmPasswordRegister.text)
                    {
                        m_ConfirmRegButton.interactable = true;
                    }
                }
            }
            else
            {
                m_ConfirmRegButton.interactable = false;
            }
        }
    }

    public void OnClickRegister()
    {
        m_RegisterPanel.SetActive(true);
    }

    public void OnClickConfirmRegister()
    {
        StartCoroutine(RegisterUser());
    }

    private IEnumerator RegisterUser()
    {
        var auth = FirebaseAuth.DefaultInstance;
        var registerTask = auth.CreateUserWithEmailAndPasswordAsync(m_EmailRegister.text, m_PasswordRegister.text);
        yield return new WaitUntil(() => registerTask.IsCompleted);
        if (registerTask.Exception != null)
        {
            m_RegErrorText.SetActive(true);
        }
        else
        {
            m_RegisterPanel.SetActive(false);
        }
    }

    public void OnClickLogIn()
    {
        StartCoroutine(LogInUser());
    }

    private IEnumerator LogInUser()
    {
        var auth = FirebaseAuth.DefaultInstance;
        var logInTask = auth.SignInWithEmailAndPasswordAsync(m_EmailLogIn.text, m_PasswordLogIn.text);
        yield return new WaitUntil(() => logInTask.IsCompleted);

        if (logInTask.Exception != null)
        {
            m_LogInErrorText.SetActive(true);
            TMP_Text errorText = m_LogInErrorText.GetComponent<TMP_Text>();
            errorText.text = "Error";

        }
        else
        {
            m_LogInErrorText.SetActive(true);
            var currentUser = FirebaseAuth.DefaultInstance.CurrentUser;
            TMP_Text errorText = m_LogInErrorText.GetComponent<TMP_Text>();
            errorText.text = "Welcome " + currentUser.UserId.ToString();
        }
    }

    public void OnClickCloseRegister()
    { 
        m_RegisterPanel.SetActive(false);
    }
}

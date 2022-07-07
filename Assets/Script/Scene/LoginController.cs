using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using System.Net;
using System.IO;
using RestClient.Core.Models;
using RestClient.Core;
using System.Net.Http;
using UnityEngine.UI;
using DG.Tweening;

public class LoginController : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_InputField userName;
    [SerializeField] private TMPro.TMP_InputField password;
    [SerializeField] private GameObject registerPopup;

    [SerializeField] private TMPro.TMP_InputField userNameRegister;
    [SerializeField] private TMPro.TMP_InputField passwordRegister;
    [SerializeField] private TMPro.TMP_InputField emailRegister;
    [SerializeField] private TMPro.TMP_InputField phoneRegister;
    [SerializeField] private TMPro.TMP_InputField addressRegister;
    [SerializeField] private Toggle isCustomer;

    [SerializeField] private TMPro.TextMeshProUGUI textError;
    [SerializeField] private GameObject loadingObject;


    public static LoginController instance;
    private string passwordTemp;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        registerPopup.SetActive(false);
        loadingObject.SetActive(false);

    }

    public void OnClick_Login()
    {
        loadingObject.SetActive(true);
        APIHelper.Instance.Login(userName.text, password.text);
    }

    public void OnClick_Register()
    {
        registerPopup.SetActive(true);
    }

    public void OnClick_ExitRegister()
    {
        registerPopup.SetActive(false);
    }

    public void OnClick_DoneRegister()
    {
        if (userNameRegister.text == "")
        {
            SetTextError("username isn't entered");
            return;
        }

        if (passwordRegister.text == "")
        {
            SetTextError("password isn't entered");
            return;
        }

        if (emailRegister.text == "")
        {
            SetTextError("email isn't entered");
            return;
            
        }
        else if(!IsValidEmail(emailRegister.text))
        {
            SetTextError("email isn't true");
            return;
        }

        if (phoneRegister.text == "")
        {
            SetTextError("phone isn't entered");
            return;
        }
        else
        {
            int phone;
            if (!int.TryParse(phoneRegister.text, out phone))
            {
                SetTextError("phone only has number");
                return;
            }
        }

        if (addressRegister.text == "")
        {
            SetTextError("address isn't entered");
            return;
        }

        int roleId = isCustomer.isOn ? 1 : 0;
        loadingObject.SetActive(true);
        APIHelper.Instance.Register(userNameRegister.text, passwordRegister.text, emailRegister.text, phoneRegister.text, addressRegister.text, roleId);
    }

    bool IsValidEmail(string email)
    {
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }

    public void SetTextError(string text)
    {
        loadingObject.SetActive(false);
        textError.gameObject.SetActive(true);
        textError.text = text;
        DOVirtual.DelayedCall(2.0f, () => { textError.gameObject.SetActive(false); });
    }
}
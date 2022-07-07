using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditUserDetailPopup : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_InputField passwordRegister;
    [SerializeField] private TMPro.TMP_InputField passwordAgainRegister;
    [SerializeField] private TMPro.TMP_InputField emailRegister;
    [SerializeField] private TMPro.TMP_InputField phoneRegister;
    [SerializeField] private TMPro.TMP_InputField addressRegister;

    [SerializeField] private GameObject loadingObject;
    [SerializeField] private ErrorPopup errorPopup;

    public void OnEnable()
    {
        emailRegister.text = UserManager.Instance.getUserDetail().email;
        phoneRegister.text = UserManager.Instance.getUserDetail().phoneNumber;
        addressRegister.text = UserManager.Instance.getUserDetail().address;
    }

    public void OnClick_DoneUpdate()
    {

        if (passwordRegister.text == "")
        {
            errorPopup.InitText("password isn't entered");
            return;
        }

        if (passwordAgainRegister.text == "" || !passwordAgainRegister.text.Equals(passwordRegister.text))
        {
            Debug.Log("Vo day");
            errorPopup.InitText("Repeat password wrong");
            return;
        }

        if (emailRegister.text == "")
        {
            errorPopup.InitText("email isn't entered");
            return;

        }
        else if (!IsValidEmail(emailRegister.text))
        {
            errorPopup.InitText("email isn't true");
            return;
        }

        if (phoneRegister.text == "")
        {
            errorPopup.InitText("phone isn't entered");
            return;
        }
        else
        {
            int phone;
            if (!int.TryParse(phoneRegister.text, out phone))
            {
                errorPopup.InitText("phone only has number");
                return;
            }
        }

        if (addressRegister.text == "")
        {
            errorPopup.InitText("address isn't entered");
            return;
        }

        loadingObject.SetActive(true);
        APIHelper.Instance.UpdateUser(passwordRegister.text, emailRegister.text, phoneRegister.text, addressRegister.text);
    }

    public void OnCLick_Close()
    {
        this.gameObject.SetActive(false);
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

}

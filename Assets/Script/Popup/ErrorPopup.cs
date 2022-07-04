using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErrorPopup : MonoBehaviour
{
    public static ErrorPopup Instance;

    [SerializeField]
    private TMPro.TextMeshProUGUI errorText;

    private void Start()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    public void InitText(string text)
    {
        this.gameObject.SetActive(true);
        errorText.text = text;
        DOVirtual.DelayedCall(1f,() => OnCliCk_Exit());
    }

    public void OnCliCk_Exit()
    {
        this.gameObject.SetActive(false);
    }
}

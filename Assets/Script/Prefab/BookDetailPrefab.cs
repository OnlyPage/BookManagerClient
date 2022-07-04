using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BookDetailPrefab : MonoBehaviour
{
    public static BookDetailPrefab Instance;

    [SerializeField]
    private Image bookPic;
    [SerializeField]
    private TMPro.TextMeshProUGUI nameBook;
    [SerializeField]
    private TMPro.TextMeshProUGUI priceBook;
    [SerializeField]
    private TMPro.TextMeshProUGUI publicDate;
    [SerializeField]
    private TMPro.TextMeshProUGUI suplierBook;
    [SerializeField]
    private TMPro.TextMeshProUGUI numberBook;
    [SerializeField]
    private TMPro.TextMeshProUGUI arthurBook;
    [SerializeField]
    private TMPro.TextMeshProUGUI raitingBook;
    [SerializeField]
    private TMPro.TextMeshProUGUI categoryBook;
    [SerializeField]
    private TMPro.TextMeshProUGUI descriptionBook;
    [SerializeField]
    private Button upNumber;
    [SerializeField]
    private Button downNumber;

    private BookDetail bookDetail;
    private int number;

    private void Start()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    public void Init(BookDetail bookDetail, bool isStore = false)
    {
        this.bookDetail = bookDetail;
        nameBook.text = bookDetail.nameBook;
        priceBook.text = bookDetail.price.ToString();
        publicDate.text = bookDetail.publicDate;
        suplierBook.text = bookDetail.userName;
        numberBook.text = "1";
        arthurBook.text = bookDetail.author;
        raitingBook.text = Math.Round(bookDetail.rating, 2).ToString();
        categoryBook.text = bookDetail.category;
        number = 0;
        if(isStore)
        {
            numberBook.text = bookDetail.number.ToString();
        }
        upNumber.gameObject.SetActive(!isStore);
        downNumber.gameObject.SetActive(!isStore);
    }

    public void ClickOrder()
    {
        if (UserManager.Instance.getRole() == RoleType.CUSTOMER)
        {
            ShopController.Instance.OpenOrderPopup();
            OrderPopup.Instance.Init(bookDetail, number);
        }
    }

    public void ClickUp()
    {
        number++;
        numberBook.text = number.ToString();
        priceBook.text = (bookDetail.price * number).ToString();
    }

    public void ClickDown()
    {
        if(number <= 1)
        {
            return;
        }
        number--;
        numberBook.text = number.ToString();
        priceBook.text = (bookDetail.price * number).ToString();
    }
}

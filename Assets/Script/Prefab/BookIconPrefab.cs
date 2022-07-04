using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BookIconPrefab : MonoBehaviour
{
    [SerializeField]
    private Image bookImg;
    [SerializeField]
    private TMPro.TextMeshProUGUI bookName;
    [SerializeField]
    private TMPro.TextMeshProUGUI priceText;
    [SerializeField]
    private TMPro.TextMeshProUGUI ratingText;
    [SerializeField]
    private Button clickBookBtn;

    private BookDetail bookDetail;

    private void Start()
    {
        clickBookBtn.onClick.AddListener(OnClick_BookDetail);
    }

    public void Init(BookDetail bookDetail)
    {
        this.bookName.text = bookDetail.nameBook;
        priceText.text = bookDetail.price.ToString();
        ratingText.text = Math.Round(bookDetail.rating, 2).ToString();
        this.bookDetail = bookDetail;
    }

    public void OnClick_BookDetail()
    {
        ShopController.Instance.OnClick_BookDetail(bookDetail);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookPrefab : MonoBehaviour
{
    private BookDetail bookDetail;

    [SerializeField]
    private TMPro.TextMeshProUGUI nameBook;

    [SerializeField]
    private TMPro.TextMeshProUGUI numberBook;

    [SerializeField]
    private TMPro.TextMeshProUGUI priceBook;

    [SerializeField]
    private GameObject editBook;

    [SerializeField]
    private GameObject orderBook;

    private int index;
    private int price;
    private int number;

    public void InitEditBook(BookDetail bookDetail)
    {
        index = bookDetail.id;
        this.bookDetail = bookDetail;
        nameBook.text = bookDetail.nameBook;
        numberBook.text = bookDetail.number.ToString();
        priceBook.text = bookDetail.price.ToString();
        editBook.SetActive(true);
        orderBook.SetActive(false);
        priceBook.color = Color.black;
    }

    public void InitorderBook(int index, BookDetail bookDetail, int number)
    {
        this.index = index;
        this.bookDetail = bookDetail;
        nameBook.text = bookDetail.nameBook;
        numberBook.text = bookDetail.number.ToString();
        this.number = number;
        price = bookDetail.price * number;
        priceBook.text = price.ToString();
        editBook.SetActive(false);
        orderBook.SetActive(true);
        priceBook.color = Color.red;
    }

    public void OnClick_Info()
    {
        StoreController.Instance?.OnClick_BookDetail(bookDetail);
    }

    public void OnClick_Remove()
    {
        APIHelper.Instance.DeleteBookByID(index);
    }

    public void OnClick_Edit()
    {
        StoreController.Instance.OnClick_UpdateBook(bookDetail);
    }

    public void OnClick_Order()
    {
        APIHelper.Instance.CreateOrder(UserManager.Instance.getUserDetail().username, bookDetail.userName, 
            System.DateTime.UtcNow, price.ToString(), "0", bookDetail.id, number, () => {
                OrderPopup.Instance.CancleOrderById(index);
            });
        
    }

    public void OnClick_Cancle()
    {
        OrderPopup.Instance.CancleOrderById(index);
    }
}

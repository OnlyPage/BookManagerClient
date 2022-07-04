using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderPrefab : MonoBehaviour
{
    private BookDetail bookDetail;

    [SerializeField]
    private TMPro.TextMeshProUGUI nameBook;

    [SerializeField]
    private TMPro.TextMeshProUGUI numberBook;

    [SerializeField]
    private TMPro.TextMeshProUGUI priceBook;

    [SerializeField]
    private TMPro.TextMeshProUGUI stateOrder;

    [SerializeField]
    private Button detailBtn;

    [SerializeField]
    private Button acceptBtn;

    [SerializeField]
    private Button deniedBtn;


    private int index;
    private int price;
    private int number;

    public void InitEditBook(BookDetail bookDetail)
    {
        this.bookDetail = bookDetail;
        nameBook.text = bookDetail.nameBook;
        numberBook.text = bookDetail.number.ToString();
        priceBook.text = bookDetail.price.ToString();
        //editBook.SetActive(true);
        //orderBook.SetActive(false);
        priceBook.color = Color.black;
    }

    public void InitorderBook(OrderDetail orderDetail)
    {
        this.index = orderDetail.id;
        nameBook.text = orderDetail.nameBook;
        numberBook.text = orderDetail.number.ToString();
        this.number = orderDetail.number;
        price = orderDetail.price;
        priceBook.text = price.ToString();

        SetState(orderDetail.state);

        priceBook.color = Color.red;
    }

    public void OnClick_Detail()
    {

    }

    public void OnClick_Order()
    {
        APIHelper.Instance.UpdateStateOrder(index, "1");
    }

    public void OnClick_Cancle()
    {
        APIHelper.Instance.DeleteOrderByID(index, () => {
            OrderedPopup.Instance.CancleOrderById(index);
        });
    }

    public void SetState(int state)
    {
        if (state == 0)
        {
            deniedBtn.gameObject.SetActive(true);
            acceptBtn.gameObject.SetActive(true);
            stateOrder.text = "Waiting";
            stateOrder.color = Color.yellow;
        }
        else if (state == -1)
        {
            detailBtn.gameObject.SetActive(false);
            deniedBtn.gameObject.SetActive(true);
            stateOrder.text = "Denied";
            stateOrder.color = Color.red;
        }
        else if (state == 1)
        {
            detailBtn.gameObject.SetActive(true);
            deniedBtn.gameObject.SetActive(false);
            stateOrder.text = "Accpet";
            stateOrder.color = Color.green;
        }

        if (UserManager.Instance.getRole() == RoleType.CUSTOMER)
        {
            acceptBtn.gameObject.SetActive(false);
        }
    }    
}

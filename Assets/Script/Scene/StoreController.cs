using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoreController : MonoBehaviour
{
    public static StoreController Instance;

    [SerializeField]
    private StorePack storePack;
    [SerializeField]
    private BookDetailPopup bookDetailPopup;
    [SerializeField]
    private StoreOrderPopup storeOrderPopup;
    [SerializeField]
    private OrderedPopup orderedPopup;
    [SerializeField]
    private AddBookPopup addBookPopup;
    [SerializeField]
    private GameObject infoPopup;
    [SerializeField]
    private GameObject editUserPopup;

    private bool isShowInfoPopup;

    [SerializeField]
    private GameObject loadingShop;
    [SerializeField]
    private GameObject loadingUser;
    private bool isResetingBookShop;

    private void Awake()
    {
        if (UserManager.Instance.getUserDetail() == null)
            SceneManager.LoadScene("Main");
    }

    // Start is called before the first frame update
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        loadingShop.SetActive(false);
        isResetingBookShop = false;
        GetAllBook();
        GetAllBookByStoreName(UserManager.Instance.getUserDetail().username);
        GetAllOrderByStoreName(UserManager.Instance.getUserDetail().username);
        bookDetailPopup.gameObject.SetActive(false);
        storeOrderPopup.gameObject.SetActive(false);
        orderedPopup.gameObject.SetActive(false);
        addBookPopup.gameObject.SetActive(false);
        isShowInfoPopup = false;
        infoPopup.gameObject.SetActive(false);
    }

    public void GetAllBookByStoreName(string storeName)
    {
        APIHelper.Instance.GetAllBooksByStoreName(storeName);
    }

    public void GetAllOrderByStoreName(string storeName)
    {
        APIHelper.Instance.GetAllOrdersByStorename(storeName);
    }

    public void SetOrders(List<OrderDetail> orderDetails)
    {
        foreach(OrderDetail orderDetail in orderDetails)
        {
            if(orderDetail.state == 0)
            {
                storeOrderPopup.AddOrder(orderDetail);
            }
            else
            {
                orderedPopup.AddOrder(orderDetail);
            }
        }    
    }    

    public void SetBookStore(List<BookDetail> bookDetails)
    {
        loadingShop.SetActive(false);
        //this.bookDetails = bookDetails;
        storePack.SetBookPack(bookDetails);
    }

    public void AddBook(BookDetail book)
    {
        storePack.AddBook(book);
    }

    public void UpdateBook(BookDetail book)
    {
        storePack.UppdateBook(book);
    }

    public void RemoveBook(BookDetail bookDetail)
    {
        storePack.RemoveBook(bookDetail.id);
    }

    public void GetAllBook()
    {
        if (!isResetingBookShop)
        {
            Debug.Log("Reset ");
            isResetingBookShop = true;
            loadingShop.SetActive(true);
            APIHelper.Instance.GetAllBooks();
            DOVirtual.DelayedCall(3f, () => { isResetingBookShop = false; });
        }
    }    

    public void SetAllBook(List<BookDetail> bookDetails)
    {
        UserManager.Instance.SetAllBook(bookDetails);
    }

    public void OnClick_BookDetail(BookDetail bookDetail)
    {
        bookDetailPopup.gameObject.SetActive(true);
        storeOrderPopup.gameObject.SetActive(false);
        orderedPopup.gameObject.SetActive(false);
        bookDetailPopup.Init(bookDetail, true);
        addBookPopup.gameObject.SetActive(false);
    }

    public void OpenOrderPopup()
    {
        storeOrderPopup.gameObject.SetActive(true);
        bookDetailPopup.gameObject.SetActive(false);
        orderedPopup.gameObject.SetActive(false);
        addBookPopup.gameObject.SetActive(false);
    }

    public void ClickHome()
    {
        bookDetailPopup.gameObject.SetActive(false);
        storeOrderPopup.gameObject.SetActive(false);
        orderedPopup.gameObject.SetActive(false);
        addBookPopup.gameObject.SetActive(false);
    }

    public void OpenOrderedPopup()
    {
        storeOrderPopup.gameObject.SetActive(false);
        bookDetailPopup.gameObject.SetActive(false);
        orderedPopup.gameObject.SetActive(true);
        addBookPopup.gameObject.SetActive(false);
    }

    public void OnClick_AddBook()
    {
        storeOrderPopup.gameObject.SetActive(false);
        bookDetailPopup.gameObject.SetActive(false);
        orderedPopup.gameObject.SetActive(false);
        addBookPopup.gameObject.SetActive(true);
        addBookPopup.SetAdd(true);
    }

    public void OnClick_UpdateBook(BookDetail book)
    {
        storeOrderPopup.gameObject.SetActive(false);
        bookDetailPopup.gameObject.SetActive(false);
        orderedPopup.gameObject.SetActive(false);
        addBookPopup.gameObject.SetActive(true);
        addBookPopup.SetAdd(false, book);
    }

    public void OnClick_ShowInfoPopup()
    {
        isShowInfoPopup = !isShowInfoPopup;
        infoPopup.gameObject.SetActive(isShowInfoPopup);
    }

    public void OnClick_Logout()
    {
        SceneManager.LoadScene("Main");
    }

    public void OnClick_EditUser()
    {
        editUserPopup.SetActive(true);
    }

    public void OnScrollShopChange(Vector2 value)
    {
        Debug.Log(value.y);
        if (value.y > 1.06)
        {
            GetAllBook();
        }
    }

    public void SetLoadingPopup(bool isActive)
    {
        loadingUser.SetActive(isActive);
    }
}

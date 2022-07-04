using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopController : MonoBehaviour
{
    public static ShopController Instance;

    private List<BookDetail> bookRecommend;

    [SerializeField]
    private BookIconPrefab bookIconPrefab;
    [SerializeField]
    private PackPrefab recommendPack;
    [SerializeField]
    private PackPrefab allPack;
    [SerializeField]
    private BookDetailPopup bookDetailPopup;
    [SerializeField]
    private OrderPopup orderPopup;
    [SerializeField]
    private OrderedPopup orderedPopup;
    [SerializeField]
    private GameObject infoPopup;
    [SerializeField]
    private TMPro.TMP_InputField searchText;


    private bool isShowInfoPopup;

    private void Awake()
    {
        if(UserManager.Instance.getUserDetail() == null)
        SceneManager.LoadScene("Main");
    }

    // Start is called before the first frame update
    void Start()
    {
        if(Instance == null)
        {
            Instance = this;
        }

        if (UserManager.Instance.getUserDetail() != null)
        {
            bookRecommend = new List<BookDetail>();
            GetNumberBookRecommend(12);       
            GetAllBook();
            GetAllFeedBackByUsername(UserManager.Instance.getUserDetail().username);
        }
        bookDetailPopup.gameObject.SetActive(false);
        orderPopup.gameObject.SetActive(false);
        orderedPopup.gameObject.SetActive(false);
        isShowInfoPopup = false;
        infoPopup.gameObject.SetActive(false);
    }

    public void OnClick_SearchBookByName()
    {
        APIHelper.Instance.SearchBookByName(searchText.text);
    }

    public void SetSearchBook(List<BookDetail> bookDetails)
    {
        //this.bookDetails = bookDetails;
        allPack.SetBookPack(bookDetails);
    }

    public void SetBook(List<BookDetail> bookDetails)
    {
        //this.bookDetails = bookDetails;
        allPack.SetBookPack(bookDetails);
        UserManager.Instance.SetAllBook(bookDetails);
    }    

    public void DeleteBookById(int idBook)
    {
        APIHelper.Instance.DeleteBookByID(idBook);
    }    
    
    public void SetBookRecommend(List<BookDetail> books)
    {
        bookRecommend = books;
        recommendPack.SetBookPack(books);
    }

    public void SetOrders(List<OrderDetail> orderDetails)
    {
        orderedPopup.Init(orderDetails);
    }    

    public void GetNumberBookRecommend(int number)
    {
        APIHelper.Instance.GetAllBooksRecommend(UserManager.Instance.getUserDetail().username, number);

    }

    public void GetAllBook()
    {
        APIHelper.Instance.GetAllBooks();
    }

    public void GetAllFeedBackByUsername(string username)
    {
        APIHelper.Instance.GetAllOrdersByUsername(username);
    }

    public void OnClick_BookDetail(BookDetail bookDetail)
    {
        bookDetailPopup.gameObject.SetActive(true);
        orderPopup.gameObject.SetActive(false);
        orderedPopup.gameObject.SetActive(false);
        bookDetailPopup.Init(bookDetail);
    }    

    public void OpenOrderPopup()
    {
        orderPopup.gameObject.SetActive(true);
        bookDetailPopup.gameObject.SetActive(false);
        orderedPopup.gameObject.SetActive(false);
    }

    public void ClickHome()
    {
        bookDetailPopup.gameObject.SetActive(false);
        orderPopup.gameObject.SetActive(false);
        orderedPopup.gameObject.SetActive(false);
    }

    public void OpenOrderedPopup()
    {
        orderPopup.gameObject.SetActive(false);
        bookDetailPopup.gameObject.SetActive(false);
        orderedPopup.gameObject.SetActive(true);
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
}

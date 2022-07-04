using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using UnityEngine;
using UnityEngine.SceneManagement;

public class APIHelper : Singleton<APIHelper>
{
#if UNITY_EDITOR
    private readonly string baseUrl = "http://localhost:8080/";
#else
    private readonly string baseUrl = "https://tanle-book.herokuapp.com/";
#endif

    private readonly HttpClient client = new HttpClient();

    public async void Login(string username, string password)
    {
        var values = new Dictionary<string, string>
          {
              { "username", username },
              { "password", password}
          };

        var content = new FormUrlEncodedContent(values);

        var response = await client.PostAsync(baseUrl + "login", content);

        var responseString = await response.Content.ReadAsStringAsync();
        if (response.IsSuccessStatusCode)
        {
            UserManager.Instance.SetUserDetail(JsonUtility.FromJson<UserDetail>(responseString));
            if (UserManager.Instance.getUserDetail().roleDetail == "CUSTOMER")
            {
                SceneManager.LoadScene("Shop");
            }
            else
            {
                SceneManager.LoadScene("Suplier");
            }
        }
        else
        {
            var reponseError = JsonUtility.FromJson<ResponseClass>(responseString);
            if (reponseError.message != null)
            {
                LoginController.instance?.SetTextError(reponseError.message);
            }
            else
            {
                LoginController.instance?.SetTextError(reponseError.error);
            }
        }   
    }

    public async void Register(string username, string password, string email, string phoneNumber, string address, int role)
    {
        var values = new Dictionary<string, string>
          {
              { "username", username },
              { "password", password},
              { "phoneNumber", phoneNumber},
              { "email", email},
              { "address", address},
              { "role", role.ToString()}
          };

        var content = new FormUrlEncodedContent(values);

        var response = await client.PostAsync(baseUrl + "users", content);

        var responseString = await response.Content.ReadAsStringAsync();
        if (response.IsSuccessStatusCode)
        {
            UserManager.Instance.SetUserDetail(JsonUtility.FromJson<UserDetail>(responseString));
            if (UserManager.Instance.getUserDetail().roleDetail == "CUSTOMER")
            {
                SceneManager.LoadScene("Shop");
            }
            else
            {
                SceneManager.LoadScene("Suplier");
            }
        }
        else
        {
            var reponseError = JsonUtility.FromJson<ResponseClass>(responseString);
            LoginController.instance?.SetTextError(reponseError.message);
        }
    }

    public async void GetAllBooks()
    {
        var response = await client.GetAsync(baseUrl + "book/");
        var responseString = await response.Content.ReadAsStringAsync();
        ShopController.Instance?.SetBook(Cutil.ImportJsonObjest<List<BookDetail>>(responseString));
        StoreController.Instance?.SetBookStore(Cutil.ImportJsonObjest<List<BookDetail>>(responseString));
    }

    public async void GetAllBooksByStoreName(string storeName)
    {
        var response = await client.GetAsync(baseUrl + "book/store/" + storeName);
        var responseString = await response.Content.ReadAsStringAsync();
        StoreController.Instance?.SetBookStore(Cutil.ImportJsonObjest<List<BookDetail>>(responseString));
    }

    public async void GetAllBooksRecommend(string username, int number = -1, Action callBack = null)
    {
        var response = await client.GetAsync(baseUrl + "book/recommend/" + username);
        var responseString = await response.Content.ReadAsStringAsync();

        List<BookDetail> bookDetails = Cutil.ImportJsonObjest<List<BookDetail>>(responseString);
        if (bookDetails.Count < 12 || number < 0)
        {
            ShopController.Instance?.SetBookRecommend(bookDetails);
        }
        else
        {
            ShopController.Instance?.SetBookRecommend(bookDetails.GetRange(0, 12));
        }
        callBack?.Invoke();
    }

    public async void GetBookById(int idBook)
    {
        var response = await client.GetAsync(baseUrl + "book/" + idBook);
        var responseString = await response.Content.ReadAsStringAsync();
        ShopController.Instance?.SetBook(JsonUtility.FromJson<List<BookDetail>>(responseString));
    }

    public async void GetBookByName(string bookName)
    {
        var response = await client.GetAsync(baseUrl + "book/" + bookName);
        var responseString = await response.Content.ReadAsStringAsync();
        ShopController.Instance?.SetBook(JsonUtility.FromJson<List<BookDetail>>(responseString));
    }    

    public async void SearchBookByName(string bookName)
    {
        var response = await client.GetAsync(baseUrl + "book/search" + bookName);
        var responseString = await response.Content.ReadAsStringAsync();
        ShopController.Instance?.SetSearchBook(JsonUtility.FromJson<List<BookDetail>>(responseString));
    }

    public async void DeleteBookByID(int id)
    {
        var response = await client.DeleteAsync(baseUrl + "book/" + id);
        var responseString = await response.Content.ReadAsStringAsync();
        BookDetail bookDetail = Cutil.ImportJsonObjest<BookDetail>(responseString);
        //ShopController.Instance?.SetBook(JsonUtility.FromJson<List<BookDetail>>(responseString));
        StoreController.Instance?.RemoveBook(bookDetail);
    }

    public async void CreateBook(string nameBook, string author, string publicDate, string category, string userName, string number, string price)
    {
        var values = new Dictionary<string, string>
          {
              { "nameBook", nameBook},
              { "author", author},
              { "publicDate", publicDate},
              { "category", category},
              { "userName", userName},
              { "number", number.ToString()},
              { "price", price }
          };

        var content = new FormUrlEncodedContent(values);

        var response = await client.PostAsync(baseUrl + "book", content);

        var responseString = await response.Content.ReadAsStringAsync();
        BookDetail bookDetail = JsonUtility.FromJson<BookDetail>(responseString);
        StoreController.Instance?.AddBook(bookDetail);
        StoreController.Instance?.ClickHome();
    }

    public async void UpdateBook(int id, string nameBook, string author, string publicDate, string category, string userName, string number, string price)
    {
        var values = new Dictionary<string, string>
          {
              { "id", id.ToString() },
              { "nameBook", nameBook},
              { "author", author},
              { "publicDate", publicDate},
              { "category", category},
              { "userName", userName},
              { "number", number.ToString()},
              { "price", price }
          };

        var content = new FormUrlEncodedContent(values);

        var response = await client.PutAsync(baseUrl + "book", content);

        var responseString = await response.Content.ReadAsStringAsync();
        BookDetail bookDetail = JsonUtility.FromJson<BookDetail>(responseString);
        StoreController.Instance?.UpdateBook(bookDetail);
        StoreController.Instance?.ClickHome();
    }

    public async void GetAllOrdersByUsername(string username, Action callBack = null)
    {
        var response = await client.GetAsync(baseUrl + "order/customer/" + username);
        var responseString = await response.Content.ReadAsStringAsync();

        List<OrderDetail> orderDetails = Cutil.ImportJsonObjest<List<OrderDetail>>(responseString);
        ShopController.Instance?.SetOrders(orderDetails);
        callBack?.Invoke();
    }

    public async void GetAllOrdersByStorename(string storeName, Action callBack = null)
    {
        var response = await client.GetAsync(baseUrl + "order/store/" + storeName);
        var responseString = await response.Content.ReadAsStringAsync();

        List<OrderDetail> orderDetails = Cutil.ImportJsonObjest<List<OrderDetail>>(responseString);
        StoreController.Instance?.SetOrders(orderDetails);
        callBack?.Invoke();
    }

    public async void CreateOrder(string customerName, string storeName, DateTime dateOrder, string price, string state, int idBook, int number, Action action = null)
    {
        var values = new Dictionary<string, string>
          {
              { "customerName", customerName},
              { "storeName", storeName},
              { "dateOrder", dateOrder.ToString()},
              { "price", price},
              { "state", state},
              { "idBook", idBook.ToString()},
              { "number", number.ToString()},
          };

        var content = new FormUrlEncodedContent(values);

        var response = await client.PostAsync(baseUrl + "order", content);

        var responseString = await response.Content.ReadAsStringAsync();
        OrderDetail orderDetail = Cutil.ImportJsonObjest<OrderDetail>(responseString);

        action?.Invoke();
    }

    public async void UpdateOrder(int id, string customerName, string storeName, DateTime dateOrder, string price, string state, int idBook, int number)
    {
        var values = new Dictionary<string, string>
          {
              { "id", id.ToString()},
              { "customerName", customerName},
              { "storeName", storeName},
              { "dateOrder", dateOrder.ToString()},
              { "price", price},
              { "state", state},
              { "idBook", idBook.ToString()},
              { "number", number.ToString()},
          };

        var content = new FormUrlEncodedContent(values);

        var response = await client.PutAsync(baseUrl + "order", content);

        var responseString = await response.Content.ReadAsStringAsync();
        OrderDetail orderDetail = Cutil.ImportJsonObjest<OrderDetail>(responseString);
    }

    public async void UpdateStateOrder(int id, string state)
    {
        var values = new Dictionary<string, string>
          {
              { "id", id.ToString()},
          };

        var content = new FormUrlEncodedContent(values);

        var response = await client.PutAsync(baseUrl + "order/" + id + "/" + state, content);

        var responseString = await response.Content.ReadAsStringAsync();
        OrderDetail orderDetail = Cutil.ImportJsonObjest<OrderDetail>(responseString);
        OrderedPopup.Instance.Init(orderDetail); 
    }

    public async void DeleteOrderByID(int id, Action callback)
    {
        var response = await client.DeleteAsync(baseUrl + "order/" + id);
        var responseString = await response.Content.ReadAsStringAsync();
        //var stringRes = Cutil.ImportJsonObjest<string>(responseString);
        //ShopController.Instance?.SetBook(JsonUtility.FromJson<List<BookDetail>>(responseString));
        callback?.Invoke();
    }

    public async void CreateFeedback(string idBook, string comment, string username, string rating)
    {
        var values = new Dictionary<string, string>
          {
              { "idBook", idBook},
              { "comment", comment},
              { "username", username},
              { "rating", rating},
          };

        var content = new FormUrlEncodedContent(values);

        var response = await client.PostAsync(baseUrl + "feedback", content);

        var responseString = await response.Content.ReadAsStringAsync();
        FeedbackDetail feedbackDetail = JsonUtility.FromJson<FeedbackDetail>(responseString);
        BookDetailPopup.Instance.AddFeedback(feedbackDetail);
    }

    public async void UpdateFeedback(int id, string idBook, string comment, string username, string rating)
    {
        var values = new Dictionary<string, string>
          {
              { "id", id.ToString()},
              { "idBook", idBook},
              { "comment", comment},
              { "username", username},
              { "rating", rating},
          };

        var content = new FormUrlEncodedContent(values);

        var response = await client.PutAsync(baseUrl + "feedback", content);

        var responseString = await response.Content.ReadAsStringAsync();
        FeedbackDetail feedbackDetail = JsonUtility.FromJson<FeedbackDetail>(responseString);
    }

    public async void DeleteFeedbackByID(int id)
    {
        var response = await client.DeleteAsync(baseUrl + "feedback/" + id);
        var responseString = await response.Content.ReadAsStringAsync();
        //ShopController.Instance?.SetBook(JsonUtility.FromJson<List<BookDetail>>(responseString));
    }

    public async void GetAllFeedBackByIdBook(int idBook)
    {
        var response = await client.GetAsync(baseUrl + "feedback/" + idBook);
        var responseString = await response.Content.ReadAsStringAsync();

        List<FeedbackDetail> feedbacks = Cutil.ImportJsonObjest<List<FeedbackDetail>>(responseString);
        BookDetailPopup.Instance?.InitFeedback(feedbacks);
    }
}

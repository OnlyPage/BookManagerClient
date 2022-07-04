using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UserDetail
{
    public string username;
    public string phoneNumber;
    public string email;
    public string birthday;
    public string createTime;
    public string address;
    public string roleDetail;
}

[System.Serializable]
public class StoreDetail
{
    public string username;
    public List<BookDetail> bookDetail;
    public List<OrderDetail> orderDetail;
}

[System.Serializable]
public enum RoleType
{
    STORE,
    CUSTOMER
}
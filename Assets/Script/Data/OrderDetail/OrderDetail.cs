using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class OrderDetail
{
    public int id;
    public string customerName;
    public string storeName;
    public string dateOrder;
    public int price;
    public int state;
    public string nameBook;
    public int number;
}

[System.Serializable]
public class FeedbackDetail
{
    public int id;
    public BookDetail bookDetailResponse;
    public UserDetail userDetailResponse;
    public string comment;
    public int rating;

}
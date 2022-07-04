using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BookDetail
{
    public int id;
    public string nameBook;
    public string author;
    public string publicDate;
    public int number;
    public string category;
    public string userName;
    public bool isCanBuy;
    public int price;
    public float rating;
}

[SerializeField]
public enum Category
{
    Comic,
    Action,
    SchoolLife,
    Romance,
    Comedy,
    Detective,
    Fiction
}



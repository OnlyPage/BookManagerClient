using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderedPopup : MonoBehaviour
{
    public static OrderedPopup Instance;
    [SerializeField]
    protected GameObject layout;
    [SerializeField]
    protected OrderPrefab orderPrefab;

    protected Dictionary<int, OrderPrefab> bookPrefabs = new Dictionary<int, OrderPrefab>();

    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void Init(List<OrderDetail> orderDetails)
    {
        foreach(OrderDetail order in orderDetails)
        {
            OrderPrefab book = Instantiate(orderPrefab, layout.transform);
            book.InitorderBook(order);
            bookPrefabs[order.id] = book;
        }
    }

    public void AddOrder(OrderDetail order)
    {
        OrderPrefab book = Instantiate(orderPrefab, layout.transform);
        book.InitorderBook(order);
        bookPrefabs[order.id] = book;
    }

    public void Init(OrderDetail order)
    {
        OrderPrefab book = Instantiate(orderPrefab, layout.transform);
        book.InitorderBook(order);
        bookPrefabs[order.id] = book;
    }

    public void CancleOrderById(int id)
    {
        if (bookPrefabs.ContainsKey(id))
        {
            Destroy(bookPrefabs[id].gameObject);
            bookPrefabs.Remove(id);
        }
    }

    public void UpdateState(int id, int state)
    {
        if (bookPrefabs.ContainsKey(id))
        {
            bookPrefabs[id].SetState(state);
        }
    }    
}

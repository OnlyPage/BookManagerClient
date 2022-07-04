using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderPopup : MonoBehaviour
{
    public static OrderPopup Instance;
    [SerializeField]
    private GameObject layout;
    [SerializeField]
    private BookPrefab bookPrefab;

    private Dictionary<int, BookPrefab> bookPrefabs;
    private int count;

    private void Start()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        bookPrefabs = new Dictionary<int, BookPrefab>();
        count = 0;
    }

    public void Init(BookDetail bookDetail, int number)
    {
        BookPrefab book = Instantiate(bookPrefab, layout.transform);
        book.InitorderBook(count, bookDetail, number);
        bookPrefabs[count] = book;
        count++;
    }

    public void CancleOrderById(int id)
    {
        if(bookPrefabs.ContainsKey(id))
        {
            Destroy(bookPrefabs[id].gameObject);
            bookPrefabs.Remove(id);
        }
    }
}

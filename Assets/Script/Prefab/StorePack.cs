using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorePack : MonoBehaviour
{
    [SerializeField]
    private GameObject pack;
    [SerializeField]
    private BookPrefab bookPrefab;
    [SerializeField]
    private GameObject title;

    private Dictionary<int, BookPrefab> bookDetails = new Dictionary<int, BookPrefab>();

    public void SetBookPack(List<BookDetail> books)
    {
        ClearBook();
        foreach (BookDetail book in books)
        {
            BookPrefab bookIcon = Instantiate(bookPrefab, pack.transform);
            bookIcon.InitEditBook(book);
            bookDetails[book.id] = bookIcon;
        }
    }

    public void RemoveBook(int idBook)
    {
        if(bookDetails.ContainsKey(idBook))
        {
            Destroy(bookDetails[idBook].gameObject);
        }    
    }    

    public void ClearBook()
    {
        foreach (Transform book in pack.transform)
        {
            Destroy(book.gameObject);
        }
    }

    public void AddBook(BookDetail book)
    {
        BookPrefab bookIcon = Instantiate(bookPrefab, pack.transform);
        bookIcon.InitEditBook(book);
        bookDetails[book.id] = bookIcon;
    }

    public void UppdateBook(BookDetail book)
    {
        if (bookDetails.ContainsKey(book.id))
        {
            bookDetails[book.id].InitEditBook(book);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PackPrefab : MonoBehaviour
{
    [SerializeField]
    private GameObject pack;
    [SerializeField]
    private BookIconPrefab bookIconPrefab;
    [SerializeField]
    private GameObject title;

    private List<BookDetail> bookDetails;
    private RectTransform rectTransform;

    private void Start()
    {
        rectTransform = this.GetComponent<RectTransform>();
    }

    public void SetBookPack(List<BookDetail> books)
    {
        this.bookDetails = books;
        ClearBook();
        foreach (BookDetail book in books)
        {
            BookIconPrefab bookIcon = Instantiate(bookIconPrefab, pack.transform);
            bookIcon.Init(book);
        }
        float width = rectTransform.rect.width;
        rectTransform.sizeDelta = new Vector2(width, title.GetComponent<RectTransform>().rect.height + 40 + (books.Count/3)*380 + (books.Count / 3 - 1)*20);
        pack.GetComponent<RectTransform>().Top( 0 - (title.GetComponent<RectTransform>().rect.height + 40 + (books.Count / 3) * 380 + (books.Count / 3 - 1) * 20)/2);
    }

    public void ClearBook()
    {
        foreach (Transform book in pack.transform)
        {
            Destroy(book.gameObject);
        }
    }
}

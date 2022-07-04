using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddBookPopup : MonoBehaviour
{
    [SerializeField]
    private TMPro.TMP_InputField bookName;

    [SerializeField]
    private TMPro.TMP_InputField author;

    [SerializeField]
    private TMPro.TMP_InputField publicDate;

    [SerializeField]
    private TMPro.TMP_InputField number;

    [SerializeField]
    private TMPro.TMP_InputField price;

    [SerializeField]
    private TMPro.TextMeshProUGUI title;

    [SerializeField]
    private TMPro.TextMeshProUGUI buttonText;

    [SerializeField]
    private TMPro.TMP_Dropdown dropdown;

    private int category;
    private bool isAdd;
    private int idBook;

    public void SetAdd(bool isAdd, BookDetail bookDetail = null)
    {
        if(isAdd)
        {
            title.text = "Add new book";
            buttonText.text = "Add book";

            bookName.text = "";
            author.text = "";
            publicDate.text = "";
            number.text = "";
            price.text = "";
        }
        else
        {
            title.text = "Upgrade book";
            buttonText.text = "Upgrade";
            if(bookDetail != null)
            {
                idBook = bookDetail.id;
                bookName.text = bookDetail.nameBook;
                author.text = bookDetail.author;
                publicDate.text = bookDetail.publicDate;
                number.text = bookDetail.number.ToString();
                price.text = bookDetail.price.ToString();
                dropdown.value = (int)bookDetail.category.ToEnum<Category>();
            }
        }
    }

    public void OnValueDropdownChange(int category)
    {
        this.category = category;
    }
   
    public void OnClick_AddBook()
    {
        if(int.Parse(number.text) <= 0)
        {
            ErrorPopup.Instance.InitText("Number just > 0");
        }
        else if(int.Parse(price.text) <= 0)
        {
            ErrorPopup.Instance.InitText("Price just > 0");
        }
        if (isAdd)
        {
            APIHelper.Instance.CreateBook(bookName.text, author.text, publicDate.text, category.ToString(), UserManager.Instance.getUserDetail().username, number.text, price.text);
        }
        else
        {
            APIHelper.Instance.UpdateBook(idBook, bookName.text, author.text, publicDate.text, category.ToString(), UserManager.Instance.getUserDetail().username, number.text, price.text);
        }
    }

    public void OnCliCk_Exit()
    {
        this.gameObject.SetActive(false);
    }
}

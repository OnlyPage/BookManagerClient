using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserManager : Singleton<UserManager>
{
    private List<BookDetail> bookDetails = new List<BookDetail>();
    private UserDetail userDetail;

    /// <summary>
    /// SetUserData
    /// </summary>
    /// <param name="userData"></param>
    public void SetUserDetail(UserDetail userDetail)
    {
        this.userDetail = userDetail;
    }

    public UserDetail getUserDetail()
    {
        return userDetail;
    }

    public RoleType getRole()
    {
        return userDetail.roleDetail.ToEnum<RoleType>();
    }

    public void SetAllBook(List<BookDetail> bookDetails)
    {
        this.bookDetails = bookDetails;
    }

    public BookDetail getBookById(int id)
    {
        foreach(BookDetail book in bookDetails)
        {
            if(book.id == id)
            {
                return book;
            }
        }
        return null;
    }
}

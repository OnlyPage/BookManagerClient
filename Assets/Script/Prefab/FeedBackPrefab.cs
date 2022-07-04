using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FeedBackPrefab : MonoBehaviour
{
    [SerializeField]
    private Image avatarUser;
    [SerializeField]
    private TMPro.TextMeshProUGUI nameUser;
    [SerializeField]
    private TMPro.TMP_InputField comment;
    [SerializeField]
    private List<Button> ratingBtn;

    private int rating;
    private int idBook;

    public void SetInfo(int idBook, string userName)
    {
        this.idBook = idBook;
        this.nameUser.text = userName;
        OnClick_Rating(4);
    }    

    public void OnClick_Rating(int index)
    {
        rating = index + 1;
        for(int i = 0 ; i < ratingBtn.Count; i++)
        {
            if (i <= index)
            {
                ratingBtn[i].image.color = Color.yellow;
            }
            else
                ratingBtn[i].image.color = Color.white;
        }    
    }    

    public void OnClick_Comment()
    {
        APIHelper.Instance.CreateFeedback(idBook.ToString(), comment.text, nameUser.text, rating.ToString());
    }    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookDetailPopup : MonoBehaviour
{
    public static BookDetailPopup Instance;
    [SerializeField]
    private BookDetailPrefab bookDetailPrefab;
    [SerializeField]
    private FeedBackPrefab feedBackPrefab;
    [SerializeField]
    private CommentPrefab commentPrefab;
    [SerializeField]
    private GameObject pack;

    private List<CommentPrefab> commentPrefabs = new List<CommentPrefab>();
    private void Start()
    {
        if(Instance == null)
        {
            Instance = this;
        }    
    }

    public void Init(BookDetail bookDetail, bool isStore = false)
    {
        bookDetailPrefab.Init(bookDetail, isStore);
        APIHelper.Instance.GetAllFeedBackByIdBook(bookDetail.id);
        feedBackPrefab.gameObject.SetActive(!isStore);
        feedBackPrefab.SetInfo(bookDetail.id, UserManager.Instance.getUserDetail().username);
    }    

    public void InitFeedback(List<FeedbackDetail> feedbackDetails)
    {
        ClearFeedBack();
        foreach (FeedbackDetail feedback in feedbackDetails)
        {
            CommentPrefab comment = Instantiate(commentPrefab, pack.transform);
            comment.Init(feedback);
            commentPrefabs.Add(comment);
        }
    }

    public void OnClick_Exit()
    {
        this.gameObject.SetActive(false);
    }

    public void ClearFeedBack()
    {
        foreach(CommentPrefab comment in commentPrefabs)
        {
            Destroy(comment.gameObject);
        }
    }

    public void AddFeedback(FeedbackDetail feedbackDetail)
    {
        CommentPrefab comment = Instantiate(commentPrefab, pack.transform);
        comment.Init(feedbackDetail);
        commentPrefabs.Add(comment);
        comment.gameObject.transform.SetSiblingIndex(2);
        feedBackPrefab.gameObject.SetActive(false);
    }
}

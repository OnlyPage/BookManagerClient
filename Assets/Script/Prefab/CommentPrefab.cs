using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommentPrefab : MonoBehaviour
{
    [SerializeField]
    private Image avatar;
    [SerializeField]
    private List<Image> ratingImg;
    [SerializeField]
    private TMPro.TextMeshProUGUI comment;
    [SerializeField]
    private TMPro.TextMeshProUGUI userName;

    public void Init(FeedbackDetail feedbackDetail)
    {
        userName.text = feedbackDetail.userDetailResponse.username;
        comment.text = feedbackDetail.comment;
        for(int i = 0; i < ratingImg.Count; i++)
        {
            if(i < feedbackDetail.rating)
            {
                ratingImg[i].color = Color.yellow;
            }
            else
            {
                ratingImg[i].color = Color.white;
            }
        }
    }
}

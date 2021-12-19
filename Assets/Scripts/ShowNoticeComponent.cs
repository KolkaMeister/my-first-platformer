using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowNoticeComponent : MonoBehaviour
{
    private TextNoticeComponent noticeComponent;




    public void TextNotice(string text)
    {
        if (noticeComponent == null) noticeComponent = FindObjectOfType<TextNoticeComponent>();
        noticeComponent.CallTextNotice(text);
    }
}

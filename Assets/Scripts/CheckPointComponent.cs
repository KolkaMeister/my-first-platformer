using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CheckPointComponent : MonoBehaviour
{
    [SerializeField] public string Id;
    [SerializeField] public SpawnComponent spawner;
    [SerializeField] private UnityEvent OnCheckPointSeted;
    [SerializeField] private UnityEvent OnCheckPointUnSeted;
    private TextNoticeComponent textNotice;

    private GameSession gameSession;
    private void Start()
    {
        gameSession = FindObjectOfType<GameSession>();
        gameSession.OnCheckPointChanged += CheckId;
        CheckId();
    }
    public void SetCheckPoint()
    {
        gameSession.SetCheckPointId(Id);
    }
    public void CheckId()
    {
        if (gameSession.checkPointId == Id)
        {
            OnCheckPointSeted?.Invoke();
        }
        else
            OnCheckPointUnSeted?.Invoke();
    }
    public void UnSetCheckPoint()
    {
        OnCheckPointUnSeted?.Invoke();
    }
    [ContextMenu("TextNotice")]
    public void CallNotice()
    {
        if (textNotice==null)
        {
          textNotice= GameObject.FindWithTag("TextNotice").GetComponent<TextNoticeComponent>();
        }
        textNotice.CallTextNotice("CheckPoint!!");
    }
}

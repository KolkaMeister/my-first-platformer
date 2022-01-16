using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TotemMobAI : MonoBehaviour
{
    [SerializeField] private List<TotemHead> heads;
    [SerializeField] private LayerCheck vision;
    [SerializeField] private float delay;
    [SerializeField] public UnityEvent OnTotemDead;
    private int index=0;
    private Coroutine currentCoroutine;
    IEnumerator enumerator;

    public int FixIndex =>index=(int)Mathf.Repeat(index, heads.Count);
    public void Agro()
    {
        StartState(Attack());
    }
    private IEnumerator Attack()
    {
        while(vision.isTouchingLayer&& heads.Count>0)
        {
            if (heads.Count < 0)
            Debug.Log("Last Time call");
            while (heads[FixIndex] ==null)
            {
                heads.Remove(heads[FixIndex]);
                index = (int)Mathf.Repeat(index, heads.Count);
                if (heads.Count <= 0)
                {
                    OnTotemDead?.Invoke();
                    StopCoroutine(currentCoroutine);
                    yield return new WaitForSeconds(2);
                }
            }
            if (heads.Count > 0) heads[FixIndex].DoAttack();
            index = (int)Mathf.Repeat(index+1, heads.Count);
            yield return new WaitForSeconds(delay);
        }

    }
    private void StartState(IEnumerator coroutine)
    {
        if (currentCoroutine!=null)
        {
            StopCoroutine(currentCoroutine);
        }
        currentCoroutine=StartCoroutine(coroutine);
    }

}

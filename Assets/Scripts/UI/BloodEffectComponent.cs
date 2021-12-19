using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodEffectComponent : MonoBehaviour
{

    private float _upperBorder;
    private Animator animator;

    private Vector3 effectSizeDelta;
    static private readonly int HpLevel = Animator.StringToHash("HpLevel");

    private GameSession session;

    private void Start()
    {
        _upperBorder = DefsFacade.I.HeroStatsDef.Get(Characteristics.Hp).LevelStats[0].value;
        effectSizeDelta = transform.localScale - Vector3.one;
        animator = GetComponent<Animator>();
        session = FindObjectOfType<GameSession>();
        session.data.Hp.OnChanged+= OnHpChanged;
        OnHpChanged(session.data.Hp.Value, 0);
    }
    public void OnHpChanged(int newValue,int _)
    {
       var _value = (float)newValue/ _upperBorder;
        transform.localScale = Vector3.one+ effectSizeDelta*Mathf.Max(_value-0.3f, 0);
        animator.SetFloat(HpLevel, _value);
    }
    public void OnDestroy()
    {
        if (session.data!=null)
        {
        session.data.Hp.OnChanged -= OnHpChanged;
        }
    }
}

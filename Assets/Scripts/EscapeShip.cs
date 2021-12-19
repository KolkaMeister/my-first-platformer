using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class EscapeShip : MonoBehaviour
{
    [SerializeField] private Transform sitPoint;
    [SerializeField] public UnityEvent onGameCompete;
    [SerializeField] private ConstantForce2D engine;
    private Rigidbody2D _rigidbody;
    private Hero _hero;

    private float Alpha
    {
        get
        {
            return _hero._renderer.color.a;
        }
         set
        {
            _hero._renderer.color = new Color(_hero._renderer.color.r, _hero._renderer.color.g, _hero._renderer.color.b,value);
        }

    }
    private Coroutine lerpCoroutne;
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _hero = FindObjectOfType<Hero>();
    }
    [ContextMenu("End")]
    public void EndGame()
    {
        _hero.GetComponent<PlayerInput>().enabled = false;
        StartCoroutine(GameCompetedOHHHOOOOAAA());
    }
    public IEnumerator GameCompetedOHHHOOOOAAA()
    {
        AnimateHeroAlpha(0);
        yield return new WaitForSeconds(1);
        _hero.transform.position = sitPoint.position;
        _hero.ConnectJoint(_rigidbody);
        AnimateHeroAlpha(1);
        yield return new WaitForSeconds(2f);
        engine.enabled = true;
        yield return new WaitForSeconds(5f);
        onGameCompete?.Invoke();
        FindObjectOfType<LevelLoader>().LoadScene("MainMenu");
    }
    public void AnimateHeroAlpha(float _val)
    {

        if (lerpCoroutne!=null)
        {
            StopCoroutine(lerpCoroutne);
            lerpCoroutne = null;
        }
        lerpCoroutne = this.LerpAnimation(_hero._renderer.color.a, _val,1, SetAlpha);
    }
    public void SetAlpha(float _val)
    {
        Alpha = _val;
    }
}

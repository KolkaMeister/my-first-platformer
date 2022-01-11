using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class LevelLoader : MonoBehaviour
{
    [SerializeField] private ProgressBarWidget progressBar;
    [SerializeField] private float LoadTime;
    private AsyncOperation operation;
    private Animator animator;

    static private readonly int Show = Animator.StringToHash("Show");

    static private readonly int Hide = Animator.StringToHash("Hide");

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static  void InitLoader()
    {
        SceneManager.LoadSceneAsync("LevelLoader", LoadSceneMode.Additive);
    }
    

    public void Awake()
    {
        animator = GetComponent<Animator>();
        DontDestroyOnLoad(gameObject);
    }

    public void LoadScene(string _sceneName)
    {
        var hero = FindObjectOfType<Hero>();
        if (hero!=null) hero.GetComponent<PlayerInput>().enabled = false;

        StartCoroutine(LoadSceneCoroutine(_sceneName));
    }
    public IEnumerator LoadSceneCoroutine(string _sceneName)
    {
        animator.SetTrigger(Show);
        yield return new WaitForSecondsRealtime(LoadTime);
        operation = SceneManager.LoadSceneAsync(_sceneName);
        while (!operation.isDone)
        {
            progressBar.SetProgress(operation.progress);
            yield return null;
        }
        animator.SetTrigger(Hide);
        Time.timeScale = 1;
        var hero = FindObjectOfType<Hero>();
        if (hero!=null)
        {
            hero.GetComponent<PlayerInput>().enabled = true;
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Analytics;
public class ExitLevelComponent : MonoBehaviour
{
    [SerializeField] private string loadSceneName;
    public void LoadLevel()
    {
        var session = FindObjectOfType<GameSession>();
        session.Save();
        var loader = FindObjectOfType<LevelLoader>();
        loader.LoadScene(loadSceneName);
    }
}

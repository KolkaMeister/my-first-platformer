using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Data/GameSettings" ,fileName ="GameSettings")]
public class GameSettings : ScriptableObject
{
    [SerializeField] public FloatPersistantProperty Music;
    [SerializeField] public FloatPersistantProperty SFX;

    public static GameSettings instanse;
    public static GameSettings I => instanse == null ? LoadGameSettings() : instanse;

    private void OnEnable()
    {
        Music = new FloatPersistantProperty(1, SoundSettings.Music.ToString());
        SFX = new FloatPersistantProperty(1, SoundSettings.SFX.ToString());
    }

    private void OnValidate()
    {
        Music.Validate();
        SFX.Validate();
    }

    private static GameSettings LoadGameSettings()
    {
        return instanse = Resources.Load<GameSettings>("GameSettings");
    }

}
public enum SoundSettings
{
    Music,
    SFX

}

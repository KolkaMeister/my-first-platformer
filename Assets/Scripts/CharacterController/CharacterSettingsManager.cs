using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSettingsManager
{
    public static readonly CharacterSettingsManager I;
    private GameObject Character;
    private StringPersistentProperty currentCharacter = new StringPersistentProperty("Captain", "CharacterName");
    static CharacterSettingsManager()
    {
        I = new CharacterSettingsManager();
    }
    public CharacterSettingsManager()
    {
        Character=Resources.Load<GameObject>($"GameObjects/{currentCharacter.Value}");
    }
    public GameObject GetCharacter()
    {
        return Character;
    }
    public void SetCharacter(string name)
    {
        var _character = Resources.Load<GameObject>($"GameObjects/{name}");
        if (Character == null) return;
        Character = _character;
        currentCharacter.Value = name;
    }
}

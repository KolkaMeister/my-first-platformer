using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPreset : MonoBehaviour
{
    
    void Start()
    {
        var session = FindObjectOfType<GameSession>();
        if (session != null) Destroy(session);
        Cursor.visible = true;
    }


}

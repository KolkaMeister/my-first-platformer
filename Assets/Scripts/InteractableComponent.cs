using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractableComponent : MonoBehaviour
{
    [SerializeField] EnterEvent action;

    public void Interact(GameObject target)
    {
        action?.Invoke(target);
    }

    [Serializable]
    public class EnterEvent : UnityEvent<GameObject>
    {
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjectComponent : MonoBehaviour
{
    [SerializeField] GameObject objectToDestroy;
    public void DestroyObject()
    {
        Destroy(objectToDestroy);
    }
}

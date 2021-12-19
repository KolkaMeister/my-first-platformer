using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportComponent : MonoBehaviour
{

    [SerializeField]private Transform teleportPoint;

    private GameObject _object;
    public void Teleport(GameObject target)
    {
        target.transform.position = teleportPoint.transform.position;
    }


    public void SetTeleportObject(GameObject target)
    {
        _object = target;
    }
    public void TeleportChosenObject()
    {
        _object.transform.position = teleportPoint.transform.position;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoInteractionComponent : MonoBehaviour
{

    public void DoInteract(GameObject overlapObject)
    {
        var interactObject= overlapObject.GetComponent<InteractableComponent>();
        if (interactObject!=null)
        {
            interactObject.Interact(gameObject.transform.parent.gameObject);
        }
    }
}

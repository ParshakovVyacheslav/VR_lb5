using UnityEngine;

public class DetailCheck : MonoBehaviour
{
    public BoxCollider collider;
    public BoxCollider other;
    
    public BoxCollider grabCollider;
    public bool isSet = false;
    public DetailCheck script;

    public void CheckAndEnableDetail()
    {
        if (!other.enabled) collider.enabled = true;
    }

    public void DisableOtherGrab()
    {
        if (!collider.enabled && script.isSet) grabCollider.enabled = false;
        if (!isSet) grabCollider.enabled = true;
    }

    public void OnTriggerEnter(Collider _) => isSet = true;
    public void OnTriggerExit(Collider _) => isSet = false;
}

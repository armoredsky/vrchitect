using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SteamVR_TrackedObject))]
public class PickupParent : MonoBehaviour {

    SteamVR_TrackedObject trackedObj;
    SteamVR_Controller.Device device;
    int count;
    public GameObject prefabSphere;

    void Awake () {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
	}
	
	void Update () {
        device = SteamVR_Controller.Input((int)trackedObj.index);

        if (device.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad))
        {
            count++;
            Debug.Log("pressed it!" + count);
            GameObject.Instantiate(prefabSphere);
        }
    }

    void FixedUpdate()
    {
       
        
    }

    private void OnTriggerStay(Collider col)
    {
        Debug.Log("I've collided with: " + col.name + " onTriggerStay");
        if(col == null)
        {
            return;
        }
        if (device.GetTouch(SteamVR_Controller.ButtonMask.Trigger))
        {
            Debug.Log("Have collided "+ col.name + "with trigger touch");
            col.attachedRigidbody.isKinematic = true;
            col.gameObject.transform.SetParent(this.gameObject.transform);
        } else if (device.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger))
        {
            col.attachedRigidbody.isKinematic = false;
            col.gameObject.transform.SetParent(null);
        }

        
    }
}

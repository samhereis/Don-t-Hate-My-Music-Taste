using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRaycast : MonoBehaviour
{
    // Data and method for main player camera's raycast, data nad methods are used by other classes

    public static CameraRaycast instance;

    public static Ray ARay;
    public static RaycastHit HitInfo;
    public static Camera CameraComponent;
    public static float DefaultRange = 2;

    private void Awake()
    {
        instance = this;
    }

    public static bool Cast(Vector3 start, Vector3 direction , float range, out dynamic obj)
    {
        obj = null;

        if(Physics.Raycast(start, direction, out RaycastHit hitObj, range))
        {
            obj = hitObj.collider.gameObject;

            return true;
        }
        else
        {
            return false;
        }
    }

    public static bool Cast(Vector3 start, float range,  out dynamic obj)
    {
        obj = null;

        if(Physics.Raycast(start, instance.transform.forward, out RaycastHit hitObj, range))
       {
            obj = hitObj.collider.gameObject;

            return true;
        }
        else
        {
            return false;
        }
    }

    public static bool Cast(float range, out dynamic obj)
    {
        obj = null;

        if(Physics.Raycast(instance.transform.position, instance.transform.forward, out RaycastHit hitObj, range))
        {
            obj = hitObj.collider.gameObject;

            return true;
        }
        else
        {
            return false;
        }
    }

    public static bool Cast<Interface>(out Interface obj)
    {
        obj = default(Interface);

        if(Physics.Raycast(instance.transform.position, instance.transform.forward, out RaycastHit hitObj, DefaultRange) && hitObj.collider.GetComponent<Interface>() != null)
        {
            obj = hitObj.collider.GetComponent<Interface>();

            return true;
        }
        else
        {
            return false;
        }
    }
}

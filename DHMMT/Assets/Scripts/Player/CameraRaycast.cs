using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRaycast : MonoBehaviour
{
    public static CameraRaycast instance;
    public static Ray ray;
    public static RaycastHit hit;
    public static Camera cam;
    public static float defaultRange = 2;
    void Awake()
    {
        instance = this;
    }
    void FixedUpdate()
    {
        
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
    /* public static bool Cast(out dynamic obj)
    {
        obj = null;

        if(Physics.Raycast(instance.transform.position, instance.transform.forward, out RaycastHit hitObj, defaultRange))
        {
            obj = hitObj.collider.gameObject;

            return true;
        }
        else
        {
            return false;
        }
    } */
    public static bool Cast<Interface>(out Interface obj)
    {
        obj = default(Interface);

        if(Physics.Raycast(instance.transform.position, instance.transform.forward, out RaycastHit hitObj, defaultRange) && hitObj.collider.GetComponent<Interface>() != null)
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

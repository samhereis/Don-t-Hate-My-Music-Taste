#if UNITY_EDITOR

using UnityEngine;

public class ReadmeLods : MonoBehaviour
{
    [TextArea(3, 10)]
    public string description = "Add your description here.";

    private void OnDrawGizmosSelected()
    {
        UnityEditor.Handles.Label(transform.position, description);
    }
}

#endif
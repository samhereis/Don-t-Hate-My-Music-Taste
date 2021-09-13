using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUIPage
{
    // Bluepring of canvas during a match

    GameObject Page { get; set; }
    void Enable();
    void Disable();
}

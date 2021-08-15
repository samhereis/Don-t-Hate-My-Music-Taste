using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUIPage
{
    GameObject Page { get; set; }
    void Enable();
    void Disable();
}

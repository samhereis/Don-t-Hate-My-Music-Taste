using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayUI : MonoBehaviour
{
    public static GameplayUI instance;

    void Awake()
    {
        instance = this;
        gameObject.SetActive(true);
    }

    void Start()
    {

    }

    void OnEnable()
    {

    }

    void OnDisable()
    {
        
    }

    public void Enable()
    {
        gameObject.SetActive(true); 
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }
}

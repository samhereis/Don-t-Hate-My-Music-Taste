using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MakeObjectsShake : MonoBehaviour
{
    public static MakeObjectsShake instance;

    public enum ShakeType { Scale, Position }

    public ShakeType shakeType = ShakeType.Scale;

    public List<Transform> objectsReactingToBasses, objectsReactingToNB, objectsReactingToMiddles, objectsReactingToHighs;
    public List<Transform> Reactors;

    public float t = 0.2f;

    Action Shake;

    void Awake()
    {
        instance = this;

        if (shakeType == ShakeType.Scale)
        {
            Shake =makeObjectsShakeScale;
        }
        else
        {
            Shake = makeObjectsShakePosition;
        }
    }

    void FixedUpdate()
    {
        Shake();
    }

    void makeObjectsShakeScale()
    {
        foreach (Transform obj in objectsReactingToBasses)
        {
            obj.DOScaleY(Cals.instance.setSoundFreq(0, 7, Cals.instance.bassSoundMult), t);
        }
        foreach (Transform obj in objectsReactingToNB)
        {
            obj.DOScaleY(Cals.instance.setSoundFreq(7, 15, Cals.instance.nextToBassSoundMult), t);
        }
        foreach (Transform obj in objectsReactingToMiddles)
        {
            obj.DOScaleY(Cals.instance.setSoundFreq(15, 30, Cals.instance.middleSoundMult), t);
        }
        foreach (Transform obj in objectsReactingToHighs)
        {
            obj.DOScaleY(Cals.instance.setSoundFreq(30, 32, Cals.instance.HighSoundMult), t);
        }
    }

    void makeObjectsShakePosition()
    {
        foreach (Transform obj in objectsReactingToBasses)
        {
            obj.DOMoveY(Cals.instance.setSoundFreq(0, 7, Cals.instance.bassSoundMult), t);
        }
        foreach (Transform obj in objectsReactingToNB)
        {
            obj.DOMoveY(Cals.instance.setSoundFreq(7, 15, Cals.instance.nextToBassSoundMult), t);
        }
        foreach (Transform obj in objectsReactingToMiddles)
        {
            obj.DOMoveY(Cals.instance.setSoundFreq(15, 30, Cals.instance.middleSoundMult), t);
        }
        foreach (Transform obj in objectsReactingToHighs)
        {
            obj.DOMoveY(Cals.instance.setSoundFreq(30, 32, Cals.instance.HighSoundMult), t);
        }
    }
}

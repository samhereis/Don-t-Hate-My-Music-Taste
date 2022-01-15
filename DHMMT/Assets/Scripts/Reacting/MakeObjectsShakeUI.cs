using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;

public class MakeObjectsShakeUI : MonoBehaviour
{
    // Changes UI objects based on playing music

    public enum ShakeType { Scale, Position }

    public ShakeType WayOfReactingToMusic = ShakeType.Scale;

    public List<Transform> ObjectsReactingToBasses = new List<Transform>(), 
        ObjectsReactingToNB = new List<Transform>(), 
        ObjectsReactingToMiddles = new List<Transform>(), 
        ObjectsReactingToHighs = new List<Transform>();

    [Header("Reaction Value Multiple")]
    public float BassSoundMult = 1;
    public float NextToBassSoundMult = 1;
    public float MiddleSoundMult = 1;
    public float HighSoundMult = 1;

    private float t = 0.3f;

    private Action _shake;

    private void OnEnable()
    {

        if (WayOfReactingToMusic == ShakeType.Scale)
        {
            _shake = makeObjectsShakeScale;
        }
        else
        {
            _shake = makeObjectsShakePosition;
        }
    }

    private void FixedUpdate()
    {
        _shake();
    }

    private void makeObjectsShakeScale()
    {
        foreach (Transform obj in ObjectsReactingToBasses)
        {
            obj.DOScaleY(PlayingMusicData.instance.setSoundFreq(0, 7, BassSoundMult), t);
        }
        foreach (Transform obj in ObjectsReactingToNB)
        {
            obj.DOScaleY(PlayingMusicData.instance.setSoundFreq(7, 15, NextToBassSoundMult), t);
        }
        foreach (Transform obj in ObjectsReactingToMiddles)
        {
            obj.DOScaleY(PlayingMusicData.instance.setSoundFreq(15, 30, MiddleSoundMult), t);
        }
        foreach (Transform obj in ObjectsReactingToHighs)
        {
            obj.DOScaleY(PlayingMusicData.instance.setSoundFreq(30, 32, HighSoundMult), t);
        }
    }

    private void makeObjectsShakePosition()
    {
        foreach (Transform obj in ObjectsReactingToBasses)
        {
            obj.DOLocalMoveY(PlayingMusicData.instance.setSoundFreq(0, 7, BassSoundMult), t);
        }
        foreach (Transform obj in ObjectsReactingToNB)
        {
            obj.DOLocalMoveY(PlayingMusicData.instance.setSoundFreq(7, 15, NextToBassSoundMult), t);
        }
        foreach (Transform obj in ObjectsReactingToMiddles)
        {
            obj.DOLocalMoveY(PlayingMusicData.instance.setSoundFreq(15, 30, MiddleSoundMult), t);
        }
        foreach (Transform obj in ObjectsReactingToHighs)
        {
            obj.DOLocalMoveY(PlayingMusicData.instance.setSoundFreq(30, 32, HighSoundMult), t);
        }
    }
}

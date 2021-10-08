using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MakeObjectsShake : MonoBehaviour
{
    // Changes objects based on playing music

    public static MakeObjectsShake instance;

    public enum ShakeType { Scale, Position }

    public ShakeType WayOfReactingToMusic = ShakeType.Scale;

    public List<Transform> ObjectsReactingToBasses = new List<Transform>(), ObjectsReactingToNB = new List<Transform>(), ObjectsReactingToMiddles = new List<Transform>(), ObjectsReactingToHighs = new List<Transform>();
    public List<List<Transform>> ObjectsReacting = new List<List<Transform>>();
    public List<Transform> Reactors;

    public float t = 0.2f;

    private Action _shake;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        if (ObjectsReactingToBasses != null)    ObjectsReacting.Add(ObjectsReactingToBasses);
        if (ObjectsReactingToNB != null)        ObjectsReacting.Add(ObjectsReactingToNB);
        if (ObjectsReactingToMiddles != null)   ObjectsReacting.Add(ObjectsReactingToMiddles);
        if (ObjectsReactingToHighs != null)     ObjectsReacting.Add(ObjectsReactingToHighs);
    }

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
            obj.DOScaleY(PlayingMusicData.instance.setSoundFreq(0, 7, PlayingMusicData.instance.BassSoundMult), t);
        }
        foreach (Transform obj in ObjectsReactingToNB)
        {
            obj.DOScaleY(PlayingMusicData.instance.setSoundFreq(7, 15, PlayingMusicData.instance.NextToBassSoundMult), t);
        }
        foreach (Transform obj in ObjectsReactingToMiddles)
        {
            obj.DOScaleY(PlayingMusicData.instance.setSoundFreq(15, 30, PlayingMusicData.instance.MiddleSoundMult), t);
        }
        foreach (Transform obj in ObjectsReactingToHighs)
        {
            obj.DOScaleY(PlayingMusicData.instance.setSoundFreq(30, 32, PlayingMusicData.instance.HighSoundMult), t);
        }
    }

    private  void makeObjectsShakePosition()
    {
        foreach (Transform obj in ObjectsReactingToBasses)
        {
            obj.DOLocalMoveY(PlayingMusicData.instance.setSoundFreq(0, 7, PlayingMusicData.instance.BassSoundMult), t);
        }
        foreach (Transform obj in ObjectsReactingToNB)
        {
            obj.DOLocalMoveY(PlayingMusicData.instance.setSoundFreq(7, 15, PlayingMusicData.instance.NextToBassSoundMult), t);
        }
        foreach (Transform obj in ObjectsReactingToMiddles)
        {
            obj.DOLocalMoveY(PlayingMusicData.instance.setSoundFreq(15, 30, PlayingMusicData.instance.MiddleSoundMult), t);
        }
        foreach (Transform obj in ObjectsReactingToHighs)
        {
            obj.DOLocalMoveY(PlayingMusicData.instance.setSoundFreq(30, 32, PlayingMusicData.instance.HighSoundMult), t);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MusicList", menuName = "Scriptable Object/MusicList")]
public class ScriptableMusicList : ScriptableObject
{
    public List<AudioClip> musicList;
}

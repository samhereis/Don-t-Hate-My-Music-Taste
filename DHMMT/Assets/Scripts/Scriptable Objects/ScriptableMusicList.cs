using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MusicList", menuName = "Scriptable Object/MusicList")]
public class ScriptableMusicList : ScriptableObject
{
    // Default music to play when player doesn't have music on computer

    public List<AudioClip> MusicList = new List<AudioClip>();
}

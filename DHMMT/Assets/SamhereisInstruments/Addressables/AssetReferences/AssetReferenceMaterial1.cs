using System;
using UnityEngine.AddressableAssets;
using UnityEngine.Video;

[Serializable]
public class AssetReferenceVideoClip : AssetReferenceT<VideoClip>
{
    public AssetReferenceVideoClip(string guid) : base(guid)
    {

    }
}
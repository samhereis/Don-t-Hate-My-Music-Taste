using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

[Serializable]
public class AssetReferenceScriptableObject : AssetReferenceT<ScriptableObject>
{
    public AssetReferenceScriptableObject(string guid) : base(guid)
    {

    }
}
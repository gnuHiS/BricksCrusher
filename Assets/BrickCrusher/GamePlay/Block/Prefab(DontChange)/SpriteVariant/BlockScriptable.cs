using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Block Variant",menuName ="New Block Variant")]
public class BlockScriptable : ScriptableObject
{
    [SerializeField] Sprite[] artworks;

    public Sprite[] GetArtworks()
    {
        return artworks;
    }
}

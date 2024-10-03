using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpritePack
{
    [SerializeField] private Sprite _idleSprite;
    [SerializeField] private List<Sprite> _spriteSequence;

    public Sprite IdleSprite => _idleSprite;
    public List<Sprite> SpriteSequence => _spriteSequence;
}
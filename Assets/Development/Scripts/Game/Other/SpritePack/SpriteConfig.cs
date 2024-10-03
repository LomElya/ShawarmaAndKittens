using UnityEngine;

[CreateAssetMenu(fileName = "SpriteConfig", menuName = "Configs/PlayerSpriteConfig", order = 51)]
public class SpriteConfig : ScriptableObject
{
    [SerializeField] private SpritePack _upSpritePack;
    [SerializeField] private SpritePack _downSpritePack;

    public SpritePack UpSpritePack => _upSpritePack;
    public SpritePack DownSpritePack => _downSpritePack;
}

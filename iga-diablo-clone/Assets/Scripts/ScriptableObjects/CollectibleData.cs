using UnityEngine;

[CreateAssetMenu(fileName = "Collectible", menuName = "New Collectible")]
public class CollectibleData : ScriptableObject
{
    public string collectibleType;
    public string collectibleTitle;
    public string description;
    public int value;
}

using System.Collections.Generic;
using UnityEngine;

public class CollectibleManager : MonoBehaviour
{
    private HashSet<Collectible> _collectedItems = new HashSet<Collectible>();

    private Dictionary<string, int> _collectibleProgress = new Dictionary<string, int>();
    private Dictionary<string, int> _collectibleGoals = new Dictionary<string, int>();

    public static CollectibleManager Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;

        // Collectible Goals
        _collectibleGoals["Cube"] = 6;
    }

    public void AddToCollection(Collectible collectible)
    {
        if (!_collectedItems.Contains(collectible))
        {
            _collectedItems.Add(collectible);
            Debug.Log($"Collected: {collectible.GetCollectibleName()} of Type: {collectible.GetCollectibleType()}!");
        }

        string collectibleType = collectible.GetCollectibleType();

        if (!_collectibleProgress.ContainsKey(collectibleType))
        {
            _collectibleProgress[collectibleType] = 0;
        }

        _collectibleProgress[collectibleType]++;
        Debug.Log($"Collected {collectibleType}: {_collectibleProgress[collectibleType]}/{_collectibleGoals[collectibleType]}");

        UpdateProgressUI(collectibleType);
    }

    public void UpdateProgressUI(string collectibleType)
    {
        Debug.Log($"Progress for {collectibleType}: {_collectibleProgress[collectibleType]}/{_collectibleGoals[collectibleType]}");
    }
}

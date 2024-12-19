using UnityEngine;

public class Collectible : Interactable
{
    [SerializeField] private CollectibleData _data;
    private bool _isCollected = false;

    protected override void Update()
    {
        base.Update();
    }

    public override void OnClick()
    {
        base.OnClick();

        InteractableUIManager.Instance.ShowPopup(_data.collectibleTitle, _data.description);
        Collect();
    }

    public void Collect()
    {
        if (!_isCollected)
        {
            CollectibleManager.Instance.AddToCollection(this);
            _isCollected = true;
        }
        else
        {
            Debug.Log($"We've collected {_data.collectibleTitle} already.");
        }
    }

    public string GetCollectibleType() => _data.collectibleType;

    public string GetCollectibleName() => _data.collectibleTitle;

    public int GetCollectibleValue() => _data.value;
}

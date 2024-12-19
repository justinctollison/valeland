using UnityEngine;

public interface IClickable
{
    bool CanBeClicked();
    void OnClick();
    void OnHover();
    void OnHoverExit();

    public void SetLayer(int layer);
    public int GetLayer();
}

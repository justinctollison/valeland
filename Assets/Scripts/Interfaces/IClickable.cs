using UnityEngine;

public interface IClickable
{
    bool CanBeClicked();
    void OnClick();
    void OnHover();
    void OnHoverExit();
}

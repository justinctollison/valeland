using UnityEngine;

public class MenuButton : MonoBehaviour
{
    public void LoadMenuScene()
    {
        SceneChanger.Instance.LoadScene(SceneChanger.Scene.MainMenu);
    }
    public void LoadHowToPlayScene()
    {
        SceneChanger.Instance.LoadScene(SceneChanger.Scene.HowToPlay);
    }
    public void LoadCreditsScene()
    {
        SceneChanger.Instance.LoadScene(SceneChanger.Scene.Credits);
    }
    public void LoadGameScene()
    {
        SceneChanger.Instance.LoadScene(SceneChanger.Scene.Game);
    }

    public void ClosePopup()
    {
        // Set parent object to inactive
    }
}

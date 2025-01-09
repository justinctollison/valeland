using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public static SceneChanger Instance;

    public enum Scene { MainMenu, HowToPlay, Credits, Game, Level1, Level2, Level3, Hub, Boss };


    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public void LoadScene(Scene scene)
    {
        AudioManager.Instance.PlaySceneSwitchSwooshSFX();
        SceneManager.LoadScene(scene.ToString());
    }
}

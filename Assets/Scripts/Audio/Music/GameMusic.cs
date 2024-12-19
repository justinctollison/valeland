using UnityEngine;

public class GameMusic : MonoBehaviour
{
    void Start()
    {
        AudioManager.Instance.PlayGameMusic();
    }

}

using UnityEngine;

public class GameMusic : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        AudioManager.Instance.PlayGameMusic();
    }
}

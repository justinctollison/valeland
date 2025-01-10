using UnityEngine;

public class TownMusic : MonoBehaviour
{
    private void Start()
    {
        AudioManager.Instance.PlayTownMusic();
    }

    private void OnTriggerEnter(Collider other)
    {
        AudioManager.Instance.PlayTownMusic();
    }
}

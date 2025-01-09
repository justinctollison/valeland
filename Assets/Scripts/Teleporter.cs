using UnityEngine;
using UnityEngine.AI;

public class Teleporter : MonoBehaviour
{
    [SerializeField] Transform _bossAreaSpawn;
    [SerializeField] GameObject _player;
    [SerializeField] GameObject _directionalLight;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            _player.GetComponent<NavMeshAgent>().enabled = false;
            _player.transform.position = _bossAreaSpawn.transform.position;
            _directionalLight.gameObject.SetActive(false);
            AudioManager.Instance.PlayBossAreaMusic();

            Invoke("EnableNavMeshAgent", 0.5f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //SceneChanger.Instance.LoadScene(SceneChanger.Scene.Boss);

        _player.GetComponent<NavMeshAgent>().enabled = false;
        _player.transform.position = _bossAreaSpawn.transform.position;
        _directionalLight.gameObject.SetActive(false);
        AudioManager.Instance.PlayBossAreaMusic();

        Invoke("EnableNavMeshAgent", 0.5f);
    }

    private void EnableNavMeshAgent()
    {
        _player.GetComponent<NavMeshAgent>().enabled = true;
    }
}

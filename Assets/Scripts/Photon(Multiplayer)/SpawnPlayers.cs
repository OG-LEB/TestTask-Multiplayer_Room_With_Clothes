using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviour
{
    [Header("Spawn Setup")]
    [SerializeField] float SPAWN_X_MIN;
    [SerializeField] float SPAWN_X_MAX;
    [SerializeField] float SPAWN_Z_MIN;
    [SerializeField] float SPAWN_Z_MAX;

    [SerializeField] GameObject PlayerPrefub;

    private void Start()
    {
        SpawnPlayer();
    }

    private void SpawnPlayer() 
    {
        Vector3 spawnpos = new Vector3(Random.Range(SPAWN_X_MIN, SPAWN_X_MAX), 0, Random.Range(SPAWN_Z_MIN, SPAWN_Z_MAX));
        PhotonNetwork.Instantiate(PlayerPrefub.name, spawnpos, Quaternion.identity);
    }
}

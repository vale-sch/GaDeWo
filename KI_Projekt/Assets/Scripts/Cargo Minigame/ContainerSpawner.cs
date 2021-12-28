using UnityEngine;

public class ContainerSpawner : MonoBehaviour {
    public GameObject[] containerPrefab;
    public Transform spawnPoint;
    public AudioSource containerSpawnSound;
    private float countdown = 5f;

    private void Update() {
        if (transform.childCount > 1 || GameData.containersInTransfer <= 0) return;
        if (countdown <= 0f) {
            SpawnContainer();
            countdown = Random.Range(4f, 8f);
            return;
        }
        countdown -= Time.deltaTime;
    }

    private void SpawnContainer() {
        GameData.containersInTransfer--;
        if (!containerSpawnSound.isPlaying) containerSpawnSound.Play();
        GameObject instContainer = Instantiate(containerPrefab[GetPrefabWithProbability()], spawnPoint.position, Quaternion.identity);
        instContainer.GetComponent<CargoContainer>().department = (Department)Random.Range(1, 6);
        instContainer.transform.parent = transform;
    }

    private int GetPrefabWithProbability() {
        float random = Random.value;
        if (random < 0.15f) return 0;
        if (random > 0.9f) return 2;
        return 1;
    }
}

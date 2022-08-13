using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour {
    private GameObject[] Spawners;
    public GameObject Seeker;
    public int kills = 0, active;
    void Start() {
        Spawners = GameObject.FindGameObjectsWithTag("Spawner");
    }

    public void deadSeeker() {
        kills += 1;
        active -= 1;
    }
    void Update() {
        if (active <= 0) {
            active = Mathf.Clamp(kills / 3 + 1, 1, 8);
            StartCoroutine(Spawn());
        }
    }

    IEnumerator Spawn() {
        int spawn = active;
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < spawn; i++) {
            Instantiate(Seeker, Spawners[(int)Random.Range(0, 3)].transform.position, Quaternion.identity);
            yield return new WaitForSeconds(0.3f);
        }
    }
}

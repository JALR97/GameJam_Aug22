using System.Collections;

using UnityEngine;

public class Spawner : MonoBehaviour {
    public GameObject[] Spawners;
    public GameObject Seeker;
    public int kills = 0, active;


    public void deadSeeker() {
        kills += 1;
        active -= 1;
    }
    void Update() {
        if (Spawners[0].activeSelf) {
            if (active <= 0) {
                active = kills / 4 + 3; 
                StartCoroutine(Spawn());
            }
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

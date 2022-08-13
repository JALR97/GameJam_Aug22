using UnityEngine;

public class RotateAround : MonoBehaviour {
    public float rotSpeed = 1;
    void Update() {
        transform.Rotate(0, 0, rotSpeed * Time.deltaTime);
    }
}

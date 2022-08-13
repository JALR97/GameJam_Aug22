using System;
using UnityEngine;

public class AttackHit : MonoBehaviour {
    private Collider2D col;
    void Start() {
        col = GetComponent<Collider2D>();
    }
    
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Enemy") && GetComponentInParent<JeffController>().dashing) {
            other.GetComponent<Seeker>().Death();
        }
    }
}

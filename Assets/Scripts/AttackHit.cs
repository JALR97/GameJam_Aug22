using System;
using UnityEngine;

public class AttackHit : MonoBehaviour {
    private JeffController jf;
    private Collider2D col;
    void Start() {
        col = GetComponent<Collider2D>();
        jf = GetComponentInParent<JeffController>();
    }
    
    private void OnTriggerStay2D(Collider2D other) {
        if (other.CompareTag("Enemy") && jf.dashing) {
            other.GetComponent<Seeker>().Death();
        }
    }
}

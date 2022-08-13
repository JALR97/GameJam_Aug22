using System;
using UnityEngine;

public class ContactDmg : MonoBehaviour {
    private Collider2D col;
    public GeneralManager gm;
    private void Start() {
        col = GetComponent<Collider2D>();
        gm = GameObject.FindGameObjectWithTag("Manager").GetComponent<GeneralManager>();
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            gm.ChgHealth(-1);
        }
    }
}

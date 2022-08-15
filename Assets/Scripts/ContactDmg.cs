using System;
using UnityEngine;

public class ContactDmg : MonoBehaviour {
    private Collider2D col;
    public float pushForce = 3;
    public Health gm;
    private void Start() {
        col = GetComponent<Collider2D>();
        gm = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            gm.Damage(1);
            collision.rigidbody.velocity = GetComponent<Rigidbody2D>().velocity;
        }
        Vector2 playerPos = collision.gameObject.transform.position;
        Vector2 dir = playerPos - collision.contacts[0].point;
        dir.Normalize();
    }
}

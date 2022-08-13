using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PlayerController : MonoBehaviour {
    
    public float speed = 10f;
    private Rigidbody2D rb;
    private Vector2 movement = new Vector2();
    private void Update() {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement.Normalize();
    }
    private void Start() {
        rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate() {
        rb.MovePosition(rb.position + movement * (speed * Time.fixedDeltaTime));
    }
}

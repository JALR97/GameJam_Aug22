using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class JeffController : MonoBehaviour {
    private float distanceFromPlayer, timer;
    [SerializeField] private float energy = 1.0f;
    private Move selectedCommand = Move.GodSpeed;
    private Rigidbody2D playerRb, rb;
    private Collider2D playerCol, col;
    private GameObject signal, laser;
    [SerializeField]private bool canRunCommand = false; // ##TEMP SERIALIZATION
    public float energyRechargeSpeed = 10.0f, dashSpeed = 1.0f, rotSpeed = 1.0f;
    public float signalRange = 4.0f, dashTime = 1.2f;
    private Camera cam;
    public bool dashing = false;
    private enum Move {
        GodSpeed, //0
        Stomp, //1
        Beam //2
    }
    Dictionary<Move, float> energyRanges = new Dictionary<Move, float>()
    {
        {Move.GodSpeed, 50},
        {Move.Stomp, 60},
        {Move.Beam, 100}
    };
    void Start() {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerRb = player.GetComponent<Rigidbody2D>();
        playerCol = player.GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
        cam = Camera.main;
        signal = GetComponentInChildren<RotateAround>().gameObject;
        laser = GameObject.FindGameObjectWithTag("Laser");
    }
    private void Update() {
        distanceFromPlayer = (playerRb.position - rb.position).magnitude;
        energy += 1 * energyRechargeSpeed * Time.deltaTime;
        energy = Mathf.Clamp(energy, 0, 100);
        if (dashing) {
            timer -= Time.deltaTime;
            if (timer <= 0) {
                dashing = false;
                Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Jeff"), LayerMask.NameToLayer("Unit"), false);
            }
        }
        if (energy >= energyRanges[selectedCommand] && distanceFromPlayer <= signalRange) {
            canRunCommand = true;
            Vector3 mouse = cam.ScreenToWorldPoint(Input.mousePosition);
            mouse.z = 0f;
            Rotate(mouse - transform.position);
        }
        else {
            canRunCommand = false;
        }
        if (Input.GetMouseButtonDown(0) && canRunCommand) {
            RunCommand();
        } 
        signal.SetActive(energy >= energyRanges[selectedCommand]);
        laser.SetActive(canRunCommand);
    }
    private void RunCommand() {
        if (selectedCommand == Move.GodSpeed) {
            Godspeed();
        }
        energy -= energyRanges[selectedCommand];
    }
    private void Godspeed() { //totally not stolen from Neon White
        Vector2 direction = transform.right;
        rb.velocity = direction.normalized * dashSpeed;
        dashing = true;
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Jeff"), LayerMask.NameToLayer("Unit"), true);
        timer = dashTime;
    }
    private void Rotate(Vector2 direction) {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion qDest = Quaternion.Euler(new Vector3(0, 0, angle));
        transform.rotation = Quaternion.RotateTowards(transform.rotation, qDest, 10 * rotSpeed * Time.deltaTime);
    }
    private void OnCollision2DEnter(Collision2D collision) {
        
    }
}

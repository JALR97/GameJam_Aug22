using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JeffController : MonoBehaviour {
    private float distanceFromPlayer, timer;
    [SerializeField] private float energy = 1.0f;
    private Move selectedCommand = Move.GodSpeed;
    private Rigidbody2D playerRb, rb;
    private Collider2D playerCol, col;
    private GameObject signal, laser;
    private ParticleSystem ps;
    private TrailRenderer tr;
    private bool canRunCommand;
    public float energyRechargeSpeed = 10.0f, dashSpeed = 1.0f, rotSpeed = 1.0f;
    public float signalRange = 4.0f, dashTime = 1.2f;
    private Camera cam;
    public bool dashing;
    public Slider bar1, bar2;
    public AudioSource ThisAudioSource;
    
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
        tr = GetComponent<TrailRenderer>();
        ps = GetComponent<ParticleSystem>();
        ps.Stop();
    }

    private void Update() {
        //continuous check of ditance
        distanceFromPlayer = (playerRb.position - rb.position).magnitude;
        
        //energy recharge
        energy += 1 * energyRechargeSpeed * Time.deltaTime;
        energy = Mathf.Clamp(energy, 0, 100);
        
        //Bar progress
        bar1.value = Mathf.Clamp(energy, 0, 50);
        bar2.value = Mathf.Clamp(energy-50, 0, 50);;    
        bar2.transform.GetChild(1).gameObject.SetActive(energy > 50);
        
        //Current action
        if (dashing) {
            timer -= Time.deltaTime;
            if (timer <= 0) {
                dashing = false;
                ps.Stop();
                StartCoroutine(DelayedTrail());
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
        signal.SetActive(energy >= energyRanges[selectedCommand] && !dashing);
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
        tr.emitting = true;
        ps.Play();
        ThisAudioSource.Play();
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Jeff"), LayerMask.NameToLayer("Unit"), true);
        timer = dashTime;
    }
    private void Rotate(Vector2 direction) {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion qDest = Quaternion.Euler(new Vector3(0, 0, angle));
        transform.rotation = Quaternion.RotateTowards(transform.rotation, qDest, 10 * rotSpeed * Time.deltaTime);
    }
    IEnumerator DelayedTrail() {
        yield return new WaitForSeconds(0.8f);
        tr.emitting = false;
    }
}

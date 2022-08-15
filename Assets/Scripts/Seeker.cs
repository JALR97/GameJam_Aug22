
using UnityEngine;

public class Seeker : MonoBehaviour {
    
    public float movSpeed = 4.0f, rotSpeed = 10.0f;
    public float chargeSpeed = 30.0f, rechargeTime = 10.0f, chargeDistanceStart = 5.0f;
    private Rigidbody2D rb, target;
    private bool isRecharging = false, moving = false;
    private GameObject manager;
    private TrailRenderer tr;
    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        manager = GameObject.FindGameObjectWithTag("Manager");
        tr = GetComponent<TrailRenderer>();
    }

    private void FixedUpdate() {
        Vector2 direction = (target.position - rb.position);
        if (moving) {
            Move(direction);
        }
        if (transform.localScale.x > 1) {
            rb.velocity = direction.normalized * chargeSpeed;
            isRecharging = false;
            moving = false;
        }
    }
    
    private void Update() {
        Vector3 sizeChange = new Vector3(0.1f, 0f, 0f);
        Vector2 direction = (target.position - rb.position);
        
        if (!isRecharging) {
            if (transform.localScale.x <= 0.7) {
                if (direction.magnitude < chargeDistanceStart) { //In range, start charging
                    isRecharging = true;
                    moving = false;
                    rb.velocity *= 0;
                    tr.emitting = true;
                }
                else { //Not in range, keep persue
                    moving = true;
                    Rotate(direction);
                    tr.emitting = false;
                }
            }
            else { //Rest before dash not finished
                transform.localScale -= sizeChange * (60 * Time.deltaTime / rechargeTime);
                tr.emitting = true;
            }
        }
        else { //Charging the dash
            transform.localScale += sizeChange * (90 * Time.deltaTime / rechargeTime);
            Rotate(direction);
        }
    }

    private void Move(Vector2 destination) {
        rb.velocity = destination.normalized * (movSpeed * Time.deltaTime);
        //rb.MovePosition(rb.position + destination.normalized * (movSpeed * Time.deltaTime)); 
    }
    private void Rotate(Vector2 direction) {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
        Quaternion qDest = Quaternion.Euler(new Vector3(0, 0, angle));
        transform.rotation = Quaternion.RotateTowards(transform.rotation, qDest, 10 * rotSpeed * Time.deltaTime);
    }

    public void Death() {
        Destroy(gameObject);
        manager.GetComponent<Spawner>().active -= 1;
        manager.GetComponent<Spawner>().kills += 1;
    }
}

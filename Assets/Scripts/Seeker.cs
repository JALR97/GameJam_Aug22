using UnityEngine;

public class Seeker : MonoBehaviour {
    
    public float speed = 4f, chargeForce = 30f, chargeTime = 10f;
    public float chargeDistanceStart = 5f;
    private Rigidbody2D rb, target;
    private bool isCharging = false;
    private readonly Vector3 sizeChange = new Vector3(+0.01f, 0f, 0f);
    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate() {
        Vector2 direction = (target.position - rb.position);
        rb.MoveRotation(Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90);
        
        if (!isCharging) {
            if (transform.localScale.x <= 0.7) {
                if (direction.magnitude < chargeDistanceStart) {
                    isCharging = true;
                }
                else {
                    rb.MovePosition(rb.position + direction.normalized * (speed * Time.fixedDeltaTime));    
                }
            }
            else {
                transform.localScale -= sizeChange / (chargeTime * 2 * Time.fixedDeltaTime);
            }
        }
        else {
            transform.localScale += sizeChange / (chargeTime * Time.fixedDeltaTime);
            if (transform.localScale.x > 1) {
                rb.AddForce(direction.normalized * (chargeForce * 10));
                isCharging = false;
            }
        }
    }
}

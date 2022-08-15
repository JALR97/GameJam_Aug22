using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.PlayerLoop;

public class PlayerController : MonoBehaviour {
    
    public float speed = 10f;
    private Rigidbody2D rb;
    private Vector2 movement = new Vector2();
    public bool canWalk = true, walking = false;
    //sounds
    public AudioClip[] steps;
    public AudioSource thisAudioS;
    [Range(0.1f, 0.5f)]
    public float volumeModifier;
    [Range(0.5f, 1f)]
    public float volumeBase;
    [Range(0.1f, 0.5f)]
    public float pitchModifier;
    private void Update() {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement.Normalize();
        walking = canWalk && movement.magnitude > 0;
        if (walking && !thisAudioS.isPlaying) {
            thisAudioS.clip = steps[Random.Range(0, steps.Length - 1)];
            thisAudioS.volume = Random.Range(volumeBase - volumeModifier, volumeBase);
            thisAudioS.pitch = Random.Range(1f - pitchModifier, 1f + pitchModifier);
            thisAudioS.Play();
        }

        if (walking) {
            float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle)); 
        }
    }
    private void Start() {
        rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate() {
        if (walking) {
            rb.velocity = movement * (speed * Time.fixedDeltaTime);
        }
    }
}

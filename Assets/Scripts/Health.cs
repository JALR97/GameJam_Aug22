using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class Health : MonoBehaviour {
    private int health;
    private bool invincible = false;
    public GameObject UIpoints;
    public SpriteRenderer sr;
    public int flashes;
    public float flashTime, knockTime;
    public Color flashColor, regularColor;
    public PlayerController playerC;
    //gameover stuff
    public GameObject deathUI;
    public MusicController musicC;
    //Sound stuff
    public AudioClip damageSound;
    public AudioSource thisAudioS;
    private void Awake() {
        HealthReset();
        
    }
    public void HealthReset() {
        health = 10;
        Time.timeScale = 1;
    }
    public void Damage(int dmg) {
        StartCoroutine(ProcessDamage(dmg));
    }
    private IEnumerator ProcessDamage(int diff) {
        if (invincible) {
            yield break;
        }
        
        //damage sound
        thisAudioS.pitch = 1;
        thisAudioS.volume = 1;
        thisAudioS.PlayOneShot(damageSound);
        
        //UI Update
        for (int i = 1; i <= diff; i++) {
            UIpoints.transform.GetChild(health - i).gameObject.SetActive(false);
        }
        health -= diff;
        
        //if health hits 0, game over
        if (health <= 0) {
            health = 0;
            Time.timeScale = 0;
            Debug.Log("Game over");
            GameObject.FindGameObjectWithTag("Jeff").SetActive(false);
            deathUI.SetActive(true);
            musicC.Stop();
            //GameObject.FindGameObjectWithTag("Player").SetActive(false);
        }
        
        //show damage and invincibility frames
        StartCoroutine(Knockback());
        invincible = true;
        for (int i = 0; i < flashes; i++) {
            sr.color = flashColor;
            yield return new WaitForSeconds(flashTime);
            sr.color = regularColor;
            yield return new WaitForSeconds(flashTime);
        }
        invincible = false;
    }
    private IEnumerator Knockback() {
        playerC.canWalk = false;
        yield return new WaitForSeconds(knockTime);
        playerC.canWalk = true;
    }

    public int GetHealth() {
        return health;
    }

}

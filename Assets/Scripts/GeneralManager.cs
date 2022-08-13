using System;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

public class GeneralManager : MonoBehaviour {
    private int health;
    public Text txt;

    private void Awake() {
        HealthReset();
    }
    private void UpdateUI() {
        txt.text = "health: " + health.ToString();
    }
    public void HealthReset() {
        health = 10;
        UpdateUI();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Application.Quit();
        }
    }

    public void ChgHealth(int diff) {
        health += diff;
        UpdateUI();
        if (health <= 0) {
            health = 0;
            Time.timeScale = 0;
            Debug.Log("Game over");
        }
    }
    public int GetHealth() {
        return health;
    }

}

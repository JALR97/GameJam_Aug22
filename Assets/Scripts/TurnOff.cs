using System;
using UnityEngine;

public class TurnOff : MonoBehaviour {
    
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Enemy")) {
            other.GetComponent<Seeker>().enabled = false;
            
        }
        
    }
}

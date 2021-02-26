using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutBounds : MonoBehaviour
{
    // Class Variables
    private float topBound = 0.0f;    // Galaga position at -9
    private float lowerBound = -11.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Player projectile destroy
        if (transform.position.y > topBound) {

            Destroy(gameObject);
        }
        // Enemy out of bounds destroy
        else if (transform.position.y < lowerBound) {

            Destroy(gameObject);
        }
    }
}

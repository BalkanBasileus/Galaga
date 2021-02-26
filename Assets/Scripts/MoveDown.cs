using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDown : MonoBehaviour
{
    // Class Variables
    public float speed = 2.0f;
    private PlayerController playerControllerScript;

    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Galaga").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        // If obstacle isn't hit, continue to move obstacles left.
        if (playerControllerScript.gameOver == false) {
            transform.Translate(Vector2.down * Time.deltaTime * speed);
        }   
    }
}

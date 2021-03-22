using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatBackround : MonoBehaviour
{
    // Class Variables
    private Vector2 startPos;
    private float repeatWidth;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        repeatWidth = GetComponent<BoxCollider2D>().size.y;
    }

    // Update is called once per frame
    void Update()
    {
        // If background moves down by its repeat width, move it back to start position
        if (transform.position.y < startPos.y - repeatWidth) {

            transform.position = startPos;
        }
    }
}

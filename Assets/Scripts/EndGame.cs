using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
  // Variables
  public float theTime = 85.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      countDown();
    }

  public void countDown() {

    theTime -= Time.deltaTime;
    if (theTime < 0.0f) { Application.Quit(); }
  }
}

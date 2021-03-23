using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sounds : MonoBehaviour
{
  // Class Variables
  private AudioSource playerAudio;
  public AudioClip deathSound;
  private GameObject galaga;
  

  // Start is called before the first frame update
  void Start(){
    galaga = GameObject.Find("Galaga");
    playerAudio = GetComponent<AudioSource>();
  }

    // Update is called once per frame
  void Update()
  {
    // Call again for new galaga if they die
    galaga = GameObject.Find("Galaga"); 

   if (galaga == null) { // If Galaga dead
        playerAudio.PlayOneShot(deathSound);
    }
  }
}

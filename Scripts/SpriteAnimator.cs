using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAnimator : MonoBehaviour
{
  // Class Variables
 // public event EventHandler OnAnimationLooped;
  [SerializeField] private Sprite[] frameArray;
  private int currentFrame;
  private float timer;
  public float framerate = 0.15f;
  private SpriteRenderer spriteRenderer;
  private bool loop = false;
  private bool isPlaying = true;
  private bool hit;

  // Start is called before the first frame update
  void Start()
  {
    spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    hit = false;
  }

  // Update is called once per frame
  void Update()
  {
    // If galaga hit.
    if( hit ) {
      RunDeathAnimation();
    }
  }

  private void StopPlaying() {
    isPlaying = false;
  }

  public void RunDeathAnimation() {
   // Debug.Log("RunDeathAnimation!");
    timer += Time.deltaTime;

    if (!isPlaying) {
      return;
    }

    if (timer >= framerate) {
      timer -= framerate;
      currentFrame = (currentFrame + 1) % frameArray.Length;

      if (!loop && currentFrame == 0) {
        StopPlaying();

        // Destroy Galaga or Enemy here, after animation!
        // Cannot call in PlayerController script.

        Destroy(gameObject); 
      }
      else {
        spriteRenderer.sprite = frameArray[currentFrame];
      }
    }
  }
  /*
  public void PlayAnimation(Sprite[] frameArray) {
    //
    this.frameArray = frameArray;
    currentFrame = 0;
    timer = 0f;
    spriteRenderer.sprite = frameArray[currentFrame];
  }
  */

  private void OnTriggerEnter2D( Collider2D other ) {
    
    // If Galaga hit   
    if (other.gameObject.CompareTag("EnemyBullet") && gameObject.CompareTag("Galaga") ) {

      hit = true;
    }
    // If Enemy hit
    if (other.gameObject.CompareTag("Bullet") && gameObject.CompareTag("EnemyBee") ) {

      hit = true;
    }
  }
}

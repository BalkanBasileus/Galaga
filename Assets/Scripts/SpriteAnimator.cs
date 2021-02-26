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
  private float framerate = 0.2f;
  private SpriteRenderer spriteRenderer;
  private bool loop = false;
  private bool isPlaying = true;

  // Start is called before the first frame update
  void Start()
  {
    spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
  }

  // Update is called once per frame
  void Update()
  {
    RunDeathAnimation();
  }

  private void StopPlaying() {
    isPlaying = false;
  }

  public void RunDeathAnimation() {

    timer += Time.deltaTime;

    if (!isPlaying) {
      return;
    }

    if (timer >= framerate) {
      timer -= framerate;
      currentFrame = (currentFrame + 1) % frameArray.Length;

      if (!loop && currentFrame == 0) {
        StopPlaying();
      }
      else {
        spriteRenderer.sprite = frameArray[currentFrame];
      }
    }
  }
}

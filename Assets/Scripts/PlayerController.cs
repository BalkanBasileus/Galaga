/*///////////////////////////////////////////////////////////////////////
 *
 //////////////////////////////////////////////////////////////////////*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
  // Class Variables
  public float speed;
  private float horizontalInput;
  private float xRange = 3.70f;
  public float shootInterval = 0.8f;
  public bool gameOver = false;
  private bool canShoot = true;
  private bool isDead;
  private int lives = 3;

  public GameObject bulletPrefab;
  public AudioClip shootSound;
  public AudioClip deathSound;
  private AudioSource playerAudio;
  private SpriteAnimator deathAnimation;
  //private SpriteAnimator spriteAnimatorScript;

  // Start is called before the first frame update
  void Start()
  {
    playerAudio = GetComponent<AudioSource>();
    deathAnimation = GetComponent<SpriteAnimator>();    
    isDead = false;
  }

    // Update is called once per frame
  void Update()
  {
    // Move player left or right
    horizontalInput = Input.GetAxis("Horizontal");
    transform.Translate(Vector3.right * speed * horizontalInput * Time.deltaTime);
    
    // Movement Boundary
    movementBoundries();

    // Spacebar shoot
    if ( (canShoot && Input.GetKeyDown(KeyCode.Space) ) || (canShoot && Input.GetKeyDown(KeyCode.Z) ) ) {

      // Shoot first, ask questions later.
      canShoot = false;
      Invoke(nameof(CanShoot), shootInterval); // Prevent player from spamming lasers.
      Instantiate(bulletPrefab, transform.position, bulletPrefab.transform.rotation);
      playerAudio.PlayOneShot(shootSound); // Play laser shot audio
      
    }
  }

  private void movementBoundries() {
    // Boundries for movement 

    // If player goes too far left.
    if (transform.position.x < -xRange) { // left x axis

      transform.position = new Vector3(-xRange, transform.position.y, transform.position.z); // Don't move any further
    }
    // If player goes too far right.
    if (transform.position.x > xRange) { // right x axis

      transform.position = new Vector3(xRange, transform.position.y, transform.position.z); // Don't move any further
    }
  }

  private void CanShoot() {
    // Prevent player from spamming lasers
    canShoot = true;
  }
  
  
  private void OnTriggerEnter2D( Collider2D other ) {
    // If Galaga hit
    if ( other.gameObject.CompareTag("EnemyBullet") || other.gameObject.CompareTag("EnemyBee") ) {

      lives--;
      isDead = true;
      // Play explosion
      playerAudio.PlayOneShot(deathSound);
      
    }
  }
  
  public bool galagaDead() {
    return isDead;
  }

  public int galagaLives() {
    return lives;
  }

}

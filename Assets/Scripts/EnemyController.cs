/*
 *
 * 
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
  // Class Variables
  [SerializeField] public float speed;
  [SerializeField] public float timer = 10;
  [SerializeField] public float shootInterval = 2.0f;
  private float travelDistance = 0.5f;
  public int direction = 1;
  private int pointValue = 100;
  private Vector3 startPosition;
  public bool canShoot = true;
  private bool startAttackVector = false;

  public GameObject bulletPrefab;
  private GameManager gameManager;

  private AudioSource enemyAudio;
  public AudioClip deathNoise;

  // Start is called before the first frame update
  void Start()
    {
      //Shoot();
      gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
      enemyAudio = GetComponent<AudioSource>();
      startPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z); // Initial pos of Enemy
  }

    // Update is called once per frame
    void Update()
    {
      Move(startPosition);
      if (startAttackVector) {
        attackVector();
        Shoot();
      }
    }

  void Move(Vector3 startPosition) {
    // Move enemy bee left and right

    var pos = transform.position; // Get current pos.

    pos.x = pos.x + direction * Time.deltaTime * speed;
    pos.x = Mathf.Clamp(pos.x, startPosition.x - travelDistance, startPosition.x + travelDistance);

    // Update Pos next call.
    transform.position = pos;

    // Slide side to side
    if (pos.x == startPosition.x - travelDistance || pos.x == startPosition.x + travelDistance) {
      direction = -1 * direction; // Switch direction
    }
  }

  public void Shoot() {

    if (canShoot) {
      canShoot = false;
      Invoke(nameof(CanShootNow), shootInterval);
      Instantiate(bulletPrefab, transform.position, bulletPrefab.transform.rotation);
    }
  }


  private void OnTriggerEnter2D( Collider2D other ) {
    // If Enemy Bee hit
    if (other.gameObject.CompareTag("Bullet")) {

      // Update Score
      enemyAudio.PlayOneShot(deathNoise);
      gameManager.UpdateScore(pointValue);
    }
    if (other.gameObject.CompareTag("Galaga")) {
      enemyAudio.PlayOneShot(deathNoise);
    }
  }

  public void attackVector() {
    Vector3 pos = transform.position;
    //pos.z = -1.0f;

    if (pos.y < -10.0f) {
      pos.y = -3.5f;
    }

    var speed = 2.0f;
    pos.y = pos.y - Time.deltaTime * speed;
    if (pos.x < 0) {
      pos.x = Mathf.Sin(pos.y) * 2 * -1;
    }
    else {
      pos.x = Mathf.Sin(pos.y) * 2;
    }
    transform.position = pos;

    // Attack for 10 seconds. Destory if not hit.
    timer -= Time.deltaTime;
    if(timer < 0) { Destroy(this.gameObject); }
  }

  public void startAttack() {
    startAttackVector = true;
  }

  private void CanShootNow() {
    canShoot = true;
  }

}

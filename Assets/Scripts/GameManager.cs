using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
  // Variables
  // public List<GameObject> targets;
  private float spawnRate = 0.1f;
  private float distance = 0.5f;
  //private float attackTime = 2.0f;
  private int score;
  private int maxColumn = 10;
  private int lives = 3;
  public bool isGameActive;
  public bool spawning = false;
  private bool attacker = false;
  public Text scoreText;
  public Text pointsText;
  public Text gameOverText;
  public Text levelCompleteText;
  public Text shotText;
  public Text shotDisplayText;
  public Text accuracyDisplayText;
  public Text accuracyText;
  public Button restartButton;
  public GameObject enemy;
  public GameObject galaga;
  public Image life1,life2,life3;
  private EnemyController enemyController;
  private PlayerController galagaController;
  private Vector3 startPos = new Vector3(-3, -4.5f, -1);
  private Vector3 lifePos = new Vector3(-5,-9,-1);

  private AudioSource gameAudio;
  public AudioClip gameOverMusic;
  public AudioClip startLevelSound;
  public AudioClip levelCompleteSound;
  public AudioClip enemyBeeAttackSound;

  private List<GameObject> targets;

  // level statistics
  float hits;
  float accuracy;

  // Start is called before the first frame update
  void Start() {
    
    galagaController = GameObject.Find("Galaga").GetComponent<PlayerController>();
    gameAudio = GetComponent<AudioSource>();
    gameAudio.PlayOneShot(startLevelSound);

    StartCoroutine(SpawnTarget());
    UpdateScore(0);
  }

  // Update is called once per frame
  void Update() {

    GameObject galaga = GameObject.FindGameObjectWithTag("Galaga");
    if (!galaga && !spawning && lives >= 0) {
      lives -= 1;
      StartCoroutine(SpawnGalaga());
    }

    //countDown();
    updateLives();
    recordData();
    
    if (checkLives()) {
      GameOver();
    }

    if( checkWin()) {
     levelComplete();
    }

    ChooseAttacker();
  }
  
  public IEnumerator SpawnTarget() {
    // Wait 7 seconds for intromusic, spawn enemies quickly.
    // Set game active to begin. Checkwin() later will
    // check if enemies are all gone to display levelcomplete().

    yield return new WaitForSeconds(7.0f);
    yield return new WaitForSeconds(spawnRate);
      
    // Spawn Brigade of Enemy Bees. Update pos.
    for(int i = 0; i< maxColumn; i++) {
      
      // Provide space for two main columns
      if(i % 5 == 0) {
        startPos.x += distance;
        continue;
      }

      Instantiate(enemy, startPos, enemy.transform.rotation);
     // GameObject thisEnemy = Instantiate(enemy, startPos, enemy.transform.rotation);
      startPos.x += distance;

      // Append
     // targets.Add(thisEnemy);
     // Debug.Log("targets count: " + targets.Count.ToString());
    }

    // Set active here, otherwise level complete screen starts at beginning.
    isGameActive = true;
  }

  // Spawn Galaga
  IEnumerator SpawnGalaga() {
    spawning = true;
    yield return new WaitForSeconds(1f);
    GameObject newGalaga = (GameObject)Instantiate(galaga);
    newGalaga.transform.position = new Vector3(0.29f, -9.17f, -1);
    newGalaga.tag = "Galaga";
    newGalaga.name = "Galaga";
    spawning = false;
  }


  // Update Score
  public void UpdateScore( int scoreToAdd ) {
    score += scoreToAdd;
    pointsText.text = score.ToString();
  }

  public void GameOver() {

    if (isGameActive) {
      GameObject galaga = GameObject.Find("Galaga");
      Destroy(galaga);
      gameOverText.gameObject.SetActive(true);
      restartButton.gameObject.SetActive(true);
      gameAudio.PlayOneShot(gameOverMusic);
      isGameActive = false;
    }
  }

  public void RestartGame() {
    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
  }

  // Display time countdown
 // private void countDown() {

   // attackTime -= Time.deltaTime;
 // }

  private void updateLives() {
    // Check galaga lives and update UI


    if (lives == 0) {
      life1.gameObject.SetActive(false);
      life2.gameObject.SetActive(false);
      life3.gameObject.SetActive(false);
    }
    if (lives == 1) {
      life1.gameObject.SetActive(true);
      life2.gameObject.SetActive(false);
      life3.gameObject.SetActive(false);
    }
    if (lives == 2) {
      life1.gameObject.SetActive(true);
      life2.gameObject.SetActive(true);
      life3.gameObject.SetActive(false);
    }
    if (lives == 3) {
      life1.gameObject.SetActive(true);
      life2.gameObject.SetActive(true);
      life3.gameObject.SetActive(true);
    }
  }

  private bool checkLives() {
    // Check if all galaga lives are lost.

    bool gameOver = false;

    if(lives == -1) {
      gameOver = true;
    }
    return gameOver;
  }

  private bool checkWin() {
    // check if all enemy bees destroyed.
    bool won = false;

    GameObject[] enemies = GameObject.FindGameObjectsWithTag("EnemyBee");

    // If no enemies and game is active, return victory.
    if ( enemies.Length == 0 && isGameActive) {
      won = true;
    }
    return won;
  }

  private void levelComplete() {
    // Displays victory and level statistics.

    //Calculate Accuracy and format
    accuracy = ( (8.0f / hits) * 100f);
    accuracyText.text = "% " + accuracy.ToString();
    shotText.text = hits.ToString();

    // Display level prompt
    if (isGameActive) {
      levelCompleteText.gameObject.SetActive(true);
      //gameAudio.PlayOneShot(levelCompleteSound);
      //yield return new WaitForSeconds(1.0f);
      shotText.gameObject.SetActive(true);
      shotDisplayText.gameObject.SetActive(true);
      accuracyDisplayText.gameObject.SetActive(true);
      accuracyText.gameObject.SetActive(true);
      gameAudio.PlayOneShot(levelCompleteSound);
      //yield return new WaitForSeconds(1.0f);
      isGameActive = false;
    }
    
  }

  private void OnMouseDown() {
    // When player clicks restart button
    restartButton.onClick.AddListener( RestartGame );
  }

  private void recordData() {
    // 

    if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Z)) {
      hits++;
    }
    
  }

  public void attackVector() {
    Vector3 pos = transform.position;
    pos.z = -1.0f;

    if (pos.y < -10.0f) {
      pos.y = 1.0f;
    }

    var speed = 1.0f;
    pos.y = pos.y - Time.deltaTime * speed;
    pos.x = Mathf.Sin(pos.y) * 3;
    transform.position = pos;
  }


  public void ChooseAttacker() {

    GameObject target = GameObject.FindGameObjectWithTag("EnemyBee");
    if (target && galaga && lives >= 0) {

      EnemyController targetScript = target.GetComponent<EnemyController>();
      targetScript.startAttack();
      //gameAudio.PlayOneShot(enemyBeeAttackSound);
      //Debug.Log("attack sound called more than once...problem here.");

    }
  }

}

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
  private float attackTime = 2.0f;
  private int score;
  private int maxColumn = 10;
  private int lives = 3;
  public bool isGameActive;
  public bool spawning = false;
  public Text scoreText;
  public Text pointsText;
  public TextMeshProUGUI gameOverText;
  public Button restartButton;
  public GameObject enemy;
  public GameObject galaga;
  public Image life1,life2,life3;
  private EnemyController enemyController;
  private PlayerController galagaController;
  private Vector3 startPos = new Vector3(-3, -4.5f, -1);
  private Vector3 lifePos = new Vector3(-5,-9,-1);

  // Start is called before the first frame update
  void Start() {
    isGameActive = true;
    StartCoroutine( SpawnTarget() );
    UpdateScore(0);
    galagaController = GameObject.Find("Galaga").GetComponent<PlayerController>();
   
  }

  // Update is called once per frame
  void Update() {

    GameObject galaga = GameObject.FindGameObjectWithTag("Galaga");
    if (!galaga && !spawning && lives > 0) {
      lives -= 1;
      StartCoroutine(SpawnGalaga());
     // Debug.Log("lives: " + lives.ToString());
    }

    countDown();
 
    // Initiate attack every n seconds.
    if (attackTime == 0) {

     // StartCoroutine(ChooseAttackForce());
     // Attack();
    }

    checkLives();
  }
  
  public IEnumerator SpawnTarget() {

    yield return new WaitForSeconds(spawnRate);
      
    // Spawn Brigade of Enemy Bees. Update pos.
    for(int i = 0; i< maxColumn; i++) {
      
      // Provide space for two main columns
      if(i % 5 == 0) {
        startPos.x += distance;
        continue;
      }

      Instantiate(enemy, startPos, enemy.transform.rotation);
      startPos.x += distance;
    }
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
    gameOverText.gameObject.SetActive(true);
    restartButton.gameObject.SetActive(true);
    isGameActive = false;
  }

  public void RestartGame() {
    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
  }

  // Display time countdown
  private void countDown() {

    attackTime -= Time.deltaTime;
  }

  private void checkLives() {
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
  /*
  public IEnumerator displayLives() {

    yield return new WaitForSeconds(0.1f);

    for(int i = 0; i < 3; i++) {
      Instantiate(life, lifePos, enemy.transform.rotation);
      lifePos.x += 0.5f;
    }
  }
  */

  /*
  void Attack() {
    //enemy.transform.parent = null;
   // specificEnemy = GameObject.FindGameObjectWithTag("EnemyBee");

    EnemyController enemyBee = specificEnemy.GetComponent<EnemyController>();
    enemyBee.attackVector();
  }
  */

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

  /*
  IEnumerator ChooseAttackForce() {
    yield return new WaitForSeconds(1);

    if (!specificEnemy && GameObject.FindGameObjectWithTag("Galaga")) {
      //var enemyCount = GetEnemyCount();
      //if (enemyCount == 0) {
       // yield return new WaitForSeconds(1);
    //  }
      int attackerIndex = Random.Range(0, enemyCount);
      foreach (GameObject enemy in enemies) {
        if (enemy) {
          enemyCount -= 1;
        }
        if (enemyCount == attackerIndex) {
          specificEnemy = enemy;
        }
      }
    }
  }
  */

}

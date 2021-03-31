using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RestartButton : MonoBehaviour
{
  // Variables
  private Button button;
  private GameManager gameManager;

  // Start is called before the first frame update
  void Start()
    {
    button = GetComponent<Button>();
    button.onClick.AddListener(restartGame);
    gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
  }

    // Update is called once per frame
    void Update()
    {
        
    }

  public void restartGame() {
    gameManager.RestartGame();
  }
}

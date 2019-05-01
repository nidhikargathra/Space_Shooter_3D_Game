using System.Collections;
using UnityEngine;

public class GameUI : MonoBehaviour {
    [SerializeField] GameObject gameUI;
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject playerPrefab;
    [SerializeField] GameObject playerStartPosition;

    private void Start()
    {
        ShowGameUI();
    }
    //private void OnEnable()
    //{
    //    EventManager.onStartGame += ShowGameUI;
    //    EventManager.onPlayerDeath += DelayMainMenuDisplay;
    //}

    //private void OnDisable()
    //{
    //    EventManager.onStartGame -= ShowGameUI;
    //    EventManager.onPlayerDeath -= DelayMainMenuDisplay;
    //}

    //void DelayMainMenuDisplay()
    //{
    //    Invoke("ShowMainMenu", Asteroid.destructionDelay * 3);
    //}

    //void ShowMainMenu()
    //{
    //    //mainMenu.SetActive(true);
    //    gameUI.SetActive(false);
    //}

    void ShowGameUI()
    {
        //mainMenu.SetActive(false);
        gameUI.SetActive(true);
        //Instantiate(playerPrefab, playerStartPosition.transform.position, playerStartPosition.transform.rotation);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    menu,
    inventory,
    inGame,
    gameOver
}

public class GameManager : MonoBehaviour
{
    public GameState currentGameState = GameState.menu;
    public static GameManager instance;

    private MenuManager menuManager;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        menuManager = this.GetComponent<MenuManager>();
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S) && currentGameState != GameState.inGame)
        {
            StartGame();
        }
        if(Input.GetKeyDown(KeyCode.F)  && currentGameState != GameState.inventory){
            EnterInventory();
        }else if(Input.GetKeyDown(KeyCode.F) && currentGameState == GameState.inventory){
            StartGame();
        }
        if(Input.GetKeyDown(KeyCode.Escape)  && currentGameState != GameState.menu){
            BackToMenu();
        }else if(Input.GetKeyDown(KeyCode.Escape) && currentGameState == GameState.menu){
            StartGame();
        }
    }
    public void StartGame()
    {
        SetGameState(GameState.inGame);
    }
    public void EnterInventory()
    {
        SetGameState(GameState.inventory);
    }
    public void GameOver()
    {
        SetGameState(GameState.gameOver);
    }
    public void BackToMenu()
    {
        SetGameState(GameState.menu);
    }
    private void SetGameState(GameState newGameState)
    {
        if (newGameState == GameState.menu)
        {
            menuManager.showPausamenu();
            //menu logic
        }
        else if (newGameState == GameState.inGame)
        {
            menuManager.HideInventory();
            menuManager.HidePausaMenu();
        }
        else if (newGameState == GameState.gameOver)
        {

        }
        else if (newGameState == GameState.inventory)
        {
            menuManager.showInventory();
        }
        this.currentGameState = newGameState;
    }
}

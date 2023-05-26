using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] Canvas PausamenuCanvas;
    [SerializeField] Canvas inventoryCanvas;

    public void showInventory(){
        Time.timeScale = 0;
        inventoryCanvas.enabled = true;
    }

    public void HideInventory(){
        Time.timeScale = 1;
        inventoryCanvas.enabled = false;
    }

    public void showPausamenu(){
        PausamenuCanvas.enabled = true;
        Time.timeScale = 0;
    }

    public void HidePausaMenu(){
        PausamenuCanvas.enabled = false;
        Time.timeScale = 1;
    }

    public void ExitGame(){
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }
    void Awake(){
        Time.timeScale = 1;
        PausamenuCanvas.enabled = false;
        inventoryCanvas.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

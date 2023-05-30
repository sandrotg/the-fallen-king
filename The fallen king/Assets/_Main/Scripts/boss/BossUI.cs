using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BossUI : MonoBehaviour
{

    public GameObject bossPanel;
    public GameObject muro;

    public static BossUI instance;

    void Awake(){
        if(instance==null){
            instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        bossPanel.SetActive(false);
        muro.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void BossActivation(){
        bossPanel.SetActive(true);
        muro.SetActive(true);
    }

    public void BossDesactivator(){
        bossPanel.SetActive(false);
        muro.SetActive(false);
    }
}

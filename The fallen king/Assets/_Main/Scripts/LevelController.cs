using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    [SerializeField] public int currentlevel;
    [SerializeField] public int currentExp;
    [SerializeField] private int[] expToLevelUp;

    [SerializeField] private int[] healthLevels, damageLevels, armorLevels;

    [SerializeField] private Text vida;
    [SerializeField] private Text daño;
    [SerializeField] private Text armadura;
    [SerializeField] private Text level;

    [SerializeField] private Text exp;
    // Start is called before the first frame update
    void Start()
    {
        /* PlayerController.instance.SetBaseArmor(armorLevels[0]);
         PlayerController.instance.SetBaseDamage(damageLevels[0]);
         PlayerController.instance.AddSwordDamage();
         PlayerController.instance.SetBaseHealth(healthLevels[0]);
         PlayerController.instance.AddExtraArmor();
         PlayerController.instance.currentHealth = PlayerController.instance.GetTotalHealth();  */
    }

    // Update is called once per frame
    void Update()
    {
        exp.text = "Exp:" + " " +currentExp.ToString() + "/" + " " + expToLevelUp[currentlevel].ToString();
        vida.text = "Vida total:" + " " + PlayerController.instance.GetTotalHealth().ToString();
        daño.text = "Daño total" + " " + PlayerController.instance.getTotalDamage().ToString();
        armadura.text = "Armadura total" + " " + PlayerController.instance.getTotalArmor().ToString();
        level.text = "Level:" + " " + currentlevel.ToString();
        if (currentlevel == 0)
        {
            PlayerController.instance.SetBaseArmor(armorLevels[0]);
            PlayerController.instance.SetBaseDamage(damageLevels[0]);
            PlayerController.instance.AddSwordDamage();
            PlayerController.instance.SetBaseHealth(healthLevels[0]);
            PlayerController.instance.AddExtraArmor();
            PlayerController.instance.currentHealth = PlayerController.instance.GetTotalHealth();
        }
        else
        {
           // expImage.fillAmount = currentExp / expToLevelUp[currentlevel];
        }
        if (currentlevel >= expToLevelUp.Length)
            return;
        if (currentExp >= expToLevelUp[currentlevel])
        {
            currentlevel++;
            PlayerController.instance.SetBaseArmor(armorLevels[currentlevel]);
            PlayerController.instance.SetBaseDamage(damageLevels[currentlevel]);
            PlayerController.instance.AddSwordDamage();
            PlayerController.instance.SetBaseHealth(healthLevels[currentlevel]);
            PlayerController.instance.AddExtraArmor();
            PlayerController.instance.currentHealth = PlayerController.instance.GetTotalHealth();
        }
         Debug.Log(expToLevelUp[currentlevel]);
         Debug.Log(currentExp);

    }

    public void AddExperience(int exp)
    {
        currentExp += exp;
    }

    public int GetHealthlevel(int pos)
    {
        return healthLevels[pos];
    }
    public int GetDamagelevel(int pos)
    {
        return damageLevels[pos];
    }
    public int GetArmorlevel(int pos)
    {
        return armorLevels[pos];
    }
    public int GetCurrentlevel()
    {
        return currentlevel;
    }
}

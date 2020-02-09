using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerUpgradeScript : MonoBehaviour
{
    public GameObject DashPanel;
    public GameObject KunaiPanel;
    public GameObject AttackPanel;
    public Text TransactionText;
    private bool lvl1unlocked = false;
    private bool lvl2unlocked = false;
    private bool lvl3unlocked = false;
    private bool lvl4unlocked = false;
    private bool lvl5unlocked = false;

    public void Start()
    {

        PlayerData data = SaveSystem.LoadRubies();
        if(data.Dashcooldown>0)
        {
            if(data.Dashcooldown==9f)
            {
                lvl1unlocked = true;
            }
            else if(data.Dashcooldown == 8f)
            {
                lvl2unlocked = true;
            }
            else if (data.Dashcooldown == 7f)
            {
                lvl3unlocked = true;
            }
            else if (data.Dashcooldown == 6f)
            {
                lvl4unlocked = true;
            }
            else if (data.Dashcooldown == 5f)
            {
                lvl5unlocked = true;
            }
        }

    }
    public void ActivateDashPanel()
    {

        DashPanel.SetActive(true);
        KunaiPanel.SetActive(false);
        AttackPanel.SetActive(false); ;
    }
    public void ActivateKunaiPanel()
    {
        DashPanel.SetActive(false);
        KunaiPanel.SetActive(true);
        AttackPanel.SetActive(false);
    }
    public void ActivateAttackPanel()
    {
        DashPanel.SetActive(false);
        KunaiPanel.SetActive(false);
        AttackPanel.SetActive(true);
    }
    public void ResetData()
    {
        SaveSystem.DeleteData();
    }
    public void Dashlvl1()
    {
        PlayerData data = SaveSystem.LoadRubies();
        //Check if unlocked
        if (!lvl1unlocked)
        {
            //CHECK IF MONEY IS ENOUGH
            if (data.Rubies >= 20)
            {
                RubyCounterScript.Rubies -= 20;
                //SAVE RUBIES and dashcooldown
                Player.dashcooldown -=1f;
                SaveSystem.SaveRubies();

                TransactionText.text = "Skill Upgraded";
            }
            else
            {
                TransactionText.text = "Not Enough Rubies";
            }
        }
        else
        {
            TransactionText.text = "Already Unlocked";
        }

    }
    public void Dashlvl2()
    {
        PlayerData data = SaveSystem.LoadRubies();
        //Check if unlocked
        if (!lvl2unlocked)
        {
            //CHECK IF MONEY IS ENOUGH
            if (data.Rubies >= 40)
            {
                RubyCounterScript.Rubies -= 40;
                //SAVE RUBIES and dashcooldown
                Player.dashcooldown -= 1f; ;
                SaveSystem.SaveRubies();

                TransactionText.text = "Skill Upgraded";
            }
            else
            {
                TransactionText.text = "Not Enough Rubies";
            }
        }
        else
        {
            TransactionText.text = "Already Unlocked";
        }
    }
    public void Dashlvl3()
    {
        PlayerData data = SaveSystem.LoadRubies();
        //Check if unlocked
        if (!lvl3unlocked)
        {
            //CHECK IF MONEY IS ENOUGH
            if (data.Rubies >= 60)
            {
                RubyCounterScript.Rubies -= 60;
                //SAVE RUBIES and dashcooldown
                Player.dashcooldown -= 1f;
                SaveSystem.SaveRubies();

                TransactionText.text = "Skill Upgraded";
            }
            else
            {
                TransactionText.text = "Not Enough Rubies";
            }
        }
        else
        {
            TransactionText.text = "Already Unlocked";
        }
    }
    public void GoBack()
    {
        transform.gameObject.SetActive(false);
    }
    public void Kunailvl1()
    {

    }
    public void Kunailvl2()
    {

    }
    public void Kunailvl3()
    {

    }
    public void Attacklvl1()
    {

    }
    public void Attacklvl2()
    {

    }
    public void Attacklvl3()
    {

    }

}

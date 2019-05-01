using Assets.Code;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [SerializeField] int maxHealth = 10;
    [SerializeField] int currHealth;
    [SerializeField] float regenerationRate = 2f;
    [SerializeField] int regenerateAmount = 1;

    private LevelManager _manager;
    private ShieldUI _shieldUI;

    private void Start()
    {
        _manager = FindObjectOfType<LevelManager>();
        _shieldUI = FindObjectOfType<ShieldUI>();
        currHealth = maxHealth;
        InvokeRepeating("Regenerate", regenerationRate, regenerationRate);
    }

    void Regenerate()
    {
        //Debug.Log("Regenerating");
        if (currHealth < maxHealth)
            currHealth += regenerateAmount;

        if (currHealth > maxHealth)
            currHealth = maxHealth;
        Debug.Log("" + gameObject.name + " curr: " + currHealth + " maxh: " + maxHealth);
        EventManager.TakeDamage((float)currHealth / (float)maxHealth);
        if (gameObject.tag.Equals("Player"))
        {
            Debug.Log("player");
            _shieldUI.UpdateShieldDisplay((float)currHealth / (float)maxHealth);
        }
    }

    public void TakeDamage(int dmg = 100)
    {
        currHealth -= dmg;
        if (currHealth < 0)
            currHealth = 0;
        Debug.Log(""+gameObject.name+" curr: " + currHealth + " maxh: " + maxHealth);

        EventManager.TakeDamage((float)currHealth / (float)maxHealth);
        if (gameObject.tag.Equals("Player"))
        {
            Debug.Log("player");
            _shieldUI.UpdateShieldDisplay((float)currHealth / (float)maxHealth);
        }
        if (currHealth < 1)
        {
            Debug.Log("I b ded :( :" + gameObject.name);
            GetComponent<Explosion>().BlowUp();
            if (gameObject.tag.Equals("Player"))
            {
                EventManager.PlayerDeath();
                _manager.PlayerDied();
            }

        }
    }
}

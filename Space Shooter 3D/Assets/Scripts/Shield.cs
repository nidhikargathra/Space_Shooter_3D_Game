using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour {
    [SerializeField] int maxHealth = 10;
    [SerializeField] int currHealth;
    [SerializeField] float regenerationRate = 2f;
    [SerializeField] int regenerateAmount = 1;

    private void Start()
    {
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
        EventManager.TakeDamage((float)currHealth / (float)maxHealth);
    }

    public void TakeDamage(int dmg = 1)
    {
        currHealth -= dmg;
        if (currHealth < 0)
            currHealth = 0;
        EventManager.TakeDamage((float)currHealth /(float)maxHealth);

        if (currHealth < 1)
        {
            EventManager.PlayerDeath();
            GetComponent<Explosion>().BlowUp();

            Debug.Log("I b ded :(");
        }
    }
}

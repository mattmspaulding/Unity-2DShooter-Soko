using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerInteract : MonoBehaviour {

	public GameObject currentInterObj = null;
    public InteractionObject currentInterObjScript = null;

    public GameObject weapon;
    public GameObject player;
    public GameObject spell;

    // To possibly add: PlayerSpeed, Slow Time,
    //private string[] potionEffects = {"TimeBetweenShots", "DamageMultiplier"}; 
    // For TESTING new potion effects.
    private string[] potionEffects = {"TimeBetweenShots"};
    public bool activeEffect = false;

    AudioSource[] sounds;
    AudioSource potionPickup;

    void Start()
    {
    	sounds = GetComponents<AudioSource>();
    	potionPickup = sounds[1];
    }

	void Update ()
	{

		// Determines what player is picking up.
		if (currentInterObj)
		{
        	if(currentInterObjScript.heart)
        	{
        		HeartAdd();
        	}
        	else if(activeEffect == false)
        	{
        		// Play sound of player picking up potion.
        		potionPickup.Play();

	        	if(currentInterObjScript.potionBlue)
	        	{		
					PotionDuration(5f, "blue");
	        	} 
	        	else if(currentInterObjScript.potionRed)
	        	{
	        		PotionDuration(10f, "red");
	        	}
	        	else if(currentInterObjScript.potionGold)
	        	{
	        		PotionDuration(20f, "gold");
	        	}
        	}
        }	    
	}

	void OnTriggerEnter2D(Collider2D other)
	{
        if (other.CompareTag("InteractionObject"))
        {
            currentInterObj = other.gameObject;
            currentInterObjScript = currentInterObj.GetComponent<InteractionObject>();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("InteractionObject"))
        {
            if(other.gameObject == currentInterObj)
            {
                currentInterObj = null;
                currentInterObjScript = null;
            }
        }
    }

    private void PotionDuration(float waitTime, string potionColor)
    {
		// Choose a random effect.
		string effect = ChooseEffect();

		// Apply the chosen effect.
		StartCoroutine(ApplyEffect(effect, potionColor, waitTime));

		// Destroy object player picked up so player can't continually pick it up.
		Destroy(currentInterObj);

		currentInterObj = null;
		currentInterObjScript = null;
	}

	string ChooseEffect()
	{
		int random = Random.Range(0, potionEffects.Length);
		return potionEffects[random];
	}

	public bool getActiveEffect()
	{
		return activeEffect;
	}

	public void setActiveEffect(bool activeEffect)
	{
		this.activeEffect = activeEffect;
	}

	IEnumerator ApplyEffect(string effect, string potionColor, float waitTime)
	{

		Weapon weapon_ref = weapon.GetComponent<Weapon>();
		Spell spell_ref = spell.GetComponent<Spell>();

		// SPELL VELOCITY.
		int blueSpellVel = 10, redSpellVel = 20, goldSpellVel = 30;

		// TIME BETWEEN SHOTS.
		float blueTimeBtwShots = .25f, redTimeBtwShots = .35f, goldTimeBtwShots = .49f;

		// DAMAGE MULTIPLIERS.
		float blueDamageMult = 1.2f, redDamageMult = 1.5f, goldDamageMult = 2.0f;

		// Add effect.
		setActiveEffect(true);
		if(potionColor.Equals("blue"))
		{
			if(effect.Equals("SpellVelocity"))
			{
				weapon_ref.spellVel += blueSpellVel;
				yield return new WaitForSeconds(waitTime);
				weapon_ref.spellVel -= blueSpellVel;
			}
			else if(effect.Equals("TimeBetweenShots"))
			{
				weapon_ref.startTimeBtwShots -= blueTimeBtwShots;
				yield return new WaitForSeconds(waitTime);
				weapon_ref.startTimeBtwShots += blueTimeBtwShots;
			}
			else if(effect.Equals("DamageMultiplier"))
			{
				spell_ref.damageMultiplier = blueDamageMult;
				yield return new WaitForSeconds(waitTime);
				spell_ref.damageMultiplier = 1;
			}	
		}
		else if(potionColor.Equals("red"))
		{
			if(effect.Equals("SpellVelocity"))
			{
				weapon_ref.spellVel += redSpellVel;
				yield return new WaitForSeconds(waitTime);
				weapon_ref.spellVel -= redSpellVel;
			}
			else if(effect.Equals("TimeBetweenShots"))
			{
				weapon_ref.startTimeBtwShots -= redTimeBtwShots;
				yield return new WaitForSeconds(waitTime);
				weapon_ref.startTimeBtwShots += redTimeBtwShots;
			}
			else if(effect.Equals("DamageMultiplier"))
			{
				spell_ref.damageMultiplier = redDamageMult;
				yield return new WaitForSeconds(waitTime);
				spell_ref.damageMultiplier = 1;
			}
		} 
		else if(potionColor.Equals("gold"))
		{
			if(effect.Equals("SpellVelocity"))
			{
				weapon_ref.spellVel += goldSpellVel;
				yield return new WaitForSeconds(waitTime);
				weapon_ref.spellVel -= goldSpellVel;
			}
			else if(effect.Equals("TimeBetweenShots"))
			{
				weapon_ref.startTimeBtwShots -= goldTimeBtwShots;
				yield return new WaitForSeconds(waitTime);
				weapon_ref.startTimeBtwShots += goldTimeBtwShots;
			}
			else if(effect.Equals("DamageMultiplier"))
			{
				spell_ref.damageMultiplier = goldDamageMult;
				yield return new WaitForSeconds(waitTime);
				spell_ref.damageMultiplier = 1;
			}
		}
		setActiveEffect(false);
	}

	// Check if hearts are full and add a heart if necessary.
	public void HeartAdd()
	{
		if(player.GetComponent<PlayerHealth>().HealthCheck())
		{
			player.GetComponent<PlayerHealth>().AddHeart();
			Destroy(currentInterObj);
		}
	}
}

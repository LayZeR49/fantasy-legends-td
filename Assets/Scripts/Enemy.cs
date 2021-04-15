using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {

	public float startSpeed = 10f;

	[HideInInspector]
	public float speed;

	public float startHealth = 100;
	private float health;

	public int worth = 50;

	
	//public AudioSource deathSound;

	[Header("Unity Stuff")]
	public Image healthBar;
	public GameObject deathEffect;
	public AudioClip deathClip;
	public Animator animator;

	private bool isDead = false;

	void Start ()
	{
		speed = startSpeed;
		health = startHealth;
	}

	public void TakeDamage (float amount)
	{
		health -= amount;

		healthBar.fillAmount = health / startHealth;

		if (health <= 0 && !isDead)
		{
			Die();
		}
	}

	public void Slow (float pct)
	{
		speed = startSpeed * (1f - pct);
	}

	void Die ()
	{
		Vector3 soundPosition = new Vector3(transform.position.x, Camera.main.transform.position.y, transform.position.z);
		Debug.Log(soundPosition);
		AudioSource.PlayClipAtPoint(deathClip, soundPosition);

		isDead = true;

		PlayerStats.Money += worth;

		GameObject effect = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
		Destroy(effect, 5f);

		WaveSpawner.EnemiesAlive--;
		
		Destroy(gameObject);
	}


}

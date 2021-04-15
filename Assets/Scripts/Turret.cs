using UnityEngine;
using System.Collections;

public class Turret : MonoBehaviour {

	private Transform target;
	private Enemy targetEnemy;

	[Header("General")]

	public float range = 15f;

	[Header("Use Bullets (default)")]
	public GameObject bulletPrefab;
	public float fireRate = 1f;
	private float fireCountdown = 0f;

	[Header("Use Laser")]
	public bool useLaser = false;

	public int damageOverTime = 30;
	public float slowAmount = .5f;

	public LineRenderer lineRenderer;
	public ParticleSystem impactEffect;
	public Light impactLight;

	[Header("Unity Setup Fields")]

	public string enemyTag = "Enemy";
	public string towerName = "";
	public Animator animator;
	//public AudioClip fireClip;
	public AudioSource fireSound;

	public GameObject rangeCircle;
	public Transform firePoint;

	private bool LaserActive = false;
	private float angle = 0f;

	//public AudioSource buildSound;


	// Use this for initialization
	void Start () {
		InvokeRepeating("UpdateTarget", 0f, 0.5f);
		rangeCircle.transform.localScale = new Vector3(range/4, 0.0001f, range/4);
	}
	
	void UpdateTarget ()
	{
		GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
		float shortestDistance = Mathf.Infinity;
		GameObject nearestEnemy = null;
		foreach (GameObject enemy in enemies)
		{
			float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
			if (distanceToEnemy < shortestDistance)
			{
				shortestDistance = distanceToEnemy;
				nearestEnemy = enemy;
			}
		}

		if (nearestEnemy != null && shortestDistance <= range)
		{
			//if(useLaser && target != nearestEnemy.transform)
			//{
			//	LaserChange = true;
			//}
			target = nearestEnemy.transform;
			targetEnemy = nearestEnemy.GetComponent<Enemy>();

			animator.SetBool("Attack", true);
		} else
		{
			animator.SetBool("Attack", false);
			target = null;
		}

	}

	// Update is called once per frame
	void Update () {
		if (target == null)
		{
			fireCountdown -= Time.deltaTime;
			//animator.SetBool("Attack", false);
			if (useLaser)
			{
				if (lineRenderer.enabled)
				{
					LaserActive = false;
					fireSound.Stop();
					lineRenderer.enabled = false;
					impactEffect.Stop();
					impactLight.enabled = false;
				}
			}

			return;
		}

		//Move();
		//animator.SetBool("Attack", true);

		if(Time.deltaTime != 0f)
		{
		if (useLaser)
		{
			//if(LaserChange)
			//{
				angle = AngleBetweenVector2();
				PlayAnimation();
			//	LaserChange = false;
			//}
			if(!LaserActive)
			{
				LaserActive = true;
				fireSound.Play();
			}
			Laser();
		} else
		{
			if (fireCountdown <= 0f)
			{
				
				angle = AngleBetweenVector2();
				PlayAnimation();
				fireSound.Play();
				Shoot();
				//AnimatorClipInfo[] current = animator.GetCurrentAnimatorClipInfo(0);
				//string name = current[0].clip.name;

				fireCountdown = 1f / fireRate;
			}

			fireCountdown -= Time.deltaTime;
		}
		}

	}

	void Move()
	{
		//float angle = AngleBetweenVector2();

		animator.SetFloat("Angle", angle);

	}

	void PlayAnimation()
	{
		

		if(angle >= 135 || angle <= -135)
		{
			animator.Play("AttackLeft");
			if(useLaser)
			{
				firePoint = transform.Find("FirePoint").Find("Left");
			}
			return;
		}

		if(angle < 135 && angle > 45)
		{
			animator.Play("AttackBack");
			if(useLaser)
			{
				firePoint = transform.Find("FirePoint").Find("Back");
			}
			return;
		}

		if(angle <= 45 && angle >= -45)
		{
			animator.Play("AttackRight");
			if(useLaser)
			{
				firePoint = transform.Find("FirePoint").Find("Right");
			}
			return;
		}

		if(angle < -45 && angle > -135)
		{
			animator.Play("AttackFront");
			if(useLaser)
			{
				firePoint = transform.Find("FirePoint").Find("Front");
			}
			return;
		}
	}

	private float AngleBetweenVector2()
 	{
		Vector2 vec1 = new Vector2(transform.position.x, transform.position.z);
		Vector2 vec2 = new Vector2(target.position.x, target.position.z);
        Vector2 diference = vec2 - vec1;
        float sign = (vec2.y < vec1.y)? -1.0f : 1.0f;
        return Vector2.Angle(Vector2.right, diference) * sign;
    }

	void Laser ()
	{
		targetEnemy.TakeDamage(damageOverTime * Time.deltaTime);
		targetEnemy.Slow(slowAmount);

		if (!lineRenderer.enabled)
		{
			lineRenderer.enabled = true;
			impactEffect.Play();
			impactLight.enabled = true;
		}

		lineRenderer.SetPosition(0, firePoint.position);
		lineRenderer.SetPosition(1, target.position);

		Vector3 dir = firePoint.position - target.position;

		impactEffect.transform.position = target.position + dir.normalized;

		impactEffect.transform.rotation = Quaternion.LookRotation(dir);
	}

	void Shoot ()
	{
		Quaternion projectileRotation = Quaternion.Euler(90, 0, angle);

		GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, projectileRotation);
		Bullet bullet = bulletGO.GetComponent<Bullet>();

		if (bullet != null)
			bullet.Seek(target);
	}

	void OnDrawGizmosSelected ()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, range);
	}

}

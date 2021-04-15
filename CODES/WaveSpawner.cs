using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour {

	public static int EnemiesAlive = 0;

	public Wave[] waves;

	public Transform spawnPoint;

	public GameManager gameManager;
	public WaveControl waveControl;
	public AudioSource startWaveSound;
	public GameObject movingArrow;

	[HideInInspector]
	public int waveIndex = 0;

	//private bool paused = false;

	void Start()
	{
		EnemiesAlive = 0;
	}

	void Update ()
	{
		if ((Input.GetKeyDown(KeyCode.RightShift) || Input.GetKeyDown(KeyCode.LeftShift)) && PauseMenu.KeysEnabled)
		{
			waveControl.FastForward();
		}

		if (EnemiesAlive > 0)
		{
			if (Input.GetKeyDown(KeyCode.Space) && PauseMenu.KeysEnabled)
			{
				waveControl.Pause();
			}
			return;
		}
		waveControl.Reset();

		if (waveIndex == waves.Length)
		{
			
			gameManager.WinLevel();
			this.enabled = false;
		}

		if (Input.GetKey(KeyCode.Space) && PauseMenu.KeysEnabled)
		{
			waveControl.ChangeImage();

				movingArrow.SetActive(false);
				StartCoroutine(SpawnWave());
				return;
		}
	}

	IEnumerator SpawnWave ()
	{
		startWaveSound.Play();
		PlayerStats.Rounds++;

		Wave wave = waves[waveIndex];

		EnemiesAlive = wave.count;

		for (int i = 0; i < wave.count; i++)
		{
			SpawnEnemy(wave.enemy);
			yield return new WaitForSeconds(1f / wave.rate);
		}

		waveIndex++;
	}

	void SpawnEnemy (GameObject enemy)
	{
		Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
	}

	public void Click()
	{
		if (EnemiesAlive > 0)
		{
			waveControl.Pause();
			return;
		}


		if (waveIndex == waves.Length)
		{
			gameManager.WinLevel();
			this.enabled = false;
		}


		waveControl.ChangeImage();

			StartCoroutine(SpawnWave());
			return;

		
	}



}

using UnityEngine;
using TMPro;

public class WavesUI : MonoBehaviour
{
	public TextMeshProUGUI wavesText;
    public WaveSpawner waveSpawner;

	// Update is called once per frame
	void Update () {
		wavesText.text = PlayerStats.Rounds.ToString() + " / " + waveSpawner.waves.Length.ToString();
	}
}

using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour {

	public TurretBlueprint arrowTower;
	public TurretBlueprint waveTower;
	public TurretBlueprint iceTower;
	public TurretBlueprint spearTower;

	private BuildManager buildManager;
	private Image prevImage = null;
	private static Color blackeu;

	void Start ()
	{
		buildManager = BuildManager.instance;
		blackeu = new Color(0, 0, 0, 0.58431372549f);
	}

	public static void ResetColor(Image prevImage)
	{
		if(prevImage != null)
		{
			prevImage.color = blackeu;
		}
	}

	public void SelectArrowTower()
	{
		if(prevImage != null)
		{
			prevImage.color = blackeu;
		}
		Transform towerTransform = transform.Find("ArrowTowerItem");
		Image BGImage = towerTransform.Find("KeyBG").GetComponent<Image>();
		BGImage.color = Color.yellow;
		prevImage = BGImage;

		int value = buildManager.SelectTowerToBuild(arrowTower, prevImage);
		if(value == -1)
		{
			BGImage.color = blackeu;
		}
	}

	public void SelectWaveTower()
	{
		if(prevImage != null)
		{
			prevImage.color = blackeu;
		}
		Transform towerTransform = transform.Find("WaveTowerItem");
		Image BGImage = towerTransform.Find("KeyBG").GetComponent<Image>();
		BGImage.color = Color.yellow;
		prevImage = BGImage;

		int value = buildManager.SelectTowerToBuild(waveTower, prevImage);
		if(value == -1)
		{
			BGImage.color = blackeu;
		}
	}

	public void SelectIceTower()
	{
		if(prevImage != null)
		{
			prevImage.color = blackeu;
		}
		Transform towerTransform = transform.Find("IceTowerItem");
		Image BGImage = towerTransform.Find("KeyBG").GetComponent<Image>();
		BGImage.color = Color.yellow;
		prevImage = BGImage;

		int value = buildManager.SelectTowerToBuild(iceTower, prevImage);
		if(value == -1)
		{
			BGImage.color = blackeu;
		}
	}
	public void SelectSpearTower()
	{
		if(prevImage != null)
		{
			prevImage.color = blackeu;
		}
		Transform towerTransform = transform.Find("SpearTowerItem");
		Image BGImage = towerTransform.Find("KeyBG").GetComponent<Image>();
		BGImage.color = Color.yellow;
		prevImage = BGImage;
		
		int value = buildManager.SelectTowerToBuild(spearTower, prevImage);
		if(value == -1)
		{
			BGImage.color = blackeu;
		}
	}

	void Update()
	{
		if(PauseMenu.KeysEnabled)
		{
			if(Input.GetKeyDown(KeyCode.Keypad1) || Input.GetKeyDown(KeyCode.Alpha1))
			{
				SelectArrowTower();
			}
			if(Input.GetKeyDown(KeyCode.Keypad2) || Input.GetKeyDown(KeyCode.Alpha2))
			{
				SelectWaveTower();
			}
			if(Input.GetKeyDown(KeyCode.Keypad3) || Input.GetKeyDown(KeyCode.Alpha3))
			{
				SelectIceTower();
			}
			if(Input.GetKeyDown(KeyCode.Keypad4) || Input.GetKeyDown(KeyCode.Alpha4))
			{
				SelectSpearTower();
			}
		}
		

	}

}

using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour {

	public Color hoverColor;
	public Color notEnoughMoneyColor;
    public Vector3 positionOffset;
	private Vector3 rangeCircleOffset;

	[HideInInspector]
	public GameObject turret;
	[HideInInspector]
	public TurretBlueprint turretBlueprint;
	[HideInInspector]
	public bool isUpgraded = false;
	
	private Renderer rend;
	private Color startColor;
	private Vector3 uiOffset;

	BuildManager buildManager;

	void Start ()
	{
		rend = GetComponent<Renderer>();
		startColor = rend.material.color;
		rangeCircleOffset = new Vector3(0f, 0.7f, 0f);
		uiOffset = new Vector3(0, -0.5f, 0);
		buildManager = BuildManager.instance;
    }

	public Vector3 GetBuildPosition ()
	{
		return transform.position + positionOffset;
	}

	public Vector3 GetUIPosition()
	{
		return transform.position + uiOffset;
	}

	void OnMouseDown ()
	{
		if (EventSystem.current.IsPointerOverGameObject())
			return;

		if (turret != null)
		{
			if(buildManager.tower.activeSelf)
			return;

			turret.GetComponent<Turret>().rangeCircle.SetActive(!turret.GetComponent<Turret>().rangeCircle.activeSelf);
			buildManager.SelectNode(this);
			return;
		}

		if (!buildManager.CanBuild)
			return;

		BuildTurret(buildManager.GetTurretToBuild());
		buildManager.SetTurretToBuild(null);
		
	}

	void BuildTurret (TurretBlueprint blueprint)
	{
		buildManager.tower.SetActive(false);
		rend.material.color = startColor;
		if (PlayerStats.Money < blueprint.cost)
		{
			StartCoroutine(buildManager.NotEnoughMoney());
			return;
		}

		

		PlayerStats.Money -= blueprint.cost;

		buildManager.buildClip.Play();
		Quaternion towerRotation = Quaternion.Euler(90, 0 , 0);
		GameObject _turret = (GameObject)Instantiate(blueprint.prefab, GetBuildPosition(), towerRotation);
		turret = _turret;
		
		
		turretBlueprint = blueprint;

		GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
		Destroy(effect, 5f);

		

	}

	public void UpgradeTurret ()
	{
		if (PlayerStats.Money < turretBlueprint.upgradeCost)
		{
			StartCoroutine(buildManager.NotEnoughMoney());
			return;
		}

		PlayerStats.Money -= turretBlueprint.upgradeCost;

		//Get rid of the old turret
		Destroy(turret);

		Quaternion towerRotation = Quaternion.Euler(90, 0 , 0);

		//Build a new one
		GameObject _turret = (GameObject)Instantiate(turretBlueprint.upgradedPrefab, GetBuildPosition(), towerRotation);
		turret = _turret;

		GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
		Destroy(effect, 5f);

		isUpgraded = true;

	}

	public void SellTurret ()
	{
		PlayerStats.Money += turretBlueprint.GetSellAmount();

		GameObject effect = (GameObject)Instantiate(buildManager.sellEffect, GetBuildPosition(), Quaternion.identity);
		Destroy(effect, 5f);

		Destroy(turret);
		turretBlueprint = null;
	}
/*
	void OnMouseEnter ()
	{
		if (EventSystem.current.IsPointerOverGameObject())
			return;

		if (!buildManager.CanBuild)
			return;

		if (turret != null)
		{
			return;
		}


		buildManager.onNode = true;
		buildManager.tower.transform.position = transform.position + rangeCircleOffset;
		buildManager.tower.SetActive(true);
		
		if (buildManager.HasMoney)
		{
			rend.material.color = hoverColor;
		} else
		{
			rend.material.color = notEnoughMoneyColor;
		}

	}
*/

	public bool PlaceOnNode()
	{
		if (turret != null)
		{
			return false;
		}

		buildManager.tower.transform.position = transform.position + rangeCircleOffset;
		
		if (buildManager.HasMoney)
		{
			rend.material.color = hoverColor;
		} else
		{
			rend.material.color = notEnoughMoneyColor;
		}

		return true;
	}

	void OnMouseExit ()
	{
		//buildManager.tower.SetActive(false);
		rend.material.color = startColor;
    }

}

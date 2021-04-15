using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BuildManager : MonoBehaviour {


	public static BuildManager instance;

	void Awake ()
	{
		if (instance != null)
		{
			Debug.LogError("More than one BuildManager in scene!");
			return;
		}
		instance = this;
	}

	public GameObject NotEnoughMoneyUI;
	public GameObject buildEffect;
	public GameObject sellEffect;

	private TurretBlueprint turretToBuild;
	private Node selectedNode;
	private Node previousNode;

	public NodeUI nodeUI;
	private Image tempImage;
	public AudioSource buildClip;
	public GameObject tower;
	private SpriteRenderer towerSpriteRenderer;
	private TowerGhost ghost;
	private Renderer ghostRend;
	//private Vector3 rangeCircleOffset;
	private Node pointNode;

	public bool CanBuild { get { return turretToBuild != null; } }
	public bool HasMoney { get { return PlayerStats.Money >= turretToBuild.cost; } }
	
	void Start()
	{
		towerSpriteRenderer = tower.GetComponent<SpriteRenderer>();
		ghost = tower.GetComponent<TowerGhost>();
		ghostRend = ghost.rangeCircle.GetComponent<Renderer>();
		//rangeCircleOffset = new Vector3(0f, 0.15f, 0f);
	}

	void Update()
	{
		if(tower.activeSelf)
		{
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out hit))
			{
				pointNode = hit.transform.GetComponent<Node>();
				if(pointNode != null)
				{
					ghostRend.material.color = new Color(0, 0, 1, 0.039f);
					if(!pointNode.PlaceOnNode())
					{
						Vector3 towerPlace = new Vector3(hit.point.x , 0.7f, hit.point.z);
						tower.transform.position = towerPlace;
						ghostRend.material.color = new Color(1, 0, 0, 0.25f);
					}
				}
				else
				{
					Vector3 towerPlace = new Vector3(hit.point.x , 0.7f, hit.point.z);
					tower.transform.position = towerPlace;
					ghostRend.material.color = new Color(1, 0, 0, 0.25f);
				}
				
			}
		}
	}



	public void SelectNode (Node node)
	{
		if (selectedNode == node)
		{
			DeselectNode();
			return;
		}
		previousNode = selectedNode;
		selectedNode = node;
		turretToBuild = null;

		if(previousNode != null)
		{
			previousNode.turret.GetComponent<Turret>().rangeCircle.SetActive(false);
		}

		nodeUI.SetTarget(node);
	}


	public void DeselectNode()
	{
		selectedNode = null;
		nodeUI.Hide();
	}

	public int SelectTowerToBuild (TurretBlueprint turret, Image prevImage = null)
	{
		int returnValue = 0;
		if(turretToBuild == turret)
		{
			tower.SetActive(false);
			turretToBuild = null;
			//towerChange = true;
			returnValue = -1;
		}
		else
		{
			turretToBuild = turret;

			tower.SetActive(true);
			//towerChange = false;
			changeTower();
			returnValue = 1;
		}
		if(selectedNode != null)
		{
			selectedNode.turret.GetComponent<Turret>().rangeCircle.SetActive(false);
		}
		DeselectNode();

		if(prevImage != null)
		{
			tempImage = prevImage;
		}

		return returnValue;
	}

	public TurretBlueprint GetTurretToBuild ()
	{
		return turretToBuild;
	}

	public void SetTurretToBuild (TurretBlueprint x)
	{
		if(x == null)
		{
			Shop.ResetColor(tempImage);
		}
		turretToBuild = x;
	}

	public IEnumerator NotEnoughMoney()
	{
		NotEnoughMoneyUI.SetActive(!NotEnoughMoneyUI.activeSelf);
		yield return new WaitForSecondsRealtime(1f);
		NotEnoughMoneyUI.SetActive(!NotEnoughMoneyUI.activeSelf);
	}

	public void changeTower()
	{
		float range = turretToBuild.prefab.GetComponent<Turret>().range;
		string name = turretToBuild.prefab.GetComponent<Turret>().name;

		if(name == "Archer")
		{
			towerSpriteRenderer.sprite = ghost.archer;
		}
		else if(name == "Red")
		{
			towerSpriteRenderer.sprite = ghost.red;
		}
		else if(name == "Wizard")
		{
			towerSpriteRenderer.sprite = ghost.wizard;
		}
		else if(name == "Spear")
		{
			towerSpriteRenderer.sprite = ghost.spear;
		}
		
		ghost.rangeCircle.transform.localScale = new Vector3(range/4, 0.0001f, range/4);
		
	}


}

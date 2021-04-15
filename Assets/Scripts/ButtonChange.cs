
using UnityEngine;
using UnityEngine.UI;

public class ButtonChange : MonoBehaviour
{
    public Sprite normal;
    public Sprite hold;
    public Image image;

    // Start is called before the first frame update
    void Start()
    {
        if(image.sprite != normal)
            image.sprite = normal;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out hit))
			{
                Debug.Log(hit.transform + " " + hit.point);
            }
    }

    void OnMouseDrag()
    {

        image.sprite = hold;
    }
}

using UnityEngine;
using System.Collections;

public class BuildingColorController : MonoBehaviour {

    public Color topColor = new Color32(52, 52, 52, 255);
    public Color bottomColor = new Color32(29, 29, 29, 255);
	public bool calculateValuesFromCameraScript = true;

	private GradientBackground backgroundScript;

	void Start () {
		if (calculateValuesFromCameraScript) 
		{
			backgroundScript = Camera.main.GetComponent<GradientBackground> ();
			this.topColor = backgroundScript.topColorGround;
			this.bottomColor = backgroundScript.bottomColorGround;
		}
		calculateLayerDepth ();
	}

	void FixedUpdate()
	{
		if (calculateValuesFromCameraScript) {
			this.topColor = backgroundScript.topColorGround;
			this.bottomColor = backgroundScript.bottomColorGround;
		}

		Camera mainCam = Camera.main;
		var objectInCameraPosition = mainCam.WorldToScreenPoint (gameObject.transform.position);

		float norm = ((float) objectInCameraPosition.y / (float) Screen.height) * 2f; //magical constant set to 2 because gradient goes to 50% of the screen

		SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
		spriteRenderer.color = Color.Lerp(bottomColor, topColor, norm);
	}

	private void calculateLayerDepth()
	{
		var spriteRenderer = GetComponent<SpriteRenderer> ();

		float zCoord = transform.position.z;
		zCoord *= -1;
		spriteRenderer.sortingOrder = Mathf.FloorToInt (zCoord);
	}
}

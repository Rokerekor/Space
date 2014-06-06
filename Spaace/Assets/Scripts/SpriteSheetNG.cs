using UnityEngine;
using System.Collections;

public class SpriteSheetNG : MonoBehaviour
{
	public int uvTieX = 1;
	public int uvTieY = 1;
	public float speed = 1;
	public bool loop = true;
	
	public Material[] materials;
	
	private Vector2 size;
	private Renderer myRenderer;
	private int _displayedIndex = -1;
	private float desiredIndex = 0;
	private float iX=0;
	private float iY=1;
	
	private float maxIndex {
		get {
			return uvTieX*uvTieY-1f;
		}
	}
	
	public bool finishedPlaying {
		get {
			return !loop && desiredIndex == maxIndex;
		}
	}

	void Start ()
	{
		size = new Vector2(1.0f / uvTieX , 1.0f / uvTieY);
		myRenderer = renderer;

		if (myRenderer == null || materials == null || materials.Length < 1)
			enabled = false;
		
		foreach (Material material in materials)
			material.SetTextureScale("_MainTex", size);
		
		SetActiveTexture(0);
	}
	
	public void SetActiveTexture(int index) {
		myRenderer.material = materials[index];
		desiredIndex = 0;
		iX = 0;
		iY = 1;
	}

	void Update()
	{
		desiredIndex += Time.deltaTime * speed;
		if (loop)
			desiredIndex %= uvTieX*uvTieY;
		else
			desiredIndex = Mathf.Min(desiredIndex, maxIndex);
		int index = (int)desiredIndex;
		
		if (index != _displayedIndex)
		{
			Vector2 offset = new Vector2(iX*size.x,
										 1-(size.y*iY));
			iX++;
			if(iX / uvTieX == 1)
			{
				if(uvTieY!=1)	iY++;
				iX=0;
				if(iY / uvTieY == 1)
				{
					iY=1;
				}
			}

			myRenderer.material.SetTextureOffset ("_MainTex", offset);

			_displayedIndex = index;
		}
	}
}
using UnityEngine;
using System.Collections;


/*
 * CONTEXT: This script belongs to the object whose pixels is getting changed.
 * SUBSTANCE: 
 * 	Texture2D newTexture, int width, int height, , GetComponent<Renderer>().material.mainTexture, SetPixel()
 * FORM: Start(), DrawPixel(), RedrawSprite()
 * PATH: 
 * 	On start, initialize newTexture with height and width.
 * 	Point it to this object's mainTexture.
 * 	When the program calls DrawPixel, change the texture with SetPixel()
 * 	When the program calls RedrawSprite, redraw the changes onto the texture.

 */
public class PixelChanger : MonoBehaviour {

	private Texture2D newTexture;
	public int width;
	public int height;
	int growth;

	Vector3 newScale;

	// Use this for initialization
	void Start () {
		newTexture = new Texture2D (width, height);
		gameObject.GetComponent<Renderer> ().material.mainTexture = newTexture;
		growth = 0;

		ResetCanvas ();
		RedrawSprite();

//		Implementation of the below code to use with a 2d sprite is involved. For now, work with a 3d game object whose texture is already available to work with through its material
//			I can either work with shaders as with CopyTexture2D and ApplyNewTexture
//			Or work with making the 2dsprite's texture be accessible
//		//this sprite.texture is not accessible
//		gameObject.GetComponent<SpriteRenderer> ().sprite.texture = newTexture;
//		//so instead let's copy it, change stuff in the copy, and then paste over this object's renderer via Sprite.Create;

	}

	public void ResetCanvas(){
		for (int i=0; i<width; i++) {
			for(int j=0; j<height; j++){
				DrawPixel(i,j, Color.black);
				
			}
		}
	}

	//uses x and y coordinates as passed by program and applies the passed color to it.
	//doesn't change pixels in real time
	public void DrawPixel(int x, int y, Color color){

//		newTexture.SetPixel (x + 1, y + 1, Color.black);
		newTexture.SetPixel (x, y, color);
//		newTexture.SetPixel (x - 1, y - 1, Color.black);

	}

	//since Apply() is expensive, we'll only draw on certain intervals as per the program
	public void RedrawSprite(){
//		PulsateObject ();
		newTexture.Apply ();
	}

	public void PulsateObject(){
		newScale = transform.localScale;
		newScale.x = Mathf.Lerp(newScale.x, newScale.x * (float)1.1, Time.deltaTime * 300);
		newScale.z = Mathf.Lerp(newScale.z, newScale.z * (float)1.1, Time.deltaTime * 300);
		transform.localScale = newScale;
		growth++;
		if (growth > 2) {
			newScale.x = Mathf.Lerp(newScale.x, newScale.x * (float)(1/1.1), Time.deltaTime * 300);
			newScale.z = Mathf.Lerp(newScale.z, newScale.z * (float)(1/1.1), Time.deltaTime * 300);
			transform.localScale = newScale; 
			growth=0;
		}

	}

	//Copies the passed onto texture and outputs another texture that can be modified
//	public Texture2D CopyTexture2D(Texture2D copiedTexture){
//		Texture2D texture = new Texture2D (copiedTexture.width, copiedTexture.height);
//		texture.filterMode = FilterMode.Point;
//		texture.wrapMode = TextureWrapMode.Clamp;
//
//		int h = 0;
//		while (h<texture.height) {
//			int w = 0;
//			while(w <texture.width){
//				texture.SetPixel (h,w,copiedTexture.GetPixel(h,w));
//				++w;
//			}
//			++h;
//		}
//		texture.Apply ();
//
//		return texture;
//	}
//
//
//	public void ApplyNewTexture(){
//
//		newTexture = CopyTexture2D (GetComponent<SpriteRenderer> ().sprite.texture);
//		SpriteRenderer sr = GetComponent<SpriteRenderer> ();
//		sr.sprite = Sprite.Create (newTexture, sr.sprite.rect);
//		sr.material.mainTexture = newTexture;
//		sr.material.shader = Shader.Find ("Sprites/Default");
//
//	}
}

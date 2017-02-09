using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GetTextureBackUp : MonoBehaviour 
{
    public float raycastTime = 2f;
	public int materialIndex = 0;
	public LayerMask colorMask;
    public Transform[] rayPoint;
    public CompareColors compareColors;
	public Camera renderCam;

	private int indexNumber = 1;
    private Color currentColor;
    private float nextRayCastTime = 0;
    private Vector3 renderCamPos;

    private Camera RenderTextureCamera;
	[Space(20)]
	public GameObject Region_Capture;

	void Start ()
    {
        RenderTextureCamera = Region_Capture.GetComponentInChildren<Camera>();
      

		GetComponent<SkinnedMeshRenderer>().materials[materialIndex].EnableKeyword("_MainTex");

        if (RenderTextureCamera.targetTexture)
			GetComponent<SkinnedMeshRenderer>().materials[materialIndex].SetTexture("_MainTex", RenderTextureCamera.targetTexture);

	    else StartCoroutine(WaitForTexture());


		renderCamPos = renderCam.transform.position;
	}

    void Update()
    {
        if (Time.time > nextRayCastTime)
        {
            nextRayCastTime = Time.time + raycastTime;

            RaycastHit hit;

            if(indexNumber > rayPoint.Length)
            {
                indexNumber = 1;
            }
            Ray ray = new Ray(renderCamPos, (rayPoint[indexNumber - 1].position - renderCamPos).normalized);
			if (Physics.Raycast(ray, out hit, Mathf.Infinity, colorMask))
            {
                indexNumber++;
			  
                //Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.red);
                IEnumerator inst = null; 
                inst = GetTextureCoords(hit);
                StopCoroutine(inst);
                StartCoroutine(inst);

            }
        }
    }

    
    
    IEnumerator GetTextureCoords(RaycastHit hit)
    {
        yield return new WaitForEndOfFrame();

        //Creates a new RenderTexture to use it to store the pixel data of the current camera view
		RenderTexture prevRender = RenderTextureCamera.targetTexture;
        RenderTexture.active = prevRender;

        //Creates a new Texture then applies the pixels rendered on the screen to that Texture
		Texture2D tex2D = new Texture2D(RenderTextureCamera.targetTexture.width, RenderTextureCamera.targetTexture.height, TextureFormat.RGB24, false);
		tex2D.ReadPixels(new Rect(0, 0, RenderTextureCamera.targetTexture.width, RenderTextureCamera.targetTexture.height), 0, 0);        
		RenderTexture.active = null;
        tex2D.Apply();
      
        
        //Get the UV Coordinates of the texture at the hit point
        Vector2 pixelUV = hit.textureCoord;
       


        //Gets the Pixel index of the current uv hit point
//        int x = Mathf.FloorToInt(pixelUV.x * tex2D.width);
//        int y = Mathf.FloorToInt(pixelUV.y * tex2D.height);
        //Debug.Log("Pixel Index: " + x + ", " + y );


        //Loops through a rectangle area of size 0.05f squared
       
        
        for (float ux = -0.02f; ux <= 0.02f; ux+= 0.01f)
        {
            for (float uy = -0.02f; uy <= 0.02; uy+= 0.01f)
            {              
                currentColor += tex2D.GetPixelBilinear(pixelUV.x + ux, pixelUV.y + uy);
            }

        }


        currentColor = currentColor / 20;
        
        //Debug.Log("Average Color: " + currentColor);
    
		compareColors.Compare(currentColor);

    }
     
    private IEnumerator WaitForTexture() 
    {

		yield return new WaitForSeconds (0.1f);

		if (RenderTextureCamera.targetTexture)
        {
			GetComponent<SkinnedMeshRenderer>().materials[materialIndex].SetTexture("_MainTex", RenderTextureCamera.targetTexture);
        }
		else StartCoroutine(WaitForTexture());

	}

}
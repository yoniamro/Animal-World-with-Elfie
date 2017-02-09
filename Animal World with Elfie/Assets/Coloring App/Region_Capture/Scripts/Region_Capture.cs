using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Vuforia;

public class Region_Capture : MonoBehaviour {

	private Camera Child_AR_Camera;
	private GameObject AR_Camera_Vector;
	[Space(10)]
	public GameObject ARCamera;
	public GameObject ImageTarget;
	public GameObject BackgroundPlane;
	[Space(20)]
	public bool AutoRegionSize = true;
	public bool HideFromARCamera = true;
	public bool CheckMarkerPosition = false;

	[Space(20)]
	public bool ColorDebugMode = false;

	[HideInInspector]
	public bool MarkerIsOUT, MarkerIsIN;

	private bool Is_Child_ImageTarget;
	public static Texture2D VideoBackgroundTexure;
	public static float CPH,CPW;
	public static bool Vector_Is_Created = false;

	void Start() {

		AR_Camera_Vector = GameObject.Find ("AR Camera Vector");

		if (AR_Camera_Vector == null) {
			AR_Camera_Vector = new GameObject ("AR Camera Vector");
			Vector_Is_Created = true;
		}

		if (GetComponentInParent<ImageTargetBehaviour>()) Is_Child_ImageTarget = true;

		if (ARCamera == null || ImageTarget == null || BackgroundPlane == null) {
			Debug.LogWarning("ARCamera, ImageTarget or BackgroundPlane not assigned!");
			this.enabled = false;
		} 
		else {

			if (AutoRegionSize) {

				if (Is_Child_ImageTarget) {
					
					transform.localPosition = Vector3.zero;
					transform.localEulerAngles = Vector3.zero;

					if (ImageTarget.GetComponent<ImageTargetBehaviour> ().GetSize ().x > ImageTarget.GetComponent<ImageTargetBehaviour> ().GetSize ().y) 
						transform.localScale = new Vector3 (0.1f, 0.1f, ImageTarget.GetComponent<ImageTargetBehaviour> ().GetSize ().y / ImageTarget.GetComponent<ImageTargetBehaviour> ().GetSize ().x * 0.1f);
					else transform.localScale = new Vector3 (ImageTarget.GetComponent<ImageTargetBehaviour> ().GetSize ().x / ImageTarget.GetComponent<ImageTargetBehaviour> ().GetSize ().y * 0.1f, 0.1f, 0.1f);
				}

				else {
					transform.position = ImageTarget.transform.position;
					transform.localRotation = ImageTarget.transform.localRotation;
					transform.localScale = new Vector3 (ImageTarget.GetComponent<ImageTargetBehaviour> ().GetSize ().x, 10.0f, ImageTarget.GetComponent<ImageTargetBehaviour> ().GetSize ().y) / 10.0f;
				}
			}

			Child_AR_Camera = ARCamera.GetComponentInChildren<Camera>();
			gameObject.layer = 20;

			if (HideFromARCamera && !ColorDebugMode) Child_AR_Camera.cullingMask &= ~(1 << LayerMask.NameToLayer("Region_Capture"));

			if (ColorDebugMode) {
				GetComponent<Renderer> ().material.SetInt ("_KR", 0);
				GetComponent<Renderer> ().material.SetInt ("_KG", 1);
			}

			CPH = Child_AR_Camera.pixelHeight;
			CPW = Child_AR_Camera.pixelWidth;

			StartCoroutine(Start_Initialize());
			StartCoroutine(CheckVideoMode());
		}		
	}
	

	private void Initialize() {

		if (VuforiaRenderer.Instance.IsVideoBackgroundInfoAvailable ()) {

			VuforiaRenderer.VideoTextureInfo videoTextureInfo = VuforiaRenderer.Instance.GetVideoTextureInfo ();

			if (videoTextureInfo.imageSize.x == 0 || videoTextureInfo.imageSize.y == 0) goto End;

			float k_x = (float)videoTextureInfo.imageSize.x / (float)videoTextureInfo.textureSize.x * 0.5f;
			float k_y = (float)videoTextureInfo.imageSize.y / (float)videoTextureInfo.textureSize.y * 0.5f;

			GetComponent<Renderer> ().material.SetFloat ("_KX", k_x);
			GetComponent<Renderer> ().material.SetFloat ("_KY", k_y);

			VideoBackgroundTexure = VuforiaRenderer.Instance.VideoBackgroundTexture;

			if (!VideoBackgroundTexure || !BackgroundPlane.GetComponent<MeshFilter>()) goto End;

			GetComponent<Renderer>().material.SetTexture ("_MainTex", VideoBackgroundTexure);
			GetComponent<Renderer>().material.SetFloat ("_Alpha", 1);

			if (Vector_Is_Created) {

				Vector_Is_Created = false;
				AR_Camera_Vector.transform.parent = ARCamera.transform;
				AR_Camera_Vector.transform.localPosition = Vector3.zero;

				#if UNITY_EDITOR
				AR_Camera_Vector.transform.localPosition = new Vector3 (0.0f, ImageTarget.GetComponent<ImageTargetBehaviour> ().GetSize ().x / 240.0f, 0.0f);
				#endif

				AR_Camera_Vector.transform.localEulerAngles = new Vector3 (0.0f, 180.0f, 180.0f);

				AR_Camera_Vector.transform.localScale = new Vector3 (1.0f, 1.0f, 1.0f);

				#if !UNITY_EDITOR
				if (Screen.orientation == ScreenOrientation.Portrait || Screen.orientation == ScreenOrientation.PortraitUpsideDown) AR_Camera_Vector.transform.localScale = new Vector3 ((float)CPW/(float)CPH, (float)CPH/(float)CPW, 1.0f);
				#endif
			}

		End:
			if (videoTextureInfo.imageSize.x == 0 || videoTextureInfo.imageSize.y == 0 || !VideoBackgroundTexure || !BackgroundPlane.GetComponent<MeshFilter> ()) 
			{
				Vector_Is_Created = true;
				StartCoroutine (Start_Initialize ());
			}

		}
		else {
			Vector_Is_Created = true;
			StartCoroutine(Start_Initialize());
		}

}

	void LateUpdate() {   

		if (AutoRegionSize && !Is_Child_ImageTarget) {
			transform.position = ImageTarget.transform.position;
			transform.localRotation = ImageTarget.transform.localRotation;
		}

		Matrix4x4 M = transform.localToWorldMatrix;
		Matrix4x4 V = AR_Camera_Vector.transform.worldToLocalMatrix;
		Matrix4x4 P = Vuforia.VuforiaUnity.GetProjectionGL (0,0,0);

		GetComponent<Renderer>().material.SetMatrix("_MATRIX_MVP", P * V * M);

		if (CheckMarkerPosition || ColorDebugMode) {
			Vector3 boundPoint1 = GetComponent<Renderer> ().bounds.min;
			Vector3 boundPoint2 = GetComponent<Renderer> ().bounds.max;
			Vector3 boundPoint3 = new Vector3 (boundPoint1.x, boundPoint1.y, boundPoint2.z);
			Vector3 boundPoint4 = new Vector3 (boundPoint2.x, boundPoint1.y, boundPoint1.z);

			Vector3 screenPos1 = Child_AR_Camera.WorldToScreenPoint (boundPoint1);
			Vector3 screenPos2 = Child_AR_Camera.WorldToScreenPoint (boundPoint2);
			Vector3 screenPos3 = Child_AR_Camera.WorldToScreenPoint (boundPoint3);
			Vector3 screenPos4 = Child_AR_Camera.WorldToScreenPoint (boundPoint4);

			if (screenPos1.x < 0 || screenPos1.y < 0 || screenPos2.x < 0 || screenPos2.y < 0 || screenPos3.x < 0 || screenPos3.y < 0 || screenPos4.x < 0 || screenPos4.y < 0 || screenPos1.x > CPW || screenPos1.y > CPH || screenPos2.x > CPW || screenPos2.y > CPH || screenPos3.x > CPW || screenPos3.y > CPH || screenPos4.x > CPW || screenPos4.y > CPH) {

				if (!MarkerIsOUT) {
					
					StartCoroutine(Start_MarkerOutOfBounds());

					MarkerIsOUT = true;
					MarkerIsIN = false;
				}
			}
			else { 
				if (!MarkerIsIN) {
					
					StartCoroutine(Start_MarkerIsReturned());

					MarkerIsIN = true;
				}
				MarkerIsOUT = false; }
		}
	}
		
	private void MarkerOutOfBounds() {

		// Add action here if marker out of bounds

			Debug.Log ("Marker out of bounds!");

		if (ColorDebugMode) {
			GetComponent<Renderer> ().material.SetInt ("_KR", 1);
			GetComponent<Renderer> ().material.SetInt ("_KG", 0);
		}

	}

	private void MarkerIsReturned() {

		// Add action here if marker is visible again
		
		 Debug.Log ("Marker is returned!");

		if (ColorDebugMode) {
			GetComponent<Renderer> ().material.SetInt ("_KR", 0);
			GetComponent<Renderer> ().material.SetInt ("_KG", 1);
		}
		
	}

	private IEnumerator Start_Initialize()
	{
		yield return StartCoroutine(Wait_Frame());
		Initialize();
	}


	private IEnumerator CheckVideoMode() {

		yield return StartCoroutine(Wait_Second(1.0f));

		if (CPH != Child_AR_Camera.pixelHeight || CPW != Child_AR_Camera.pixelWidth) {

			CPH = Child_AR_Camera.pixelHeight;
			CPW = Child_AR_Camera.pixelWidth;
			Vector_Is_Created = true;
			StartCoroutine(Start_Initialize());
			if (GetComponent<RenderTextureCamera>() && GetComponent<RenderTextureCamera>().enabled) GetComponent<RenderTextureCamera>().RecalculateTextureSize();
		}

		StartCoroutine(CheckVideoMode());
	}


	private IEnumerator Start_MarkerOutOfBounds()
	{
		yield return StartCoroutine(Wait_Frame());
		MarkerOutOfBounds();
	}

	private IEnumerator Start_MarkerIsReturned()
	{
		yield return StartCoroutine(Wait_Frame());
		MarkerIsReturned();
	}

	private IEnumerator Wait_Second(float seconds) {
		yield return new WaitForSeconds(seconds);
	}

	private IEnumerator Wait_Frame() {
		yield return new WaitForEndOfFrame();

	}

	public void RecalculateRegionSize() {
		transform.position = ImageTarget.transform.position;
		transform.localRotation = ImageTarget.transform.localRotation;
		transform.localScale = new Vector3(ImageTarget.GetComponent<ImageTargetBehaviour>().GetSize().x, 10.0f, ImageTarget.GetComponent<ImageTargetBehaviour>().GetSize().y) / 10.0f;
	}
}
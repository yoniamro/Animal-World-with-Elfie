using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using Vuforia;

public class ColoringTrackingBehavior : MonoBehaviour, ITrackableEventHandler
{
    public ParticleSystem particleEffect;
    [Space(10)]
    public GameObject child;
    [Space(10)]
    public GameObject regionCapture;
    [Space(10)]
    public ObjectMovement objectMovement;

    private TrackableBehaviour mTrackableBehaviour;
    private bool playedAnimalParticleOnce = false;
    private bool tracked = false;
    private bool showedObject = false;
    private float animalShowUpTimer = 0f;


    private Vector3 modelStartPos;

    void Start()
    {
        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
        {
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
        }

        if (child != null)
        {
            child.SetActive(false);
        }

        if (regionCapture != null)
        {
            regionCapture.SetActive(false);
        }

        if (objectMovement != null)
        {
            modelStartPos = objectMovement.agent.transform.localPosition;
        }
    }

    public void OnTrackableStateChanged(
                                       TrackableBehaviour.Status previousStatus,
                                       TrackableBehaviour.Status newStatus)
    {
        if (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED ||
            newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {

            if (!playedAnimalParticleOnce)
            {
                if (particleEffect != null)
                {
                    if (transform.childCount > 0)
                    {
                        GameObject particle = Instantiate(particleEffect.gameObject, transform.GetChild(0).position, particleEffect.transform.rotation) as GameObject;
                        particle.transform.parent = gameObject.transform;
                        particle.transform.localPosition = Vector3.zero;
                        particle.transform.localRotation = Quaternion.Euler(90, 0, 0);
                        Destroy(particle, particleEffect.startLifetime);
                    }
                }
                playedAnimalParticleOnce = true;
            }
           
            tracked = true;
        }
        else
        {
            tracked = false;
            showedObject = false;
            playedAnimalParticleOnce = false;
            animalShowUpTimer = 0;
            //OnTrackingLost();

            if (objectMovement != null)
            {
                objectMovement.tracked = false;
                objectMovement.StopMovement();
                objectMovement.agent.transform.localPosition = modelStartPos;
                objectMovement.enabled = false;
            }

            if (child != null)
            { 
                child.SetActive(false); 
            }

            if(regionCapture != null)
            {
                regionCapture.SetActive(false);
            }
        }
    }

    void Update()
    {

        if (tracked)
        {
            animalShowUpTimer += Time.deltaTime;

            if (particleEffect != null)
            {
                if (animalShowUpTimer > particleEffect.startLifetime && !showedObject)
                {
                    //OnTrackingFound();
                    if (regionCapture != null)
                    {
                        regionCapture.SetActive(true);
                    }

                    if (child != null)
                    {
                        child.SetActive(true);
                    }

                    if (objectMovement != null)
                    {
                        objectMovement.enabled = true;
                        objectMovement.tracked = true;
                    }

                    showedObject = true;

                    //if (Input.touchCount > 0)
                    //{
                    //    Touch touch = Input.GetTouch(0);

                    //    if (touch.phase == TouchPhase.Began)
                    //    {
                            
                    //        RaycastHit hit;
                    //        Ray ray = Camera.main.ScreenPointToRay(touch.position);

                    //        //if (touch.tapCount == 1)
                    //        //{

                    //        if (Physics.Raycast(ray, out hit, 1 << 31))
                    //        {
                    //            child.GetComponent<Animator>().SetTrigger("Sound");
                    //        }
                    //        //}

                    //    }
                    //}
                }
            }
            else
            {
                if (!showedObject)
                {
                    //OnTrackingFound();
                    if (regionCapture != null)
                    {
                        regionCapture.SetActive(true);
                    }

                    if(child != null)
                    {
                        child.SetActive(true);
                    }

                    if (objectMovement != null)
                    {
                        objectMovement.enabled = true;
                        objectMovement.tracked = true;
                    }

                    showedObject = true;

                    //if (Input.touchCount > 0)
                    //{
                    //    Touch touch = Input.GetTouch(0);

                    //    if (touch.phase == TouchPhase.Began)
                    //    {
                    //        RaycastHit hit;
                    //        Ray ray = Camera.main.ScreenPointToRay(touch.position);

                    //        //if (touch.tapCount == 1)
                    //        //{

                    //        if (Physics.Raycast(ray, out hit, 1 << 31))
                    //        {
                    //            child.GetComponent<Animator>().SetTrigger("Sound");
                    //        }
                    //        //}

                    //    }
                    //}
                }
            }
        }

    }

    private void OnTrackingFound()
    {

        Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);
        Collider[] colliderComponents = GetComponentsInChildren<Collider>(true);

        // Enable rendering:
        foreach (Renderer component in rendererComponents)
        {
            component.enabled = true;
        }
       
        // Enable colliders:
        foreach (Collider component in colliderComponents)
        {
            component.enabled = true;
        }
        



        //Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " found");
    }


    private void OnTrackingLost()
    {
        
        Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);
        Collider[] colliderComponents = GetComponentsInChildren<Collider>(true);

        // Enable rendering:
        foreach (Renderer component in rendererComponents)
        {
            component.enabled = false;
        }
        

        // Enable colliders:
        foreach (Collider component in colliderComponents)
        {
            component.enabled = false;
        }
       
        //Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " lost");
    }

}

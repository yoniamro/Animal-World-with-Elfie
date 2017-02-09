using UnityEngine;
using System.Collections;
using Vuforia;

public class MazeBigMarkerHandler : MonoBehaviour, ITrackableEventHandler
{
    public ParticleSystem particleEffect;
    [Space(10)]
    public GameObject childObject;

    private bool playedAnimalParticleOnce = false;
    private bool tracked = false;
    private bool showedObject = false;
    private float objectShowUpTimer = 0f;


    private TrackableBehaviour mTrackableBehaviour;

    void Start ()
    {
        mTrackableBehaviour = GetComponent<TrackableBehaviour>();

        if (mTrackableBehaviour)
        {
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
        }
    }

    void Update()
    {

        if (tracked)
        {
            objectShowUpTimer += Time.deltaTime;

            if (particleEffect != null)
            {
                if (objectShowUpTimer > particleEffect.main.startLifetime.constant && !showedObject)
                {
                    //OnTrackingFound();

                    if (childObject != null)
                    {
                        childObject.SetActive(true);
                    }

                    showedObject = true;
                }
            }
            else
            {
                if (!showedObject)
                {
                    //OnTrackingFound();

                    if (childObject != null)
                    {
                        childObject.SetActive(true);
                    }

                    showedObject = true;
                }
            }

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
                        Destroy(particle, particleEffect.main.startLifetime.constant);
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
            objectShowUpTimer = 0;
            
            //OnTrackingLost();

            if (childObject != null)
            {
                childObject.SetActive(false);
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
    }
}

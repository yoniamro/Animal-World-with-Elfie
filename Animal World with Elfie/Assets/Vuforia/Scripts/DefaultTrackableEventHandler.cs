/*==============================================================================
Copyright (c) 2010-2014 Qualcomm Connected Experiences, Inc.
All Rights Reserved.
Confidential and Proprietary - Qualcomm Connected Experiences, Inc.
==============================================================================*/

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace Vuforia
{
    /// <summary>
    /// A custom handler that implements the ITrackableEventHandler interface.
    /// </summary>
    public class DefaultTrackableEventHandler : MonoBehaviour,
                                                ITrackableEventHandler
    {
        #region PRIVATE_MEMBER_VARIABLES

        private TrackableBehaviour mTrackableBehaviour;

        #endregion // PRIVATE_MEMBER_VARIABLES
        public ParticleSystem particleEffect;

        private bool playedAnimalParticleOnce = false;
        private bool tracked = false;
        private bool showedObject = false;
        private float animalShowUpTimer = 0f;


        #region UNTIY_MONOBEHAVIOUR_METHODS

        void Start()
        {

            mTrackableBehaviour = GetComponent<TrackableBehaviour>();
            if (mTrackableBehaviour)
            {
                mTrackableBehaviour.RegisterTrackableEventHandler(this);
            }


        }

        #endregion // UNTIY_MONOBEHAVIOUR_METHODS



        #region PUBLIC_METHODS

        /// <summary>
        /// Implementation of the ITrackableEventHandler function called when the
        /// tracking state changes.
        /// </summary>
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
                OnTrackingLost();
            }
        }

        #endregion // PUBLIC_METHODS

        void Update()
        {

            if (tracked)
            {
                animalShowUpTimer += Time.deltaTime;



                if (particleEffect != null)
                {
                    if (animalShowUpTimer > particleEffect.startLifetime && !showedObject)
                    {
                        OnTrackingFound();
                        showedObject = true;
                      
                    }
                }
                else
                {
                    if (!showedObject)
                    {
                        OnTrackingFound();
                        showedObject = true;
                       
                    }
                }

            }
        }

        #region PRIVATE_METHODS


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
    
       
        #endregion // PRIVATE_METHODS
    }
}

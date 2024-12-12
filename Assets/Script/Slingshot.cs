using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slingshot : MonoBehaviour
{
    static private Slingshot S;
    [SerializeField] private AudioClip pullBack;
    [SerializeField] private AudioClip release;

    [Header("Set in Inspector")]
    public GameObject prefabProjectile;
    public GameObject prefabProjectileR;
    public float velocityMult = 8f;
    public float soundLevel = .05f;

    [Header("Set Dynamically")]
    public GameObject launchPoint;
    public Vector3 launchPos;
    public GameObject projectile;
    public bool aimingMode;

    private Rigidbody projectileRigidbody;

    static public Vector3 LAUNCH_POS {
        get {
            if (S == null) return Vector3.zero;
            return S.launchPos;
        }
    }

    // Start is called before the first frame update
    void Awake()
    {
        S = this;
        Transform launchPointTrans = transform.Find("LaunchPoint");
        launchPoint = launchPointTrans.gameObject;
        launchPoint.SetActive(false);
        launchPos = launchPointTrans.position;
    }
    void OnMouseEnter()
    {
        launchPoint.SetActive(true);
    }

    // Update is called once per frame
    void OnMouseExit()
    {
        launchPoint.SetActive(false);
    }

    void OnMouseDown()
    {
        if(!PauseMenu.isPaused){
        aimingMode = true;
        
        SoundsFXManager.instance.PlaySoundFXClip(pullBack,transform, soundLevel);

        int randomNumber = (int)UnityEngine.Random.Range(0f, 9f);
        if(randomNumber == 0)
        {
        projectile = Instantiate(prefabProjectileR) as GameObject;
        }
        else
        {
            projectile = Instantiate(prefabProjectile) as GameObject;
        }
        projectile.transform.position = launchPos;
        projectile.GetComponent<Rigidbody>().isKinematic = true;

        projectileRigidbody = projectile.GetComponent<Rigidbody>();
        projectileRigidbody.isKinematic = true; //Note already set by  getComponent
        BandLine.projectile = projectile;
        }
    }

    void Update() {
        if(!PauseMenu.isPaused){
// If Slingshot is not in aimingMode, don't run this code
if (!aimingMode) return; // b
// Get the current mouse position in 2D screen coordinates
Vector3 mousePos2D = Input.mousePosition; // c
mousePos2D.z = -Camera.main.transform.position.z;
Vector3 mousePos3D = Camera.main.ScreenToWorldPoint( mousePos2D );
// Find the delta from the launchPos to the mousePos3D
Vector3 mouseDelta = mousePos3D-launchPos;
// Limit mouseDelta to the radius of the Slingshot SphereCollider // d
float maxMagnitude = this.GetComponent<SphereCollider>().radius;


if (mouseDelta.magnitude > maxMagnitude) {
mouseDelta.Normalize();
mouseDelta *= maxMagnitude;
}

// Move the projectile to this new position
Vector3 projPos = launchPos + mouseDelta;
projectile.transform.position = projPos;

if ( Input.GetMouseButtonUp(0) ) { // e
// The mouse has been released
aimingMode = false;
SoundsFXManager.instance.PlaySoundFXClip(release,transform, soundLevel);
projectileRigidbody.isKinematic = false;
projectileRigidbody.velocity = -mouseDelta * velocityMult;
FollowCam.POI = projectile;
projectile = null;
MissionDemolition.ShotFired();
BandLine.projectile = null;
}
}
    }

}

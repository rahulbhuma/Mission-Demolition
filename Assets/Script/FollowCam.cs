using UnityEngine;
using System.Collections;
public class FollowCam : MonoBehaviour {
static public GameObject POI; // The static point of interest // a
[Header("Set in Inspector")]
public float easing = 0.05f;
public Vector2 minXY = Vector2.zero;

[Header("Set Dynamically")]
public float camZ; // The desired Z pos of the camera
void Awake() {
camZ = this.transform.position.z;
}
void FixedUpdate () {




//destination = Vector3.Lerp(transform.position,destination,easing);

Vector3 destination;
// If there is no poi, return to P:[ 0, 0, 0 ]
if (POI ==null ) {
destination =Vector3.zero;
}else {
// Get the position of the poi
destination = POI.transform.position;
// If poi is a Projectile, check to see if it's at rest
if (POI.tag == "Projectile" ) {
// if it is sleeping (that is, not moving)
if ( POI.GetComponent<Rigidbody>().IsSleeping() ) {
// return to default view
POI =null ;
// in the next update
return ;
}
}
}
destination.x = Mathf.Max(minXY.x,destination.x);
destination.y = Mathf.Max(minXY.y,destination.y);
destination.z = camZ;
// Set the camera to the destination
transform.position = destination;

Camera.main.orthographicSize = destination.y + 10;
}
}

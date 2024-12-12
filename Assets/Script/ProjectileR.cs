using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileR : MonoBehaviour
{
    void onCollissionEnter(Collision coll)
    {
        GameObject collideWith = coll.gameObject;
        if (collideWith.tag == "Projectile")
        {
            // Destroy the other GameObject
            print("ribs");
            Destroy(collideWith);
        }
    }   

}

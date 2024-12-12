using UnityEngine;

public class BandLine : MonoBehaviour {

    public static GameObject projectile;
    public GameObject centerPoint;


    private LineRenderer line;
    void Awake()
    {
        line = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (projectile != null)
        {
            line.SetPosition(0, projectile.transform.position);
            line.SetPosition(1, transform.position);
        } else
        {
            line.SetPosition(0, centerPoint.transform.position);
            line.SetPosition(1, transform.position);
        }
    }
       
}
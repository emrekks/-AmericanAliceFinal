using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshairr : MonoBehaviour
{
    public Vector3 rayhittransform;
    public LayerMask rayMask;
    public ParticleSystem _particle;
    public GameObject crosshairObject;
    public Camera cam;
    private Vector3 up;
    public WeaponController _weaponController;
    // Start is called before the first frame update
    void Start()
    {
        crosshairObject.SetActive(false);
        up = new Vector3(0, 0.34f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100, rayMask))
        {
            rayhittransform = hit.point;
        }


        if (hit.collider != null && !_weaponController.autoAimTrue)
        {
            crosshairObject.SetActive(true);
            _particle.transform.position = up + rayhittransform;
        }
        else
        {
            crosshairObject.SetActive(false);
        }

    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Camera cam;
    Mover mover;

    bool hasHit;
    RaycastHit hit;

    [SerializeField]
    LayerMask whatIsGround;
    private void Awake()
    {
        mover = GetComponent<Mover>();
    }

    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            hasHit = Physics.Raycast(ray, out hit, 1000f, whatIsGround);

            if (hasHit)
            {
                mover.Move(hit.point);
            }
        }
    }
    private void LateUpdate()
    {
        if (hasHit)
        {
            mover.HandleTargetPointSprite(hit.point);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scMouseMapController : MonoBehaviour
{
    void OnMouseEnter()
    {
        scOperatorController.instance.UpdateStateCamera(true);
    }

    

    void OnMouseExit()
    {
        scOperatorController.instance.UpdateStateCamera(false);
    }
}

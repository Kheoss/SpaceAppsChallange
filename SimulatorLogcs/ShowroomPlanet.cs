using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowroomPlanet : MonoBehaviour{

    public bool isReverse;
    private void FixedUpdate(){
        if(isReverse)
        transform.RotateAround(transform.position, -transform.up, Time.fixedDeltaTime * 15f);
        else transform.RotateAround(transform.position, transform.up, Time.fixedDeltaTime * 15f);
    }

}

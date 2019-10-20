using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowcAseChose : MonoBehaviour
{
    public CreatePlanetManager manager;
    public int indexToReturn;


    private void OnMouseDown(){
        manager.SetTemplate(indexToReturn);
    }


}

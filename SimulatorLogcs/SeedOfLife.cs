using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedOfLife : MonoBehaviour{

    public Planet target;

    private void FixedUpdate(){
        if(target != null){
            Vector3 vct =  target.transform.position - transform.position;
            transform.Translate(vct.normalized * 10 * Time.fixedDeltaTime);
        }
    }
    private void OnCollisionEnter(Collision collision){
        Destroy(this.gameObject);
     }

}

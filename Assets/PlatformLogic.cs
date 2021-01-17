using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformLogic : MonoBehaviour
{



    // Update is called once per frame
    void Update()
    {
        if(GamePlayUI.SharedInstance.pause==false){
        
        float speedx = PlayerLogic.SharedInstance.get_speed(); 
        transform.Translate(speedx*Time.deltaTime, 0 , 0);
        if(transform.position.x>=100){
          

            if(this.gameObject.CompareTag("upperPlatform")){
                this.gameObject.transform.position  = new Vector3(-700, 10, 0);
            }
            if(this.gameObject.CompareTag("lowerPlatform")){
                this.gameObject.transform.position  = new Vector3(-700, 0, 0);
            }


        }



    }
}
}

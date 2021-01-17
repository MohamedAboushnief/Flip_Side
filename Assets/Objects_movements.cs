using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objects_movements : MonoBehaviour
{

    private float Red;
    private float Green;
    private float Blue;
    private float Alpha=0;







    void randomColor_generate(){
            float random_color = Random.Range(1,4);
            if(random_color==1){
                Red=1;
                Green=0;
                Blue=0;
            }
            if(random_color==2){
                Red=0;
                Green=1;
                Blue=0;
            }
            if(random_color==3){
                Red=0;
                Green=0;
                Blue=1;
            }
            

        }




    void generate_objects(){

        
        float x = Random.Range(-100, -300);
        float z;
        float y = Random.Range(1, 10);


        //if y is even then generate object on the upper platform and vice versa
        if(y%2==0){
            if(this.gameObject.CompareTag("wall")){
                z=0;
                y=1.58f;
            }
            else if(this.gameObject.CompareTag("cube1")){
                y=1.62f;
                z = Random.Range(0, 10);
                if(z%2==0)
                    z=2.5f;
                else
                    z=-2.5f;
            }
            else if(this.gameObject.CompareTag("cube2")){
                y=1.62f;
                z = Random.Range(0, 10);
                if(z<=3){
                    z=-3.45f;
                }
                else if(z>3 && z<=6){
                    z=0;
                }
                else{
                    z=3.45f;
                }

            }
            else if(this.gameObject.CompareTag("collectible2")){
                z = Random.Range(-4, 4);
                x = Random.Range(-700, -1000);
                y=1.58f;

            }
            else {    
                z = Random.Range(-4, 4);
                y=1.28f;
            }
        }
        else{
            if(this.gameObject.CompareTag("wall")){
                z=0;
                y=8.5f;
            }
            else if(this.gameObject.CompareTag("cube1")){
                y=8.53f;
                z = Random.Range(0, 10);
                if(z%2==0)
                z=2.5f;
                else
                z=-2.5f;
            }
            else if(this.gameObject.CompareTag("cube2")){
                y=8.53f;
                z = Random.Range(0, 10);
                if(z<=3){
                    z=-3.45f;
                }
                else if(z>3 && z<=6){
                    z=0;
                }
                else{
                    z=3.45f;
                }
            }
            else if(this.gameObject.CompareTag("collectible2")){
                x = Random.Range(-700, -1000);
                z = Random.Range(-4, 4);
                y=8.4f;
            }
            else{    
                z = Random.Range(-4, 4);
                y=8.76f;
            }
        }




        this.gameObject.transform.position  = new Vector3(x,y,z);


        
       
        //if it is collectible1 then assign it the random color
        if(this.gameObject.CompareTag("collectible2")){
            randomColor_generate();
            this.gameObject.GetComponent<Renderer>().material.color = new Color (Red, Green, Blue, Alpha);
        }
  
        
        
    }


 


    // Update is called once per frame
    void Update()
    {
        if(GamePlayUI.SharedInstance.pause==false){
        
        float speedx = PlayerLogic.SharedInstance.get_speed(); 


        transform.Translate(speedx*Time.deltaTime, 0 , 0);
        if(transform.position.x>=50){
            generate_objects();
        }
    }
    }
}

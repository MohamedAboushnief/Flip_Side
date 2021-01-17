using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerLogic : MonoBehaviour
{

    private float speedZ = 30;
    bool flipMode= false;
    public GameObject collectible1_prefab;
    public GameObject collectible2_prefab;
    public GameObject wall_prefab;
    public GameObject cube1_prefab;
    public GameObject cube2_prefab;
    float FlipSpeedY=35;    //speed of translating to the other platform
    bool goUp=false;
    bool goDown= false;
    private int new_score=0;
    private int healthPoints=3;
    private float Red;
    private float Green;
    private float Blue;
    private float Alpha=0;
    private float objects_speedx=30.0f;
    


    public AudioSource Health_increase;
    public AudioSource Health_decrease;
    public AudioSource Score_increase;
    public AudioSource Score_decrease;
    public AudioSource Game_over;
    public AudioSource Jump;
    public AudioSource[] sounds;

    
    
    


    public static PlayerLogic SharedInstance;

    void Awake() {
        SharedInstance = this;
    }



    public float get_speed(){
        return objects_speedx;
    }
      public int get_score(){
        return new_score;
    }



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


   

    void change_color()
    {
        randomColor_generate();
        this.GetComponent <Renderer> ().material.color = new Color (Red, Green, Blue, Alpha);
    }


    // Start is called before the first frame update
    void Start()
    {

        sounds = GetComponents<AudioSource>();
        Score_increase = sounds[0];
        Score_decrease = sounds[1];
        Health_increase= sounds[2];
        Health_decrease= sounds[3];
        Game_over = sounds[4];
        Jump = sounds[5];

        InvokeRepeating("change_color", 0.0f, 15.0f);
        for (int i = 0; i < 3; i++) {

            float x = Random.Range(-100, -1300);
            float z = Random.Range(-3, 3);
            GameObject newPrefabBottom = (GameObject)Instantiate(collectible1_prefab, new Vector3(x, 1.28f, z), Quaternion.identity);
            randomColor_generate();
            newPrefabBottom.GetComponent <Renderer> ().material.color = new Color (Red, Green, Blue, Alpha);

            x = Random.Range(-500, -100);
            z = Random.Range(-3, 3);
            GameObject newPrefabTop= (GameObject)Instantiate(collectible1_prefab, new Vector3(x, 8.76f, z), Quaternion.identity);
            randomColor_generate();
            newPrefabTop.GetComponent <Renderer> ().material.color = new Color (Red, Green, Blue, Alpha);


        }


        for (int i = 0; i < 3; i++) {
            float collectible2_x = Random.Range(-800, -1300);
            float collectible2_z = Random.Range(-3, 3);
            GameObject newPrefabTop = (GameObject)Instantiate(collectible2_prefab, new Vector3(collectible2_x, 1.58f, collectible2_z), Quaternion.identity);

            collectible2_x = Random.Range(-1300, -800);
            collectible2_z = Random.Range(-3, 3);
            GameObject newPrefabBottom= (GameObject)Instantiate(collectible2_prefab, new Vector3(collectible2_x, 8.4f, collectible2_z), Quaternion.identity);
        }




        for (int i = 0; i < 1; i++) {

            float wall_x = Random.Range(-100, -1300);
            GameObject newPrefabTop = (GameObject)Instantiate(wall_prefab, new Vector3(wall_x, 1.58f, 0), Quaternion.identity);           

            float wall_x2 = Random.Range(-100, -1300);
            while(true){
                if(wall_x2-wall_x >= 50 || wall_x-wall_x2 <= -50){
                    break;
                }
                else{
                    wall_x2 = Random.Range(-100, -1300);
                }
            }
            
            GameObject newPrefabBottom = (GameObject)Instantiate(wall_prefab, new Vector3(wall_x2, 8.5f, 0), Quaternion.identity);
        }



        for (int i = 0; i < 1; i++) {

            float cube1_x = Random.Range(-100, -1100);
            float cube1_z = Random.Range(0, 10);
            if(cube1_z%2==0)
                cube1_z=2.5f;
            else
                cube1_z=-2.5f;
            GameObject newPrefabTop = (GameObject)Instantiate(cube1_prefab, new Vector3(cube1_x, 1.62f, cube1_z), Quaternion.identity);

            cube1_x = Random.Range(-1100, -100);
            cube1_z = Random.Range(0, 10);
            if(cube1_z%2==0)
                cube1_z=2.5f;
            else
                cube1_z=-2.5f;

            GameObject newPrefabBottom = (GameObject)Instantiate(cube1_prefab, new Vector3(cube1_x, 8.53f, cube1_z), Quaternion.identity);

            
        }


        for (int i = 0; i < 1; i++) {

            float cube2_x = Random.Range(-100, -1100);
            float cube2_z = Random.Range(0, 10);
            if(cube2_z<=3){
                cube2_z=-3.45f;
            }
            else if(cube2_z>3 && cube2_z<=6){
                cube2_z=0;
            }
            else{
                cube2_z=3.45f;
            }
            GameObject newPrefabTop = (GameObject)Instantiate(cube2_prefab, new Vector3(cube2_x, 1.62f, cube2_z), Quaternion.identity);


            cube2_x = Random.Range(-100, -1100);
            cube2_z = Random.Range(0, 10);
            if(cube2_z<=3){
                cube2_z=-3.45f;
            }
            else if(cube2_z>3 && cube2_z<=6){
                cube2_z=0;
            }
            else{
                cube2_z=3.45f;
            }
            GameObject newPrefabBottom = (GameObject)Instantiate(cube2_prefab, new Vector3(cube2_x, 8.53f, cube2_z), Quaternion.identity);
       
        }

    }


    void generate_objects(Collider other){

        
        float x = Random.Range(-100, -1300);
        float z;
        float y = Random.Range(1, 10);


        //if y is even then generate object on the upper platform and vice versa
        if(y%2==0){
            if(other.gameObject.CompareTag("wall")){
                z=0;
                y=1.58f;
            }
            else if(other.gameObject.CompareTag("cube1")){
                y=1.62f;
                z = Random.Range(0, 10);
                if(z%2==0)
                    z=2.5f;
                else
                    z=-2.5f;
            }
            else if(other.gameObject.CompareTag("cube2")){
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
            else if(other.gameObject.CompareTag("collectible2")){
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
            if(other.gameObject.CompareTag("wall")){
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
            else if(other.gameObject.CompareTag("cube2")){
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
            else if(other.gameObject.CompareTag("collectible2")){
                x = Random.Range(-700, -1000);
                z = Random.Range(-4, 4);
                y=8.4f;
            }
            else{    
                z = Random.Range(-4, 4);
                y=8.76f;
            }
        }




        other.gameObject.transform.position  = new Vector3(x,y,z);


        
       
        //if it is collectible1 then assign it the random color
        if(other.gameObject.CompareTag("collectible2")){
            randomColor_generate();
            other.gameObject.GetComponent<Renderer>().material.color = new Color (Red, Green, Blue, Alpha);
        }
  
        
        
    }




    void OnTriggerEnter(Collider other)
    {

        Color object_color = other.gameObject.GetComponent<Renderer> ().material.color;
        Color sphere_color = this.gameObject.GetComponent<Renderer> ().material.color;
        //check identical color for each platform
        
        generate_objects(other);

        if(other.gameObject.CompareTag("collectible1")){
            if(sphere_color.Equals(object_color)){
                if(flipMode==false){
                    new_score=new_score+10;
                    //play sound effect for score increase
                    Score_increase.Play();              
                }
                else{
                    new_score=new_score-5;
                    //play sound effect for score decrease
                    Score_decrease.Play();
                }
                
            }
            else{
                if(flipMode==false){
                    new_score=new_score-5;
                    //play sound effect for score decrease
                    Score_decrease.Play();
                }
                else{
                    new_score=new_score+10;
                    //play sound effect for score increase
                    Score_increase.Play();
                    
                }
            }
        }
        if(other.gameObject.CompareTag("collectible2")){
            if(healthPoints<3){
                healthPoints++;
            }
            //play sound effect for health increase
            Health_increase.Play();
            
        }

        if(other.gameObject.CompareTag("cube1") || other.gameObject.CompareTag("cube2") || other.gameObject.CompareTag("wall") ){
            if(healthPoints>0){
                healthPoints--;
                //play sound effect for obstacle collision
                Health_decrease.Play();
            }
            else{
                Game_over.Play();
                GamePlayUI.SharedInstance.game_over();
                

            }
            
            


        }

        if(new_score<0){
            new_score=0;
        }

        //speed up
        objects_speedx = (10.0f * (new_score/50)) + 30.0f;

        //display new score and health
        GamePlayUI.SharedInstance.change_score(new_score);
        GamePlayUI.SharedInstance.change_health(healthPoints);


        
        

    }

  
    public void JumpButtonUI(){
        if(GamePlayUI.SharedInstance.pause==false){


        //flip mode using spacebar key
        if (flipMode==false)
        {
            goUp=true;
        }
        if( goUp==true ){

            if(transform.position.y<=8.9 ){
                transform.Translate(0, Time.deltaTime*FlipSpeedY, 0);

            }
            else{
                goUp=false;
                flipMode=true;
                Jump.Play();
            }
        }
        

        if (flipMode==true)
        {
            goDown=true; 
        }
        if(goDown==true){

            if(transform.position.y>=1.1){
                transform.Translate(0, Time.deltaTime*-FlipSpeedY, 0);
                
            }
            else{
                goDown=false;
                flipMode=false;
                Jump.Play();
            }
        }

    }
    }



    public void switch_cameras(){
        GamePlayUI.SharedInstance.switch_cameras();
    }



    // Update is called once per frame
    void Update()
    {

        if(GamePlayUI.SharedInstance.pause==false){

        //flip mode using spacebar key
        if (Input.GetKeyDown(KeyCode.Space) && flipMode==false )
        {
            goUp=true;
        }
        if( goUp==true ){

            if(transform.position.y<=8.9 ){
                transform.Translate(0, Time.deltaTime*FlipSpeedY, 0);

            }
            else{
                goUp=false;
                flipMode=true;
                Jump.Play();
            }
        }
        




        if(Input.GetKeyDown(KeyCode.Space) && flipMode==true)
        {
            goDown=true; 
        }
        if(goDown==true){

            if(transform.position.y>=1.1){
                transform.Translate(0, Time.deltaTime*-FlipSpeedY, 0);
                
            }
            else{
                goDown=false;
                flipMode=false;
                Jump.Play();
            }
        }

        //change camera view
        if(Input.GetKeyDown(KeyCode.C)){
            GamePlayUI.SharedInstance.switch_cameras();
        }

      
        

        //if device is phone
        if(SystemInfo.deviceType == DeviceType.Handheld)
        {
             if(Input.acceleration.x <0 && transform.position.z>=-4.3){
                transform.Translate(0, 0, (Input.acceleration.x)*Time.deltaTime*speedZ);  
            }
            if(Input.acceleration.x>0 && transform.position.z<=4.3 ){
                transform.Translate(0, 0, (Input.acceleration.x)*Time.deltaTime*speedZ);  
            } 
        }


        //if device is phone
        else{  
            //translation of the sphere left and right
            float v = Input.GetAxis("Horizontal");

            if(v<0 && transform.position.z>=-4.3){
                transform.Translate(0, 0, v*Time.deltaTime*speedZ);
            }
            if(v>0 && transform.position.z<=4.3 ){
                transform.Translate(0, 0, v*Time.deltaTime*speedZ);
            }

        }



        //cheat codes
        if(Input.GetKeyDown(KeyCode.R)){
            change_color();
        }
        if(Input.GetKeyDown(KeyCode.E)){
            healthPoints++;
            GamePlayUI.SharedInstance.change_health(healthPoints);
        }
        if(Input.GetKeyDown(KeyCode.Q)){
            new_score=new_score+10;
            GamePlayUI.SharedInstance.change_score(new_score);
            objects_speedx = (10.0f * (new_score/50)) + 30.0f;

        }
  



    }
    }
}

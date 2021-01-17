using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GamePlayUI : MonoBehaviour
{
    public static GamePlayUI SharedInstance;
    public GameObject Canvas;
    public bool pause= false;
    private bool camera_switch=false;
    public GameObject Camera1;
    public GameObject Camera2;
    public GameObject BackgroundMusic;
    private bool mute = false;




    public void mute_backgroundMusic(){
        if(mute==false){
            BackgroundMusic.transform.gameObject.SetActive(false);
            mute=true;
        }
        else{
            BackgroundMusic.transform.gameObject.SetActive(true);
            mute=false;
        }



    }


    public void switch_cameras(){
        camera_switch= !camera_switch;
        if(camera_switch==false){
            Camera1.transform.gameObject.SetActive(false);
            Camera2.transform.gameObject.SetActive(true);

        }else{
            Camera1.transform.gameObject.SetActive(true);
            Camera2.transform.gameObject.SetActive(false);

        }

    }

    public void pause_game(){
        pause=true;
        Canvas.transform.Find("PauseCanvas").gameObject.SetActive(true);
    }

    public void resume_game(){
        pause=false;
        Canvas.transform.Find("PauseCanvas").gameObject.SetActive(false);
    }

    public void restart_game(){
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
        Canvas.transform.Find("PauseCanvas").gameObject.SetActive(false);
    }

    public void quit_game(){
        SceneManager.LoadScene("MainMenu");
    }

     public void game_over(){
        Canvas.transform.Find("GameOverCanvas").gameObject.SetActive(true);
        pause = true;
        BackgroundMusic.transform.gameObject.SetActive(false);
    }


    



    void Start()
    {

        Canvas.transform.Find("Score").gameObject.GetComponent<Text>().text="Score: "+0 ;
        Canvas.transform.Find("Health").gameObject.GetComponent<Text>().text="Health: "+3;

        
    }

     void Awake() {
        SharedInstance = this;
    }


    public void change_score(int score){
        Canvas.transform.Find("Score").gameObject.GetComponent<Text>().text="Score: "+score ;
    }
     public void change_health(int health){
        Canvas.transform.Find("Health").gameObject.GetComponent<Text>().text="Health: "+health ;
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)){
            if(pause==false){
                pause_game();
            }
            else{
                resume_game();
            }
            
        }
        // score = PlayerLogic.SharedInstance.get_score();
        
    }
}

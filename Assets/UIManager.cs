using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject Help_view;
    public GameObject Credits_view;
    public GameObject BackgroundMusic;
    private bool mute = false;
    
    // Start is called before the first frame update
   public void StartGame() 
    {
    SceneManager.LoadScene("Gameplay");
    }

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

    public void view_credits(){
        Credits_view.transform.gameObject.SetActive(true);
    }

    public void view_help(){
        Help_view.transform.gameObject.SetActive(true);
    }

    public void close_credits(){
        Credits_view.transform.gameObject.SetActive(false);
    }

    public void close_help(){
        Help_view.transform.gameObject.SetActive(false);
    }
    public void close_application(){
        #if UNITY_EDITOR
         UnityEditor.EditorApplication.isPlaying = false;
        #else
         Application.Quit();
        #endif
    }
}

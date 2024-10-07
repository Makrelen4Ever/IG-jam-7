using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public float StartTime;
    private Text text;

    private bool IsStarted;

    void Awake(){
        Init();
        text = GetComponent<Text>();
    }

    void Init(){
        StartTime = Time.time;
        DontDestroyOnLoad(transform.parent);
    }

    void Update(){
        if(Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0 && !IsStarted){
            Init();
            IsStarted = true;
        }

        if(!IsStarted){
            return;
        }

        if(SceneManager.GetActiveScene().buildIndex == 7){
            return;
        }
        text.text = (Time.time - StartTime).ToString("0.00");
    }
}

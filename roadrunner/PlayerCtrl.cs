using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

enum Gesture { tap,up,down,forawrd,backward}

public class PlayerCtrl : MonoBehaviour {
    private Gesture gesture;
    private Vector2 Input_Start;
    private Vector2 Input_End;
    private float Position_x;
    private float Speed;
    public GameObject GameOverUI;

	// Use this for initialization
	void Start () {
        Position_x = 0.0f;
        Speed = 10.0f;
        StartCoroutine(SpeedUP());
        GameOverUI.SetActive(false);
		
	}
	
	// Update is called once per frame
	void Update () {
        GestureInput();
        Run();
        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            Position_x = 2.5f;
        }
        if(Input.GetKeyUp(KeyCode.RightArrow))
        {
            Position_x = 0;
        }
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Position_x = -2.5f;
        }
        if(Input.GetKeyUp(KeyCode.LeftArrow))
        {
            Position_x = 0;
        }
		
	}
    void GestureInput()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Input_Start = Input.mousePosition;
        }
        if(Input.GetMouseButtonUp(0))
        {
            Input_End = Input.mousePosition;
            Vector2 CurrentInput = (Input_End - Input_Start);
            CurrentInput.Normalize();
            if(CurrentInput.x > 0 && CurrentInput.y < 0.5f && CurrentInput.y > -0.5f)
            {
                gesture = Gesture.backward;
            }
           else if (CurrentInput.x < 0 && CurrentInput.y < 0.5f && CurrentInput.y > -0.5f)
            {
                gesture = Gesture.forawrd;
            }
            else if (CurrentInput.y < 0 && CurrentInput.x < 0.5f && CurrentInput.x > -0.5f)
            {
                gesture = Gesture.down;
            }
            else if (CurrentInput.y > 0 && CurrentInput.x < 0.5f && CurrentInput.x > -0.5f)
            {
                gesture = Gesture.up;
            }
                else{
                gesture = Gesture.tap;
                }
            Move_x();
            }
        }
    void Run()
    {
        transform.position += transform.forward * Speed * Time.deltaTime;
        Vector3 MoveDir = new Vector3(Position_x, transform.position.y, transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, MoveDir, 10.0f * Time.deltaTime);
    }
    void Move_x()
    {
        if (gesture == Gesture.forawrd)
        {
            if(Position_x < 2.5f)
            {
                Position_x += 2.5f;
            }
        }
        if (gesture == Gesture.backward)
        {
            if (Position_x > -2.5f)
            {
                Position_x -= 2.5f;
            }
        }
    }
   IEnumerator SpeedUP()
    {
        while(true)
        {
            yield return new WaitForSeconds(3.0f);
            Speed += 0.1f;
        }
    }
    IEnumerator GameOVer()
    {
        GameOverUI.SetActive(true);
        Speed = 0.0f;
        StopCoroutine(SpeedUP());
        yield return new WaitForSeconds(3.0f);
        SceneManager.LoadScene(0);
    }
    void OnCollisionEnter(Collision coll)
    {
        if(coll.collider.CompareTag("Trap"))
        {
            StartCoroutine(GameOVer());
        }

    }
}

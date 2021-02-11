using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class player1 : MonoBehaviour
{

    [HideInInspector]
    public PlayerDirection1 direction;

    [HideInInspector]
    public float stepLength = 0.2f;

    [HideInInspector]
    public float moveFreq = 0.1f;

    private float counter;
    private bool move;


    private List<Vector3> delta_Position;

    private List<Rigidbody> nodes;

    private Rigidbody main_Body;
    private Rigidbody head_Body;
    private Transform tr;

    bool create_Node_At_Tail;
    bool remove_Node_At_Tail;

    private Text scoreText;
    int score=2;
    public GameObject T;
    
    [SerializeField]
    GameObject grow;
    public GameObject removed;




    // Start is called before the first frame update
    void Awake()
    {
        
        tr = transform;
        main_Body = GetComponent<Rigidbody>();

        InitSnakeNodes();
        //InitPlayer();

        delta_Position = new List<Vector3>() {
        new Vector3(-stepLength,0f ,0f), //-x ->left
        new Vector3 (0f,0f,stepLength), //z ->Front
        new Vector3(stepLength,0f,0f),  // x ->right
        new Vector3 (0f,0f,-stepLength) //-z ->Back
        };
        scoreText = T.GetComponent<Text>();

        
    }

    // Update is called once per frame
    void Update()
    {
        CheckMovementFrequenct();

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            
            SetInputDirection(PlayerDirection1.Front);
            

        }

        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {        
              
   
            SetInputDirection(PlayerDirection1.RIGHT);
           


        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
           
            SetInputDirection(PlayerDirection1.Back);
           
        }

        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
           
            SetInputDirection(PlayerDirection1.LEFT);
           
        }

    }

    void FixedUpdate()
    {
        if (move)
        {
            move = false;
            Move();
        }
    }

    void InitSnakeNodes()
    {
        nodes = new List<Rigidbody>();

        nodes.Add(tr.GetChild(0).GetComponent<Rigidbody>());
        nodes.Add(tr.GetChild(1).GetComponent<Rigidbody>());
        nodes.Add(tr.GetChild(2).GetComponent<Rigidbody>());

        head_Body = nodes[0];
    }

    void Move()
    {
        Vector3 dPosition = delta_Position[(int)direction];
        Vector3 parentPos = head_Body.position;
        Vector3 prevPosition;
  

        main_Body.position = main_Body.position + dPosition;
        head_Body.position = head_Body.position + dPosition;
        
        


        for (int i = 1; i < nodes.Count; i++)
        {
            
            prevPosition = nodes[i].position;

            nodes[i].position = parentPos;
            parentPos = prevPosition;
            
        }


        if (create_Node_At_Tail)
        {
            create_Node_At_Tail = false;

            GameObject newNode = Instantiate(grow, nodes[nodes.Count - 1].position,Quaternion.identity);

            newNode.transform.SetParent(transform, true);
            nodes.Add(newNode.GetComponent<Rigidbody>());
        }


        if (remove_Node_At_Tail)
        {
            remove_Node_At_Tail = false;
            removed = nodes[nodes.Count - 1].gameObject;
            nodes.Remove(nodes[nodes.Count - 1].GetComponent<Rigidbody>());
            removed.SetActive(false);



        }

    }


    void OnTriggerEnter(Collider target)
    {

        if (target.tag == "Fruit")
        {
            target.gameObject.SetActive(false);
            create_Node_At_Tail = true;
            score++;
            Debug.Log(score);
            scoreText.text = "Score: " + score;

        }

        if (target.tag == "Tail")
        {
            Time.timeScale = 0f;

            SceneManager.LoadScene("Game Over");
        }



        if (target.tag == "Harm")
        {
            target.gameObject.SetActive(false);
            remove_Node_At_Tail = true;
            score--;
            Debug.Log(score);
            scoreText.text = "SCORE: " + score;
            if (score < 0)
            {
                SceneManager.LoadScene("Game Over");
            }
        }

        if (target.tag == "Die")
        {
            SceneManager.LoadScene("Game Over");
        }


        if (target.tag == "snake")
        {
            Time.timeScale = 0f;
            SceneManager.LoadScene("Game Over");
        }
    }

    void CheckMovementFrequenct()
    {
        counter += Time.deltaTime;
        if (counter >= moveFreq)
        {
            counter = 0f;
            move = true;

        }
    }

    public void SetInputDirection(PlayerDirection1 dir)
    {
        if (dir == PlayerDirection1.Front && direction == PlayerDirection1.Back ||
            dir == PlayerDirection1.Back && direction == PlayerDirection1.Front ||
            dir == PlayerDirection1.RIGHT && direction == PlayerDirection1.LEFT ||
            dir == PlayerDirection1.LEFT && direction == PlayerDirection1.RIGHT)
        {
            return;
        }
        direction = dir;
        ForceMove();
    }

    void ForceMove()
    {
        counter = 0;
        move = false;
        Move();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Wall")
        {
            SceneManager.LoadScene("Game Over");
        }


    }



}


public enum PlayerDirection1
{
    LEFT = 0,
    Front = 1,
    RIGHT = 2,
    Back = 3,
}



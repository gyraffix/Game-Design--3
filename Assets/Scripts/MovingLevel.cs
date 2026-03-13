using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class MovingLevel : MonoBehaviour
{

    private bool moving;
    private bool started;
    private bool goalMoved;

    private bool levelMoving = true;

    public GameObject currentPlatform;
    private GameObject goal;
    private GameObject goalPlatform;
    private GameObject player;
    private float goalHeight;
    private AudioSource whirr;

    public float speed;
    public float risingSpeed;
    public float platformDistance;
    public float timeBetween;

    void StartMoving()
    {
        moving = true;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        goalPlatform = transform.Find("NewLevelPlatform").gameObject;
        goalHeight = goalPlatform.transform.position.y;        
        currentPlatform = GameObject.Find("CurrentPlatform").gameObject;
        player = GameObject.Find("Player");
        whirr = GameObject.Find("Whirr").GetComponent<AudioSource>();
        
    }

    private void GoalSequence()
    {
        goal = Instantiate(
            goalPlatform, 
            new Vector3(currentPlatform.transform.position.x + platformDistance, currentPlatform.transform.position.y - 10, goalPlatform.transform.position.z),
            goalPlatform.transform.rotation
            );
        goal.name = ("CurrentPlatform");

        Destroy(goalPlatform);
        started = true;
    }

    IEnumerator TimeBetween()
    {
        yield return new WaitForSeconds(timeBetween);
        StartMoving();
    }

    private void FixedUpdate()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!started)
        {
            GoalSequence();
        }

        if (started && !goalMoved)
        {
            if (!whirr.isPlaying)
                whirr.Play();

            //Move Up
            Vector3 moveDirection = (Vector3.up * (goalHeight - goal.transform.position.y)).normalized;

            if (goal.transform.position.y != goalHeight)
            {
                goal.transform.Translate(moveDirection * risingSpeed * Time.deltaTime);

                if ((new Vector3(goal.transform.position.x, goalHeight, goal.transform.position.z) - goal.transform.position).magnitude < 0.1f)
                {
                    goal.transform.position = new Vector3(goal.transform.position.x, goalHeight, goal.transform.position.z);

                }
            }
            else
            {
                whirr.Stop();
                goalMoved = true;
                StartCoroutine(TimeBetween());
            }
        }

        if (moving)
        {
            if (levelMoving)
                transform.Translate(Vector3.right * Time.deltaTime * speed * LevelSpawner.instance.gameSpeed);
            else
            {
                goal.transform.Translate(Vector3.left * Time.deltaTime * speed * LevelSpawner.instance.gameSpeed);
            }


            if ((transform.position - goal.transform.position).magnitude > 40 + platformDistance)
            {
                Destroy(gameObject);
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision!");
        if (other.CompareTag("Player"))
        {
            levelMoving = false;
            Destroy(currentPlatform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            levelMoving = true;
        }
    }
}

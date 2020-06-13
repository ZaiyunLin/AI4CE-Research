using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour
{
    public float rotate;
    public enum State { playing, waiting };
    public State state;
    //timer checking obj is fixed for a time
    int checkFixed;
    //list of all obj instances in the scene
    public List<GameObject> obj = new List<GameObject>();
    //all primitive shape
    public Object[] prim;
    // current controlling or observing shape
    public GameObject cur;

    public int rotation;
    public float translation;

    public bool reset;
    float height;
    public float maxheight;
    float prevheight;
    public Transform heightDetect;
    public Camera mainCamera = null;
    public float angularDrag = 2;

    public AgentLogic agent;
    public ScoreCollector scorecollector;
    public Transform spawn;
    public Transform area;
    float reward = 0.01f;
    // Start is called before the first frame update
    void Start()
    {


    }

    public void Initialize() {
        maxheight = -1.9f;
        prevheight = maxheight;
        prim = Resources.LoadAll("square", typeof(Object));
        state = State.playing;
        checkFixed = 0;
        reset = false;
        height = 5;
        rotation = 0;
        translation = 0;
        RandomGenerate();
    }

    public void ObjectRelease()
    {
        cur.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        state = State.waiting;
    }

 
    public void Reset()
    {
        reward = 0.01f;
        agent.AddReward(-0.01f);
        scorecollector.UpdateScore(maxheight);
        maxheight = -1.9f;
        prevheight = maxheight;
        height = 5;
        state = State.playing;
        checkFixed = 0;
        reset = false;
        mainCamera.transform.localPosition = new Vector3(0, 3, -15);
        foreach (GameObject o in obj)
        {
            Destroy(o.gameObject);
        }
        rotation = 0;
        translation = 0;
        RandomGenerate();
       
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Raycast();
        UpdateHeight();

        // DetectHeight();
        if (reset) {
           
            agent.EndEpisode();
            reset = false;
           }


        if (state == State.waiting) {
            GameWaiting();
            cur.GetComponent<SpriteRenderer>().color = Color.white;
        }
        if (state == State.playing) {
            GamePlaying();
        }

    }
    

    void GameWaiting() {
        if (cur.GetComponent<Rigidbody2D>().velocity.magnitude <= 0.1)
        {

            checkFixed++;
            if (checkFixed > 20)
            {
                
                agent.AddReward(reward);
                reward += 0.01f;
                //Debug.Log(maxheight - prevheight + " :reward");
                // agent.AddReward(maxheight - prevheight);
                state = State.playing;
                checkFixed = 0;
                prevheight = maxheight;
                RandomGenerate();
            }
        }
    }

    void GamePlaying() {
        agent.RequestDecision();
        Vector3 r = new Vector3(0, 0, rotation);
        Vector3 trans = new Vector3(translation, 0, 0);
        cur.transform.Rotate(r);
        cur.transform.Translate(trans, Space.World);
        cur.GetComponent<SpriteRenderer>().color = Color.gray;
        BoundDetect(cur.transform);
    }
    

    private void RandomGenerate() {
        int i = Random.Range(0, prim.Length);
        cur = Instantiate(prim[i],area)as GameObject;

        cur.transform.localPosition = new Vector3(Random.Range(-5,5), height, 0);
        cur.transform.eulerAngles = new Vector3(0, 0, Random.Range(0,360));
        var size = Random.Range(1, 2);
        cur.transform.localScale = new Vector3(size, size, 1);
        cur.GetComponent<Rigidbody2D>().angularDrag = angularDrag;
        obj.Add(cur);

    }
    void BoundDetect(Transform o) { 
        if(o.localPosition.x <-5 || o.localPosition.x > 5)
        {
            o.localPosition = new Vector3(0, height, 0);
        }

    }
 
    void Raycast()
    {
        RaycastHit2D hit = Physics2D.Raycast( area.position+ new Vector3(7,maxheight), Vector2.left,15);

        if (hit.collider!= null&&hit.transform.gameObject != cur) {
            maxheight += 0.1f;
        }

        
        Debug.DrawRay(area.position + new Vector3(7, maxheight), Vector2.left*15);
    }

    void UpdateHeight() {
        height = maxheight + 5;
        spawn.localPosition =new Vector3 (0,height,0);
        if (maxheight > 2) {
            mainCamera.transform.localPosition = new Vector3(0, height-2, -15);
        }
    }
}

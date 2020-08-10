using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerCamera : MonoBehaviour
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
    public float height;
    public float maxheight;
    float prevheight;

    public Camera mainCamera = null;
    public float angularDrag = 3;

    public AgentLogic agent;
   
    public Transform spawn;
    public Transform area;
  
     float reward = 0.2f;
    public int maxPlaytime = 400;
    //finish detecting height;    
    public bool detfin;

    // Start is called before the first frame update
    void Start()
    {

    }

 

    // Update is called once per frame
    void FixedUpdate()
    {
        Raycast();
        UpdateHeight();

        // DetectHeight();
        if (reset) {
            agent.AddReward(-1);
            agent.EndEpisode();
            reset = false;
        }


        if (state == State.waiting) {
            GameWaiting();
            cur.GetComponent<SpriteRenderer>().color = Color.white;
            cur.tag ="shape";
            
        }
        if (state == State.playing) {
            
            cur.tag ="transparent";
            GamePlaying();
        }

    }

    public void Initialize()
    {
        detfin=false;
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
       // ResetSensorHeight();
    }

    public void ObjectRelease()
    {
        cur.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        state = State.waiting;
    }


    public void Reset()
    {   
        detfin=false;
        maxPlaytime = 400;
        reward = 1;
        //scorecollector.UpdateScore(maxheight);
        maxheight = -1.9f;
        prevheight = maxheight;
        height = 5;
        state = State.playing;
        checkFixed = 0;
        reset = false;
        mainCamera.transform.localPosition = new Vector3(0, 3, -12);
        foreach (GameObject o in obj)
        {
            Debug.Log("destroy");
            Destroy(o.gameObject);
        }
        obj = new List<GameObject>();
        rotation = 0;
        translation = 0;
        RandomGenerate();
     //   ResetSensorHeight();

    }

    void GameWaiting() {
        
        maxPlaytime = 400;
        if (cur.GetComponent<Rigidbody2D>().velocity.magnitude <= 0.1)
        {
           
            checkFixed++;
            if (checkFixed > 20&&detfin)
            {

                agent.AddReward(reward);
                state = State.playing;
                checkFixed = 0;
                prevheight = maxheight;
                RandomGenerate();
   
            }
        }


    }

    void GamePlaying() {
        detfin=false;
        maxPlaytime--;
        if(maxPlaytime==0){
            reset = true;

        }
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
         float xpos;
        cur = Instantiate(prim[i], area) as GameObject;
        int rdm = Random.Range(0,2);
        if(rdm ==0){
         xpos = Random.Range(-5.0f,-3.0f);
        }else{
         xpos = Random.Range(3.0f,5.0f);
        }

        cur.transform.localPosition = new Vector3(xpos, maxheight+1.9f+4, 0);

        cur.transform.eulerAngles = new Vector3(0, 0, Random.Range(0, 360));
        var size = Random.Range(1, 2);
        cur.transform.localScale = new Vector3(size, size, 1);
        cur.GetComponent<Rigidbody2D>().angularDrag = angularDrag;
        obj.Add(cur);
       

    }
    void BoundDetect(Transform o) {
        if (o.localPosition.x < -5 )
        {
            o.localPosition = new Vector3(-5, maxheight+1.9f+4, 0);
        }
          if (o.localPosition.x > 5)
        {
            o.localPosition = new Vector3(5, maxheight+1.9f +4, 0);
        }

    }

    void Raycast()
    {
        RaycastHit2D hit = Physics2D.Raycast(area.position + new Vector3(7, maxheight), Vector2.left, 15);
        
            if (hit.collider != null && hit.transform.gameObject != cur) {    
                maxheight += 0.1f;
                detfin = false;
            }
            detfin = true;
        
        Debug.DrawRay(area.position + new Vector3(7, maxheight), Vector2.left * 15);
    }

    void UpdateHeight() {
        height = maxheight + 5+ 1.9f;
        spawn.localPosition = new Vector3(0, maxheight+3 +1.9f, 0);
     
        if (maxheight > 2) {
            mainCamera.transform.localPosition = new Vector3(0, maxheight + 3 - 1.9f, -12);
        }


      
    }


}

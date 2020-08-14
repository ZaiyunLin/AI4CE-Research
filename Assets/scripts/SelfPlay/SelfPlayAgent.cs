using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Policies;

public class SelfPlayAgent : Agent
{
    public SelfPlayGM gm;
    public int team;
    // Start is called before the first frame update
    void Start()
    {

        gm.Initialize();
    }
    private void Update()
    {

    }

    public override void Initialize() {
        BehaviorParameters m_BehaviorParameters = gameObject.GetComponent<BehaviorParameters>();
        team = m_BehaviorParameters.TeamId;
    }
    // Update is called once per frame
    public override void OnEpisodeBegin()
    {
        gm.Reset();
        //base.OnEpisodeBegin();
    }
    public override void CollectObservations(VectorSensor sensor)
    {
        //sensor.AddObservation(gm.maxheight);
        sensor.AddObservation(gm.cur.transform.position);
        sensor.AddObservation(gm.cur.transform.rotation);
        sensor.AddObservation(gm.maxPlaytime);
        sensor.AddObservation(gm.maxheight);

        //base.CollectObservations(sensor);
    }
    public override void OnActionReceived(float[] vectorAction)

    {
        if (vectorAction[0] == 1)
        {
            gm.ObjectRotate(45);
        }
        else if (vectorAction[0] == 2)
        {
            gm.ObjectRotate(-45);
        }
        if (vectorAction[1] == 2)
        {
            gm.ObjectMovement(-0.05f);
        }
        else if (vectorAction[1] == 1)
        {
            gm.ObjectMovement(0.05f);
        }
        if (vectorAction[2] == 1)
        {
            gm.ObjectRelease();
        }

    }
    public override void Heuristic(float[] actionsOut)
    {

        if (Input.GetKey(KeyCode.Space))
        {
            actionsOut[2] = 1;
        }
        else
        {
            actionsOut[2] = 0;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {

            actionsOut[1] = 2;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            actionsOut[1] = 1;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            Debug.Log("up");
            actionsOut[0] = 1;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            Debug.Log("down");
            actionsOut[0] = 2;
        }
        if (!(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow)))
        {
            actionsOut[1] = 0;
        }
        if (!(Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow)))
        {
            actionsOut[0] = 0;
        }

    }
}

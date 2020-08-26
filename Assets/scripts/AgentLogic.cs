using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;


public class AgentLogic : Agent
{
    public GameManagerCamera gm;
    // Start is called before the first frame update
    void Start()
    {
       
        gm.Initialize();
    }
    private void Update()
    {

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
        //sensor.AddObservation(gm.cur.transform.position);
        //sensor.AddObservation(gm.cur.transform.rotation);
        //sensor.AddObservation(gm.maxPlaytime);
        //sensor.AddObservation(gm.maxheight);
       
        //base.CollectObservations(sensor);
    }
    public override void OnActionReceived(float[] vectorAction)

    {
        AddReward(-0.0005f);
        if (vectorAction[0] == 1)
        {
            gm.rotation = 1;
        }
         if (vectorAction[0] == 2)
        {
            gm.rotation = -1;
        }
        // no rotate/ no move / no release
        if (vectorAction[0] ==0)
        {
            gm.rotation = 0;
            gm.translation = 0;
        }
         if (vectorAction[0] == 3)
        {
            gm.translation = -0.05f;
        }
         if (vectorAction[0] == 4)
        {
            gm.translation = 0.05f;
        }
        if (vectorAction[0] == 5)
        {
            gm.ObjectRelease();
        }

    }
    public override void Heuristic(float[] actionsOut)
    {

        AddReward(-0.0005f);
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
            actionsOut[0] = 1;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
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

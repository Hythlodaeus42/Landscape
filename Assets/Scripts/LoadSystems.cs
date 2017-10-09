using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using System.Data;
using System.IO;
//using System.Linq;
//using System.Math;

public class LoadSystems : MonoBehaviour {
    public Transform prefabSystemNode;
    public Transform prefabSystemEdge;
    public Transform prefabSystemSoftwareNode;
    public Transform prefabOSNode;
    public Transform prefabDataNode;
    public Transform prefabBusinessFunctionNode;

    bool rotateGraph = false;

    // Use this for initialization
    void Start () {
        //DataTable nodes = GetDataTableFromCsv("E:\\Users\\Brendan\\Documents\\R\\DataLineage\\nodes_coord.csv");
        //string path = "E:\\Users\\Brendan\\Documents\\R\\DataLineage\\nodes_coord.csv";

        TextAsset nodeTextAsset = Resources.Load("nodes_system") as TextAsset;

        string nodeText = nodeTextAsset.text;
        string[] nodeRows = nodeText.Split("\n"[0]);


        //string[] csv = File.ReadAllLines(path);

        int nodecount = 0;

        float x = 0;
        float y = 0;
        float z = 0;
        string nodeName = "dummy";

        foreach (string nodeRow in nodeRows)
        {
            string[] rowAttributes = nodeRow.Split(","[0]);
            //Debug.Log(nodeRow.ToString().TrimStart().Substring(0, 2));
            //Debug.Log(nodecount.ToString());

            x = float.Parse(rowAttributes[4]) * 10;
            y = float.Parse(rowAttributes[6]);
            z = float.Parse(rowAttributes[5]) * 10;
            nodeName = rowAttributes[0].Trim();

            Transform graphTransform = GameObject.Find("Landscape").transform;

            //node.name = nodeName;
            //Transform nodeInstance = Instantiate(node, new Vector3(x, y, z), Quaternion.identity, graphTransform);
            Instantiate(prefabSystemNode, graphTransform);

            Transform nodeInstance = graphTransform.GetChild(graphTransform.childCount - 1);
            nodeInstance.transform.localPosition = new Vector3(x, y, z);
            nodeInstance.name = nodeName;

            Text txt = nodeInstance.GetComponentInChildren<Text>();
            txt.text = nodeName.Split("|"[0])[1];

            //nodeInstance.name = nodeName;
            Debug.Log(nodeName);
            //nodeInstance.name = nodeName;

            //NodeData nodeData = GetComponent<NodeData>("NodeData");
            //nodeData.Name = rowAttributes[0];

            //Text systemText = GameObject.Find("SystemText").GetComponent<Text>();
            //systemText.text = nodeName.Split("|"[0])[1];

            nodecount++;
        }

        DrawEdges();

        //Instantiate(node, new Vector3(0.134f, 2.564f, 0), Quaternion.identity);
    }

    void DrawEdges()
    {
        TextAsset edgeTextAsset = Resources.Load("edges") as TextAsset;

        string edgeData = edgeTextAsset.text;
        string[] edgeRows = edgeData.Split("\n"[0]);

        //Color c1 = Color.yellow;
        //Color c2 = Color.red;

        Transform graphTransform = GameObject.Find("Landscape").transform;

        foreach (string edgeRow in edgeRows)
        {
            //Debug.Log(edgeRow.ToString());
            
            string[] rowAttributes = edgeRow.Split(","[0]);

            GameObject startNode = GameObject.Find(rowAttributes[0].Trim());
            GameObject endNode = GameObject.Find(rowAttributes[1].Trim());

            if (startNode == null)
            {
                
            }
            else if (endNode == null)
            {
                AddNonSystemNode(startNode, rowAttributes[1].Trim());
            } else { 
                Vector3 centerPosition = (startNode.transform.position + endNode.transform.position) / 2f;
                float dist = Vector3.Distance(startNode.transform.position, endNode.transform.position);

                //Transform edgeInstance = Instantiate(prefabEdge, startNode.transform.position, Quaternion.identity, graphTransform);
                Transform edgeInstance = Instantiate(prefabSystemEdge, centerPosition, Quaternion.identity, graphTransform);
                edgeInstance.LookAt(endNode.transform);
                edgeInstance.transform.localScale = new Vector3(0.5f, 0.05f, dist);
            }


        }
    }
    
    void AddNonSystemNode(GameObject startNode, string nodeid)
    {
        Transform parentTransform = startNode.transform;
        var nodeData = parentTransform.GetComponent<NodeData>();

        string[] nodeAttributes = nodeid.Split("|"[0]);
        float x = 0;
        float y = 0;
        float z = 0;
        float xzscale = 0.2f;
        float yscale = 5; 

        //node.name = nodeName;
        //Transform nodeInstance = Instantiate(node, new Vector3(x, y, z), Quaternion.identity, graphTransform);
        switch (nodeAttributes[0])
        {
            case "os":
                Instantiate(prefabOSNode, parentTransform);
                nodeData.countOS++;
                y = nodeData.countOS;
                x = -1;
                break;
            case "data":
                Instantiate(prefabDataNode, parentTransform);
                nodeData.countData++;
                y = nodeData.countData;
                x = 1;
                break;
            case "businessfunction":
                Instantiate(prefabBusinessFunctionNode, parentTransform);
                nodeData.countBusinessFunction++;
                y = nodeData.countBusinessFunction;
                z = -1;
                break;
            case "systemsoftware":
                Instantiate(prefabSystemSoftwareNode, parentTransform);
                nodeData.countSystemSoftware++;
                y = nodeData.countSystemSoftware;
                z = 1;
                break;
            default:
                Instantiate(prefabSystemSoftwareNode, parentTransform);
                nodeData.countSystemSoftware++;
                y = nodeData.countSystemSoftware;
                break;
        }

        Transform nodeInstance = parentTransform.GetChild(parentTransform.childCount - 1);
        //Debug.Log(nodeInstance.transform.parent.name);
        //nodeInstance.transform.parent = graphTransform;
        nodeInstance.transform.localPosition = new Vector3(x * xzscale, y * yscale, z * xzscale);
        //nodeInstance.name = nodecount.ToString();
        nodeInstance.name = startNode.name + ">" + nodeid;

    }

    void RotateGraph()
    {
        rotateGraph = true;
    }

    void StopRotateGraph()
    {
        rotateGraph = false;
    }

    private void Update()
    {
        if (rotateGraph)
        {
            transform.Rotate(new Vector3(0, 2, 0) * Time.deltaTime);
        }
        
    }

}

/***
 * Created by: JP Tucker
 * Date: Jan 24, 2022
 * Last Edit By: NA
 * Last edit Day: Jan 26, 2022
 * 
 * Description: Spawn multiple cube prefabs into the scene.
 ***/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomCubes : MonoBehaviour
{
    public GameObject cubePrefab; //new Gameobject
    public float scalingFactor = 0.95f; //amount each cube will shrink each frame
    public int numberOfCubes = 0; //total number of cubes

    [HideInInspector]
    public List<GameObject> gameObjectList; //list for all the cubes

    // Start is called before the first frame update
    void Start()
    {
        gameObjectList = new List<GameObject>(); //instantiate the list
    }

    // Update is called once per frame
    void Update()
    {
        numberOfCubes++; //increase num cubes
        GameObject gObj = Instantiate<GameObject>(cubePrefab); //create cube instance
        
        gObj.name = "Cube" + numberOfCubes; //name of cube instance

        Color randColor = new Color(Random.value, Random.value, Random.value);

        gObj.GetComponent<Renderer>().material.color = randColor;

        gObj.transform.position = Random.insideUnitSphere; //spawns at random location within 1 m radius
        gameObjectList.Add(gObj); //adds to list

        List<GameObject> toRemove = new List<GameObject>();
        foreach (GameObject goTemp in gameObjectList)
        {
            float size = goTemp.transform.localScale.x; //records current scale
            size *= scalingFactor; //multiplies by factor
            goTemp.transform.localScale = Vector3.one*size; //sets new scale

            if (size <= 0.1f)
                toRemove.Add(goTemp);
        }

        foreach (GameObject goTemp in toRemove)
        {
            numberOfCubes--;
            gameObjectList.Remove(goTemp); //removes from list
            Destroy(goTemp); //destroys from game
        }
    }
}

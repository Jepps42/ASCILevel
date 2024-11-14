using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;
using UnityEditor.Build;

//LOAD a level based off a text file
public class ASCIILevelLoader : MonoBehaviour
{
    //Variables

    //Offsets for positions in the level
    public float xOffset;

    public float yOffset;

    //If 3D can do zOffset

    //Prefabs for the GameObjects we want in the scene
    public GameObject player;

    public GameObject wall;

    public GameObject obstacle;

    public GameObject goal;

    //Variable for the current player
    public GameObject currentPlayer;

    //Variables for the starting position of our player

    Vector2 startPos;

    //Name for the level file
    public string fileName;

    //Variable for our current level number
    public int currentLevel = 0;


    //Empty game object to hold our level
    public GameObject level;
    //When the level changes, we want to load that level
    //Also make currentLevel a property
    public int CurrentLevel
    {
        get { return currentLevel; }
        set
        {
            currentLevel = value;
            LoadLevel();
        }
    }

    //Start is called before the first frame update
    private void Start()
    {
        LoadLevel();
    }

    //Load a level based on an ASCII text file
    void LoadLevel()
    {
        //Destroy the current level
        Destroy(level);

        //Create a new level gameObject
        level = new GameObject("Level");

        //Build a new level path based on the currentLevel
        string current_file_path = Application.dataPath + "/Resources/" + fileName.Replace("Num", currentLevel + "");

        //Pull the contents of that file into a string array
        //Each line of the file will be an item in the array
        string[] fileLines = File.ReadAllLines(current_file_path);

        //Loop through each line in the file
        for (int y = 0; y < fileLines.Length; y++)
        {
            //Get the current line
            string lineText = fileLines[y];

            //Split the line into a character array
            char[] characters = lineText.ToCharArray();

            //Loop through each character in the array we just made
            for(int x = 0; x < characters.Length; x++)
            {
                //Take the current character
                char c = characters[x];

                //Variable for the new object
                GameObject newObject;

                //Write a switch statement for the character to determine what it means
                switch (c)
                {

                    //Check if the character is the ketter p and make that my player
                    case 'p': //checks if its p
                        //Make a player gamerObject
                        newObject = Instantiate<GameObject>(player);
                        //check to see if we have a player already and if we do not, make this the player
                        if (currentPlayer = null)
                            currentPlayer = newObject;
                        //save this position to the startPos to use for resetting the player
                        startPos = new Vector2(x + xOffset, -y + yOffset);
                        break;

                    //Write the case where if the character is w we make a wall
                    case 'w':
                        //Make a wall
                        newObject = Instantiate<GameObject>(wall);
                        break;

                    //Write a case where if the character is an * make an obstacle
                    case '*':
                        newObject = Instantiate<GameObject>(obstacle);
                        break;

                    //Write a case where if the character is an & make a goal
                    case '&':
                        newObject = Instantiate<GameObject>(goal);
                        break;
                    //If it is any other character, go to default and leave the space blank
                    default:
                        newObject = null;
                        break;
                }

                //Take the new object and check if its null
                if (newObject != null )
                {
                    //Check if its a player
                    if (!newObject.name.Contains("Player"))
                    {
                        //Make the level gameObject the parent of newObject
                        newObject.transform.parent = level.transform;
                    }

                    //No matter what the new object is, set its postion based on the offsets
                    //And also the position in the file
                    newObject.transform.position = new Vector3(x + xOffset, -y + yOffset, 0);
                }
            }
        }


    }
}

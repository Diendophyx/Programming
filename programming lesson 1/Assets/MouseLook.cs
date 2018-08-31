using UnityEngine;
using System.Collections;
//this script can be found in the Component section under the option Character Set Up 
//Mouse Look
[AddComponentMenu("FirstPerson/Camera Mouse Look")]
public class MouseLook : MonoBehaviour 
{
    //Before you write this section please scroll to the bottom of the page
    #region Variables
    [Header("Rotational Axis")]
	// create a public link to the rotational axis called axis and set a defualt axis
    public RotationalAxis Axis = RotationalAxis.MouseX;
    [Header("sensitivity")]
	//public floats for our x and y sensitivity
    public float sensitivityX = 10f;
    public float sensitivityY = 10f;
    [Header("Y Rotation Clamp")]
    //max and min Y rotation
    public float minimumY = -60f;
    public float maximumY = 60f;
    //we will have to invert our mouse position later to calculate our mouse look correctly
    //float for rotation Y
    float rotationY = 0f;
    

    #endregion
    #region Start
    private void Start()
    {
        // if this gameobject has a rigidbody attached to it
        if (this.GetComponent<Rigidbody>())
        {
            ///set the rigidbody's freezeRotation to true 
            this.GetComponent<Rigidbody>().freezeRotation = true;
        }
    }
    #endregion
    #region Update
    private void Update()
    {
        if (Axis == RotationalAxis.MouseXandY)
        {
            float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityX;

            rotationY += Input.GetAxis("Mouse Y") * sensitivityY;

            rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

            transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
        }
        else if(Axis == RotationalAxis.MouseX)
        {
            transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityX, 0);
        }

        else

        {

            rotationY += Input.GetAxis("Mouse Y") * sensitivityY;

            rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

            transform.localEulerAngles = new Vector3(-rotationY,0,0); 
        }
    } 

    #region Mouse X and Y

    //if our axis is set to Mouse X and Y
    //float rotation x is equal to our y axis plus the mouse input on the Mouse X times our x sensitivity
    //our rotation Y is pulse equals  our mouse input for Mouse Y times Y sensitivity
    //the rotation Y is clamped using Mathf and we are clamping the y rotation to the Y min and Y max
    //transform our local position to the nex vector3 rotaion - y rotaion on the x axis and x rotation on the y axis
    #endregion
    #region Mouse X
    //else if we are rotating on the X
    //transform the rotation on our game objects Y by our Mouse input Mouse X times X sensitivity
    #endregion
    #region Mouse Y
    //else we are only rotation on the Y
    //our rotation Y is pulse equals  our mouse input for Mouse Y times Y sensitivity
    //the rotation Y is clamped using Mathf (math function) and we are clamping the y rotation to the Y min and Y max
    //transform our local position to the nex vector3 rotaion - y rotaion on the x axis and local euler angle Y on the y axis
    #endregion
    #endregion
    }
#region RotationalAxis
/*
enums are what we call state value variables 
they are comma separated lists of identifiers
we can use them to create Type or Category variables
meaning each heading of the list is a type or category element that this can be
eg weapons in an inventory are a type of inventory item
if the item is a weapon we can equipt it
if it is a consumable we can drink it
it runs different code depending on what that objects enum is set to
you can also have subtypes within those types
eg weapons are an inventory category or type
we can then have ranged, melee weapons
or daggers, short swords, long swords, mace, axe, great axe, war axe and so on
each Type or Category holds different infomation the game needs like 
what animation plays, where the hands sit on the weapon, how many hands sit on the weapon and so on
*/
//Create a public enum called RotationalAxis

public enum RotationalAxis
{
    MouseXandY,
    MouseX,
    MouseY

}


    // <> to be used as brackets when getting components
    // [] to be used for things such as lists and arrays
    // {} to hold blocks of code
    // () to 
#endregion












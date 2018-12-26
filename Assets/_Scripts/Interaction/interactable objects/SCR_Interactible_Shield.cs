using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_Interactible_Shield : SCR_Interaction_Object
{
    public float myrotationx, myrotationy, myrotationz;
    public float speed;
    public float floatSpeed;
    Vector3 startPos;
    bool moveUp;
	public GameObject pSGlow;

    bool isgrabed;

    void Start()
    {
        ID = "Shield";
        startPos = transform.position;
    }

    void Update()
    {
        if (transform.parent != null)
        {
            if (transform.parent.name != "Traps" && !isgrabed)
            {
				if (pSGlow != null)
					Destroy (pSGlow);

                transform.position = startPos;
                rotate();
              
                isgrabed = true;
            }
        }
        if (!isgrabed)
        {
            transform.Rotate(Vector3.up * Time.deltaTime * speed);
            if (transform.position.y >= startPos.y + 0.5f)
                moveUp = false;
            else if (transform.position.y <= startPos.y)
                moveUp = true;

            if (moveUp)
                transform.Translate(Vector3.up * Time.deltaTime * floatSpeed);
            else
                transform.Translate(Vector3.down * Time.deltaTime * floatSpeed);
        }
    }
    public void rotate()
    {
		transform.GetChild(0).transform.localRotation = new Quaternion(myrotationx, myrotationy, myrotationz, transform.localRotation.w);
    }

}

using UnityEngine;
using System.Collections;

public class DirectionChanger : MonoBehaviour {
    public Enums.ChangerType type;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D (Collider2D coll) {
        GameObject obj = coll.gameObject;
        if(obj.tag == "Player") {
            Debug.Log("Turn Player");
            switch(type) {
                case Enums.ChangerType.TurnLeft :
                    obj.SendMessage("TurnPlayer", Enums.MoveDirection.MoveLeft);
                    break;

                case Enums.ChangerType.TurnRight:
                    obj.SendMessage("TurnPlayer", Enums.MoveDirection.MoveRight);
                    break;
            }
        }
    }
}

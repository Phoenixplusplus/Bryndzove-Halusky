using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_Paintball : MonoBehaviour {

    [Header("Attributes")]
    public Vector3 Position;
    public Quaternion Rotation;
    public string Team;
    public float Speed = 1f;
    public float Timeout = 0f;
    public float maxTimeout = 5f;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.position = transform.position + transform.forward * (Time.deltaTime * Speed);

        Timeout += Time.deltaTime;
        if (Timeout > maxTimeout) DestroyPaintball();
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            C_Character character = other.transform.root.GetComponent<C_Character>();
            if (character.Team != Team)
            {
                other.transform.root.GetComponent<C_Character>().Health--;
                DestroyPaintball();
            }
        }
    }

    void DestroyPaintball()
    {
        Destroy(gameObject);
    }
}

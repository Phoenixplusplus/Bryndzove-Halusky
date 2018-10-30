using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_Paintball : MonoBehaviour {

    private GameManager gameManager;

    [Header("Attributes")]
    public Vector3 Position;
    public Quaternion Rotation;
    public string Team;
    public float Speed = 1f;
    public float Timeout = 0f;
    public float maxTimeout = 4f;
    public bool isInit = true; // to keep paintballs where they are when manager spawns the pool of them

    [Header("Decal Attributes")]
    public GameObject SplatDecal;
    public Material[] SplatMaterials;
    private Vector3 surfaceNormal;
    private Vector3 surfacePosition;
    public LayerMask layerMask;

    // Use this for initialization
    void Start ()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        // main paintball behaviour
        if (!isInit)
        {
            transform.position = transform.position + transform.forward * (Time.deltaTime * Speed);

            Timeout += Time.deltaTime;
            if (Timeout > maxTimeout) ResetPainball();
        }
	}

    void OnTriggerEnter(Collider other)
    {
        // collision with player
        if (other.gameObject.tag == "Player")
        {
            C_Character character = other.transform.root.GetComponent<C_Character>();
            if (character.Team != Team)
            {
                character.Health--;
                ResetPainball();
            }
        }

        // collision with scene (things that can be painted in the level)
        if (other.gameObject.tag == "Scene")
        {
            // paint them
            if (Team == "Red")
            {
                // red
                gameManager.SetSplatDecal(surfacePosition, Quaternion.LookRotation(surfaceNormal), SplatMaterials[Random.Range(0, 10)]);
                if (other.GetComponent<ApplyPaint>().RedTeam == false)
                {
                    other.GetComponent<ApplyPaint>().RedTeam = true;
                    gameManager.redTeamPaintCount++;
                    if (other.GetComponent<ApplyPaint>().BlueTeam == true)
                    {
                        other.GetComponent<ApplyPaint>().BlueTeam = false;
                        gameManager.blueTeamPaintCount--;
                    }
                }
            }
            else
            {
                // blue
                gameManager.SetSplatDecal(surfacePosition, Quaternion.LookRotation(surfaceNormal), SplatMaterials[Random.Range(10, 20)]);
                if (other.GetComponent<ApplyPaint>().BlueTeam == false)
                {
                    other.GetComponent<ApplyPaint>().BlueTeam = true;
                    gameManager.blueTeamPaintCount++;
                    if (other.GetComponent<ApplyPaint>().RedTeam == true)
                    {
                        other.GetComponent<ApplyPaint>().RedTeam = false;
                        gameManager.redTeamPaintCount--;
                    }
                }
            }
            ResetPainball();
        }
    }

    // ray must be cast to determine the position and rotation of the splat (only done once on initial fire), it helps that the paintball does not change direction and keeps going forward
    // this method was used because triggers cannot get normals of face that was collided with - normal collisions with rigidbody and colliders can, however the normal of the object and collision
    // is unrealiable, so raycast was used (ignoring the paintballs themselves in case the player fired in the exact same spot constantly
    public void PaintballRaycast()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 5000f, layerMask))
        {
            surfaceNormal = hit.normal;
            surfacePosition = hit.point;
        }
    }

    void ResetPainball()
    {
        transform.position = gameManager.paintballsStartPosition;
        Timeout = 0f;
        isInit = true;
    }
}

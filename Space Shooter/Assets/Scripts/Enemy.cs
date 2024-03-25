using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    public float minSpeed;

    [SerializeField]
    public float maxSpeed;

    [SerializeField]
    private float currentSpeed;

    [SerializeField]
    private float MinRotateSpeed;

    [SerializeField]
    private float MaxRotateSpeed;

    [SerializeField]
    private float MinScale;

    [SerializeField]
    private float MaxScale;


    private float currentRotationSpeed;
    
    private float currentScaleX;
    private float currentScaleY;
    private float currentScaleZ;


    // Start is called before the first frame update
    void Start()
    {
        SetPositionAndSpeed();
    }

    // Update is called once per frame
    void Update()
    {

        float rotationSpeed = currentRotationSpeed * Time.deltaTime;
        transform.Rotate(new Vector3(-1, 0, 0) * rotationSpeed);
        //move enemy
        float amtToMove = currentSpeed * Time.deltaTime;
        transform.Translate(Vector3.down * amtToMove, Space.World);

        if(transform.position.y <= -5)
        {
            SetPositionAndSpeed();
            Player.missed++;
            Player.UpdadeStats();
        }
        
    }

    public void SetPositionAndSpeed()
    {
        //set new position
        
        currentRotationSpeed = Random.Range(MinRotateSpeed, MaxRotateSpeed);
        

        currentScaleX = Random.Range(MinScale, MaxScale);
        currentScaleY = Random.Range(MinScale, MaxScale);
        currentScaleZ = Random.Range(MinScale, MaxScale);
        currentSpeed = Random.Range(minSpeed, maxSpeed);

        float x = Random.Range(-6, +6);
        transform.position = new Vector3(x, 7f, 0);
        transform.rotation = Quaternion.Euler(0, 0, 0);

        transform.localScale = new Vector3(currentScaleX, currentScaleY,currentScaleZ);

    }
   
}

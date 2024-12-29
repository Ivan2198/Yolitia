using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlatformPlacer : MonoBehaviour
{
    public GameObject platformPrefab;  
    public Transform cylinder;         
    public float radius = 5f;         
    public float minY = 0f;            
    public float maxY = 10f;         

    private Vector3 platformPreviewPosition; 
    private bool hasValidPosition = false; 

    private Vector3 screenPosition;
    private Vector3 worldPosition;

    [SerializeField] private Camera mainCamera;
    private float yPos;

    // Cooldown and Lifetime Settings
    [SerializeField] private float objectLifetime = 15f; 
    private float cooldownTime = 0f;                     
    private State state;

    private enum State
    {
        Active,      
        NotActive    
    }

    private void Start()
    {
        state = State.Active;  
    }

    void Update()
    {
        screenPosition = Input.mousePosition;
        worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
        yPos = worldPosition.y;

      
        UpdatePreviewPosition();

        
        switch (state)
        {
            case State.Active:
               
                if (Input.GetMouseButtonDown(0) && hasValidPosition)
                {
                    PlacePlatformAtMousePosition();
                    state = State.NotActive; 
                }
                break;

            case State.NotActive:
                
                cooldownTime += Time.deltaTime;

                
                if (cooldownTime >= objectLifetime)
                {
                    cooldownTime = 0f;   
                    state = State.Active; 
                }
                break;
        }
    }

    void UpdatePreviewPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Vector3 hitPoint = hit.point;
            Vector3 directionFromCenter = hitPoint - cylinder.position;
            float angle = Mathf.Atan2(directionFromCenter.z, directionFromCenter.x);

            float x = Mathf.Cos(angle) * radius;
            float z = Mathf.Sin(angle) * radius;

           
            float y = Mathf.Clamp(yPos, minY, maxY);

            platformPreviewPosition = new Vector3(x + cylinder.position.x, y, z + cylinder.position.z);
            hasValidPosition = true;
        }
        else
        {
            hasValidPosition = false;
        }
    }

    void PlacePlatformAtMousePosition()
    {
       
        GameObject platform = Instantiate(platformPrefab, platformPreviewPosition, Quaternion.identity);

       
        Vector3 directionToCenter = cylinder.position - platform.transform.position;
        float yRotation = Mathf.Atan2(directionToCenter.x, directionToCenter.z) * Mathf.Rad2Deg;

        
        platform.transform.rotation = Quaternion.Euler(0, yRotation, 0);

        
        Destroy(platform, objectLifetime);
    }
 

    public float GetCooldownTimer()
    {
        return  cooldownTime / objectLifetime;
    }

    void OnDrawGizmos()
    {
        if (hasValidPosition)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(platformPreviewPosition, 0.5f);

            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(cylinder.position, platformPreviewPosition);
        }
    }
}
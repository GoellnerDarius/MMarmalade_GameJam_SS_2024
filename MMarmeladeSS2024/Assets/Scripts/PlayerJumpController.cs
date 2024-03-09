using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpController : MonoBehaviour
{
    [SerializeField] private float _speed = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //todo remove
        float horizontalInput = UnityEngine.Input.GetAxis("Horizontal");
        float verticalInput = UnityEngine.Input.GetAxis("Vertical");
        transform.Translate(Vector3.right * (_speed * horizontalInput * Time.deltaTime));
        transform.Translate(Vector3.forward * (_speed * verticalInput * Time.deltaTime)); 
    }
}

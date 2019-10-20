using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ZoomTarget : MonoBehaviour {
	public Transform target;
	public float smoothTime = 0.3F;
	private Vector3 velocity = Vector3.zero;
	public static bool zoom = false;
	public static int target_tag = 0;
	public static string target_string = "Sun";
    public GameObject _detailsCanvas;
    public TMP_Text massText;
    public TMP_Text radiusText;
    public TMP_Text PopProbability;
    public TMP_Text TimePopulation;
    public static bool isPlaying;

    public Camera cam;
	public float turnSpeed = 4.0f;		
	public float panSpeed = 4.0f;		
	public float zoomSpeed = 4.0f;		
    

	private Vector3 mouseOrigin;	
	private bool isRotating;	
	private bool isZooming;
    private static bool isLooking;

    private void Start(){
        isPlaying = true;
        cam = GetComponent<Camera>();
        isLooking = false;
        target = GameObject.FindGameObjectWithTag("Sun").transform;
    }

    void Update () {
        if (Input.GetKey(KeyCode.W)){
            transform.Translate(transform.forward * 10f * Time.deltaTime);
            isPlaying = true;
            if (isLooking)
            {
                isLooking = false;
                _detailsCanvas.SetActive(false);
                target_tag = 0;
                target = null;
                isPlaying = true;
            }
        }
        if(Input.GetKey(KeyCode.S)){
            transform.Translate(-transform.forward * 10f * Time.deltaTime);
            isPlaying = true;
            if (isLooking){
                isLooking = false;
                _detailsCanvas.SetActive(false);
                target_tag = 0;
                target = null;
                isPlaying = true;
            }
        }
         if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * 10f * Time.deltaTime);
            isPlaying = true;
            if (isLooking)
            {
                isLooking = false;
                _detailsCanvas.SetActive(false);
                target_tag = 0;
                target = null;
                isPlaying = true;
            }
        }
         if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * 10f * Time.deltaTime);
            isPlaying = true;
            if (isLooking)
            {
                isLooking = false;
                _detailsCanvas.SetActive(false);
                target_tag = 0;
                target = null;
                isPlaying = true;
            }
        }


        if (Input.GetMouseButtonDown(0))
		{
			mouseOrigin = Input.mousePosition;
			isRotating = true;
		}

		/*if(Input.GetMouseButtonDown(1))
		{
			mouseOrigin = Input.mousePosition;
			isZooming = true;
		}*/

		if (!Input.GetMouseButton(0)) isRotating=false;
		if (!Input.GetMouseButton(1)) isZooming=false;

		if (isRotating){
			Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - mouseOrigin);
			transform.RotateAround(transform.position, transform.right, -pos.y * turnSpeed);
			transform.RotateAround(transform.position, Vector3.up, pos.x * turnSpeed);
		}
		if (isZooming){
			Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - mouseOrigin);
			Vector3 move = pos.y * zoomSpeed * transform.forward; 
			transform.Translate(move, Space.World);
		}

		if (Input.GetMouseButtonDown (1)) {
            if (isLooking){
                isLooking = false;
                _detailsCanvas.SetActive(false);
                target_tag = 0;
                target = null;
                isPlaying = true;
            }
            if (zoom) {
				zoom = false;
				target_tag = 0;
			}
		}
        if (!isLooking) return;
            Vector3 targetPosition = target.TransformPoint(new Vector3(0f, 0.1f, -2f));
            this.transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
    public void ShowDetails(Planet _selectedPlanet){
        isLooking = true;
         _detailsCanvas.SetActive(true);
        massText.text = "Mass : " + _selectedPlanet.mass.ToString() + "E24 KG";
        radiusText.text = "Radius : " +  _selectedPlanet.radius.ToString() + " KM";
        if(_selectedPlanet.LifeProb >= 0){
            if (_selectedPlanet.PopTime >= 0) TimePopulation.text = "Time Population : " + _selectedPlanet.PopTime + " years";
                PopProbability.text = "Live probability : " + _selectedPlanet.LifeProb + " %";
        }
        target = _selectedPlanet.transform;
        isPlaying = false;
    }
    public void CollideWithSeed(){
        target.gameObject.GetComponent<Planet>().CollideWithSeed();
    }
    public void Delete(){
        Destroy(target.gameObject);
        if (isLooking){
            isLooking = false;
            _detailsCanvas.SetActive(false);
            target_tag = 0;
            target = null;
            isPlaying = true;
        }
        if (zoom)
        {
            zoom = false;
            target_tag = 0;
        }
    }


}

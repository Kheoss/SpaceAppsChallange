using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CreatePlanetManager : MonoBehaviour
{

    public GameObject canvas1;
    public GameObject canvas2;
    public GameObject camera_main;
    public GameObject camera_creator;
    public GameObject camera_placer;
    public Slider mass_slider;
    public TMP_Text mass_text;
    public Slider radius_slider;
    public TMP_Text radius_text;
    public GameObject _planetPrefab;
    public GameObject _planetPrefabOriginal;
    public List<GameObject> PlanetsPrefabs = new List<GameObject>();
    public TMP_Dropdown dropDown;
    public GameObject preview_Planet;

    public Camera cam4;

    private Camera _positionTakerCamera;
    private GameObject _curPlanet;
    private float mass;
    private float radius;
    public static bool _isDragging;
    public List<GameObject> plantes = new List<GameObject>();
    public List<float> radiuses = new List<float>();
    public List<float> masses = new List<float>();
    public GameObject spawnPoint;

    private void Start(){
        _positionTakerCamera = camera_placer.GetComponent<Camera>();
        radius = radius_slider.value;
        float _constSizePerOneUnit = 243 * 2;
        float size = radius / _constSizePerOneUnit;
        preview_Planet.transform.localScale = new Vector3(size, size, size);
    }

    public void StartCreatingPlanet(){
        camera_creator.SetActive(false);
        cam4.gameObject.SetActive(true);
        camera_main.SetActive(false);
        canvas1.SetActive(false);
        canvas2.SetActive(false);
        mass = mass_slider.minValue;
        _isDragging = false;    
    }
    public void ChangeMass(){
        mass = mass_slider.value;
        mass_text.text = mass.ToString() + "E24 KG";
    }
    public void SetTemplate(int index){
        _planetPrefabOriginal = plantes[index];
        _planetPrefab = plantes[index];
        camera_creator.SetActive(true);
        cam4.gameObject.SetActive(false);
        canvas2.SetActive(true);
        mass = mass_slider.minValue;
        _isDragging = false;
        preview_Planet = Instantiate(plantes[index], spawnPoint.transform.position, Quaternion.identity);
        preview_Planet.transform.GetComponent<Planet>().enabled = false;
        preview_Planet.AddComponent<ShowroomPlanet>();
        mass = masses[index];
        radius = radiuses[index];
    }
    public void ChangeRadius(){
        radius = radius_slider.value;
        radius_text.text = radius.ToString() + " KM";

        float _constSizePerOneUnit = 243 * 2;
        float size = radius / _constSizePerOneUnit;
        preview_Planet.transform.localScale = new Vector3(size, size, size);
    }
    public void PlacePlanet(){
        canvas2.SetActive(false);
        camera_creator.SetActive(false);
        camera_main.SetActive(false);
        camera_placer.SetActive(true);
        _isDragging = true;
        _curPlanet = Instantiate(_planetPrefab, Vector3.zero, Quaternion.identity);
        float _constSizePerOneUnit = 2439 * 2;
        float size = radius / _constSizePerOneUnit;
        _curPlanet.transform.localScale = new Vector3(size, size, size);
        canvas1.gameObject.SetActive(true);
        Destroy(preview_Planet.gameObject);
    }
    private void Update(){
        if (_isDragging){
            Ray ray = _positionTakerCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray,out hit)){
                Vector3 _newPos = hit.point;
                _newPos.y = 0;
                _curPlanet.transform.position = _newPos;
            }
            if (Input.GetMouseButtonDown(0)){
                _isDragging = false;
                Destroy(_curPlanet);
                Vector3 _spawnPos = hit.point;
                _spawnPos.y = 0;
                GameObject _newPlanet = Instantiate(_planetPrefabOriginal,_spawnPos, Quaternion.identity);
                Planet _localPlanet = _newPlanet.GetComponent<Planet>();
                _localPlanet.mass = mass;
                _localPlanet.SetSize(radius);
                camera_placer.SetActive(false);
                camera_main.SetActive(true);
            }
        }
        if (Input.GetKeyDown(KeyCode.Space)){
            Destroy(preview_Planet);
            StartCreatingPlanet();
        }
    }

}

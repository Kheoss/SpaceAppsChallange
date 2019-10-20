using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

class NewPlanet
{
    public double Fc;
    public double LifeExpect;
    public string LifeForm;
    public double LifeProb;
    public double N0;
    public double Nb;
    public double Pa;
    public double Time;
    public bool Populated;
}
    //{"Fc":98.65202309611172,"LifeExpect":0.07172120680198078,"LifeForm":"Bacterie","LifeProb":93.15642494949032,"N0":990582.6696190401,"Nb":7.578866299973599,"Pa":95.02285450872023,"Time":6.979999999999896}
    [RequireComponent(typeof(Rigidbody))]
public class Planet : MonoBehaviour{
    public const float VelocityStandardFactorForBasicMassSun = 500;
    public float radius = 11.6f;
    public float speed = 0.8f;
    public float mass;
    public int LifeExpectacy;
    public string LifeForm;
    public long  N0;
    public int FC;
    public float NB;
    public int PA;
    public bool populated;
    public double PopTime;
    public GameObject _astPrefab;
    public double LifeProb;

    private float _velocity;
    private GameObject _sun;
    private ZoomTarget zoom;
    private Rigidbody _rb;
    private void Start(){
        _rb = gameObject.AddComponent<Rigidbody>();
        _rb.useGravity = false;
        GetComponent<SphereCollider>().isTrigger = true;

        PopTime = -1;
        populated = false;
        LifeForm = "Bacterie";
        PA = Random.Range(0, 100);
        FC = Random.Range(0, 100);
        N0 = 10000000 + Random.Range(-500000, 500000);
        NB = Random.Range(0f, 8f);
        LifeExpectacy = Random.Range(0, 50);

        zoom = FindObjectOfType<ZoomTarget>();
        _sun = GameObject.FindGameObjectWithTag("Sun");
        Vector3 _dir = _sun.transform.position - transform.position;
        float _distance = _dir.magnitude;
        _velocity = VelocityStandardFactorForBasicMassSun / _distance;
    }
    public void CollideWithSeed(){
        StartCoroutine(requestForLife());
    }
    IEnumerator requestForLife(){
        WWWForm form = new WWWForm();
        form.AddField("lifex", LifeExpectacy.ToString());
        form.AddField("lifey", "100");
        form.AddField("lifeformx", LifeForm);
        form.AddField("lifeformy", LifeForm);
        form.AddField("nbx", NB.ToString());
        form.AddField("nby", "8");
        form.AddField("n0x", N0.ToString());
        form.AddField("n0y", "1");
        form.AddField("fcx", FC.ToString());
        form.AddField("fcy", "100");
        form.AddField("pax", PA.ToString());
        form.AddField("pay", "100");

        UnityWebRequest www = UnityWebRequest.Post("http://31f7d9c6.ngrok.io", form);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError){
            Debug.Log(www.error);
        }
        else{
            string JSN = www.downloadHandler.text;
            NewPlanet pln = JsonUtility.FromJson<NewPlanet>(JSN);
            populated = pln.Populated;
            PopTime = pln.Time;
            LifeProb = pln.LifeProb;
        }
       /* GameObject _ast = Instantiate(_astPrefab, zoom.cam.transform.position + 20 * zoom.cam.transform.forward, Quaternion.identity);
        _ast.GetComponent<SeedOfLife>().target = this;*/
    }
    void Update(){
        if (!ZoomTarget.isPlaying) return;
        transform.RotateAround(Vector3.zero, Vector3.up, _velocity * Time.deltaTime);
    }
    private Vector3 GetPosition(float angle){
        return new Vector3(radius * Mathf.Sin(angle), 0, radius * Mathf.Cos(angle));
    }
    private void OnMouseDown(){
        zoom.ShowDetails(this);
    }
    public void SetSize(float radius){
        float _constSizePerOneUnit = 2439 * 2;
        float size = radius / _constSizePerOneUnit;
        transform.localScale = new Vector3(size, size, size);
    }
    private void OnTriggerEnter(Collider collision){
        Planet planet = collision.gameObject.GetComponent<Planet>();
        if (planet != null){
            if (CreatePlanetManager._isDragging) return;
            if (planet.mass < mass)
                Destroy(planet.gameObject);
            else Destroy(gameObject);
        }
        else{
            Debug.Log("planeta nu a fost gasita");
        }
    }
}
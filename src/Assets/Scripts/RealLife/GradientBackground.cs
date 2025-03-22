using UnityEngine;
using System.Collections;

public class GradientBackground : MonoBehaviour
{

    public Color topColorGround = new Color32(122, 122, 122, 255);
    public Color bottomColorGround = Color.black;
    public int gradientLayer = 7;

    public Color topColorSky = Color.blue;
    public Color bottomColorSky = new Color32(122, 122, 122, 255);

    private GameObject gradientPlane;
    private GameObject gradientPlane2;

    void Awake()
    {
        gradientLayer = Mathf.Clamp(gradientLayer, 0, 31);
        if (!camera)
        {
            Debug.LogError("Must attach GradientBackground script to the camera");
            return;
        }

        camera.clearFlags = CameraClearFlags.Depth;
        camera.cullingMask = camera.cullingMask & ~(1 << gradientLayer);
        Camera gradientCam = new GameObject("Gradient Cam", typeof(Camera)).camera;
        gradientCam.depth = camera.depth - 1;
        gradientCam.cullingMask = 1 << gradientLayer;
        gradientCam.backgroundColor = new Color32(122, 122, 122, 255);

        //Ground
        Mesh mesh = new Mesh();
        mesh.vertices = new Vector3[4] { new Vector3(-100f, .0f, 1f), new Vector3(100f, .0f, 1f), new Vector3(-100f, -.577f, 1f), new Vector3(100f, -.577f, 1f) };
        mesh.colors = new Color[4] { topColorGround, topColorGround, bottomColorGround, bottomColorGround };
        mesh.triangles = new int[6] { 0, 1, 2, 1, 3, 2 };

        //Sky
        Mesh mesh2 = new Mesh();
        mesh2.vertices = new Vector3[4] { new Vector3(-100f, .577f, 1f), new Vector3(100f, .577f, 1f), new Vector3(-100f, .0f, 1f), new Vector3(100f, .0f, 1f) };
        mesh2.colors = new Color[4] { topColorSky, topColorSky, bottomColorSky, bottomColorSky };
        mesh2.triangles = new int[6] { 0, 1, 2, 1, 3, 2 };

        Material mat = new Material("Shader \"Vertex Color Only\"{Subshader{BindChannels{Bind \"vertex\", vertex Bind \"color\", color}Pass{}}}");
        
        gradientPlane = new GameObject("Gradient Plane", typeof(MeshFilter), typeof(MeshRenderer));
        ((MeshFilter)gradientPlane.GetComponent(typeof(MeshFilter))).mesh = mesh;
        gradientPlane.renderer.material = mat;
        gradientPlane.layer = gradientLayer;

        gradientPlane2 = new GameObject("Gradient Plane2", typeof(MeshFilter), typeof(MeshRenderer));
        ((MeshFilter)gradientPlane2.GetComponent(typeof(MeshFilter))).mesh = mesh2;
        gradientPlane2.renderer.material = mat;
        gradientPlane2.layer = gradientLayer;

    }

    void Update()
    {
		((MeshFilter)gradientPlane2.GetComponent(typeof(MeshFilter))).mesh.colors = new Color[4] { topColorSky, topColorSky, bottomColorSky, bottomColorSky };
		((MeshFilter)gradientPlane.GetComponent(typeof(MeshFilter))).mesh.colors = new Color[4] { topColorGround, topColorGround, bottomColorGround, bottomColorGround };
	}

}
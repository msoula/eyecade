using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_STANDALONE_LINUX
#else
using Tobii.Gaming;
#endif

public class Eyex : MonoBehaviour {

    [Tooltip("Sprite to use for gaze points in the point cloud.")]
    public Sprite PointSprite;
    [Tooltip("Distance from screen to visualization plane in the World.")]
    public float VisualizationDistance = 10f;
    [Range(0.1f, 1.0f), Tooltip("How heavy filtering to apply to gaze point bubble movements. 0.1f is most responsive, 1.0f is least responsive.")]
    public float FilterSmoothingFactor = 0.15f;

    private float _pauseTimer;
    private bool _hasHistoricPoint;
    private Vector3 _historicPoint;

    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = PointSprite;
    }

#if UNITY_STANDALONE_LINUX
    void Update()
    {
        transform.position = (Vector2) Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
#else
    void Update()
    {
        if (_pauseTimer > 0)
        {
            _pauseTimer -= Time.deltaTime;
            return;
        }

        GazePoint gazePoint = TobiiAPI.GetGazePoint();
        if (gazePoint.IsRecent())
        {
            Vector3 gazePointInWorld = ProjectToPlaneInWorld(gazePoint);
            transform.position = Smoothify(gazePointInWorld);
            _pauseTimer = 0.1f;
        }
    }

    private Vector3 ProjectToPlaneInWorld(GazePoint gazePoint)
    {
        Vector3 gazeOnScreen = gazePoint.Screen;
        gazeOnScreen += (transform.forward * VisualizationDistance);
        return Camera.main.ScreenToWorldPoint(gazeOnScreen);
    }

    private Vector3 Smoothify(Vector3 point)
    {
        if (!_hasHistoricPoint)
        {
            _historicPoint = point;
            _hasHistoricPoint = true;
        }

        var smoothedPoint = new Vector3(
            point.x * (1.0f - FilterSmoothingFactor) + _historicPoint.x * FilterSmoothingFactor,
            point.y * (1.0f - FilterSmoothingFactor) + _historicPoint.y * FilterSmoothingFactor,
            point.z * (1.0f - FilterSmoothingFactor) + _historicPoint.z * FilterSmoothingFactor);

        _historicPoint = smoothedPoint;

        return smoothedPoint;
    }
#endif
}

/******************************************************************************
 * File         : pLab_3DViewerManager.cs
 * Author       : Toni Westerlund (toni.westerlund@lapinamk.fi)
 * Lisence      : MIT Licence
 * Copyright    : Lapland University of Applied Sciences
 * 
 * MIT License
 * 
 * Copyright (c) 2019 Lapland University of Applied Sciences
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 *****************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;


/// <summary>
/// pLab_VRTK3DObjectViewer
/// </summary>
public class pLab_3DViewerManager : MonoBehaviour {
    #region // SerializeField

    /// <summary>
    /// listOfObjects
    /// </summary>
    [SerializeField] private List<GameObject> listOfObjects = new List<GameObject>();


    /// <summary>
    /// viewCamera
    /// </summary>
    [SerializeField] private Camera viewCamera;

    #endregion

    #region // Private Attributes

    /// <summary>
    /// currentIndex
    /// </summary>
    private int currentIndex = 0;

    /// <summary>
    /// objectOrigo
    /// </summary>
    private Vector3 objectOrigo = new Vector3();

    /// <summary>
    /// Object Pivot Point
    /// </summary>
    private GameObject pivotPoint = null;

    /// <summary>
    /// zoomValue
    /// </summary>
    private float zoomValue = 1;


    /// <summary>
    /// origoDiff
    /// </summary>
    private Vector3 origoDiff = new Vector3();

    /// <summary>
    /// xRot
    /// </summary>
    private float xRot = 0;

    /// <summary>
    /// yRot
    /// </summary>
    private float yRot = 0;

    /// <summary>
    /// defaultZ
    /// </summary>
    private float defaultZ = 0f;

    #endregion

    #region // Public Attributes

    /// <summary>
    /// instance
    /// </summary>
    public static pLab_3DViewerManager instance = null;

    /// <summary>
    /// usePivot
    /// </summary>
    public bool usePivot = false;


    #endregion

    #region // Protected Attributes

    #endregion

    #region // Set/Get

    #endregion

    #region // Base Class Methods

    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    private void OnEnable() {
    }

    /// <summary>
    /// This function is called when the behaviour becomes disabled.
    /// </summary>
    private void OnDisable() {

    }

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake() {
        if (null == instance) {
            instance = this;
        }


        if (null == viewCamera) {
            Debug.LogError("pLab_3DViewerManager -> Awake() --> viewCamera is NULL");
            this.gameObject.SetActive(false);
            return;
        }

        currentIndex = 0;
        if (0 == listOfObjects.Count) {
            return;
        }

        foreach(GameObject o in listOfObjects) {
            o.SetActive(false);
        }
        ActivateCurrent();




    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled
    /// </summary>
    private void Update() {

    }

    #endregion

    #region // Private Methods

    /// <summary>
    /// ActivateCurrent
    /// </summary>
    private void ActivateCurrent() {
        listOfObjects[currentIndex].SetActive(true);

        if (usePivot) {
            UsePivot();
        }
    }

    /// <summary>
    /// EnableCurrent
    /// </summary>
    private void DisableCurrent() {
        listOfObjects[currentIndex].SetActive(false);
    }


    /// <summary>
    /// UsePivot()
    /// </summary>
    private void UsePivot() {
        if (null == pivotPoint) {
            pivotPoint = new GameObject("Pivot Point");
        }
        pLab_3DViewerManager.instance.GetCurrentObject().transform.SetParent(pivotPoint.transform);
        pLab_3DViewerManager.instance.GetCurrentObject().transform.localPosition = GetObjectCenter() * -1f;
        pivotPoint.transform.localPosition = GetObjectCenter();
    }

    #endregion

    #region // Public Methods


    /// <summary>
    /// RotateObject
    /// </summary>
    /// <param name="aDeltaPosition"></param>
    public void RotateObject(Vector3 aDeltaPosition) {
        xRot += Time.deltaTime * 50 * aDeltaPosition.y;
        yRot -= Time.deltaTime * 50 * aDeltaPosition.x;
        PivotPointObject().transform.rotation = Quaternion.Euler(xRot, yRot, 0);
    }

    /// <summary>
    /// ActivateNext
    /// </summary>
    public void ActivateNext() {
        DisableCurrent();
        currentIndex++;

        if (currentIndex >= listOfObjects.Count) {
            currentIndex = 0;
        }
        ActivateCurrent();
    }

    /// <summary>
    /// GetCurrentObject
    /// </summary>
    /// <returns></returns>
    public GameObject GetCurrentObject() {

        if (0 == listOfObjects.Count)
            return null;
        return listOfObjects[currentIndex];
    }

    /// <summary>
    /// GetObjectCenter
    /// </summary>
    /// <returns></returns>
    public Vector3 GetObjectCenter() {
        Bounds bounds = GetCurrentObject().GetComponent<MeshRenderer>().bounds;
        return bounds.center;
    }

    /// <summary>
    /// PivotPointObject
    /// </summary>
    /// <returns></returns>
    public GameObject PivotPointObject() {
        return pivotPoint;
    }


    /// <summary>
    /// RotateCameraMode
    /// </summary>
    /// <param name="aPrevPos"></param>
    public void RotateCameraMode(Vector3 aPrevPos) {
        Vector3 deltaPosition = Input.mousePosition - aPrevPos;
        xRot -= Time.deltaTime * 50 * deltaPosition.y;
        yRot += Time.deltaTime * 50 * deltaPosition.x;

        viewCamera.gameObject.transform.rotation = Quaternion.Euler(xRot, yRot, 0);
    }


    /// <summary>
    /// PanObject
    /// </summary>
    /// <param name="aPrevPos"></param>
    public void PanObject(Vector3 aPrevPos) {
        Vector3 deltaPosition = Input.mousePosition - aPrevPos;
        origoDiff.x -= (deltaPosition.x / 50f) * Time.deltaTime;
        origoDiff.y += (deltaPosition.y / 50f) * Time.deltaTime;
    }


    /// <summary>
    /// UpdateCamera
    /// </summary>
    /// <param name="aZoomValue"></param>
    public void UpdateCamera(float aZoomValue) {
        viewCamera.transform.position = new Vector3(viewCamera.transform.position.x, viewCamera.transform.position.y, defaultZ * aZoomValue);
        pivotPoint.transform.position = origoDiff;
    }


    /// <summary>
    /// SetCamera
    /// </summary>
    /// <param name="aZoomValue"></param>
    public void SetCamera(float aZoomValue) {
        Bounds bounds = pLab_3DViewerManager.instance.GetCurrentObject().GetComponent<MeshRenderer>().bounds;
        objectOrigo = bounds.center;
        float distanceFactor = 2.0f;
        Vector3 objectSizes = bounds.max - bounds.min;
        float objectSize = Mathf.Max(objectSizes.x, objectSizes.y, objectSizes.z);
        float cameraView = 2.0f * Mathf.Tan(0.5f * Mathf.Deg2Rad * viewCamera.fieldOfView);
        float distance = aZoomValue * distanceFactor * objectSize / cameraView;
        viewCamera.transform.position = (bounds.center + origoDiff) - distance * viewCamera.transform.forward;
        defaultZ = viewCamera.transform.position.z;
    }


    #endregion
}

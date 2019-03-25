/******************************************************************************
 * File         : pLab_VRViewerControls.cs
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
/// Rotation Type
/// </summary>
public enum ViewType {
    // Rotate Camera 
    ERotateCamera,
    // Rotate Object
    ERotateObject,
    // Simple VR Mode
    ESimpleVRMode
};

/// <summary>
/// pLab_VRViewerControls
/// </summary>
public class pLab_VRViewerControls : MonoBehaviour
{
    #region // SerializeField


    /// <summary>
    /// View Type
    /// </summary>
    [SerializeField] private ViewType viewType = ViewType.ERotateCamera;


    /// <summary>
    /// Main Camera
    /// </summary>
    [SerializeField] private Camera viewCamera;

    /// <summary>
    /// Zoom Factor
    /// </summary>
    [SerializeField] private float mouseScrollFactor = 0.1f;

    /// <summary>
    /// Max Zoom Value
    /// </summary>
    [SerializeField] private float maxZoomValue = 10f;

    /// <summary>
    /// Min Zoom Value
    /// </summary>
    [SerializeField] private float minZoomValue = 0f;

    #endregion

    #region // Private Attributes
    /// <summary>
    /// Default Zoom Value
    /// </summary>
    private float zoomValue = 1;

    /// <summary>
    /// X-Axis Rotation
    /// </summary>
    private float xRot = 0f;

    /// <summary>
    /// Y-Axis Rotation
    /// </summary>
    private float yRot = 0f;

    /// <summary>
    /// Previous Mouse position
    /// </summary>
    private Vector3 prevMousePosition = new Vector3();

    /// <summary>
    /// tmp Origo point
    /// </summary>
    private Vector3 origoDiff = new Vector3();

    /// <summary>
    /// Object Oritgo
    /// </summary>
    private Vector3 objectOrigo = new Vector3();


    /// <summary>
    /// defaultZ
    /// </summary>
    private float defaultZ = 0f;


    #endregion

    #region // Public Attributes
    public static pLab_VRViewerControls instance = null;
    #endregion

    #region // Protected Attributes

    #endregion

    #region // Set/Get

    #endregion

    #region // Base Class Methods

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake() {

        if (null == instance) {
            instance = this;
        }

        if (null == viewCamera){
            Debug.LogError("pLab_VRViewerControls -> Awake() --> viewCamera is NULL");
            this.gameObject.SetActive(false);
            return;
        }
        if (null == pLab_3DViewerManager.instance.GetCurrentObject()) {
            Debug.LogError("pLab_VRViewerControls -> Awake() --> objectToView is NULL");
            this.gameObject.SetActive(false);
            return;
        }



        if (ViewType.ERotateCamera == viewType){
            SetCamera();
        } else if (ViewType.ESimpleVRMode == viewType) {
            SetCamera();
            pLab_3DViewerManager.instance.usePivot = true;

        } else {
            SetCamera();
            pLab_3DViewerManager.instance.usePivot = true;
        }
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled
    /// </summary>
    private void Update() {


        if(ViewType.ERotateCamera == viewType) {
            zoomValue += Input.mouseScrollDelta.y * mouseScrollFactor;
            if (zoomValue < minZoomValue) {
                zoomValue = minZoomValue;
            } else if (zoomValue > maxZoomValue) {
                zoomValue = maxZoomValue;
            }
            RotateCameraMode();
            SetCamera();
        }
        else if (ViewType.ERotateObject == viewType) {
            zoomValue += Input.mouseScrollDelta.y * mouseScrollFactor;
            if (zoomValue < minZoomValue) {
                zoomValue = minZoomValue;
            } else if (zoomValue > maxZoomValue) {
                zoomValue = maxZoomValue;
            }

            if (Input.GetMouseButtonDown(0)) {
                prevMousePosition = Input.mousePosition;
            }

            if (Input.GetMouseButton(0)) {
                Vector3 deltaPosition = Input.mousePosition - prevMousePosition;
                RotateObject(deltaPosition);
            }

            
            UpdateCamera();
        }
    }

    #endregion

    #region // Private Methods
    

    /// <summary>
    /// RotateObject
    /// </summary>
    public void RotateObject(Vector3 aDeltaPosition) {
        xRot += Time.deltaTime * 50 * aDeltaPosition.y;
        yRot -= Time.deltaTime * 50 * aDeltaPosition.x;
        pLab_3DViewerManager.instance.PivotPointObject().transform.rotation = Quaternion.Euler(xRot, yRot, 0);
        prevMousePosition = Input.mousePosition;
    }

    /// <summary>
    /// RotateCameraMode
    /// </summary>
    private void RotateCameraMode() {

        if (Input.GetMouseButtonDown(0)){
            prevMousePosition = Input.mousePosition;
        }
        if (Input.GetMouseButtonDown(2)){
            prevMousePosition = Input.mousePosition;
        }
        if (Input.GetMouseButton(2)){
            Vector3 deltaPosition = Input.mousePosition - prevMousePosition;

            origoDiff.x -= (deltaPosition.x / 50f) * Time.deltaTime;
            origoDiff.y += (deltaPosition.y / 50f) * Time.deltaTime;
            prevMousePosition = Input.mousePosition;
        }

        if (Input.GetMouseButton(0)){
            Vector3 deltaPosition = Input.mousePosition - prevMousePosition;
            xRot -= Time.deltaTime * 50 * deltaPosition.y;
            yRot += Time.deltaTime * 50 * deltaPosition.x;

            viewCamera.gameObject.transform.rotation = Quaternion.Euler(xRot, yRot, 0);

            prevMousePosition = Input.mousePosition;
        }
    }


    /// <summary>
    /// Set Camera to correct Position
    /// </summary>
    private void SetCamera() {

        Bounds bounds = pLab_3DViewerManager.instance.GetCurrentObject().GetComponent<MeshRenderer>().bounds;
        objectOrigo = bounds.center;

        float distanceFactor = 2.0f; 
        Vector3 objectSizes = bounds.max - bounds.min;
        float objectSize = Mathf.Max(objectSizes.x, objectSizes.y, objectSizes.z);
        float cameraView = 2.0f * Mathf.Tan(0.5f * Mathf.Deg2Rad * viewCamera.fieldOfView);
        float distance = zoomValue * distanceFactor * objectSize / cameraView;
        viewCamera.transform.position = (bounds.center+ origoDiff) - distance * viewCamera.transform.forward;
        defaultZ = viewCamera.transform.position.z;

        Debug.LogError(defaultZ);
    }

    /// <summary>
    /// UpdateCamera
    /// </summary>
    private void UpdateCamera() {

        viewCamera.transform.position = new Vector3(viewCamera.transform.position.x, viewCamera.transform.position.y, defaultZ * zoomValue);
    }

    #endregion

    #region // Public Methods

    #endregion
}

/******************************************************************************
 * File         : pLab_VRViewerControls.cs
 * Author       : Toni Westerlund (toni.westerlund@lapinamk.fi)
 * Lisence      : BSD Licence
 * Copyright    : Lapland University of Applied Sciences
 * 
 * Copyright (c) 2019, Lapland University of Applied Sciences
 * All rights reserved.
 * Redistribution and use in source and binary forms, with or without
 * modification, are permitted provided that the following conditions are met:
 *
 *     * Redistributions of source code must retain the above copyright
 *       notice, this list of conditions and the following disclaimer.
 *     * Redistributions in binary form must reproduce the above copyright
 *       notice, this list of conditions and the following disclaimer in the
 *       documentation and/or other materials provided with the distribution.
 *     * Neither the name of the Lapland University of Applied Sciences nor the
 *       names of its contributors may be used to endorse or promote products
 *       derived from this software without specific prior written permission.
 *
 * THIS SOFTWARE IS PROVIDED BY THE REGENTS AND CONTRIBUTORS ``AS IS'' AND ANY
 * EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
 * WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
 * DISCLAIMED. IN NO EVENT SHALL THE REGENTS AND CONTRIBUTORS BE LIABLE FOR ANY
 * DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
 * (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
 * LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
 * ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
 * (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
 * SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 *****************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Rotation Type
/// </summary>
public enum ViewType {
    // Rotate Camera 
    ERotateCamera,
    // Rotate Object
    ERotateObject
};

/// <summary>
/// pLab_VRViewerControls
/// </summary>
public class pLab_VRViewerControls : MonoBehaviour
{
    #region // SerializeField

    /// <summary>
    /// Object To View
    /// </summary>
    [SerializeField] private GameObject objectToView;

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
    /// Object Pivot Point
    /// </summary>
    private GameObject pivotPoint;

    #endregion

    #region // Public Attributes

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
        if(null == viewCamera)
        {
            Debug.LogError("pLab_VRViewerControls -> Awake() --> viewCamera is NULL");
            this.gameObject.SetActive(false);
            return;
        }

        if (null == objectToView)
        {
            Debug.LogError("pLab_VRViewerControls -> Awake() --> objectToView is NULL");
            this.gameObject.SetActive(false);
            return;
        }

        SetCamera();

        if (ViewType.ERotateCamera == viewType){
        }
        else{
            pivotPoint = new GameObject("Pivot Point");
            objectToView.transform.SetParent(pivotPoint.transform);
            objectToView.transform.localPosition = objectOrigo;
        }
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled
    /// </summary>
    private void Update() {
        zoomValue += Input.mouseScrollDelta.y * mouseScrollFactor;
        if(zoomValue < minZoomValue){
            zoomValue = minZoomValue;
        }
        else if (zoomValue > maxZoomValue){
            zoomValue = maxZoomValue;
        }

        if(ViewType.ERotateCamera == viewType) {
            RotateCameraMode();
            SetCamera();
        }
        else{
            RotateObject();

        }
    }

    #endregion

    #region // Private Methods

    /// <summary>
    /// RotateObject
    /// </summary>
    private void RotateObject() {

        if (Input.GetMouseButtonDown(0)){
            prevMousePosition = Input.mousePosition;
        }
        if (Input.GetMouseButtonDown(2)){
            prevMousePosition = Input.mousePosition;
        }
        if (Input.GetMouseButton(2)){
            Vector3 deltaPosition = Input.mousePosition - prevMousePosition;
            prevMousePosition = Input.mousePosition;
        }
        if (Input.GetMouseButton(0)){
            Vector3 deltaPosition = Input.mousePosition - prevMousePosition;
            xRot += Time.deltaTime * 50 * deltaPosition.y;
            yRot -= Time.deltaTime * 50 * deltaPosition.x;

            pivotPoint.gameObject.transform.rotation = Quaternion.Euler(xRot, yRot, 0);
            prevMousePosition = Input.mousePosition;
        }
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
        MeshRenderer meshR =  objectToView.GetComponent<MeshRenderer>();

        if (null == meshR) {
            Debug.LogError("pLab_VRViewerControls -> SetCamera() --> MeshRenderer is NULL");
            this.gameObject.SetActive(false);
            return;
        }
        Bounds bounds = objectToView.GetComponent<MeshRenderer>().bounds;
        objectOrigo = bounds.center;

        float distanceFactor = 2.0f; 
        Vector3 objectSizes = bounds.max - bounds.min;
        float objectSize = Mathf.Max(objectSizes.x, objectSizes.y, objectSizes.z);
        float cameraView = 2.0f * Mathf.Tan(0.5f * Mathf.Deg2Rad * viewCamera.fieldOfView);
        float distance = zoomValue * distanceFactor * objectSize / cameraView;
        viewCamera.transform.position = (bounds.center+ origoDiff) - distance * viewCamera.transform.forward;

    }

    #endregion

    #region // Public Methods

    #endregion
}

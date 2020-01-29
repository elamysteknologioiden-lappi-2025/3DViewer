/******************************************************************************
 * File         : pLab_VRViewerControls.cs
 * Author       : Toni Westerlund (toni.westerlund@lapinamk.fi)
 * Lisence      : MIT Licence
 * Copyright    : Lapland University of Applied Sciences, Lapin AMK
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
    /// Previous Mouse position
    /// </summary>
    private Vector3 prevMousePosition = new Vector3();
    #endregion

    #region // Public Attributes

    #endregion

    #region // Protected Attributes

    #endregion

    #region // Set/Get

    #endregion

    #region // Base Class Methods

    private void Awake() {

        if (null == pLab_3DViewerManager.instance.GetCurrentObject()) {
            Debug.LogError("pLab_VRViewerControls -> Awake() --> objectToView is NULL");
            this.gameObject.SetActive(false);
            return;
        }

        if (ViewType.ERotateCamera == viewType){
            pLab_3DViewerManager.instance.SetCamera(zoomValue);
        } else if (ViewType.ERotateObject == viewType) {
            pLab_3DViewerManager.instance.SetCamera(zoomValue);
            pLab_3DViewerManager.instance.usePivot = true;
        } else {

        }
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled
    /// </summary>
    private void Update() {
        if(ViewType.ERotateCamera == viewType) {
            // Zoom value
            zoomValue += Input.mouseScrollDelta.y * mouseScrollFactor;
            if (zoomValue < minZoomValue) {
                zoomValue = minZoomValue;
            } else if (zoomValue > maxZoomValue) {
                zoomValue = maxZoomValue;
            }

            if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(2)) {
                prevMousePosition = Input.mousePosition;
            }
            if (Input.GetMouseButton(0)) {
                pLab_3DViewerManager.instance.RotateCameraMode(prevMousePosition);
                prevMousePosition = Input.mousePosition;
            }
            if (Input.GetMouseButton(2)) {
                pLab_3DViewerManager.instance.PanObject(prevMousePosition);
                prevMousePosition = Input.mousePosition;
            }
            pLab_3DViewerManager.instance.SetCamera(zoomValue);
        }
        else if (ViewType.ERotateObject == viewType) {
            // Zoom value
            zoomValue += Input.mouseScrollDelta.y * mouseScrollFactor;
            if (zoomValue < minZoomValue) {
                zoomValue = minZoomValue;
            } else if (zoomValue > maxZoomValue) {
                zoomValue = maxZoomValue;
            }

            if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(2)) {
                prevMousePosition = Input.mousePosition;
            }
            if (Input.GetMouseButton(0)) {
                Vector3 deltaPosition = Input.mousePosition - prevMousePosition;
                pLab_3DViewerManager.instance.RotateObject(deltaPosition);
                prevMousePosition = Input.mousePosition;
            }
            if (Input.GetMouseButton(2)) {
                pLab_3DViewerManager.instance.PanObject(prevMousePosition);
                prevMousePosition = Input.mousePosition;
            }
            pLab_3DViewerManager.instance.UpdateCamera(zoomValue);
        }
    }

    #endregion

    #region // Private Methods
    
    #endregion

    #region // Public Methods

    #endregion
}


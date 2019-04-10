/******************************************************************************
 * File         : pLab_VRTK3DObjectViewer.cs
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
public class pLab_VRTK3DObjectViewer : MonoBehaviour {
    #region // SerializeField


    #endregion

    #region // Private Attributes
    #endregion

    #region // Public Attributes

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



       // GetComponent<VRTK_ControllerEvents>().TriggerAxisChanged += new ControllerInteractionEventHandler(DoTriggerAxisChanged);
       /* GetComponent<VRTK_ControllerEvents>().TouchpadAxisChanged += new ControllerInteractionEventHandler(DoTouchpadAxisChanged);

        GetComponent<VRTK_ControllerEvents>().TriggerReleased += new ControllerInteractionEventHandler(DoTriggerReleased);
        GetComponent<VRTK_ControllerEvents>().TouchpadTouchEnd += new ControllerInteractionEventHandler(DoTouchpadTouchEnd);

        GetComponent<VRTK_ControllerEvents>().ButtonTwoPressed += new ControllerInteractionEventHandler(DoCarReset);*/
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
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled
    /// </summary>
    private void Update() {

    }

    #endregion

    #region // Private Methods

    

    #endregion

    #region // Public Methods

    #endregion
}

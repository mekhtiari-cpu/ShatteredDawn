/*
 * ====================================================================================
 * Rotate.cs
 * Author: Nathanael Hondi
 * Copyright: © 2022 NPIREEXE. All rights reserved.
 * ====================================================================================
 */
// Mote* This is my own script that i've made.
using UnityEngine;

namespace NPIREEXE.CORE{
    public class Rotate : MonoBehaviour
    {
        public AxisConfig[] axisConfigs = new AxisConfig[0];

        private AxisConfig x, y, z;

        private void OnValidate()
        {
            if(axisConfigs.Length == 3)
            {
                return;
            }

            // Ensure the list always contains "X", "Y", and "Z" at initialisation
            if (axisConfigs.Length != 3)
            {
                axisConfigs = new AxisConfig[3];
            }

            axisConfigs[0] = new AxisConfig("X");
            axisConfigs[1] = new AxisConfig("Y");
            axisConfigs[2] = new AxisConfig("Z");
        }

        private void Start()
        {
            if (axisConfigs.Length == 3)
            {
                x = axisConfigs[0];
                y = axisConfigs[1];
                z = axisConfigs[2];
            }
        }

        void Update()
        {
            if(TryGetComponent<RectTransform>(out RectTransform rectTransform))
            {
                rectTransform.Rotate(new Vector3(
                x.clockwise ? -x.value : x.value,
                y.clockwise ? -y.value : y.value,
                z.clockwise ? -z.value : z.value)
                * Time.deltaTime);
            }
            else
            {
                transform.Rotate(new Vector3(
                x.clockwise ? -x.value : x.value,
                y.clockwise ? -y.value : y.value,
                z.clockwise ? -z.value : z.value)
                * Time.deltaTime);
            }  
        }
    }

    public enum AXIS_ENUM
    {
        X, Y, Z
    }

    [System.Serializable]
    public class AxisConfig
    {
        [Tooltip("The label (e.g., \"X\", \"Y\", \"Z\")")]
        public string axis;
        [Tooltip("Whether it rotates clockwise")]
        public bool clockwise = true;
        [Tooltip("How much does it rotate by")]
        public int value;

        public AxisConfig(string ax)
        {
            axis = ax;
        }

        public AxisConfig(string axis, bool clockwise, int value) : this(axis)
        {
            this.clockwise = clockwise;
            this.value = value;
        }
    }
}

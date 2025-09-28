﻿using System;
using UnityEditor;
using UnityEngine;

namespace CW.Common
{
    /// <summary>This component will change the light intensity based on the current render pipeline.</summary>
    [ExecuteInEditMode]
    [RequireComponent(typeof(Light))]
    [AddComponentMenu("CW/Common/CW Light Intensity")]
    public class CwLightIntensity : MonoBehaviour
    {
        /// <summary>All light values will be multiplied by this before use.</summary>
        public float Multiplier
        {
            set => multiplier = value;
            get => multiplier;
        }

        [SerializeField] private float multiplier = 1.0f;

        /// <summary>
        ///     This allows you to control the intensity of the attached light when using the <b>Standard</b> rendering pipeline.
        ///     -1 = The attached light intensity will not be modified.
        /// </summary>
        public float IntensityInStandard
        {
            set => intensityInStandard = value;
            get => intensityInStandard;
        }

        [SerializeField] private float intensityInStandard = 1.0f;

        /// <summary>
        ///     This allows you to control the intensity of the attached light when using the <b>URP</b> rendering pipeline.
        ///     -1 = The attached light intensity will not be modified.
        /// </summary>
        public float IntensityInURP
        {
            set => intensityInURP = value;
            get => intensityInURP;
        }

        [SerializeField] private float intensityInURP = 1.0f;

        /// <summary>
        ///     This allows you to control the intensity of the attached light when using the <b>HDRP</b> rendering pipeline.
        ///     -1 = The attached light intensity will not be modified.
        /// </summary>
        public float IntensityInHDRP
        {
            set => intensityInHDRP = value;
            get => intensityInHDRP;
        }

        [SerializeField] private float intensityInHDRP = 120000.0f;

        [NonSerialized] private Light cachedLight;

        [NonSerialized] private bool cachedLightSet;

#if __HDRP__
		[System.NonSerialized]
		private UnityEngine.Rendering.HighDefinition.HDAdditionalLightData cachedLightData;
#endif

        public Light CachedLight
        {
            get
            {
                if (!cachedLightSet)
                {
                    cachedLight = GetComponent<Light>();
                    cachedLightSet = true;
                }

                return cachedLight;
            }
        }

        protected virtual void Update()
        {
            if (CwHelper.IsBIRP)
                ApplyIntensity(intensityInStandard);
            else if (CwHelper.IsURP)
                ApplyIntensity(intensityInURP);
            else if (CwHelper.IsHDRP) ApplyIntensity(intensityInHDRP);
        }

        private void ApplyIntensity(float intensity)
        {
            if (intensity >= 0.0f)
            {
                if (!cachedLightSet)
                {
                    cachedLight = GetComponent<Light>();
                    cachedLightSet = true;
                }

#if __HDRP__
					if (cachedLightData == null)
					{
						cachedLightData = GetComponent<UnityEngine.Rendering.HighDefinition.HDAdditionalLightData>();
					}

					if (cachedLightData != null)
					{
						cachedLightData.SetIntensity(intensity * multiplier, UnityEngine.Rendering.HighDefinition.LightUnit.Lux);
					}
#else
                cachedLight.intensity = intensity * multiplier;
#endif
            }
        }
    }
}

#if UNITY_EDITOR
namespace CW.Common
{
    using TARGET = CwLightIntensity;

    [CanEditMultipleObjects]
    [CustomEditor(typeof(TARGET))]
    public class P3dLight_Editor : CwEditor
    {
        protected override void OnInspector()
        {
            Draw("multiplier", "All light values will be multiplied by this before use.");
            Draw("intensityInStandard",
                "This allows you to control the intensity of the attached light when using the Standard rendering pipeline.\n\n-1 = The attached light intensity will not be modified.");
            Draw("intensityInURP",
                "This allows you to control the intensity of the attached light when using the URP rendering pipeline.\n\n-1 = The attached light intensity will not be modified.");
            Draw("intensityInHDRP",
                "This allows you to control the intensity of the attached light when using the HDRP rendering pipeline.\n\n-1 = The attached light intensity will not be modified.");
        }
    }
}
#endif
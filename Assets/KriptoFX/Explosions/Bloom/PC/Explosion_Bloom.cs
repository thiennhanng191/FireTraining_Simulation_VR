using System;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;


[ExecuteInEditMode]
    [RequireComponent(typeof(Camera))]
    [AddComponentMenu("KriptoFX/Explosion_Bloom")]
#if UNITY_5_4_OR_NEWER
    [ImageEffectAllowedInSceneView]
#endif
    public class Explosion_Bloom : MonoBehaviour
    {
        [Serializable]
        public struct Settings
        {
            [SerializeField]
            [Tooltip("Filters out pixels under this level of brightness.")]
            public float threshold;

            public float thresholdGamma
            {
                set { threshold = value; }
                get { return Mathf.Max(0.0f, threshold); }
            }

            public float thresholdLinear
            {
                set { threshold = Mathf.LinearToGammaSpace(value); }
                get { return Mathf.GammaToLinearSpace(thresholdGamma); }
            }

            [SerializeField, Range(0, 1)]
            [Tooltip("Makes transition between under/over-threshold gradual.")]
            public float softKnee;

            [SerializeField, Range(1, 7)]
            [Tooltip("Changes extent of veiling effects in a screen resolution-independent fashion.")]
            public float radius;

            [SerializeField]
            [Tooltip("Blend factor of the result image.")]
            public float intensity;

            [SerializeField]
            [Tooltip("Controls filter quality and buffer resolution.")]
            public bool highQuality;

            [SerializeField]
            [Tooltip("Reduces flashing noise with an additional filter.")]
            public bool antiFlicker;

            public static Settings defaultSettings
            {
                get
                {
                    var settings = new Settings
                    {
                        threshold = 2f,
                        softKnee = 0f,
                        radius = 7f,
                        intensity = 0.7f,
                        highQuality = true,
                        antiFlicker = true,
                    };
                    return settings;
                }
            }
        }

        #region Public Properties

        [SerializeField]
        public Settings settings = Settings.defaultSettings;

        #endregion

        [SerializeField, HideInInspector]
        private Shader m_Shader;

        public Shader shader
        {
            get
            {
                if (m_Shader == null)
                {
                    const string shaderName = "Hidden/KriptoFX/PostEffects/Explosion_Bloom";
                    m_Shader = Shader.Find(shaderName);
                }

                return m_Shader;
            }
        }

        private Material m_Material;
        public Material material
        {
            get
            {
                if (m_Material == null)
                    m_Material = CheckShaderAndCreateMaterial(shader);

                return m_Material;
            }
        }
    public static bool IsSupported(Shader s, bool needDepth, bool needHdr, MonoBehaviour effect)
    {
#if UNITY_EDITOR
        // Don't check for shader compatibility while it's building as it would disable most effects
        // on build farms without good-enough gaming hardware.
        if (!BuildPipeline.isBuildingPlayer)
        {
#endif
            if (s == null || !s.isSupported)
            {
                Debug.LogWarningFormat("Missing shader for image effect {0}", effect);
                return false;
            }

#if UNITY_5_5_OR_NEWER
                if (!SystemInfo.supportsImageEffects)
#else
            if (!SystemInfo.supportsImageEffects || !SystemInfo.supportsRenderTextures)
#endif
            {
                Debug.LogWarningFormat("Image effects aren't supported on this device ({0})", effect);
                return false;
            }

            if (needDepth && !SystemInfo.SupportsRenderTextureFormat(RenderTextureFormat.Depth))
            {
                Debug.LogWarningFormat("Depth textures aren't supported on this device ({0})", effect);
                return false;
            }

            if (needHdr && !SystemInfo.SupportsRenderTextureFormat(RenderTextureFormat.ARGBHalf))
            {
                Debug.LogWarningFormat("Floating point textures aren't supported on this device ({0})", effect);
                return false;
            }
#if UNITY_EDITOR
        }
#endif

        return true;
    }

    public static Material CheckShaderAndCreateMaterial(Shader s)
    {
        if (s == null || !s.isSupported)
            return null;

        var material = new Material(s);
        material.hideFlags = HideFlags.DontSave;
        return material;
    }

    public static bool supportsDX11
    {
        get { return SystemInfo.graphicsShaderLevel >= 50 && SystemInfo.supportsComputeShaders; }
    }
    #region Private Members

    const int kMaxIterations = 16;
        RenderTexture[] m_blurBuffer1 = new RenderTexture[kMaxIterations];
        RenderTexture[] m_blurBuffer2 = new RenderTexture[kMaxIterations];

        int m_Threshold;
        int m_Curve;
        int m_PrefilterOffs;
        int m_SampleScale;
        int m_Intensity;
       
        int m_BaseTex;

        private void Awake()
        {
            m_Threshold = Shader.PropertyToID("_Threshold");
            m_Curve = Shader.PropertyToID("_Curve");
            m_PrefilterOffs = Shader.PropertyToID("_PrefilterOffs");
            m_SampleScale = Shader.PropertyToID("_SampleScale");
            m_Intensity = Shader.PropertyToID("_Intensity");
          
            m_BaseTex = Shader.PropertyToID("_BaseTex");
        }

        private void OnEnable()
        {
            if (!IsSupported(shader, true, false, this))
                enabled = false;
        }

        private void OnDisable()
        {
            if (m_Material != null)
                DestroyImmediate(m_Material);

            m_Material = null;
        }

        private void OnRenderImage(RenderTexture source, RenderTexture destination)
        {
            var useRGBM = Application.isMobilePlatform;

            // source texture size
            var tw = source.width;
            var th = source.height;

            // halve the texture size for the low quality mode
            if (!settings.highQuality)
            {
                tw /= 2;
                th /= 2;
            }

            // blur buffer format
            var rtFormat = useRGBM ? RenderTextureFormat.Default : RenderTextureFormat.DefaultHDR;

            // determine the iteration count
            var logh = Mathf.Log(th, 2) + settings.radius - 8;
            var logh_i = (int)logh;
            var iterations = Mathf.Clamp(logh_i, 1, kMaxIterations);

            // update the shader properties
            var threshold = settings.thresholdLinear;
            material.SetFloat(m_Threshold, threshold);

            var knee = threshold * settings.softKnee + 1e-5f;
            var curve = new Vector3(threshold - knee, knee * 2, 0.25f / knee);
            material.SetVector(m_Curve, curve);

            var pfo = !settings.highQuality && settings.antiFlicker;
            material.SetFloat(m_PrefilterOffs, pfo ? -0.5f : 0.0f);

            material.SetFloat(m_SampleScale, 0.5f + logh - logh_i);
            material.SetFloat(m_Intensity, Mathf.Max(0.0f, settings.intensity));

            
            // prefilter pass
            var prefiltered = RenderTexture.GetTemporary(tw, th, 0, rtFormat);
            Graphics.Blit(source, prefiltered, material, settings.antiFlicker ? 1 : 0);

            // construct a mip pyramid
            var last = prefiltered;
            for (var level = 0; level < iterations; level++)
            {
                m_blurBuffer1[level] = RenderTexture.GetTemporary(last.width / 2, last.height / 2, 0, rtFormat);
                Graphics.Blit(last, m_blurBuffer1[level], material, level == 0 ? (settings.antiFlicker ? 3 : 2) : 4);
                last = m_blurBuffer1[level];
            }

            // upsample and combine loop
            for (var level = iterations - 2; level >= 0; level--)
            {
                var basetex = m_blurBuffer1[level];
                material.SetTexture(m_BaseTex, basetex);
                m_blurBuffer2[level] = RenderTexture.GetTemporary(basetex.width, basetex.height, 0, rtFormat);
                Graphics.Blit(last, m_blurBuffer2[level], material, settings.highQuality ? 6 : 5);
                last = m_blurBuffer2[level];
            }

            // finish process
            int pass = 7;
            pass += settings.highQuality ? 1 : 0;

            material.SetTexture(m_BaseTex, source);
            Graphics.Blit(last, destination, material, pass);

            // release the temporary buffers
            for (var i = 0; i < kMaxIterations; i++)
            {
                if (m_blurBuffer1[i] != null) RenderTexture.ReleaseTemporary(m_blurBuffer1[i]);
                if (m_blurBuffer2[i] != null) RenderTexture.ReleaseTemporary(m_blurBuffer2[i]);
                m_blurBuffer1[i] = null;
                m_blurBuffer2[i] = null;
            }

            RenderTexture.ReleaseTemporary(prefiltered);
        }

        #endregion
    }

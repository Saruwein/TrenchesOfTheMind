Shader "Custom/VignetteVideo"
{
    Properties
    {
        // _VignetteColor ("Vignette Color", Color) = (0,0,0,1) // Color inside the vignette
        _RenderTex ("Render Texture", 2D) = "white" {}       // Render texture property (video)
        _ScreenResolution ("Screen Resolution", Vector) = (1920, 1080, 0, 0) // For scaling UV to pixels
        _MinRadius ("Min Radius", Range(0, 2400)) = 720      // Inner radius of vignette
        _MaxRadius ("Max Radius", Range(0, 2400)) = 960      // Outer radius of vignette
        _Center ("Vignette Center", Vector) = (0.5, 0.5, 0)  // Center point of vignette
    }
    SubShader
    {
        Tags { "RenderType" = "Transparent" } // Keep it transparent for blending
        LOD 100

        Blend SrcAlpha OneMinusSrcAlpha // Enable alpha blending

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            // fixed4 _VignetteColor;      // Color inside the vignette
            sampler2D _RenderTex;       // Render texture (video feed)
            float2 _ScreenResolution;   // Screen resolution in pixels
            float2 _Center;             // Center of vignette
            float _MinRadius;           // Inner radius of vignette
            float _MaxRadius;           // Outer radius of vignette

            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // Convert UV coordinates to pixel space using screen resolution
                float2 pixelUV = i.uv * _ScreenResolution;
                float2 pixelCenter = _Center * _ScreenResolution;

                // Calculate the distance from the current pixel to the center of the vignette
                float dist = distance(pixelUV, pixelCenter);

                // Calculate vignette intensity based on distance (between min and max radius)
                float vignetteAlpha = smoothstep(_MinRadius, _MaxRadius, dist);

                // Sample the render texture (which contains your video)
                fixed4 renderTexColor = tex2D(_RenderTex, i.uv);

                // Inside the vignette area, show the vignette color
                // Outside the vignette, show the render texture (video)
                fixed4 finalColor = lerp(fixed4(0, 0, 0, 0), renderTexColor, vignetteAlpha);

                return finalColor;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}

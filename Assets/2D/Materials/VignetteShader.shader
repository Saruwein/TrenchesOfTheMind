Shader "Custom/VignetteShader"
{
    Properties
    {
        _VignetteColor ("Vignette Color", Color) = (0,0,0,1) // Color at the edges
        _Center ("Vignette Center", Vector) = (0.5, 0.5, 0) // Center point
        _MinRadius ("Min Radius", Range(0, 2400)) = 720 // Adjusted range for better control
        _MaxRadius ("Max Radius", Range(0, 2400)) = 960 // Adjusted range for better control
    }
    SubShader
    {
        Tags { "RenderType" = "Transparent" } // Change to Transparent
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

            fixed4 _VignetteColor; // Color at the edges
            float2 _Center; // Center of vignette effect
            float _MinRadius; // Minimum radius for vignette
            float _MaxRadius; // Maximum radius for vignette

            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 uv = i.uv;
                float2 center = _Center;

                // Calculate the distance from the current pixel to the center
                float dist = distance(uv, center);

                // Calculate the alpha based on distance
                float alpha = smoothstep(_MinRadius, _MaxRadius, dist);

                // Calculate the final color: mix transparent center with vignette color at the edges
                fixed4 vignette = lerp(fixed4(0, 0, 0, 0), _VignetteColor, alpha);

                return vignette;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}

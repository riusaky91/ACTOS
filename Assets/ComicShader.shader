Shader "Custom/ComicShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _OutlineColor ("Outline Color", Color) = (0,0,0,1)
        _OutlineThickness ("Outline Thickness", Range (0.0, 0.03)) = 0.005
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 pos : POSITION;
                float4 color : COLOR;
            };

            uniform float _OutlineThickness;
            uniform float4 _OutlineColor;

            v2f vert (appdata v)
            {
                // Extrude along normals
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex + float4(v.normal * _OutlineThickness, 0.0));
                o.color = _OutlineColor;
                return o;
            }

            half4 frag (v2f i) : COLOR
            {
                return i.color;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
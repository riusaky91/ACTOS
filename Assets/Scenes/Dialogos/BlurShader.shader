Shader "Custom/TransparentBlurShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}   // Textura principal
        _BlurAmount ("Blur Amount", Range(0, 10)) = 1 // Cantidad de desenfoque
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" }    // Etiqueta para la transparencia
        LOD 200

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha    // Configuración de transparencia
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;               // Textura principal
            float4 _MainTex_ST;               // Transformación de la textura
            float _BlurAmount;                // Cantidad de desenfoque

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 pos : POSITION;
                float2 uv : TEXCOORD0;
            };

            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv * _MainTex_ST.xy + _MainTex_ST.zw; // Transformar coordenadas UV
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 col = fixed4(0, 0, 0, 0);
                float2 offset = float2(_BlurAmount / 1000.0, _BlurAmount / 1000.0);

                // Muestras para el desenfoque
                for (int x = -2; x <= 2; x++)
                {
                    for (int y = -2; y <= 2; y++)
                    {
                        col += tex2D(_MainTex, i.uv + offset * float2(x, y));
                    }
                }

                col /= 25.0; // Promediar las muestras para suavizar
                col.a = tex2D(_MainTex, i.uv).a; // Mantener la transparencia de la textura
                return col;
            }
            ENDCG
        }
    }
}

Shader "NPIRE Original/ToonShader"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _Brightness("Brightness", Range(0,1)) = 0.3
        _Strength("Strength", Range(0,1)) = 0.5
        _Color("Color", COLOR) = (1,1,1,1)
        _Detail("Detail", Range(0,1)) = 0.3

        _OutlineColor("Outline Color", Color) = (0,0,0,1)
        _Outline("Outline width", Range(0, 0.05)) = 0.0001
    }
    SubShader
    {
        Tags { "RenderType" = "Opaque" }
        LOD 100
        Lighting Off Fog { Mode Off }
        ColorMask RGB
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                half3 worldNormal: NORMAL;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _Brightness;
            float _Strength;
            float4 _Color;
            float _Detail;

            float Toon(float3 normal, float3 lightDir) {
                float NdotL = max(0.0,dot(normalize(normal), normalize(lightDir)));

                return floor(NdotL / _Detail);
            }

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.worldNormal = UnityObjectToWorldNormal(v.normal);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
                col *= Toon(i.worldNormal, _WorldSpaceLightPos0.xyz) * _Strength * _Color + _Brightness;
                return col;
            }
            ENDCG
        }
        Pass
        {
            Name "OUTLINE"
            Tags { "LightMode" = "Always" }

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag_mult
            #pragma fragmentoption ARB_precision_hint_fastest
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

            float _Outline;
            float4 _OutlineColor;

            v2f vert(appdata v) 
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                float3 norm = UnityObjectToViewPos(v.normal);
                norm.x *= UNITY_MATRIX_P[0][0];
                norm.y *= UNITY_MATRIX_P[1][1];
                o.pos.xy += norm.xy * o.pos.z * _Outline;
                o.pos.z += 0.001;

                o.color = _OutlineColor;
                return o;
            }

            fixed4 frag_mult(v2f i) : COLOR
            {
                return i.color;
            }
            ENDCG

            Cull Front
            ZWrite On
            ColorMask RGB
            Blend SrcAlpha OneMinusSrcAlpha
            //? -Note: I don't remember why I put a "?" here 
            SetTexture[_MainTex] { combine primary }
        }
    }
    Fallback "Diffuse"
}
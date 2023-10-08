Shader"Custom/Texture Blend" {
    Properties {
        _Color ("Color", Color) = (1,1,1,1)
        _Blend ("Texture Blend", Range(0,1)) = 0.0
        _Tex1Brightness("Texture 1 Brightness",Range(0,5))=1.0
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _MainTex2 ("Albedo 2 (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
    }
    SubShader {
        Tags { "RenderType"="Opaque" }
LOD 200
        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // Use shader model 3.0 target, to get nicer looking lighting
            #pragma target 3.0

#include "UnityCG.cginc"

struct appdata_t
{
    float4 vertex : POSITION;
    float2 uv_MainTex : TEXCOORD0;
    float2 uv_MainTex2 : TEXCOORD1;
    float2 uv_Emission : TEXCOORD2;
};

struct v2f
{
    float2 uv_MainTex : TEXCOORD0;
    float2 uv_MainTex2 : TEXCOORD1;
    float2 uv_Emission : TEXCOORD2;
    float4 vertex : SV_POSITION;
};
sampler2D _MainTex;
sampler2D _MainTex2;
half4 _Color;
half _Blend;
half _Tex1Brightness;
half _Glossiness;
half _Metallic;

v2f vert(appdata_t v)
{
    v2f o;
    o.vertex = UnityObjectToClipPos(v.vertex);
    o.uv_MainTex = v.uv_MainTex;
    o.uv_MainTex2 = v.uv_MainTex2;
    return o;
}

half4 frag(v2f i) : SV_Target
{
                // Albedo comes from a texture tinted by color
    half4 c = lerp(tex2D(_MainTex, i.uv_MainTex) * _Tex1Brightness, tex2D(_MainTex2, i.uv_MainTex2), _Blend) * _Color;
                // Metallic and smoothness come from slider variables
    half4 o;
    o.rgb = c.rgb;
    o.a = c.a;
    return o;
}
            ENDCG
        }
    }
}

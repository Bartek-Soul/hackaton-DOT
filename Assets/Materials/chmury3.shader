Shader"Custom/Texture Blend" {
        Properties {
            _Color ("Color", Color) = (1,1,1,1)
            _Blend ("Texture Blend", Range(0,1)) = 0.0
            _Tex1Brightness("Texture 1 Brightness",Range(0,5))=1.0
            _MainTex ("Albedo (RGB)", 2D) = "white" {}
            _MainTex2 ("Albedo 2 (RGB)", 2D) = "white" {}
            _Glossiness ("Smoothness", Range(0,1)) = 0.5
            _Metallic ("Metallic", Range(0,1)) = 0.0
            _Emission("Emission", 2D) = "white" {}
            [HDR] _EmissionColor("Color", Color) = (0,0,0)
        }
        SubShader {
            Tags { "RenderType"="Opaque" }
            LOD 200
           
            CGPROGRAM
            // Physically based Standard lighting model, and enable shadows on all light types
            #pragma surface surf Standard fullforwardshadows
     
            // Use shader model 3.0 target, to get nicer looking lighting
            #pragma target 3.0
     
        sampler2D _MainTex;
        sampler2D _MainTex2;
        struct Input
        {
            float2 uv_MainTex;
            float2 uv_MainTex2;
        };
        half _Blend;
        half _Glossiness;
        half _Metallic;
        half _Tex1Brightness;
        fixed4 _Color;
     
        void surf(Input IN, inout SurfaceOutputStandard o)
        {
                        // Albedo comes from a texture tinted by color
            fixed4 c = lerp(tex2D(_MainTex, IN.uv_MainTex)*_Tex1Brightness, tex2D(_MainTex2, IN.uv_MainTex2), _Blend) * _Color;
            o.Albedo = c.rgb;
            clip(o.Albedo - 0.0005);
                        // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
        }
        ENDCG
    }
    SubShader
        {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                fixed4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                fixed4 color:COLOR;
            };

            uniform sampler2D _EmissionMap;
            uniform float4 _EmissionColor;

            v2f vert(appdata v)
            {
                v2f o;
                half4 emission = tex2Dlod(_EmissionMap, float4(v.uv, 0.0, 0.0)) * _EmissionColor;
                o.color = emission;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                return i.color;
            }
        ENDCG
        }
    }
}

Shader "Custom/EnhancedHueShiftShader"
{
    Properties
    {
        _MainTex ("Sprite Texture", 2D) = "white" {}
        _HueShift ("Hue Shift", Range(0, 1)) = 0.0
        _SaturationBoost ("Saturation Boost", Range(1, 2)) = 1.2
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        LOD 100

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            Cull Off
            ZWrite Off

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

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _HueShift;
            float _SaturationBoost;

            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            float3 RGBToHSV(float3 rgb)
            {
                float4 K = float4(0.0, -1.0 / 3.0, 2.0 / 3.0, -1.0);
                float4 p = rgb.g < rgb.b ? float4(rgb.bg, K.wz) : float4(rgb.gb, K.xy);
                float4 q = rgb.r < p.x ? float4(p.xyw, rgb.r) : float4(rgb.r, p.yzx);
                float d = q.x - min(q.w, q.y);
                float e = 1.0e-10;
                return float3(abs(q.z + (q.w - q.y) / (6.0 * d + e)), d / (q.x + e), q.x);
            }

            float3 HSVToRGB(float3 hsv)
            {
                float4 K = float4(1.0, 2.0 / 3.0, 1.0 / 3.0, 3.0);
                float3 p = abs(frac(hsv.xxx + K.xyz) * 6.0 - K.www);
                return hsv.z * lerp(K.xxx, saturate(p - K.xxx), hsv.y);
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float4 texColor = tex2D(_MainTex, i.uv);

                // Convert to HSV
                float3 hsv = RGBToHSV(texColor.rgb);

                // Adjust hue and amplify saturation
                hsv.x = frac(hsv.x + _HueShift);
                hsv.y = clamp(hsv.y * _SaturationBoost, 0.0, 1.0);

                // Convert back to RGB
                float3 rgb = HSVToRGB(hsv);

                // Output the modified color
                return fixed4(rgb, texColor.a);
            }
            ENDCG
        }
    }
}
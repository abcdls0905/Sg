Shader "H16_Particle/Dissolution_Alpha" {
    Properties {
        _TintColor ("Color&Alpha", Color) = (1,1,1,1)
        _MainTex ("Diffuse Texture", 2D) = "white" {}
        _N_mask ("N_mask", Float ) = 0.3
        _MaskTex ("Mask (R)", 2D) = "white" {}
        _MaskColor ("Mask Color", Color) = (1,0,0,1)
        _MaskIntensity ("Mask Intensity", Float ) = 3
        _N_BY_KD ("N_BY_KD", Float ) = 0.01
        [Enum(UnityEngine.Rendering.BlendMode)]_BloomFactor ("BloomFactor", float) = 1
    }
    SubShader {
		Tags { 
			"IgnoreProjector"="True" 
			"Queue"="Transparent" 
			"RenderType"="Transparent" 
		}
        Pass {
	        
            Blend SrcAlpha OneMinusSrcAlpha, [_BloomFactor] OneMinusSrcAlpha
            Cull Off Lighting Off ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MaskTex; 
            float4 _MaskTex_ST;
            sampler2D _MainTex; 
            float4 _MainTex_ST;
            half4 _TintColor;
            float _N_mask;
            float _N_BY_KD;
            half4 _MaskColor;
            float _MaskIntensity;

            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord : TEXCOORD0;
                half4 vertexColor : COLOR;
            };

            struct VertexOutput {
                float4 pos : SV_POSITION;
                float4 uv : TEXCOORD0;
                half4 vertexColor : COLOR;
            };

            VertexOutput vert (VertexInput v) 
            {
                VertexOutput o;
                o.uv.xy = TRANSFORM_TEX(v.texcoord, _MainTex);
                o.uv.zw = TRANSFORM_TEX(v.texcoord, _MaskTex);
                o.vertexColor = v.vertexColor;
                o.pos = UnityObjectToClipPos(v.vertex);
                return o;
            }

            float4 frag(VertexOutput i) : SV_Target 
            {
                half4 main = tex2D(_MainTex,i.uv.xy);
                half3 emissive = _TintColor.rgb * main.rgb * i.vertexColor.rgb;
                float value1 = i.vertexColor.a * _N_mask;
                half4 mask = tex2D(_MaskTex,i.uv.zw);
                float value2 = step(value1, mask.r);
                float value3 = step(mask.r, value1);
                float value4 = lerp(value3, 1.0, value2 * value3);

                float value5 = step(value1, (mask.r + _N_BY_KD));
                float value6 = step(mask.r+_N_BY_KD, value1);
                float value7 = value4 - lerp(value6, 1.0, value5 * value6);

                half4 col;
                col.rgb = emissive + value7 * _MaskColor.rgb * _MaskIntensity;
                col.a = saturate(_TintColor.a * main.a * (value4 + value7));
                return col;
            }
            ENDCG
        }
    }
}

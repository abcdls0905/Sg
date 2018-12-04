Shader "H16_Particle/Alpha" {
	Properties {
		_TintColor ("Tint Color", Color) = (0.5,0.5,0.5,0.5)
		_MainTex ("Particle Texture", 2D) = "white" {}
		[Toggle] _ZWrite ("ZWrite On", Float) = 0
		[Enum(UnityEngine.Rendering.CompareFunction)] _ZTest ("ZTest On", Float) = 4
		[Enum(UnityEngine.Rendering.BlendMode)]_BloomFactor ("BloomFactor", float) = 1
	}

	Category {
		Tags { 
			"IgnoreProjector"="True" 
			"Queue"="Transparent" 
			"RenderType"="Transparent" 
		}
		Cull Off Lighting Off ZWrite [_ZWrite] ZTest[_ZTest]
		Blend SrcAlpha OneMinusSrcAlpha, [_BloomFactor] OneMinusSrcAlpha

		SubShader {
			Pass {
				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#include "UnityCG.cginc"

				sampler2D _MainTex;
				float4 _MainTex_ST;
				half4 _TintColor;
				
				struct appdata_t {
					float4 vertex : POSITION;
					half4 color : COLOR;
					float2 texcoord : TEXCOORD0;
				};

				struct v2f {
					float4 vertex : SV_POSITION;
					half4 color : COLOR;
					float2 texcoord : TEXCOORD0;
				};

				v2f vert (appdata_t v)
				{
					v2f o;
					o.vertex = UnityObjectToClipPos(v.vertex);
					o.color = v.color;
					o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
					return o;
				}
				
				half4 frag (v2f i) : SV_Target
				{
					half4 col = i.color * _TintColor * tex2D(_MainTex, i.texcoord);
					col.rgb *= 2.0f;
					return col;
				}
				ENDCG 
			}
		}	
	}
}

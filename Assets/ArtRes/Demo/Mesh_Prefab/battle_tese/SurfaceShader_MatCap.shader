// Upgrade NOTE: upgraded instancing buffer 'Props' to new syntax.

Shader "sugar/SurfaceShader_MatCap"
{
	Properties
	{
		[Header(BaseColor)]
		_Color ("Color", Color) = (1, 1, 1, 1)
		_MainTex ("Albedo (RGB)", 2D) = "white" { }
		[Header(Normal)]
		[NoScaleOffset]_NormalTex ("NormalMap", 2D) = "bump" { }
		[Header(Metallic)]
		_Metallic ("Metallic", Range(0, 1)) = 1
		[NoScaleOffset]_MetallicTex ("MetailicMap", 2D) = "white" { }
		[Header(Roughness)]
		_Roughness ("Roughness", Range(0, 1)) = 1
		[NoScaleOffset]_RoughnessTex ("Roughnessmap", 2D) = "white" { }
		[Header(AO)]
		_AO ("AO", Range(0, 1)) = 1
		[NoScaleOffset]_AOTex ("AoMap", 2D) = "white" { }
		[Header(MatCap)]
		[NoScaleOffset]_MatCap ("MatCap", 2D) = "black" { }
		[HDR]_MatColor ("MatCap Color", Color) = (0.5, 0.5, 0.5, 0.5)
	}
	SubShader
	{
		Tags { "RenderType" = "Opaque" }
		LOD 200
		
		CGPROGRAM
		
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows vertex:myvert
		
		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0
		
		sampler2D _MainTex;
		sampler2D _MatCap;
		sampler2D _NormalTex;
		sampler2D _MetallicTex;
		sampler2D _RoughnessTex;
		sampler2D _AOTex;
		
		struct Input
		{
			float3 worldNormal;
			float2 uv_MainTex;
			float2 MatCapUV;
		};
		
		half _Roughness;
		half _Metallic;
		half4 _Color;
		half4 _MatColor;
		half _AO;
		
		
		UNITY_INSTANCING_BUFFER_START(Props)
		// put more per-instance properties here
		UNITY_INSTANCING_BUFFER_END(Props)
		
		void myvert(inout appdata_full v, out Input o)
		{
			UNITY_INITIALIZE_OUTPUT(Input, o);
			o.MatCapUV.x = mul(normalize(UNITY_MATRIX_IT_MV[0].xyz), normalize(v.normal));
			o.MatCapUV.y = mul(normalize(UNITY_MATRIX_IT_MV[1].xyz), normalize(v.normal));
			o.MatCapUV = o.MatCapUV * 0.5 + 0.5;
		}
		
		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			// Albedo comes from a texture tinted by color
			half4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
			half3 n = UnpackNormal(tex2D(_NormalTex, IN.uv_MainTex));
			half m = tex2D(_MetallicTex, IN.uv_MainTex).r * _Metallic;
			half r = tex2D(_RoughnessTex, IN.uv_MainTex).r * _Roughness;
			half ao = tex2D(_AOTex, IN.uv_MainTex) * _AO;
			
			fixed3 mat = tex2D(_MatCap, IN.MatCapUV) * _MatColor;
			o.Albedo = c.rgb;
			o.Metallic = m;
			o.Smoothness = 1 - r;
			o.Normal = n;
			
			o.Occlusion = ao;
			o.Emission = mat;
			o.Alpha = c.a;
		}
		ENDCG
		
	}
	FallBack "Diffuse"
}

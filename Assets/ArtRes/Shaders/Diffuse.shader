Shader "Sugar/Solid/Diffuse" {
	Properties
	{
		_Color("Tint Color", color) = (1, 1, 1, 1)
		_MainTex("Base (RGB)", 2D) = "white" {}
	}
		SubShader
	{
		Tags{ "RenderType" = "Opaque" "IgnoreProjector" = "True" }
		LOD 200

		Pass
		{
			Name "Diffuse_Common"
			Tags{ "LightMode" = "ForwardBase" }
			CGPROGRAM
			#include "CommonCG.cginc" 
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_fwdbase nodynlightmap nodirlightmap
			ENDCG
		}

		// ---- meta information extraction pass:
		Pass
		{
			Name "Diffuse_Meta"
			Tags{ "LightMode" = "Meta" }
			Cull Off
			CGPROGRAM
			#include "CommonCG.cginc"
			#pragma vertex vert_meta
			#pragma fragment frag_meta
			#pragma skip_variants FOG_LINEAR FOG_EXP FOG_EXP2
			#pragma shader_feature EDITOR_VISUALIZATION
			ENDCG
		}
	}
	Fallback "Mobile/VertexLit"
}
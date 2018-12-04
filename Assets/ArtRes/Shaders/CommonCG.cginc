// Upgrade NOTE: replaced 'defined UNITY_PASS_META' with 'defined (UNITY_PASS_META)'

#include "AutoLight.cginc"
#include "Lighting.cginc"
#include "UnityCG.cginc"
#include "UnityMetaPass.cginc"

struct vInput
{
	float4 vertex : POSITION;
	float4 uv : TEXCOORD0;
	float3 normal : NORMAL;
#ifdef LIGHTMAP_ON
	float4 texcoord1: TEXCOORD1;
#endif
};

struct vOutput
{
	UNITY_POSITION(pos);
	float2 uv : TEXCOORD0;
	half3 worldNormal : TEXCOORD1;
	float3 worldPos : TEXCOORD2;
#ifdef LIGHTMAP_ON
	float2 lmap : TEXCOORD3;
#else
	half3 sh : TEXCOORD3;
#endif
	SHADOW_COORDS(5)
};

sampler2D _MainTex;
float4 _MainTex_ST;
float4 _Color;
#ifdef _USEEMISSION_ON
sampler2D _Illum;
fixed _Emission;
#endif

vOutput vert(vInput v)
{
	vOutput o;
	UNITY_INITIALIZE_OUTPUT(vOutput, o);
	o.pos = UnityObjectToClipPos(v.vertex);
	o.uv.xy = TRANSFORM_TEX(v.uv, _MainTex);
	float3 worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
	fixed3 worldNormal = UnityObjectToWorldNormal(v.normal);
	o.worldPos = worldPos;
	o.worldNormal = worldNormal;
#ifdef LIGHTMAP_ON
	o.lmap.xy = v.texcoord1.xy * unity_LightmapST.xy + unity_LightmapST.zw;
#else
#ifdef UNITY_SHOULD_SAMPLE_SH
	o.sh = 0;
#ifdef VERTEXLIGHT_ON
	o.sh += Shade4PointLights(unity_4LightPosX0, unity_4LightPosY0, unity_4LightPosZ0,
		unity_LightColor[0].rgb, unity_LightColor[1].rgb, unity_LightColor[2].rgb, unity_LightColor[3].rgb,
		unity_4LightAtten0, worldPos, worldNormal);
#endif
	o.sh = ShadeSHPerVertex(worldNormal, o.sh);
#endif
#endif
	TRANSFER_SHADOW(o);
	return o;
}

fixed4 frag(vOutput IN) : SV_Target
{
	fixed4 mainTex = tex2D(_MainTex, IN.uv);
	mainTex.rgb *= _Color.rgb;
	half3 color = _LightColor0.rgb;
	half3 diffuse = half3(0, 0, 0);
#ifdef LIGHTMAP_ON
	diffuse = DecodeLightmap(UNITY_SAMPLE_TEX2D(unity_Lightmap, IN.lmap));
#else
	fixed atten = LIGHT_ATTENUATION(IN);
	color *= atten;
#if UNITY_SHOULD_SAMPLE_SH
	diffuse = ShadeSHPerPixel(IN.worldNormal, IN.sh, IN.worldPos);
#endif
#endif
#ifndef USING_DIRECTIONAL_LIGHT
	fixed3 lightDir = normalize(UnityWorldSpaceLightDir(IN.worldPos));
#else
	fixed3 lightDir = _WorldSpaceLightPos0.xyz;
#endif
	fixed diff = max(0, dot(IN.worldNormal, lightDir));
	fixed4 c;
	c.rgb = mainTex.rgb * color * diff;
	c.a = mainTex.a;
#ifdef UNITY_LIGHT_FUNCTION_APPLY_INDIRECT
	c.rgb += mainTex.rgb * diffuse;
#endif
#ifdef _USEEMISSION_ON
	fixed3 Emission = mainTex.rgb * UNITY_SAMPLE_1CHANNEL(_Illum, IN.uv);
	c.rgb += Emission;
#endif
	//Update Mark 20180223
	UNITY_OPAQUE_ALPHA(c.a);
	return c;
}

//-------------------------------META_PASS
struct vOutput_meta {
	UNITY_POSITION(pos);
	float2 uv : TEXCOORD0; // _MainTex
	float3 worldPos : TEXCOORD1;
};

vOutput_meta vert_meta(appdata_full v) {
	vOutput_meta o;
	UNITY_INITIALIZE_OUTPUT(vOutput_meta, o);
	o.pos = UnityMetaVertexPosition(v.vertex, v.texcoord1.xy, v.texcoord2.xy, unity_LightmapST, unity_DynamicLightmapST);
	o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
	float3 worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
	o.worldPos = worldPos;
	return o;
}

fixed4 frag_meta(vOutput_meta IN) : SV_Target{
#ifndef USING_DIRECTIONAL_LIGHT
	fixed3 lightDir = normalize(UnityWorldSpaceLightDir(worldPos));
#else
	fixed3 lightDir = _WorldSpaceLightPos0.xyz;
#endif
	fixed4 mainTex = tex2D(_MainTex, IN.uv);
	mainTex.rgb *= _Color.rgb;
	fixed3 Albedo = mainTex.rgb;

	fixed3 Emission = 0.0;
#ifdef _USEEMISSION_ON
	Emission = mainTex.rgb * UNITY_SAMPLE_1CHANNEL(_Illum, IN.uv);
    Emission *= _Emission.rrr;
#endif
	UnityMetaInput metaIN;
	UNITY_INITIALIZE_OUTPUT(UnityMetaInput, metaIN);
	metaIN.Albedo = Albedo;
	metaIN.Emission = Emission;
	metaIN.SpecularColor = 0.0;
	return UnityMetaFragment(metaIN);
}
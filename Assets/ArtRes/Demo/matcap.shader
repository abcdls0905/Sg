Shader "ASEMatCap"
{
    Properties
    {
        [HideInInspector] __dirty( "", Int ) = 1
        _MatCap("MatCap", 2D) = "white" {}
    }

    SubShader
    {
        Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
        Cull Back
        CGINCLUDE
        #include "UnityShaderVariables.cginc"
        #include "UnityPBSLighting.cginc"
        #include "Lighting.cginc"
        #pragma target 2.0
        #ifdef UNITY_PASS_SHADOWCASTER
            #undef INTERNAL_DATA
            #undef WorldReflectionVector
            #undef WorldNormalVector
            #define INTERNAL_DATA half3 internalSurfaceTtoW0; half3 internalSurfaceTtoW1; half3 internalSurfaceTtoW2;
            #define WorldReflectionVector(data,normal) reflect (data.worldRefl, half3(dot(data.internalSurfaceTtoW0,normal), dot(data.internalSurfaceTtoW1,normal), dot(data.internalSurfaceTtoW2,normal)))
            #define WorldNormalVector(data,normal) fixed3(dot(data.internalSurfaceTtoW0,normal), dot(data.internalSurfaceTtoW1,normal), dot(data.internalSurfaceTtoW2,normal))
        #endif
        struct Input
        {
            float3 worldNormal;
        };

        uniform sampler2D _MatCap;

        void surf( Input i , inout SurfaceOutputStandard o )
        {
            float2 componentMask6 = mul( UNITY_MATRIX_V , float4( i.worldNormal , 0.0 ) ).xy;
            o.Emission = tex2D( _MatCap, ( 0.5 + ( 0.5 * componentMask6 ) ) ).rgb;
            o.Alpha = 1;
        }

        ENDCG
        CGPROGRAM
        #pragma surface surf Standard keepalpha fullforwardshadows exclude_path:deferred 

        ENDCG
        Pass
        {
            Name "ShadowCaster"
            Tags{ "LightMode" = "ShadowCaster" }
            ZWrite On
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 3.0
            #pragma multi_compile_shadowcaster
            #pragma multi_compile UNITY_PASS_SHADOWCASTER
            #pragma skip_variants FOG_LINEAR FOG_EXP FOG_EXP2
            # include "HLSLSupport.cginc"
            #if ( SHADER_API_D3D11 || SHADER_API_GLCORE || SHADER_API_GLES3 || SHADER_API_METAL || SHADER_API_VULKAN )
                #define CAN_SKIP_VPOS
            #endif
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            sampler3D _DitherMaskLOD;
            struct v2f
            {
                V2F_SHADOW_CASTER;
                float3 worldPos : TEXCOORD6;
                float4 tSpace0 : TEXCOORD1;
                float4 tSpace1 : TEXCOORD2;
                float4 tSpace2 : TEXCOORD3;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };
            v2f vert( appdata_full v )
            {
                v2f o;
                UNITY_SETUP_INSTANCE_ID( v );
                UNITY_INITIALIZE_OUTPUT( v2f, o );
                UNITY_TRANSFER_INSTANCE_ID( v, o );
                float3 worldPos = mul( unity_ObjectToWorld, v.vertex ).xyz;
                half3 worldNormal = UnityObjectToWorldNormal( v.normal );
                o.worldPos = worldPos;
                TRANSFER_SHADOW_CASTER_NORMALOFFSET( o )
                return o;
            }
            fixed4 frag( v2f IN
            #if !defined( CAN_SKIP_VPOS )
            , UNITY_VPOS_TYPE vpos : VPOS
            #endif
            ) : SV_Target
            {
                UNITY_SETUP_INSTANCE_ID( IN );
                Input surfIN;
                UNITY_INITIALIZE_OUTPUT( Input, surfIN );
                float3 worldPos = IN.worldPos;
                fixed3 worldViewDir = normalize( UnityWorldSpaceViewDir( worldPos ) );
                surfIN.worldNormal = float3( IN.tSpace0.z, IN.tSpace1.z, IN.tSpace2.z );
                SurfaceOutputStandard o;
                UNITY_INITIALIZE_OUTPUT( SurfaceOutputStandard, o )
                surf( surfIN, o );
                #if defined( CAN_SKIP_VPOS )
                float2 vpos = IN.pos;
                #endif
                SHADOW_CASTER_FRAGMENT( IN )
            }
            ENDCG
        }
    }
    Fallback "Diffuse"
    CustomEditor "ASEMaterialInspector"
}
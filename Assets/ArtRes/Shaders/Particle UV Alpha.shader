Shader "H16_Particle/UV_Alpha" {
    Properties {
    	_MainTex ("Texture (RGB)", 2D) = "white" {}
    	_ScrollX ("Scroll speed X", Float) = 1.0
    	_ScrollY ("Scroll speed Y", Float) = 0.0
    	_RotationStart("Rotation Start", Float) = 0
    	_RotationX("Rotation center Y", Float) = 0.5	
    	_RotationY("Rotation center X", Float) = 0.5
    	_Rotation ("Rotation speed", Float) = 1.0
    	_Color("Color", Color) = (1,1,1,1)
    	_MMultiplier ("Multiplier", Float) = 2.0
    	[Enum(UnityEngine.Rendering.BlendMode)]_BloomFactor ("BloomFactor", float) = 1
    }

    SubShader {
		Tags { 
			"Queue"="Transparent" 
			"IgnoreProjector"="True" 
			"RenderType"="Transparent" 
		}
        Cull Off Lighting Off ZWrite Off
    	Blend SrcAlpha OneMinusSrcAlpha, [_BloomFactor] OneMinusSrcAlpha

        CGINCLUDE
        #include "UnityCG.cginc"

        sampler2D _MainTex;   
        float4 _MainTex_ST;
        
        float _ScrollX;
        float _ScrollY;
        float _RotationStart;
        float _RotationX;
        float _RotationY;
        float _Rotation;
        float _MMultiplier;
        half4 _Color;

        struct appdata {
            float4 vertex : POSITION;
            float2 texcoord : TEXCOORD0;
            half4 color : COLOR;
        };

        struct v2f {
            float4 pos : SV_POSITION;
            float2 uv : TEXCOORD0;
            half4 color : TEXCOORD1;
        };

        float2 CalcUV(float2 uv, float tx, float ty, float rstart, float rx, float ry, float r)
        {
            float s = sin(r * _Time.y + rstart);
            float c = cos(r * _Time.y + rstart);
            float2x2 m = {c, -s, s, c};
            
            uv -= float2(rx, ry);
            uv = mul(uv, m);
            uv += float2(rx, ry);
            uv += frac(float2(tx, ty) * _Time.y);
            
            return uv;
        }
        
        v2f vert (appdata v)
        {
            v2f o;
            o.pos = UnityObjectToClipPos(v.vertex);
            float2 uv = TRANSFORM_TEX(v.texcoord,_MainTex);
            o.uv = CalcUV(uv, _ScrollX, _ScrollY, _RotationStart, _RotationX, _RotationY, _Rotation);
            o.color = _MMultiplier * _Color * v.color;
            
            return o;
        }
        ENDCG

        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
                    
            half4 frag (v2f i) : SV_Target
            {
            	half4 col;
    			col = tex2D(_MainTex, i.uv) * i.color;
    			col.a = saturate(col.a);
    			return col;
            }
            ENDCG 
        }   
    }
}

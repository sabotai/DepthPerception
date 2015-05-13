Shader "Aubergine/Replacement/Wiggle" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
    	_Intensity ("Intensity", Float) = 10
	}
	SubShader {

		CGINCLUDE
			#include "UnityCG.cginc"
			
			struct a2v {
				float4 vertex : POSITION;
				float4 texcoord : TEXCOORD0;
			};
			
			struct v2f {
				float4 pos : SV_POSITION;
				half2 uv : TEXCOORD0;
			};
			
			v2f vert (a2v v) {
				v2f o;
				o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
				o.uv = v.texcoord.xy;
				return o;
			}
		ENDCG

		Pass {
			Lighting Off Fog { Mode Off }
			CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag

				sampler2D _MainTex;
				Float _Intensity;

				half4 frag (v2f i) : COLOR {
					i.uv.x += sin(_Time.y + i.uv.x * _Intensity) * 0.01;
					i.uv.y += cos(_Time.y + i.uv.y * _Intensity) * 0.01;
					half4 col = tex2D(_MainTex, i.uv);
					return col;
				}
			ENDCG
		}
	}
}
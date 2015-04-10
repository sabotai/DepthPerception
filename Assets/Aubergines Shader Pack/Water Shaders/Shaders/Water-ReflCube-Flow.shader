#warning Upgrade NOTE: unity_Scale shader variable was removed; replaced 'unity_Scale.w' with '1.0'

Shader "Aubergine/Objects/NoLights/Water/ReflCube-Flow" {
	Properties {
		_Color("Main Color", Color) = (0, 0.15, 0.115, 1)
		_FlowTex("Flow Texture", 2D) = "white" {}
		_NoiseTex("Noise Texture", 2D) = "white" {}
		_Wave1Tex("Wave1 Texture", 2D) = "bump" {}
		_Wave2Tex("Wave2 Texture", 2D) = "bump" {}
		_CubeTex("Reflection Texture", Cube) = "" {}
		_Refraction("Refraction Amount", Range(0.0, 1.0)) = 0.5
	}
	SubShader {
		Tags { "Queue"="Geometry" "RenderType"="Opaque" }
		LOD 200

		Pass {
			Lighting Off Fog { Mode Off }

			CGPROGRAM
				#pragma fragmentoption ARB_precision_hint_fastest
				#pragma vertex vert
				#pragma fragment frag
				#include "UnityCG.cginc"

				fixed4 _Color;
				sampler2D _Wave1Tex, _Wave2Tex;
				fixed4 _Wave1Tex_ST;
				samplerCUBE _CubeTex;
				fixed _Refraction;
				sampler2D _FlowTex, _NoiseTex;
				uniform float _FlowOffset0, _FlowOffset1, _HalfCycle;

				struct v2f {
					float4 Pos : SV_POSITION;
					float4 Uv : TEXCOORD0;
					fixed3 View : TEXCOORD1;
					fixed3 T2W0 : TEXCOORD2;
					fixed3 T2W1 : TEXCOORD3;
					fixed3 T2W2 : TEXCOORD4;
				};

				v2f vert (appdata_tan v) {
					v2f o;
					o.Pos = mul(UNITY_MATRIX_MVP, v.vertex);
					o.Uv.xy = TRANSFORM_TEX(v.texcoord, _Wave1Tex);
					o.Uv.zw = v.texcoord.xy;
					o.View = WorldSpaceViewDir(v.vertex);
					o.View.x *= -1;
					TANGENT_SPACE_ROTATION;
					o.T2W0 = mul(rotation, _Object2World[0].xyz * 1.0);
					o.T2W1 = mul(rotation, _Object2World[1].xyz * 1.0);
					o.T2W2 = mul(rotation, _Object2World[2].xyz * 1.0);
					return o;
				}

				fixed4 frag(v2f i) : COLOR {
					half2 flow = tex2D(_FlowTex, i.Uv.zw).rg * 2.0 - 1.0;
					float cycleOffs = tex2D(_NoiseTex, i.Uv.zw).r;
					float phase0 = cycleOffs * 0.5f + _FlowOffset0;
					float phase1 = cycleOffs * 0.5f + _FlowOffset1;
					
					fixed4 bump1 = tex2D(_Wave1Tex, i.Uv.xy + flow * phase0);
					fixed4 bump2 = tex2D(_Wave2Tex, i.Uv.xy + flow * phase1);
					float f = (abs(_HalfCycle - _FlowOffset0) / _HalfCycle);
					float4 bump = lerp(bump1, bump2, f);
					
					//Tangent Space Normals
					fixed3 normal = UnpackNormal(bump);
					fixed3 normalW;
					normalW.x = dot(i.T2W0, normal.xyz);
					normalW.y = dot(i.T2W1, normal.xyz);
					normalW.z = dot(i.T2W2, normal.xyz);
					normalW = normalize(normalW);
					i.View = normalize(i.View);

					//Reflection
					fixed3 reflCol = texCUBE(_CubeTex, reflect(-i.View, normalW)).rgb;
					//Refraction
					fixed3 refrCol = texCUBE(_CubeTex, refract(-i.View, normalW, 0.66)).rgb;
					//Fresnel term
					fixed EdotN = max(dot(i.View, normalW), 0);
					fixed facing = (1.0 - EdotN);
					//half fresnel = 1 - EdotN * 1.3f;
					//half fresnel = 0.20 + (1.0 - 0.20) - pow(facing, 5.0);
					//half fresnel = 0.02 + 0.97 * pow(facing, 5.0);
					fixed fresnel = 1 / pow(1 + EdotN, 5);

					fixed3 deepCol = (refrCol * _Refraction + _Color * (1 - _Refraction));
					fixed3 waterCol = (_Color * facing + deepCol * (1.0 - facing));
					fixed3 finalColor = fresnel * reflCol + waterCol;

					return fixed4(finalColor, 1);
				}
			ENDCG
		}
	} 
	FallBack Off
}
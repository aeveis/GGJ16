﻿Shader "Custom/UnlitDistort"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_Difference ("Texture", 2D) = "white" {}
		_DistortAmount ("Distort Amount", Range(0,.2)) = 0.1
	}
	SubShader
	{
		Tags { "RenderType"="Transparent" }
		LOD 100

 	 	ZWrite Off
		Blend SrcAlpha OneMinusSrcAlpha

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			#pragma multi_compile_fog
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			sampler2D _Difference;
			float _DistortAmount;
			float4 _MainTex_ST;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				fixed2 diff = (tex2D(_Difference, i.uv)-.5)*_DistortAmount;
				fixed4 col = tex2D(_MainTex, i.uv+diff);
				// apply fog
				UNITY_APPLY_FOG(i.fogCoord, col);
				return col;
			}
			ENDCG
		}
	}
}

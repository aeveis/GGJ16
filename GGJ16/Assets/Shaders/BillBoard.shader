Shader "Custom/BillBoard"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
	}
	SubShader
	{

 	Tags { "QUEUE"="Transparent" "IGNOREPROJECTOR"="true" "RenderType"="Transparent" }
 	 ZWrite Off
 	 ZTest Off
	 Blend SrcAlpha One
		LOD 100
		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			
			v2f vert (appdata v)
			{
				v2f o;
				//o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				//o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				//return o;

              

				o.vertex = mul(UNITY_MATRIX_P, 
              	mul(UNITY_MATRIX_MV, float4(0.0, 0.0, 0.0, 1.0))
              	+ float4(v.vertex.x, v.vertex.y, 0.0, 0.0));
 
            	o.uv = v.uv;

            	return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				fixed4 col = tex2D(_MainTex, i.uv);
				return col;
			}
			ENDCG
		}
		Pass{ 
            //Avoid batching so billboards properly ughhh
            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag

            float4 vert(float4 v:POSITION) : SV_POSITION {
                return mul (UNITY_MATRIX_MVP, v);
            }

            fixed4 frag() : SV_Target {
                return 0;
            }

            ENDCG
	 	}
	}
}

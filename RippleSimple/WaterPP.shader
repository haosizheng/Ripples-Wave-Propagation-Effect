Shader "Unlit/WaterPP"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _ReflectionTex ("Reflection Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

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
            float4 _MainTex_ST;
            sampler2D _ReflectionTex;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                

            float2 resolution = float2(512, 512);
            float3 e = float3(float2(1.0, 1.0) / resolution.xy, 0.0);

            float p10 = tex2D(_MainTex, i.uv - e.zy).x;
            float p01 = tex2D(_MainTex, i.uv - e.xz).x;
            float p21 = tex2D(_MainTex, i.uv + e.xz).x;
            float p12 = tex2D(_MainTex, i.uv + e.zy).x;

            float3 grad = normalize(float3(p21-p01,p12-p10,1.0));
            fixed4 c = tex2D(_ReflectionTex, i.uv*2.0 + grad.xy*.35);

            float3 light = normalize(float3(.2,-.5,.7));
            float diffuse = dot(grad,light);
            float spec = pow(max(0.,-reflect(light,grad).z),12.);
            
            return lerp(c, float4(.7,.8,1.0,1.0),.25)*max(diffuse,0.0) + spec;

            }
            ENDCG
        }
    }
}

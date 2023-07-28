Shader "Custom/SphericalGradient" {
    Properties {
        _Color1 ("Color 1", Color) = (1, 0, 0, 1)
        _Test1("Modifier1", float) = 1
        _Test2("Modifier2", float) = 1
    }

    SubShader {
        Tags { "RenderType"="Opaque" }
        LOD 100
       
       // Cull off

        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"
            
            struct appdata {
                float4 vertex : POSITION;
            };

            struct v2f {
                float4 vertex : SV_POSITION;
            };

            float4 _Color1;
            float _Test1;
            float _Test2;
            

            v2f vert(appdata v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);

                return o;
            }

            fixed4 frag(v2f i) : SV_Target {
                //float3 viewDir = normalize(i.vertex.xyz);
                
                return _Color1;
            }
            ENDCG
        }
        Pass {
            ZTest Always
            
            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"
            
            struct appdata {
                float4 vertex : POSITION;
            };

            struct v2f {
                float4 vertex : SV_POSITION;
            };

            float4 _Color1;
            float _Test1;
            float _Test2;
            

            v2f vert(appdata v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);

                return o;
            }

            fixed4 frag(v2f i) : SV_Target {
                //float3 viewDir = normalize(i.vertex.xyz);

                
                
                return _Color1;
            }

            
            ENDCG
        }
    }
}

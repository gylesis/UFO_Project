Shader "Hidden/NewImageEffectShader"
{
    Properties
    {
        _Test ("Test", float) = 1
        _Pos ("Pos", vector) = (0,0,0,0)
        _ColorOne ("Col1", Color) = (1,1,1,1)
        _ColorTwo ("Col2", Color) = (0,0,0,0.5)
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

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

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;
            float4 _ColorOne;
            float4 _ColorTwo;
            vector _Pos;
            float _Test;

            fixed4 frag(v2f i) : SV_Target
            {

                
                float4 dist = distance(i.vertex ,_Pos);

                float length1 = length(dist);

                float4 outputColor;
                
                if(length1 <= _Test)
                {
                    outputColor = _ColorOne;                    
                }
                else
                {
                    outputColor = _ColorTwo;
                }

                return outputColor;

                fixed4 col = tex2D(_MainTex, i.uv);
                // just invert the colors
                col.rgb = 1 - col.rgb;
                return col;
            }
            ENDCG
        }
    }
}
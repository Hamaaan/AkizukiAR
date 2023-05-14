Shader "Custom/LineArt" {
    Properties {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader {
        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            sampler2D _MainTex;

            struct vertexInput {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct vertexOutput {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            vertexOutput vert(vertexInput input) {
                vertexOutput output;
                output.pos = UnityObjectToClipPos(input.vertex);
                output.uv = input.uv;
                return output;
            }

            float4 frag(vertexOutput input) : COLOR {
                // エッジ検出アルゴリズムの実装
                float4 color = tex2D(_MainTex, input.uv);
                float2 dx = ddx(input.uv);
                float2 dy = ddy(input.uv);
                float gradient = length(float2(dx.x, dy.x)) + length(float2(dx.y, dy.y));
                float threshold = 1.0;
                color.rgb = saturate((gradient - threshold) * 5.0);
                color.a = 1.0;
                return color;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}

Shader "Custom/StencilSkySphere"
{
    Properties
    {
        _MainTex ("Sky Texture", 2D) = "white" {}
    }

    SubShader
    {
        Tags { "Queue" = "Background" "RenderType" = "Opaque" "RenderPipeline" = "UniversalPipeline" }

        Pass
        {
            Stencil
            {
                Ref 1
                Comp Equal  // ステンシルが1の場所だけ天球を描画
                Pass Keep
            }
            Cull Off       // 裏面も描画（カメラが球の中にいるため）
            ZWrite Off     // 深度書き込みなし
            Blend SrcAlpha OneMinusSrcAlpha

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct Attributes
            {
                float4 positionOS : POSITION;
                float2 uv         : TEXCOORD0;
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
                float2 uv          : TEXCOORD0;
            };

            sampler2D _MainTex;
            float4    _MainTex_ST;

            Varyings vert(Attributes IN)
            {
                Varyings OUT;
                OUT.positionHCS = TransformObjectToHClip(IN.positionOS.xyz);
                OUT.uv = TRANSFORM_TEX(IN.uv, _MainTex);
                return OUT;
            }

            half4 frag(Varyings IN) : SV_Target
            {
                return tex2D(_MainTex, IN.uv);
            }
            ENDHLSL
        }
    }
}

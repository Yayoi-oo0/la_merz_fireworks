Shader "Custom/StencilPassthrough"
{
    Properties
    {
        _WorldMin ("World Min (Start of Range)", Vector) = (0,0,0,0)
        _WorldRange ("World Size (X,Y,Z)", Vector) = (1,1,1,0)
    }

    SubShader
    {
        Tags
        {
            "Queue" = "Geometry-1" "RenderType" = "Opaque" "RenderPipeline" = "UniversalPipeline"
        }

        Pass
        {
            Stencil
            {
                Ref 1
                Comp Always
                Pass Replace
            }
            ColorMask 0 // 色は描画しない（ステンシルのみ）
            ZWrite Off
            Cull Off // 両面描画（必要なら）

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct Attributes
            {
                float4 positionOS : POSITION;
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
                float3 worldPos : TEXCOORD0;
            };

            float3 _WorldMin;
            float3 _WorldRange;

            Varyings vert(Attributes IN)
            {
                Varyings OUT;
                float3 worldPos = TransformObjectToWorld(IN.positionOS.xyz);
                OUT.positionHCS = TransformWorldToHClip(worldPos);
                OUT.worldPos = worldPos;
                return OUT;
            }

            half4 frag(Varyings IN) : SV_Target
            {
                float3 relPos = IN.worldPos - _WorldMin;
                float3 normPos = relPos / max(_WorldRange, 1e-4); // 正規化 0〜1

                return half4(0, 0, 0, 0); // ステンシルだけ書く
            }
            ENDHLSL
        }
    }
}

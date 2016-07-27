Shader "Sample/ColorShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Tint Color", Color) = (1, 1, 1, 1)
    }
    SubShader
    {
        Tags { "Queue" = "Transparent" "RenderType" = "Opaque" }
        Blend SrcAlpha OneMinusSrcAlpha
 
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
           
            #include "UnityCG.cginc"
 
            struct input
            {
                float4 pos : POSITION;
                float2 uv : TEXCOORD0;
            };
 
            struct output
            {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
            };
 
            uniform sampler2D _MainTex;
            uniform float4 _MainTex_ST;
            uniform fixed4 _Color;
           
            output vert (input i)
            {
                output o;
 
                o.pos = mul(UNITY_MATRIX_MVP, i.pos);
                o.uv = TRANSFORM_TEX(i.uv, _MainTex);
 
                return o;
            }
           
            half4 frag (output i) : COLOR
            {
                half4 col = tex2D(_MainTex, i.uv);
 
                col *= _Color;
 
                return col;
            }
            ENDCG
        }
    }
}
// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'

// Shader created with Shader Forge Beta 0.34 
// Shader Forge (c) Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:0.34;sub:START;pass:START;ps:flbk:Rutger/WaterBorderOptimized,lico:1,lgpr:1,nrmq:1,limd:1,uamb:True,mssp:True,lmpd:False,lprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,blpr:1,bsrc:3,bdst:7,culm:0,dpts:2,wrdp:False,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:1,x:32505,y:32582|diff-2-RGB,alpha-1322-OUT;n:type:ShaderForge.SFN_Tex2d,id:2,x:33160,y:32775,ptlb:Main Texture,ptin:_MainTexture,ntxv:0,isnm:False|UVIN-16-UVOUT;n:type:ShaderForge.SFN_Panner,id:16,x:33377,y:33003,spu:1,spv:0|UVIN-24-UVOUT,DIST-1304-OUT;n:type:ShaderForge.SFN_Time,id:21,x:33983,y:32978;n:type:ShaderForge.SFN_TexCoord,id:24,x:33592,y:32765,uv:0;n:type:ShaderForge.SFN_ValueProperty,id:1300,x:33884,y:33182,ptlb:Speed,ptin:_Speed,glob:False,v1:2;n:type:ShaderForge.SFN_Multiply,id:1304,x:33644,y:33082|A-21-TSL,B-1300-OUT;n:type:ShaderForge.SFN_Tex2d,id:1308,x:33188,y:32511,ptlb:Distortion Texture,ptin:_DistortionTexture,ntxv:0,isnm:False|UVIN-1310-UVOUT;n:type:ShaderForge.SFN_Panner,id:1310,x:33410,y:32456,spu:1,spv:0|UVIN-1319-UVOUT,DIST-1316-OUT;n:type:ShaderForge.SFN_ValueProperty,id:1315,x:33960,y:32531,ptlb:Speed Distortion,ptin:_SpeedDistortion,glob:False,v1:0;n:type:ShaderForge.SFN_Multiply,id:1316,x:33663,y:32473|A-21-TSL,B-1315-OUT;n:type:ShaderForge.SFN_TexCoord,id:1319,x:33623,y:32343,uv:0;n:type:ShaderForge.SFN_Multiply,id:1322,x:32821,y:32783|A-2-A,B-1308-A,C-1325-OUT;n:type:ShaderForge.SFN_Slider,id:1325,x:33188,y:32698,ptlb:Opacity,ptin:_Opacity,min:0,cur:0,max:1;proporder:1300-1315-2-1308-1325;pass:END;sub:END;*/

Shader "Rutger/WaterBorder" {
    Properties {
        _Speed ("Speed", Float ) = 2
        _SpeedDistortion ("Speed Distortion", Float ) = 0
        _MainTexture ("Main Texture", 2D) = "white" {}
        _DistortionTexture ("Distortion Texture", 2D) = "white" {}
        _Opacity ("Opacity", Range(0, 1)) = 0
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        Pass {
            Name "ForwardBase"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma exclude_renderers xbox360 ps3 flash 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform float4 _TimeEditor;
            uniform sampler2D _MainTexture; uniform float4 _MainTexture_ST;
            uniform float _Speed;
            uniform sampler2D _DistortionTexture; uniform float4 _DistortionTexture_ST;
            uniform float _SpeedDistortion;
            uniform float _Opacity;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o;
                o.uv0 = v.texcoord0;
                o.normalDir = mul(float4(v.normal,0), unity_WorldToObject).xyz;
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
/////// Normals:
                float3 normalDirection =  i.normalDir;
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
////// Lighting:
                float attenuation = 1;
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = dot( normalDirection, lightDirection );
                float3 diffuse = max( 0.0, NdotL) * attenColor + UNITY_LIGHTMODEL_AMBIENT.rgb;
                float3 finalColor = 0;
                float3 diffuseLight = diffuse;
                float4 node_21 = _Time + _TimeEditor;
                float2 node_16 = (i.uv0.rg+(node_21.r*_Speed)*float2(1,0));
                float4 node_2 = tex2D(_MainTexture,TRANSFORM_TEX(node_16, _MainTexture));
                finalColor += diffuseLight * node_2.rgb;
                float2 node_1310 = (i.uv0.rg+(node_21.r*_SpeedDistortion)*float2(1,0));
/// Final Color:
                return fixed4(finalColor,(node_2.a*tex2D(_DistortionTexture,TRANSFORM_TEX(node_1310, _DistortionTexture)).a*_Opacity));
            }
            ENDCG
        }
        Pass {
            Name "ForwardAdd"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            ZWrite Off
            
            Fog { Color (0,0,0,0) }
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdadd
            #pragma exclude_renderers xbox360 ps3 flash 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform float4 _TimeEditor;
            uniform sampler2D _MainTexture; uniform float4 _MainTexture_ST;
            uniform float _Speed;
            uniform sampler2D _DistortionTexture; uniform float4 _DistortionTexture_ST;
            uniform float _SpeedDistortion;
            uniform float _Opacity;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                LIGHTING_COORDS(3,4)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o;
                o.uv0 = v.texcoord0;
                o.normalDir = mul(float4(v.normal,0), unity_WorldToObject).xyz;
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
/////// Normals:
                float3 normalDirection =  i.normalDir;
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = dot( normalDirection, lightDirection );
                float3 diffuse = max( 0.0, NdotL) * attenColor;
                float3 finalColor = 0;
                float3 diffuseLight = diffuse;
                float4 node_21 = _Time + _TimeEditor;
                float2 node_16 = (i.uv0.rg+(node_21.r*_Speed)*float2(1,0));
                float4 node_2 = tex2D(_MainTexture,TRANSFORM_TEX(node_16, _MainTexture));
                finalColor += diffuseLight * node_2.rgb;
                float2 node_1310 = (i.uv0.rg+(node_21.r*_SpeedDistortion)*float2(1,0));
/// Final Color:
                return fixed4(finalColor * (node_2.a*tex2D(_DistortionTexture,TRANSFORM_TEX(node_1310, _DistortionTexture)).a*_Opacity),0);
            }
            ENDCG
        }
    }
    FallBack "Rutger/WaterBorderOptimized"
    CustomEditor "ShaderForgeMaterialInspector"
}

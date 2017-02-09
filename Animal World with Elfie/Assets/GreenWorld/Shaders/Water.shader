// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Upgrade NOTE: commented out 'float4 unity_DynamicLightmapST', a built-in variable
// Upgrade NOTE: commented out 'float4 unity_LightmapST', a built-in variable

// Shader created with Shader Forge v1.04 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.04;sub:START;pass:START;ps:flbk:Unlit/Transparent,lico:1,lgpr:1,nrmq:1,limd:1,uamb:True,mssp:True,lmpd:False,lprd:False,rprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,tesm:0,blpr:1,bsrc:3,bdst:7,culm:0,dpts:2,wrdp:False,dith:2,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:1,x:33462,y:32456,varname:node_1,prsc:2|diff-395-RGB,alpha-1243-OUT;n:type:ShaderForge.SFN_Panner,id:93,x:31811,y:32667,varname:node_93,prsc:2,spu:1,spv:1|DIST-192-OUT;n:type:ShaderForge.SFN_Time,id:96,x:31358,y:32554,varname:node_96,prsc:2;n:type:ShaderForge.SFN_Multiply,id:192,x:31615,y:32688,varname:node_192,prsc:2|A-96-TSL,B-717-OUT;n:type:ShaderForge.SFN_Tex2d,id:205,x:32181,y:32698,ptovrint:False,ptlb:Distortion Texture,ptin:_DistortionTexture,varname:node_6769,prsc:2,ntxv:3,isnm:False|UVIN-93-UVOUT;n:type:ShaderForge.SFN_Color,id:395,x:32470,y:32335,ptovrint:False,ptlb:Water Color,ptin:_WaterColor,varname:node_1226,prsc:2,glob:False,c1:0.7794118,c2:0.8356997,c3:1,c4:1;n:type:ShaderForge.SFN_ValueProperty,id:717,x:31358,y:32706,ptovrint:False,ptlb:Speed,ptin:_Speed,varname:node_7568,prsc:2,glob:False,v1:1;n:type:ShaderForge.SFN_TexCoord,id:995,x:32259,y:32531,varname:node_995,prsc:2,uv:0;n:type:ShaderForge.SFN_Tex2d,id:1070,x:32833,y:32677,ptovrint:False,ptlb:MainTex,ptin:_MainTex,varname:node_3424,prsc:2,ntxv:0,isnm:False|UVIN-1236-OUT;n:type:ShaderForge.SFN_Multiply,id:1125,x:32493,y:32614,varname:node_1125,prsc:2|A-995-UVOUT,B-205-RGB;n:type:ShaderForge.SFN_Panner,id:1176,x:32466,y:32785,varname:node_1176,prsc:2,spu:1,spv:1|UVIN-995-UVOUT,DIST-1185-OUT;n:type:ShaderForge.SFN_ValueProperty,id:1184,x:32028,y:33069,ptovrint:False,ptlb:Speed 2,ptin:_Speed2,varname:node_7184,prsc:2,glob:False,v1:-1;n:type:ShaderForge.SFN_Multiply,id:1185,x:32220,y:32929,varname:node_1185,prsc:2|A-192-OUT,B-1184-OUT;n:type:ShaderForge.SFN_Add,id:1236,x:32698,y:32614,varname:node_1236,prsc:2|A-1125-OUT,B-1176-UVOUT;n:type:ShaderForge.SFN_Multiply,id:1243,x:33020,y:32599,varname:node_1243,prsc:2|A-395-A,B-1070-A;proporder:395-717-205-1184-1070;pass:END;sub:END;*/

Shader "Rutger/Water" {
    Properties {
        _WaterColor ("Water Color", Color) = (0.7794118,0.8356997,1,1)
        _Speed ("Speed", Float ) = 1
        _DistortionTexture ("Distortion Texture", 2D) = "bump" {}
        _Speed2 ("Speed 2", Float ) = -1
        _MainTex ("MainTex", 2D) = "white" {}
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
            #pragma multi_compile_fog
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform float4 _TimeEditor;
            // float4 unity_LightmapST;
            #ifdef DYNAMICLIGHTMAP_ON
                // float4 unity_DynamicLightmapST;
            #endif
            uniform sampler2D _DistortionTexture; uniform float4 _DistortionTexture_ST;
            uniform float4 _WaterColor;
            uniform float _Speed;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform float _Speed2;
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
                UNITY_FOG_COORDS(3)
                #ifndef LIGHTMAP_OFF
                    float4 uvLM : TEXCOORD4;
                #else
                    float3 shLight : TEXCOORD4;
                #endif
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = mul(unity_ObjectToWorld, float4(v.normal,0)).xyz;
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
/////// Vectors:
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = 1;
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 indirectDiffuse = float3(0,0,0);
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                indirectDiffuse += UNITY_LIGHTMODEL_AMBIENT.rgb; // Ambient Light
                float3 diffuse = (directDiffuse + indirectDiffuse) * _WaterColor.rgb;
/// Final Color:
                float3 finalColor = diffuse;
                float4 node_96 = _Time + _TimeEditor;
                float node_192 = (node_96.r*_Speed);
                float2 node_93 = (i.uv0+node_192*float2(1,1));
                float4 _DistortionTexture_var = tex2D(_DistortionTexture,TRANSFORM_TEX(node_93, _DistortionTexture));
                float3 node_1236 = ((float3(i.uv0,0.0)*_DistortionTexture_var.rgb)+float3((i.uv0+(node_192*_Speed2)*float2(1,1)),0.0));
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(node_1236, _MainTex));
                fixed4 finalRGBA = fixed4(finalColor,(_WaterColor.a*_MainTex_var.a));
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
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
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdadd
            #pragma multi_compile_fog
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform float4 _TimeEditor;
            // float4 unity_LightmapST;
            #ifdef DYNAMICLIGHTMAP_ON
                // float4 unity_DynamicLightmapST;
            #endif
            uniform sampler2D _DistortionTexture; uniform float4 _DistortionTexture_ST;
            uniform float4 _WaterColor;
            uniform float _Speed;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform float _Speed2;
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
                #ifndef LIGHTMAP_OFF
                    float4 uvLM : TEXCOORD5;
                #else
                    float3 shLight : TEXCOORD5;
                #endif
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = mul(unity_ObjectToWorld, float4(v.normal,0)).xyz;
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
/////// Vectors:
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float3 diffuse = directDiffuse * _WaterColor.rgb;
/// Final Color:
                float3 finalColor = diffuse;
                float4 node_96 = _Time + _TimeEditor;
                float node_192 = (node_96.r*_Speed);
                float2 node_93 = (i.uv0+node_192*float2(1,1));
                float4 _DistortionTexture_var = tex2D(_DistortionTexture,TRANSFORM_TEX(node_93, _DistortionTexture));
                float3 node_1236 = ((float3(i.uv0,0.0)*_DistortionTexture_var.rgb)+float3((i.uv0+(node_192*_Speed2)*float2(1,1)),0.0));
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(node_1236, _MainTex));
                return fixed4(finalColor * (_WaterColor.a*_MainTex_var.a),0);
            }
            ENDCG
        }
    }
    FallBack "Unlit/Transparent"
    CustomEditor "ShaderForgeMaterialInspector"
}

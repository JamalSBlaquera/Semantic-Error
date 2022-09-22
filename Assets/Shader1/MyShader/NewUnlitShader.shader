// Shader created with Shader Forge v1.40 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.40;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,cpap:True,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:6522,x:33172,y:32657,varname:node_6522,prsc:2|custl-5022-OUT;n:type:ShaderForge.SFN_LightVector,id:544,x:31715,y:32704,varname:node_544,prsc:2;n:type:ShaderForge.SFN_NormalVector,id:6730,x:31715,y:32855,prsc:2,pt:False;n:type:ShaderForge.SFN_Dot,id:1853,x:31936,y:32704,varname:node_1853,prsc:2,dt:0|A-544-OUT,B-6730-OUT;n:type:ShaderForge.SFN_LightAttenuation,id:3271,x:31936,y:32882,varname:node_3271,prsc:2;n:type:ShaderForge.SFN_Multiply,id:3304,x:32142,y:32782,varname:node_3304,prsc:2|A-1853-OUT,B-3271-OUT;n:type:ShaderForge.SFN_Append,id:7923,x:32357,y:32770,varname:node_7923,prsc:2|A-3304-OUT,B-3304-OUT;n:type:ShaderForge.SFN_Tex2d,id:479,x:32556,y:32770,ptovrint:False,ptlb:node_479,ptin:_node_479,varname:node_479,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:a18067604964b5a4d8fd5552ed0d789e,ntxv:0,isnm:False|UVIN-7923-OUT;n:type:ShaderForge.SFN_Blend,id:8132,x:32772,y:32618,varname:node_8132,prsc:2,blmd:5,clmp:True|SRC-8731-OUT,DST-479-RGB;n:type:ShaderForge.SFN_Slider,id:8731,x:32326,y:32573,ptovrint:False,ptlb:node_8731,ptin:_node_8731,varname:node_8731,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:1;n:type:ShaderForge.SFN_Blend,id:5022,x:32790,y:32860,varname:node_5022,prsc:2,blmd:3,clmp:True|SRC-8132-OUT,DST-5983-RGB;n:type:ShaderForge.SFN_Tex2d,id:5983,x:32537,y:32978,ptovrint:False,ptlb:Color Map,ptin:_ColorMap,varname:node_5983,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:89ede59217e3847c39c444f223f4084e,ntxv:0,isnm:False;proporder:479-8731-5983;pass:END;sub:END;*/

Shader "Unlit/NewUnlitShader" {
    Properties {
        _node_479 ("node_479", 2D) = "white" {}
        _node_8731 ("node_8731", Range(0, 1)) = 1
        _ColorMap ("Color Map", 2D) = "white" {}
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        LOD 100
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_instancing
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile_fog
            #pragma target 3.0
            uniform sampler2D _node_479; uniform float4 _node_479_ST;
            uniform sampler2D _ColorMap; uniform float4 _ColorMap_ST;
            UNITY_INSTANCING_BUFFER_START( Props )
                UNITY_DEFINE_INSTANCED_PROP( float, _node_8731)
            UNITY_INSTANCING_BUFFER_END( Props )
            struct VertexInput {
                UNITY_VERTEX_INPUT_INSTANCE_ID
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                UNITY_VERTEX_INPUT_INSTANCE_ID
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                LIGHTING_COORDS(3,4)
                UNITY_FOG_COORDS(5)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                UNITY_SETUP_INSTANCE_ID( v );
                UNITY_TRANSFER_INSTANCE_ID( v, o );
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                UNITY_SETUP_INSTANCE_ID( i );
                i.normalDir = normalize(i.normalDir);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float _node_8731_var = UNITY_ACCESS_INSTANCED_PROP( Props, _node_8731 );
                float node_3304 = (dot(lightDirection,i.normalDir)*attenuation);
                float2 node_7923 = float2(node_3304,node_3304);
                float4 _node_479_var = tex2D(_node_479,TRANSFORM_TEX(node_7923, _node_479));
                float4 _ColorMap_var = tex2D(_ColorMap,TRANSFORM_TEX(i.uv0, _ColorMap));
                float3 finalColor = saturate((saturate(max(_node_8731_var,_node_479_var.rgb))+_ColorMap_var.rgb-1.0));
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "FORWARD_DELTA"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_instancing
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"
            #pragma multi_compile_fwdadd_fullshadows
            #pragma multi_compile_fog
            #pragma target 3.0
            uniform sampler2D _node_479; uniform float4 _node_479_ST;
            uniform sampler2D _ColorMap; uniform float4 _ColorMap_ST;
            UNITY_INSTANCING_BUFFER_START( Props )
                UNITY_DEFINE_INSTANCED_PROP( float, _node_8731)
            UNITY_INSTANCING_BUFFER_END( Props )
            struct VertexInput {
                UNITY_VERTEX_INPUT_INSTANCE_ID
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                UNITY_VERTEX_INPUT_INSTANCE_ID
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                LIGHTING_COORDS(3,4)
                UNITY_FOG_COORDS(5)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                UNITY_SETUP_INSTANCE_ID( v );
                UNITY_TRANSFER_INSTANCE_ID( v, o );
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                UNITY_SETUP_INSTANCE_ID( i );
                i.normalDir = normalize(i.normalDir);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float _node_8731_var = UNITY_ACCESS_INSTANCED_PROP( Props, _node_8731 );
                float node_3304 = (dot(lightDirection,i.normalDir)*attenuation);
                float2 node_7923 = float2(node_3304,node_3304);
                float4 _node_479_var = tex2D(_node_479,TRANSFORM_TEX(node_7923, _node_479));
                float4 _ColorMap_var = tex2D(_ColorMap,TRANSFORM_TEX(i.uv0, _ColorMap));
                float3 finalColor = saturate((saturate(max(_node_8731_var,_node_479_var.rgb))+_ColorMap_var.rgb-1.0));
                fixed4 finalRGBA = fixed4(finalColor * 1,0);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}

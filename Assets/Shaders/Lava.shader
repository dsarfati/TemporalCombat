// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:True,fgod:False,fgor:False,fgmd:0,fgcr:0,fgcg:0,fgcb:0,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:True,fnfb:True,fsmp:False;n:type:ShaderForge.SFN_Final,id:4795,x:32724,y:32693,varname:node_4795,prsc:2|emission-2393-OUT,alpha-7806-OUT;n:type:ShaderForge.SFN_Tex2d,id:6074,x:32168,y:32539,ptovrint:False,ptlb:MainTex,ptin:_MainTex,varname:_MainTex,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:2393,x:32495,y:32793,varname:node_2393,prsc:2|A-6074-RGB,B-2053-RGB,C-4441-OUT;n:type:ShaderForge.SFN_VertexColor,id:2053,x:32130,y:32759,varname:node_2053,prsc:2;n:type:ShaderForge.SFN_Slider,id:4441,x:32150,y:32918,ptovrint:False,ptlb:Intensity,ptin:_Intensity,varname:_Intensity,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0.1,cur:1,max:5;n:type:ShaderForge.SFN_Tex2d,id:4956,x:31574,y:32901,ptovrint:False,ptlb:AlphaMap,ptin:_AlphaMap,varname:_AlphaMap,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:28c7aad1372ff114b90d330f8a2dd938,ntxv:0,isnm:False|UVIN-8748-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:1149,x:31121,y:32538,varname:node_1149,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Panner,id:8748,x:31206,y:32843,varname:node_8748,prsc:2,spu:0,spv:0.055|UVIN-1149-UVOUT;n:type:ShaderForge.SFN_RemapRange,id:4592,x:31818,y:32923,varname:node_4592,prsc:2,frmn:0,frmx:1,tomn:-0.5,tomx:1|IN-4956-RGB;n:type:ShaderForge.SFN_Desaturate,id:5787,x:32150,y:33044,varname:node_5787,prsc:2|COL-4592-OUT;n:type:ShaderForge.SFN_Multiply,id:7806,x:32525,y:32949,varname:node_7806,prsc:2|A-6074-A,B-7812-OUT;n:type:ShaderForge.SFN_OneMinus,id:6633,x:31618,y:33183,varname:node_6633,prsc:2|IN-7728-RGB;n:type:ShaderForge.SFN_RemapRange,id:7076,x:31976,y:33126,varname:node_7076,prsc:2,frmn:0,frmx:1,tomn:-0.25,tomx:1|IN-6633-OUT;n:type:ShaderForge.SFN_Desaturate,id:5636,x:32150,y:33188,varname:node_5636,prsc:2|COL-7076-OUT;n:type:ShaderForge.SFN_Add,id:7812,x:32369,y:33044,varname:node_7812,prsc:2|A-5787-OUT,B-5636-OUT;n:type:ShaderForge.SFN_Tex2d,id:7728,x:31155,y:33200,ptovrint:False,ptlb:AlphaMap_copy,ptin:_AlphaMap_copy,varname:_AlphaMap_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:28c7aad1372ff114b90d330f8a2dd938,ntxv:0,isnm:False|UVIN-3570-UVOUT;n:type:ShaderForge.SFN_Panner,id:3570,x:31719,y:32756,varname:node_3570,prsc:2,spu:0.05,spv:-0.03|UVIN-5262-UVOUT;n:type:ShaderForge.SFN_Rotator,id:5262,x:31458,y:32574,varname:node_5262,prsc:2|UVIN-1149-UVOUT,SPD-4406-OUT;n:type:ShaderForge.SFN_Time,id:3825,x:30907,y:32672,varname:node_3825,prsc:2;n:type:ShaderForge.SFN_Multiply,id:4406,x:31163,y:32687,varname:node_4406,prsc:2|A-3825-TSL,B-3223-OUT;n:type:ShaderForge.SFN_Slider,id:3223,x:30750,y:32863,ptovrint:False,ptlb:SecondaryRotation,ptin:_SecondaryRotation,varname:_SecondaryRotation,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.001,max:1;proporder:6074-4441-4956-7728-3223;pass:END;sub:END;*/

Shader "Shader Forge/Lava" {
    Properties {
        _MainTex ("MainTex", 2D) = "white" {}
        _Intensity ("Intensity", Range(0.1, 5)) = 1
        _AlphaMap ("AlphaMap", 2D) = "white" {}
        _AlphaMap_copy ("AlphaMap_copy", 2D) = "white" {}
        _SecondaryRotation ("SecondaryRotation", Range(0, 1)) = 0.001
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        Pass {
            Name "FORWARD"
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
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform float _Intensity;
            uniform sampler2D _AlphaMap; uniform float4 _AlphaMap_ST;
            uniform sampler2D _AlphaMap_copy; uniform float4 _AlphaMap_copy_ST;
            uniform float _SecondaryRotation;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 vertexColor : COLOR;
                UNITY_FOG_COORDS(1)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
////// Lighting:
////// Emissive:
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                float3 emissive = (_MainTex_var.rgb*i.vertexColor.rgb*_Intensity);
                float3 finalColor = emissive;
                float4 node_992 = _Time;
                float2 node_8748 = (i.uv0+node_992.g*float2(0,0.055));
                float4 _AlphaMap_var = tex2D(_AlphaMap,TRANSFORM_TEX(node_8748, _AlphaMap));
                float4 node_3825 = _Time;
                float node_5262_ang = node_992.g;
                float node_5262_spd = (node_3825.r*_SecondaryRotation);
                float node_5262_cos = cos(node_5262_spd*node_5262_ang);
                float node_5262_sin = sin(node_5262_spd*node_5262_ang);
                float2 node_5262_piv = float2(0.5,0.5);
                float2 node_5262 = (mul(i.uv0-node_5262_piv,float2x2( node_5262_cos, -node_5262_sin, node_5262_sin, node_5262_cos))+node_5262_piv);
                float2 node_3570 = (node_5262+node_992.g*float2(0.05,-0.03));
                float4 _AlphaMap_copy_var = tex2D(_AlphaMap_copy,TRANSFORM_TEX(node_3570, _AlphaMap_copy));
                fixed4 finalRGBA = fixed4(finalColor,(_MainTex_var.a*(dot((_AlphaMap_var.rgb*1.5+-0.5),float3(0.3,0.59,0.11))+dot(((1.0 - _AlphaMap_copy_var.rgb)*1.25+-0.25),float3(0.3,0.59,0.11)))));
                UNITY_APPLY_FOG_COLOR(i.fogCoord, finalRGBA, fixed4(0,0,0,1));
                return finalRGBA;
            }
            ENDCG
        }
    }
    CustomEditor "ShaderForgeMaterialInspector"
}

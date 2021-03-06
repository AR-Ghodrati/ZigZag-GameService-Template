﻿// Upgrade NOTE: upgraded instancing buffer 'Props' to new syntax.

Shader "Custom/BasicSurface" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Lambert

		struct Input {
			half nothing;
		};

		// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
		// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
		// #pragma instancing_options assumeuniformscaling
		UNITY_INSTANCING_BUFFER_START(Props)
			UNITY_DEFINE_INSTANCED_PROP(float4, _Color)
#define _Color_arr Props
		UNITY_INSTANCING_BUFFER_END(Props)

		void surf (Input IN, inout SurfaceOutput o) {
			o.Albedo = UNITY_ACCESS_INSTANCED_PROP(_Color_arr, _Color);
		}
		ENDCG
	}
	FallBack "Diffuse"
}

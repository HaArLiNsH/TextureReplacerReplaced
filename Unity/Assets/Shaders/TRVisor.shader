// Updated by RangeMachine
Shader "KSP/TR/Visor"
{
	Properties
	{
		_Color("Main Color", Color) = (1,1,1,1)
		_BumpMap("_BumpMap", 2D) = "bump" {}
		_SpecColor("Specular Color", Color) = (0.5, 0.5, 0.5, 0)
		_Shininess("Shininess", Range(0.01, 1)) = 0.078125
		_ReflectColor("Reflection Color", Color) = (1,1,1,0.5)
		_MainTex("Base (RGB) Gloss (A)", 2D) = "white" {}
		_Cube("Reflection Cubemap", Cube) = "_Skybox" {}
	}
		SubShader
	{
		Tags{ "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" }

		CGPROGRAM

#pragma surface surf BlinnPhong alpha
#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _BumpMap;
	samplerCUBE _Cube;

	fixed4 _Color;
	fixed4 _ReflectColor;
	half _Shininess;

	struct Input
	{
		float2 uv_MainTex;
		float2 uv_BumpMap;
		float3 worldRefl;
		INTERNAL_DATA
	};

	void surf(Input IN, inout SurfaceOutput o)
	{
		fixed4 tex = tex2D(_MainTex, IN.uv_MainTex);
		float3 normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));


		o.Albedo = tex.rgb * _Color.rgb;
		o.Specular = max(0.01, _Shininess);
		o.Normal = normal;

		float3 worldRefl = WorldReflectionVector(IN, o.Normal);
		fixed4 reflcol = texCUBE(_Cube, worldRefl);

		o.Emission = reflcol.rgb * _ReflectColor.rgb * tex.a;
		o.Alpha = tex.a;
	}

	ENDCG
	}
}
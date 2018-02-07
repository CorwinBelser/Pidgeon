Shader "Custom/SandyStepShader" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_Ramp ("Ramp (RGB)", 2D) = "white" {}
		_NoiseTexture ("_NoiseTexture", 2D) = "white" {}
		_Shift ("Shift", Range(0,1)) = .5
		_Cutoff ("Cutoff", Range(0,1)) = .6
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200

	CGPROGRAM
    #pragma surface surf Ramp

    sampler2D _Ramp;
    fixed4 _Color;
    sampler2D _NoiseTexture;
    float _Shift;
    float _Cutoff;

    half4 LightingRamp (SurfaceOutput s, half3 lightDir, half atten) {
        half NdotL = dot (s.Normal, lightDir);
        half diff = (NdotL * .5 + .5);
        half3 ramp = tex2D (_Ramp, float2(diff,.5)).rgb;
        half4 c;
        c.rgb = s.Albedo * _LightColor0.rgb * ramp * atten;
//        c.rgb = s.Albedo * atten;
//        c.a = s.Alpha;
		c.a = 1;
        return c;
    }

    struct Input {
        float2 uv_NoiseTexture;
    };
    // turn this also into a pixel disintegrate shader with the pixel noise texture
    void surf (Input IN, inout SurfaceOutput o) {
    	half3 noise = tex2D (_NoiseTexture, IN.uv_NoiseTexture).rgb;
    	half3 noise2 = tex2D (_NoiseTexture, fixed2(frac(IN.uv_NoiseTexture.x + _Shift), frac(IN.uv_NoiseTexture.y + _Shift))).rgb;
    	half3 combined = noise - noise2;
    	o.Albedo = .8 + (1 - combined) * step(_Cutoff, combined);
//        o.Albedo = tex2D (_MainTex, IN.uv_MainTex).rgb;
    }
    ENDCG
	}
	FallBack "Diffuse"
}

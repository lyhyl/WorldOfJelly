texture2D mrk_texture;
float threshold = 0.0f;
float delta = 0.2f;
float invdelta = 5.0f;

sampler2D dis_sampler;
sampler2D mrk_sampler = sampler_state
{
	Texture = mrk_texture;
	
	AddressU = Clamp;
    AddressV = Clamp;
};

float4 Transition(float2 texcood : TEXCOORD0) : COLOR0
{
    float4 color;
    color = tex2D(mrk_sampler, texcood.xy);
	float trigray = color.r + color.g + color.b;
	float trithr = 3 - threshold * 3;
	if(trigray < trithr)
	{
		color.rgb = tex2D(dis_sampler, texcood.xy);
		color.a = 1;

		float tridelta = delta * 3;
		float tridiff = trithr - tridelta;
		if(trigray > tridiff)
			color.a -= (trigray - tridiff) * invdelta;
	}
	else
		color.a = 0;
	return color;
}

technique TechTransition
{
	pass PassTransition
	{
		PixelShader = compile ps_2_0 Transition();
	}
}
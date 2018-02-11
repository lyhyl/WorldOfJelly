float4x4 Transform;
float a;
float3 r,g,b;

sampler2D texsampler;

struct Vertex
{
	float4 pos : POSITION;
	float2 uv : TEXCOORD;
};

Vertex VertexShaderFunction(Vertex v)
{
	Vertex ov;
	ov.pos = mul(v.pos, Transform);
	ov.uv = v.uv;
    return ov;
}

float4 PixelShaderFunction(float2 texcood : TEXCOORD) : COLOR
{
	float4 outpx;
	outpx = tex2D(texsampler, texcood.xy);
	float trigray = outpx.r + outpx.g + outpx.b;
	outpx.rgb = r, g, b;
	outpx.a = 1 - trigray / 3.0f;
	outpx.a *= a;
    return outpx;
}

technique Technique1
{
    pass Pass1
    {
        VertexShader = compile vs_2_0 VertexShaderFunction();
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
}

sampler uImage0 : register(s0);
float3 uColor;
float3 uSecondaryColor;
float2 uScreenResolution;
float uOpacity;
float uSaturation;
float uRotation;
float uTime;
float4 uSourceRect;
float2 uWorldPosition;
float uDirection;
float3 uLightSource;
float2 uImageSize0;
float2 uImageSize1;


float4 PixelShaderFunction(float2 coord : TEXCOORD0) : COLOR0
{
	float4 color = tex2D(uImage0, coord);
	
	if (!any(color))
        return color;

	float2 up = float2(coord.x, coord.y - 1.0/1024);
	float4 colorUp = tex2D(uImage0, up);

	if (!any(colorUp))
        return color;

	float2 down = float2(coord.x, coord.y + 1.0/1024);
	float4 colorDown = tex2D(uImage0, down);
	if (!any(colorDown))
        return color;
	float4 delY = colorUp - colorDown;
	
	// Vertical energy
	float vertEnergy = delY.x * delY.x + delY.y * delY.y + delY.z * delY.z;

	float2 left = float2(coord.x - 1.0/1024, coord.y);
	float4 colorLeft = tex2D(uImage0, left);
	if (!any(colorLeft))
        return color;

	float2 right = float2(coord.x + 1.0/1024, coord.y);
	float4 colorRight = tex2D(uImage0, right);
	if (!any(colorRight))
        return color;
	float4 delX = colorLeft - colorRight;

	float horiEnergy = delX.x * delX.x + delX.y * delX.y + delX.z * delX.z;
	float energy = sqrt(vertEnergy + horiEnergy);
	if(energy > 20000)
		energy = 1.0
	else
		energy = 0.0
	
    return float4(energy, energy, energy, color.w);

}

technique Technique1
{
    pass Pass1
    {
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
}


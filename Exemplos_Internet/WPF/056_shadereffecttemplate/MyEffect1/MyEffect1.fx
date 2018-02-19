sampler2D input : register(s0); 
float someInput : register(c0); 

float4 main(float2 uv : TEXCOORD) : COLOR { 
	float4 color;
	uv.x = uv.x + cos((uv.x-someInput)*50)*0.02;
	uv.y = uv.y + sin((uv.y-someInput)*50)*0.02;
	color = tex2D(input, uv.xy); 
	
	// uncomment the line below to invert the red color
	//color.r = -color.r; 
	return color;    
}

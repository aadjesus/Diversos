// remember to save .fx file using Codepage 1252

sampler2D input1 : register(S0); // right image input
sampler2D input2 : register(S1); // left image input
 
float4 main(float2 uv : TEXCOORD) : COLOR
{ 
	float4 Color1; 
	Color1 = tex2D( input1 , uv.xy); 
	
	float4 Color2; 
	Color2 = tex2D( input2 , uv.xy); 

	Color1.r = Color2.r; 
	Color1.g = Color1.g; 
	Color1.b = Color1.b; 
    Color1.a = max(Color1.a,Color2.a);
    
	return Color1;
}

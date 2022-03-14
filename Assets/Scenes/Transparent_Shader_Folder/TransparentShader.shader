Shader "Custom/TransparentShader"
{
	SubShader
	{	
		Tags{"Queue" = "Transparent+1"}

		Pass
		{
			Blend Zero One
		}

	}
}

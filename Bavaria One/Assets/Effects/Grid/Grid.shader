Shader "Grid" 
{
  Properties 
  {
    [PerRendererData] _GridSpacing ("Grid Spacing", Float) = 1.0
    [PerRendererData] _ConnectionTexture ("Connection Texture", 2D) = "white" {}
    [PerRendererData] _GridThickness ("Grid Thickness", Float) = 0.03
    [PerRendererData] _GridColor ("Grid Color", Color) = (0.5, 0.5, 0.5, 0.5)
    [PerRendererData] _ConnectionThickness ("Connection Thickness", Float) = 0.1
    [PerRendererData] _ConnectionColor ("Connection Color", Color) = (0.5, 0.5, 0.5, 0.5)
    [PerRendererData] _BaseColor ("Base Color", Color) = (0.0, 0.0, 0.0, 0.0)
  }
    
  SubShader 
  {
    Lighting Off
    Cull Off
    ZWrite Off
    ZTest LEqual
    Fog { Mode Off }
    
    Tags { "Queue"="Transparent+1" }
    
    Pass 
    {
      Blend SrcAlpha OneMinusSrcAlpha
    
      CGPROGRAM
    
      #pragma vertex vert
      #pragma fragment frag
    
      float _GridSpacing;
      sampler2D _ConnectionTexture;
      float _GridThickness;
      float4 _GridColor;
      float _ConnectionThickness;
      float4 _ConnectionColor;
      float4 _BaseColor;
    
      struct vertexInput 
      {
          float4 vertex : POSITION;
      };

      struct vertexOutput 
      {
        float4 pos : SV_POSITION;
        float4 worldPos : TEXCOORD0;
      };
    
      // VERTEX SHADER
      vertexOutput vert(vertexInput input) 
      {
        vertexOutput output;
        output.pos = UnityObjectToClipPos(input.vertex);
        output.worldPos = mul(unity_ObjectToWorld, input.vertex);
        return output;
      }

      // FRAGMENT SHADER
      float4 frag(vertexOutput input) : COLOR 
      {
        float xOffset = frac(input.worldPos.x / _GridSpacing);
        float x = (floor(input.worldPos.x / _GridSpacing) + 0.5) / 100;
        int xHigh = 0;
        if (xOffset > 0.5f) 
        {
          xOffset = 1 - xOffset;
          xHigh = 1;
        }
        float zOffset = frac(input.worldPos.z / _GridSpacing);
        float z = (floor(input.worldPos.z / _GridSpacing) + 0.5) / 100;
        int zHigh = 0;
        if (zOffset > 0.5f) 
        {
          zOffset = 1 - zOffset;
          zHigh = 1;
        }

        if (xOffset < _ConnectionThickness || zOffset < _ConnectionThickness)
        {
          float4 cons = tex2D(_ConnectionTexture, float2(x, z));
          if(zOffset < xOffset && zHigh == 1 && cons.r > 0.0)
            return _ConnectionColor;
          if(xOffset < zOffset && xHigh == 1 && cons.g > 0.0)
            return _ConnectionColor;
          if(zOffset < xOffset && zHigh != 1 && cons.b > 0.0)
            return _ConnectionColor;
          if(xOffset < zOffset && xHigh != 1 && cons.a > 0.0)
            return _ConnectionColor;
        }
        if (xOffset < _GridThickness || zOffset < _GridThickness)
        {
          return _GridColor;
        }
        return _BaseColor;
      }
      ENDCG
    }
  }
}
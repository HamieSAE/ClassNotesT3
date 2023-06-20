Shader "Custom/GridWaveShader"
{
    Properties
    {
        _Color("Color", Color) = (1,1,1,1)
        _MainTex("Albedo (RGB)", 2D) = "white" {}
        _Glossiness("Smoothness", Range(0,1)) = 0.5
        _Metallic("Metallic", Range(0,1)) = 0.0
    }
        SubShader
        {
            Tags { "RenderType" = "Opaque" }
            LOD 200

            CGPROGRAM
            // Made by Danilo Paulo, 09/09/2022
    // Don't forget to hit play on the iChannel0 \/
    //------------------------------------------
    // Global Variables, feel free to play with them! :D

    float GridSize = 50.0; // Grid Sizes lower than 30 don't work very well
    float LineThick = 0.1; // Min 0.05, Max 0.45 for better results;
    float Amplitude = 1.5; // How High is the wave
    float Animating = 0.0;
    float beatTime = 0.5;
    float WaveCurve = 2.0;

    float getFrequencies(in vec2 uv)
    {
        return texture(iChannel0, vec2(0.0001)).r;
    }

    //------------------------------------------
    // Hash23 Function for Random Colors
    vec3 Hash23(vec2 UV) {

        vec3 RGB1 = vec3(UV.x,UV.y,UV.x) * vec3(127.0, 312.0, 74.7);
        vec3 RGB2 = vec3(UV.y,UV.x,UV.x) * vec3(270.0, 183.0, 246.0);
        vec3 RGB3 = vec3(UV.x,UV.y,UV.y) * vec3(114.0, 272.0, 125.0);

        vec3 RGBF = fract(sin(vec3(RGB1.x + RGB1.y + RGB1.z,RGB2.x + RGB2.y + RGB2.z,RGB3.x + RGB3.y + RGB3.z)) * 43758.546875);

        return RGBF;

    }

    //------------------------------------------
    // Grid and Vertical Bars
    vec3 GridOutput(vec2 UV, vec2 grid) {

        float Grid = 0.0;
        float GridV = 0.0;
        float GridH = 0.0;


        vec2 UVTiled = UV * grid;

        vec2 GridM = fract(UVTiled);
        GridM = ceil(clamp((1.0 - smoothstep(0.0,LineThick,GridM)) + (1.0 - smoothstep(0.0,LineThick,(1.0 - GridM))),0.0,1.0));


        Grid = 1.0 - (GridM.x + GridM.y);


        vec2 Square = 2.0 * min((1.0 - ceil(UVTiled) / grid),floor(UVTiled) / grid);
        GridV = Square.x;
        GridH = Square.y;


        return vec3(Grid,GridH,GridV);

    }
    vec3 TimeLineOffset(vec2 UV, vec2 grid,float aspect) {


        UV = vec2(UV.x * aspect,0.0);

        vec2 steppedTimer = UV + (ceil(fract(iTime * beatTime) * grid.y));

        steppedTimer = floor(steppedTimer * grid.y) / grid.y;

        float freq = getFrequencies(UV) * 1.0;

        vec2 SteppedUVs = UV * GridSize;
        SteppedUVs = floor(SteppedUVs);
        SteppedUVs /= GridSize;
        vec4 a = texture(iChannel0,vec2(SteppedUVs.x,0.));

        vec3 random = Hash23(steppedTimer) * Amplitude * a.x;

        return random;

    }

    //------------------------------------------
    //Basic Functions
    float Sine01(float speed) { //Sine Remapped to 0-1

        return float(sin(iTime * speed) * 0.5) + 0.5;
    }
    float Lerp3(float X, float Y, float Z, float A) {


            float Lerp3Colors = mix(X,Y,clamp(A * 2.0,0.0,1.0));
            Lerp3Colors = mix(Lerp3Colors,Z,clamp((A * 2.0) - 1.0,0.0,1.0));

            return Lerp3Colors;
    }


    //-------------------
    //Main 
    void mainImage(out vec4 fragColor, in vec2 fragCoord) {

       vec2 uv = fragCoord / iResolution.xy;
       float aspect = iResolution.x / iResolution.y;
       vec2 FinalGrid = vec2(GridSize * aspect,GridSize);
       vec3 GridOut = GridOutput(uv, FinalGrid); // XYZ = (Grid / HorizontalBars / VerticalBars)


       vec3 randombands = vec3(TimeLineOffset(uv, FinalGrid, aspect));// * (pow(GridOut.z,WaveCurve));

       float final = Lerp3(randombands.x, randombands.y, randombands.z, Sine01(1.0));
       final *= Sine01(Animating);
       float finalbg = final * 0.3;
       final += (GridOut.y + 0.05);
       finalbg += final;
       finalbg = clamp(floor(finalbg),0.0,1.0) * GridOut.x * 0.25;
       final = clamp(floor(final),0.0,1.0) * GridOut.x;
       final = mix(finalbg,final,final * final);

       vec3 FinalColor = final * mix(vec3(2.0,0.0,0.0),vec3(0.0,1.0,0.0),GridOut.y);

       fragColor = vec4(FinalColor,1.0);
    }
    Shader "Custom/GridWaveShader"
    {
        Properties
        {
            _Color("Color", Color) = (1,1,1,1)
            _MainTex("Albedo (RGB)", 2D) = "white" {}
            _Glossiness("Smoothness", Range(0,1)) = 0.5
            _Metallic("Metallic", Range(0,1)) = 0.0
        }
            SubShader
            {
                Tags { "RenderType" = "Opaque" }
                LOD 200

                CGPROGRAM
                // Made by Danilo Paulo, 09/09/2022
                // Don't forget to hit play on the iChannel0 \/

                // ... Rest of the shader code ...

            ENDCG
            }
        }

    // FallBack "Diffuse"

        }
}

    //    ENDCG
    
    //FallBack "Diffuse"

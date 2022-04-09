#version 330 core
out vec4 FragColor;

in vec2 TexCoords;

uniform sampler2DMS screenTexture;
uniform bool grayscale;
uniform bool inversion;

void main()
{
    ivec2 viewportDim = ivec2(800, 600);
    ivec2 coords = ivec2(viewportDim * TexCoords);
    vec3 sample0 = texelFetch(screenTexture, coords, 0).rgb;
    vec3 sample1 = texelFetch(screenTexture, coords, 1).rgb;
    vec3 sample2 = texelFetch(screenTexture, coords, 2).rgb;
    vec3 sample3 = texelFetch(screenTexture, coords, 3).rgb;

    vec3 col = 0.25 * (sample0 + sample1 + sample2 + sample3);

    if (grayscale) {
        float gray = 0.2126 * col.r + 0.7152 * col.g + 0.0722 * col.b;
        FragColor = vec4(vec3(gray), 1.0);
    }
    else if(inversion){
        FragColor=vec4(vec3(1-col),1.0);
    }
//     else if(inversion){
//             const float offset = 1.0 / 300.0;
//
//             vec2 offsets[9] = vec2[](
//                 vec2(-offset, offset), // top/left
//                 vec2(0.0f, offset), // top-center
//                 vec2(offset, offset),
//                 vec2(-offset, 0.0f),
//                 vec2(0.0f, 0.0f),
//                 vec2(0.0f, offset),
//                 vec2(-offset, -offset),
//                 vec2(0.0f, -offset),
//                 vec2(offset, -offset)
//             );
//
//             float kernel[9] = float[](
//                 1.0 / 16, 2.0/16, 1.0/ 16,
//                 2.0 / 16, 4.0/16, 2.0/ 16,
//                 1.0 / 16, 2.0/16, 1.0/ 16
//             );
//
//             vec3 sampleTex[9];
//             for(int i=0;i<9;++i){
//                 vec3 sample0=texelFetch(screenTexture,ivec2(coords+offsets[i]),0).rgb;
//                 vec3 sample1=texelFetch(screenTexture,ivec2(coords+offsets[i]),1).rgb;
//                 vec3 sample2=texelFetch(screenTexture,ivec2(coords+offsets[i]),2).rgb;
//                 vec3 sample3=texelFetch(screenTexture,ivec2(coords+offsets[i]),3).rgb;
//
//                 sampleTex[i]=0.10*(sample0+sample1+sample2+sample3);
//             }
//             vec3 col=vec3(0.0);
//             for (int i=0;i<9;++i){
//                 col+=sampleTex[i]*kernel[i];
//             }
//             FragColor=vec4(col,1.0);
//         }
    else{
        FragColor = vec4(vec3(col), 1.0);
    }
}
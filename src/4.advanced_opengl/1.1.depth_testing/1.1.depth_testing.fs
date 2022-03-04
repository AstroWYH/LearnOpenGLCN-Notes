#version 330 core
out vec4 FragColor;

in vec2 TexCoords;

uniform sampler2D texture1;

void main()
{    
    FragColor = texture(texture1, TexCoords); // wyh K?采样器和纹理坐标读取纹理颜色, 跟第1章纹理一样, 最基础的纹理颜色读取方式
}
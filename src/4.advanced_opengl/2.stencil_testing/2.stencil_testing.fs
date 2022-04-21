#version 330 core
out vec4 FragColor;

in vec2 TexCoords;

uniform sampler2D texture1;

void main()
{    
    FragColor = texture(texture1, TexCoords); // wyh 最基础的纹理用法, 没有光照, 直接读取1张主纹理
}
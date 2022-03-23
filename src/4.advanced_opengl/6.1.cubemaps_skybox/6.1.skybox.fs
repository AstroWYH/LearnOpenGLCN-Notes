#version 330 core
out vec4 FragColor;

in vec3 TexCoords; // wyh 代表3D纹理坐标的方向向量, 立方体贴图就不是之前的vec2 uv坐标了

uniform samplerCube skybox; // wyh 换成立方体贴图的采样器

void main()
{    
    FragColor = texture(skybox, TexCoords); // wyh 从立方体贴图采样, 形成天空盒的效果
}
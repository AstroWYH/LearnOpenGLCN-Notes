#version 330 core
layout (location = 0) in vec3 aPos;
layout (location = 1) in vec3 aNormal;
layout (location = 2) in vec2 aTexCoords;

out vec3 FragPos; // wyh 传入fs后, FragPos就在光栅化过程中自动变成每个片元的坐标
out vec3 Normal; // wyh 传入fs后, Normal就会利用重心坐标插值, 得到每个片元的法线 (知道3个顶点坐标, 知道当前片元的坐标, 又知道3个顶点的法线, 就可以重心坐标插值了)
out vec2 TexCoords; // wyh 传入fs后, TexCoords就会利用重心坐标插值, 得到每个片元的纹理

uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;

void main()
{
    FragPos = vec3(model * vec4(aPos, 1.0)); // wyh 所有顶点, 在世界坐标的表示。然而, 当FragPos传入fs后, 就都是所有片元的坐标了
    Normal = mat3(transpose(inverse(model))) * aNormal; // wyh 经过法线矩阵变换后的法线, 在世界坐标中也一定与表面垂直
    TexCoords = aTexCoords;
    
    gl_Position = projection * view * vec4(FragPos, 1.0);
}
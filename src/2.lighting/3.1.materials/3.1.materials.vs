#version 330 core
layout (location = 0) in vec3 aPos;
layout (location = 1) in vec3 aNormal;

out vec3 FragPos;
out vec3 Normal;

uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;

void main()
{
    FragPos = vec3(model * vec4(aPos, 1.0)); // wyh 片元(像素位置)位置
    Normal = mat3(transpose(inverse(model))) * aNormal; // wyh 法线矩阵, 防止不等比缩放导致的异常
    
    gl_Position = projection * view * vec4(FragPos, 1.0);
}
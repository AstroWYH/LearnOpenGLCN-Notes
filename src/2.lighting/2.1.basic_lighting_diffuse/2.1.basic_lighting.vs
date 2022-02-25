#version 330 core
layout (location = 0) in vec3 aPos;
layout (location = 1) in vec3 aNormal;

out vec3 FragPos;
out vec3 Normal; // wyh 新增的2个out给fs用

uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;

void main()
{
    FragPos = vec3(model * vec4(aPos, 1.0)); // wyh all vertex pos to worldspace
    Normal = aNormal; // wyh 法线值在VBO赋, 法线在vs里定义, 同顶点
    
    gl_Position = projection * view * vec4(FragPos, 1.0);
}
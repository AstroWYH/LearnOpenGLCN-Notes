#version 330 core
layout (location = 0) in vec3 aPos;
layout (location = 1) in vec3 aNormal;

out vec3 Normal;
out vec3 Position;

uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;

void main()
{
    Normal = mat3(transpose(inverse(model))) * aNormal; // wyh 世界空间的经过法线变换的法线
    Position = vec3(model * vec4(aPos, 1.0)); // wyh 世界空间的顶点
    gl_Position = projection * view * model * vec4(aPos, 1.0);
}
#version 330 core
layout (location = 0) in vec3 aPos;

layout (std140) uniform Matrices // wyh 8.1核心: uniform块和uniform缓冲的使用, 更有效率更高级
{
    mat4 projection;
    mat4 view;
};
uniform mat4 model; // wyh 4个立方体的model不一样, 所以不放在uniform块里

void main()
{
    gl_Position = projection * view * model * vec4(aPos, 1.0);
}
// wyh 4个不同颜色的立方体共用这个vs
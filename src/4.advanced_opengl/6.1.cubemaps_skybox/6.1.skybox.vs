#version 330 core
layout (location = 0) in vec3 aPos;

out vec3 TexCoords; // wyh 代表3D纹理坐标的方向向量

uniform mat4 projection;
uniform mat4 view;

void main()
{
    TexCoords = aPos; // wyh 顶点坐标传到fs当纹理坐标, 6.1立方体贴图(天空盒)的纹理坐标的方向向量, 正好和顶点坐标一样
    vec4 pos = projection * view * vec4(aPos, 1.0);
    gl_Position = pos.xyww; // wyh 这样z分量在透视除法(z/w)永远为1, 保持在远平面最远处; 这里gl_Position接收的是裁剪空间的坐标, 即透视除法前
}  
#version 330 core
layout (location = 0) in vec2 aPos;
layout (location = 1) in vec3 aColor;

out VS_OUT { // wyh 传给gs用的接口块
    vec3 color;
} vs_out;

void main()
{
    vs_out.color = aColor; // wyh 这个传过去的颜色是跟顶点存在一起的
    gl_Position = vec4(aPos.x, aPos.y, 0.0, 1.0); // wyh 2d的渲染, 没有z坐标
}
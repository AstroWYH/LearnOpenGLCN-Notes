#version 330 core
layout (location = 0) in vec2 aPos; // wyh 四边形是二维图像的坐标
layout (location = 1) in vec2 aTexCoords;

out vec2 TexCoords;

void main()
{
    TexCoords = aTexCoords;
    gl_Position = vec4(aPos.x, aPos.y, 0.0, 1.0); // wyh 横跨屏幕的二维的四边形, 将帧缓冲的颜色缓冲作为它的纹理; 这就是帧缓冲的纹理附件, 渲染到纹理
}  